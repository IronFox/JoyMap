using JoyMap.Util;
using SharpDX.DirectInput;
using System.Collections.Concurrent;

namespace JoyMap.ControllerTracking
{

    record TrackedInput(InputMonitor Owner, DeviceInstance Device, InstanceStatus TargetStatus) : IDisposable
    {
        public Joystick? Joystick { get; private set; }
        public bool IsAcquired { get; private set; } = false;

        public const int Resolution = 10000;
        public void Begin(DirectInput di, IntPtr windowHandle, CancellationToken cancel)
        {
            if (IsAcquired)
                return;
            try
            {
                Joystick = new Joystick(di, Device.InstanceGuid);
                Joystick.Properties.AxisMode = DeviceAxisMode.Absolute;
                Joystick.Properties.BufferSize = 10000;
                Joystick.Properties.Range = new InputRange(-Resolution, Resolution);
                Joystick.SetCooperativeLevel(
                    windowHandle,
                    CooperativeLevel.Background |
                    CooperativeLevel.NonExclusive);



                Joystick.Acquire();
                IsAcquired = true;

                TargetStatus.SignalBegin(Device.InstanceGuid);

                Task.Run(() => RunAsync(cancel).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                System.Diagnostics.Debug.WriteLine($"Failed to initialize joystick: {ex.Message}");
            }
        }



        private async Task RunAsync(CancellationToken cancel)
        {
            if (Joystick is null)
                return;
            while (IsAcquired)
            {
                cancel.ThrowIfCancellationRequested();
                try
                {
                    Joystick.Poll();
                    TargetStatus.UpdateInstance(Joystick.GetCurrentState(), Resolution);
                }
                catch (Exception)
                {
                    Owner.SignalDisturbance(this);
                    return;
                }
                await Task.Delay(5, cancel).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            if (IsAcquired && Joystick != null)
            {
                TargetStatus.SignalEnd(Device.InstanceGuid);
                try
                {
                    Joystick.Unacquire();
                }
                catch (Exception)
                { }
                try
                {
                    Joystick.Dispose();
                }
                catch (Exception)
                { }
                try
                {
                    EventRecorder.SignalGone(Device);
                }
                catch (Exception)
                { }
            }
        }

    }

    internal readonly record struct EventKey(
        Guid DeviceInstanceId,
        InputAxis Which,
        bool AxisNegated
        )
    {
        public EventKey(DeviceEvent ev)
        : this(
              DeviceInstanceId: ev.InputId.ControllerId.InstanceGuid,
              Which: ev.InputId.Axis,
              AxisNegated: ev.InputId.AxisNegated)
        { }
    }

    internal readonly record struct EventValue(
        ControllerInputId InputId,
        float Status,
        Func<float?> GetLatestStatus
        );

    public readonly record struct DeviceEvent(
        ControllerInputId InputId,
        float Status,
        Func<float?> GetLatestStatus
        )
    {
        public string DeviceName => InputId.ControllerName;
    }

    public class EventRecorder : IDisposable
    {
        private static ConcurrentDictionary<EventRecorder, bool> Active { get; } = new();
        private ConcurrentDictionary<EventKey, EventValue> Events = new();
        internal static void SignalSignificantChanges(InstanceStatus state, IReadOnlyCollection<InputAxisChange> changes)
        {
            var devId = state.ControllerId;
            foreach (var change in changes)
            {
                var ev = new EventKey(
                    DeviceInstanceId: state.Id,
                    Which: change.Which,
                    AxisNegated: change.Status < 0);

                foreach (var recorder in Active.Keys)
                {
                    var nv = new EventValue(new(devId, state.ControllerName, change.Which, ev.AxisNegated), change.Status, () => state.Get(change.Which));
                    recorder.Events.AddOrUpdate(ev, nv, (_, _) => nv);
                }
            }
        }

        internal static void SignalGone(DeviceInstance input)
        {
            var evsToRemove = Active.Keys.SelectMany(r => r.Events.Keys
                .Where(k => k.DeviceInstanceId == input.InstanceGuid)
                .Select(k => (recorder: r, key: k)))
                .ToList();
            foreach (var (recorder, key) in evsToRemove)
            {
                recorder.Events.TryRemove(key, out _);
            }
        }

        public EventRecorder()
        {
            Active.TryAdd(this, true);
        }
        public void Dispose()
        {
            Active.TryRemove(this, out _);
        }

        public IEnumerable<DeviceEvent> GetAll()
        {
            return Events.Select(kvp => new DeviceEvent(
                InputId: kvp.Value.InputId,
                Status: kvp.Value.Status,
                GetLatestStatus: kvp.Value.GetLatestStatus));
        }

    }

    public class InputMonitor : IDisposable
    {
        private readonly CancellationTokenSource cancel = new();
        private readonly RawMouseInputMonitor _mouseMonitor;
        public InputMonitor(IntPtr windowHandle, IReadOnlyCollection<JsonControllerFamily> controllerFamilies)
        {
            var di = new DirectInput();
            foreach (var jf in controllerFamilies)
            {
                var family = new ControllerFamily(jf);
                AllFamilies.Add(family);
                foreach (var pg in jf.Members)
                    ProductToFamilyMap[pg.Guid] = family;
            }

            Task.Run(() => RunAsync(di, windowHandle, cancel.Token));

            // Add mouse as virtual joystick
            _mouseMonitor = new RawMouseInputMonitor(windowHandle);
        }
        public void Dispose()
        {
            cancel.Cancel();
            _mouseMonitor?.Dispose();
            cancel.Dispose();
        }

        private ConcurrentDictionary<Guid, TrackedInput> TrackedInputs { get; } = new();

        private ConcurrentDictionary<Guid, ControllerStatus> ProductStatusMap { get; } = new();
        private ConcurrentDictionary<ControllerId, InstanceStatus> InstanceStatusMap { get; } = new();

        private async Task RunAsync(DirectInput di, IntPtr windowHandle, CancellationToken cancel)
        {
            try
            {
                while (true)
                {
                    var devices = di.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
                    HashSet<Guid> newGuids = [.. devices.Select(x => x.InstanceGuid)];
                    foreach (var dev in devices)
                    {
                        TrackedInput? instantiated = null;
                        var iid = ControllerId.From(dev);

                        var instanceStatus = CreateInstanceStatus(iid, dev.ProductName);

                        var instance = TrackedInputs.GetOrAdd(dev.InstanceGuid, _ => instantiated = new(this, dev, instanceStatus));
                        if (instance == instantiated)
                            instance.Begin(di, windowHandle, cancel);
                    }
                    var toRemove = TrackedInputs.Keys.Except(newGuids).ToList();
                    foreach (var guid in toRemove)
                    {
                        if (TrackedInputs.TryRemove(guid, out var input))
                        {
                            input.Dispose();
                        }
                    }
                    await Task.Delay(500, cancel).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected on cancellation
            }

            await Task.Delay(100).ConfigureAwait(false);

            foreach (var input in TrackedInputs.Values)
            {
                input.Dispose();
            }

            di.Dispose();
        }

        private InstanceStatus CreateInstanceStatus(ControllerId iid, string productName)
        {
            var productStatus = ProductStatusMap.GetOrAdd(iid.ProductGuid, id => new ControllerStatus(id));
            var result = InstanceStatusMap.GetOrAdd(iid, _ => new InstanceStatus(iid.InstanceGuid, productName, productStatus));

            result.Family = GetFamilyOf(new(iid.ProductGuid, productName));

            return result;
        }

        internal void SignalDisturbance(TrackedInput trackedInput)
        {
            if (TrackedInputs.TryRemove(trackedInput.Device.InstanceGuid, out var input))
            {
                input.Dispose();
            }
        }

        internal Func<float?> GetFunction(ControllerInputId inputId)
        {
            // Check if this is the mouse virtual joystick
            if (inputId.ControllerId.ProductGuid == MouseControllerId.ProductGuid)
            {
                // Return raw value WITHOUT negation - BuildGetter will handle it
                return () => _mouseMonitor.GetAxisValue(inputId.Axis);
            }

            var instanceStatus = CreateInstanceStatus(inputId.ControllerId, inputId.ControllerName);
            return () => instanceStatus.Get(inputId.Axis);
        }


        private LockedList<ControllerFamily> AllFamilies { get; } = [];
        private ConcurrentDictionary<Guid, ControllerFamily> ProductToFamilyMap { get; } = [];
        public ControllerFamily GetFamilyOf(Product product)
        {
            return ProductToFamilyMap.GetOrAdd(product.Guid, _ =>
            {
                var family = new ControllerFamily(Guid.NewGuid(), "Private family of " + product.Name, true);
                family.Members.Add(product);
                return family;
            });
        }

        public JsonControllerFamily CreateFamily(string name, IReadOnlyCollection<Product> productGuids)
        {
            var family = new ControllerFamily(Guid.NewGuid(), name, false);
            foreach (var pg in productGuids)
                family.Members.Add(pg);
            AllFamilies.Add(family);
            foreach (var pg in productGuids)
                ProductToFamilyMap[pg.Guid] = family;

            foreach (var pg in InstanceStatusMap.Values)
            {
                if (productGuids.Any(x => x.Guid == pg.ControllerId.ProductGuid))
                {
                    if (pg.Family is not null && !pg.Family.IsGeneric)
                        pg.Family.Members.RemoveAll(x => x.Guid == pg.ControllerId.ProductGuid);
                    pg.Family = family;
                }
            }

            return family.ToJson();
        }

        public JsonControllerFamily UpdateFamily(JsonControllerFamily original, string name, IReadOnlyCollection<Product> productGuids)
        {
            var family = AllFamilies.First(x => x.Id == original.Id);
            family.FamilyName = name;
            foreach (var pg in family.Members)
                if (!productGuids.Any(x => x.Guid == pg.Guid))
                    ProductToFamilyMap.TryRemove(pg.Guid, out _);
            family.Members.Clear();
            foreach (var pg in productGuids)
                family.Members.Add(pg);
            foreach (var pg in productGuids)
                ProductToFamilyMap[pg.Guid] = family;
            foreach (var pg in InstanceStatusMap.Values)
            {
                if (pg.Family == family)
                    continue;
                if (productGuids.Any(x => x.Guid == pg.ControllerId.ProductGuid))
                {
                    if (pg.Family is not null && !pg.Family.IsGeneric)
                        pg.Family.Members.RemoveAll(x => x.Guid == pg.ControllerId.ProductGuid);
                    pg.Family = family;
                }
            }
            return family.ToJson();
        }

        public void DeleteFamily(JsonControllerFamily family)
        {
            var fam = AllFamilies.FirstOrDefault(x => x.Id == family.Id);
            if (fam is null)
                return;
            AllFamilies.Remove(fam);
            foreach (var pg in fam.Members)
                ProductToFamilyMap.TryRemove(pg.Guid, out _);
            foreach (var pg in InstanceStatusMap.Values)
            {
                if (pg.Family != fam)
                    continue;
                pg.Family = GetFamilyOf(new(pg.ControllerId.ProductGuid, pg.ControllerName));
            }
        }

        internal IReadOnlyList<JsonControllerFamily> ExportAllFamilies()
        {
            return [.. AllFamilies.Select(x => x.ToJson())];
        }

        /// <summary>
        /// Gets all available input sources including mouse
        /// </summary>
        public IEnumerable<ControllerInputId> GetAvailableInputs()
        {
            // Mouse axes
            yield return MouseInputIdFactory.CreateMouseAxis(MouseAxisType.X, false);
            yield return MouseInputIdFactory.CreateMouseAxis(MouseAxisType.X, true);
            yield return MouseInputIdFactory.CreateMouseAxis(MouseAxisType.Y, false);
            yield return MouseInputIdFactory.CreateMouseAxis(MouseAxisType.Y, true);

            // ... existing DirectInput enumeration ...
        }

        /// <summary>
        /// Process raw mouse input from WndProc
        /// </summary>
        public void ProcessMouseInput(IntPtr lParam)
        {
            _mouseMonitor?.ProcessRawInput(lParam);
        }
    }
}

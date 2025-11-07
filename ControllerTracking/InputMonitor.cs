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
                await Task.Delay(50, cancel).ConfigureAwait(false);
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
        InputAxis Which
        )
    {
        public EventKey(DeviceEvent ev)
        : this(
              DeviceInstanceId: ev.InputId.ControllerId.InstanceGuid,
              Which: ev.InputId.Axis)
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
                    Which: change.Which);

                foreach (var recorder in Active.Keys)
                {
                    var nv = new EventValue(new(devId, state.ControllerName, change.Which, change.Status < 0), change.Status, () => state.Get(change.Which));
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
        public InputMonitor(IntPtr windowHandle)
        {
            var di = new DirectInput();

            Task.Run(() => RunAsync(di, windowHandle, cancel.Token));
        }
        public void Dispose()
        {
            cancel.Cancel();
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
            return InstanceStatusMap.GetOrAdd(iid, _ => new InstanceStatus(iid.InstanceGuid, productName, productStatus));
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
            var instanceStatus = CreateInstanceStatus(inputId.ControllerId, inputId.ControllerName);
            return () => instanceStatus.Get(inputId.Axis);
        }


    }
}

using SharpDX.DirectInput;
using System.Collections.Concurrent;

namespace JoyMap
{

    record TrackedInput(InputMonitor Owner, DeviceInstance Device) : IDisposable
    {
        public InputState? LastState { get; set; }
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

                Task.Run(() => RunAsync(cancel).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                System.Diagnostics.Debug.WriteLine($"Failed to initialize joystick: {ex.Message}");
            }
        }


        private float XOf(int[] povs, int index)
        {
            if (povs.Length > index)
            {
                var pov = povs[index];
                if (pov >= 0 && pov <= 36000)
                {
                    var deg = pov / 100f;
                    var rad = deg * (MathF.PI / 180f);
                    var rs = MathF.Sin(rad);
                    return rs;
                }
            }
            return 0;
        }
        private float YOf(int[] povs, int index)
        {
            if (povs.Length > index)
            {
                var pov = povs[index];
                if (pov >= 0 && pov <= 36000)
                {
                    var deg = pov / 100f;
                    var rad = deg * (MathF.PI / 180f);
                    var rs = MathF.Cos(rad);
                    return rs;
                }
            }
            return 0;
        }

        private async Task RunAsync(CancellationToken cancel)
        {
            if (Joystick is null)
                return;
            List<InputAxisChange> changes = [];
            while (IsAcquired)
            {
                cancel.ThrowIfCancellationRequested();
                try
                {
                    Joystick.Poll();
                    var state = Joystick.GetCurrentState();
                    if (state != null)
                    {
                        //state.PointOfViewControllers
                        var currentState = new InputState(
                            Slider: (float)state.Sliders.Length > 0 ? (float)state.Sliders[0] / Resolution : 0,
                            PovX: XOf(state.PointOfViewControllers, 0),
                            PovY: YOf(state.PointOfViewControllers, 0),
                            X: (float)state.X / Resolution,
                            Y: (float)state.Y / Resolution,
                            Z: (float)state.Z / Resolution,
                            RotationX: (float)state.RotationX / Resolution,
                            RotationY: (float)state.RotationY / Resolution,
                            RotationZ: (float)state.RotationZ / Resolution,
                            Buttons: (bool[])state.Buttons.Clone());

                        if (LastState != null)
                        {
                            InputState.DetectSignificantChanges(LastState.Value, currentState, changes);
                            if (changes.Count > 0)
                                EventRecorder.SignalSignificantChanges(this, changes);
                        }
                        LastState = currentState;
                    }
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

        internal float GetLatestValue(Input which)
        {
            if (this.LastState is null)
                return 0;
            return LastState.Value.Get(which);
        }
    }

    internal readonly record struct EventKey(
        Guid DeviceInstanceId,
        Input Which
        )
    {
        public EventKey(Event ev)
        : this(
              DeviceInstanceId: ev.DeviceInstance.InstanceGuid,
              Which: ev.Which)
        { }
    }

    internal readonly record struct EventValue(
        DeviceInstance DeviceInstance,
        float Status,
        Func<float> GetLatestStatus
        );

    public readonly record struct Event(
        DeviceInstance DeviceInstance,
        Input Which,
        float Status,
        Func<float> GetLatestStatus
        )
    {
        public bool Signed => Which < Input.Button0;
        public bool Positive => Status >= 0 || !Signed;
        public string DeviceName => DeviceInstance.ProductName;
    }

    public class EventRecorder : IDisposable
    {
        private static ConcurrentDictionary<EventRecorder, bool> Active { get; } = new();
        private ConcurrentDictionary<EventKey, EventValue> Events = new();
        internal static void SignalSignificantChanges(TrackedInput input, IReadOnlyCollection<InputAxisChange> changes)
        {
            foreach (var change in changes)
            {
                var ev = new EventKey(
                    DeviceInstanceId: input.Device.InstanceGuid,
                    Which: change.Which);

                foreach (var recorder in Active.Keys)
                {
                    var nv = new EventValue(input.Device, change.Status, () => input.GetLatestValue(change.Which));
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

        public IEnumerable<Event> GetAll()
        {
            return Events.Select(kvp => new Event(
                DeviceInstance: kvp.Value.DeviceInstance,
                Which: kvp.Key.Which,
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
                        var instance = TrackedInputs.GetOrAdd(dev.InstanceGuid, _ => instantiated = new(this, dev));
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

        internal void SignalDisturbance(TrackedInput trackedInput)
        {
            if (TrackedInputs.TryRemove(trackedInput.Device.InstanceGuid, out var input))
            {
                input.Dispose();
            }
        }
    }
}

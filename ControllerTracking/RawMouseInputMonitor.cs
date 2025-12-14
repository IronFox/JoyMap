using System.Runtime.InteropServices;

namespace JoyMap.ControllerTracking
{
    /// <summary>
    /// Monitors raw mouse input and exposes it as joystick-like axes
    /// </summary>
    public class RawMouseInputMonitor : IDisposable
    {
        private readonly IntPtr _windowHandle;
        private readonly MouseToAxisConverter _xAxis = new();
        private readonly MouseToAxisConverter _yAxis = new();
        private readonly object _lock = new();
        private readonly System.Threading.Timer _decayTimer;

        public ControllerId ControllerId => MouseControllerId.Mouse;

        public RawMouseInputMonitor(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
            RegisterRawInput();

            // Start decay timer: call ApplyDecay every 16ms (~60Hz)
            _decayTimer = new System.Threading.Timer(
                _ => ApplyDecay(),
                null,
                TimeSpan.FromMilliseconds(16),
                TimeSpan.FromMilliseconds(16)
            );
        }

        /// <summary>
        /// Apply decay to axes even when mouse isn't moving
        /// </summary>
        private void ApplyDecay()
        {
            lock (_lock)
            {
                // Call Update with 0 delta to trigger decay
                _xAxis.Update(0);
                _yAxis.Update(0);

                // Update InstanceStatus with decayed values
                var instanceStatus = GetOrCreateMouseInstanceStatus();
                var mouseState = new InputState(
                    X: _xAxis.CurrentValue,
                    Y: _yAxis.CurrentValue,
                    Z: 0,
                    Slider1: 0,
                    RotationX: 0,
                    RotationY: 0,
                    RotationZ: 0,
                    PovX: 0,
                    PovY: 0,
                    ButtonCount: 0,
                    ButtonsBits: 0
                );
                instanceStatus.Update(mouseState);
            }
        }

        private void RegisterRawInput()
        {
            var devices = new RAWINPUTDEVICE[1];

            devices[0].usUsagePage = 0x01; // HID_USAGE_PAGE_GENERIC
            devices[0].usUsage = 0x02;     // HID_USAGE_GENERIC_MOUSE
            devices[0].dwFlags = RIDEV_INPUTSINK;
            devices[0].hwndTarget = _windowHandle;

            if (!RegisterRawInputDevices(devices, 1, Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
            {
                throw new InvalidOperationException("Failed to register raw input device");
            }
        }

        public void ProcessRawInput(IntPtr lParam)
        {
            uint size = 0;
            GetRawInputData(lParam, RID_INPUT, IntPtr.Zero, ref size, Marshal.SizeOf<RAWINPUTHEADER>());

            if (size == 0) return;

            IntPtr buffer = Marshal.AllocHGlobal((int)size);
            try
            {
                if (GetRawInputData(lParam, RID_INPUT, buffer, ref size, Marshal.SizeOf<RAWINPUTHEADER>()) != size)
                    return;

                var raw = Marshal.PtrToStructure<RAWINPUT>(buffer);

                if (raw.header.dwType == RIM_TYPEMOUSE)
                {
                    lock (_lock)
                    {
                        int deltaX = raw.mouse.lLastX;
                        int deltaY = raw.mouse.lLastY;

                        _xAxis.Update(deltaX);
                        _yAxis.Update(deltaY);

                        // Update the InstanceStatus with current mouse state
                        var instanceStatus = GetOrCreateMouseInstanceStatus();
                        var mouseState = new InputState(
                            X: _xAxis.CurrentValue,
                            Y: _yAxis.CurrentValue,
                            Z: 0,
                            Slider1: 0,
                            RotationX: 0,
                            RotationY: 0,
                            RotationZ: 0,
                            PovX: 0,
                            PovY: 0,
                            ButtonCount: 0,
                            ButtonsBits: 0
                        );
                        instanceStatus.Update(mouseState);
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        private void SignalMouseChange(ControllerInputId inputId, float value)
        {
            // Create a virtual InstanceStatus for the mouse
            var changes = new[]
            {
                new InputAxisChange(inputId.Axis, value)
            };

            // You'll need to create a mouse InstanceStatus
            // This is a simplified version - you may need to adjust
            EventRecorder.SignalSignificantChanges(
                GetOrCreateMouseInstanceStatus(),
                changes
            );
        }

        private InstanceStatus? _mouseInstanceStatus;
        private InstanceStatus GetOrCreateMouseInstanceStatus()
        {
            if (_mouseInstanceStatus == null)
            {
                // Create a virtual instance status for mouse
                var productStatus = new ControllerStatus(MouseControllerId.ProductGuid);
                _mouseInstanceStatus = new InstanceStatus(
                    MouseControllerId.Mouse.InstanceGuid,
                    "System Mouse",
                    productStatus
                );

                // CRITICAL: Signal that this "device" is acquired!
                _mouseInstanceStatus.SignalBegin(MouseControllerId.Mouse.InstanceGuid);
            }
            return _mouseInstanceStatus;
        }

        /// <summary>
        /// Gets current axis value for specified mouse axis (joystick-compatible)
        /// </summary>
        public float? GetAxisValue(InputAxis axis)
        {
            lock (_lock)
            {
                return axis switch
                {
                    InputAxis.X => _xAxis.CurrentValue,
                    InputAxis.Y => _yAxis.CurrentValue,
                    _ => null
                };
            }
        }

        /// <summary>
        /// Configure sensitivity for X axis
        /// </summary>
        public void ConfigureXAxis(float sensitivity, float deadZone)
        {
            lock (_lock)
            {
                _xAxis.Sensitivity = sensitivity;
                _xAxis.DeadZone = deadZone;
            }
        }

        /// <summary>
        /// Configure sensitivity for Y axis
        /// </summary>
        public void ConfigureYAxis(float sensitivity, float deadZone)
        {
            lock (_lock)
            {
                _yAxis.Sensitivity = sensitivity;
                _yAxis.DeadZone = deadZone;
            }
        }

        public void Dispose()
        {
            _decayTimer?.Dispose();  // Stop decay timer

            var devices = new RAWINPUTDEVICE[1];
            devices[0].usUsagePage = 0x01;
            devices[0].usUsage = 0x02;
            devices[0].dwFlags = RIDEV_REMOVE;
            devices[0].hwndTarget = IntPtr.Zero;

            RegisterRawInputDevices(devices, 1, Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
        }

        // P/Invoke declarations (unchanged from previous version)
        private const int RID_INPUT = 0x10000003;
        private const int RIM_TYPEMOUSE = 0;
        private const int RIDEV_INPUTSINK = 0x00000100;
        private const int RIDEV_REMOVE = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        private struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public int dwFlags;
            public IntPtr hwndTarget;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RAWINPUTHEADER
        {
            public int dwType;
            public int dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct RAWINPUT
        {
            [FieldOffset(0)] public RAWINPUTHEADER header;
            [FieldOffset(24)] public RAWMOUSE mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RAWMOUSE
        {
            public ushort usFlags;
            public uint ulButtons;
            public uint ulRawButtons;
            public int lLastX;
            public int lLastY;
            public uint ulExtraInformation;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterRawInputDevices(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] RAWINPUTDEVICE[] pRawInputDevices,
            int uiNumDevices,
            int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetRawInputData(
            IntPtr hRawInput,
            uint uiCommand,
            IntPtr pData,
            ref uint pcbSize,
            int cbSizeHeader);
    }
}
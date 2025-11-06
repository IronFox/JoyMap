using SharpDX.DirectInput;

namespace JoyMap
{
    internal record DeviceDescription(
        string Name,
        Guid InstanceId,
        Guid ProductId,

        DeviceInstance Instance,
        Joystick? Joystick,
        DirectInput Input,
        object? _
        )

    {

        public JoystickState? ReadAllAxes()
        {
            if (Joystick is null)
                return null;
            return Joystick.GetCurrentState();
        }

        public bool Begin(IntPtr p)
        {
            if (Joystick is null)
                return false;
            Joystick.Properties.AxisMode = DeviceAxisMode.Absolute;
            Joystick.Properties.BufferSize = 10000;
            Joystick.Properties.Range = new InputRange(-5000, 5000);
            Joystick.SetCooperativeLevel(
                p,
                CooperativeLevel.Background |
                CooperativeLevel.NonExclusive);

            Joystick.Acquire();
            return true;
        }

        public void End()
        {
            if (Joystick is null)
                return;
            Joystick.Unacquire();
        }


        internal static DeviceDescription From(DirectInput input, DeviceInstance di)
        {
            return new(
                Name: di.Type + ":" + di.ProductName,
                InstanceId: di.InstanceGuid,
                ProductId: di.ProductGuid,
                di,
                Joystick: di.Type == DeviceType.Joystick
                || di.Type == DeviceType.Gamepad
                || di.Type == DeviceType.Flight
                || di.Type == DeviceType.Driving
                || di.Type == DeviceType.FirstPerson
                || di.Type == DeviceType.Supplemental
                    ? new Joystick(input, di.InstanceGuid)
                    : null,
                input,
                null
            );
        }
    }
}
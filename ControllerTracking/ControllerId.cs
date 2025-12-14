using SharpDX.DirectInput;

namespace JoyMap.ControllerTracking
{
    public readonly record struct ControllerId(
        Guid ProductGuid,
        Guid InstanceGuid
        )
    {
        public static ControllerId From(DeviceInstance device)
        {
            return new ControllerId(
                ProductGuid: device.ProductGuid,
                InstanceGuid: device.InstanceGuid
                );
        }
    }

    public static class MouseControllerId
    {
        // Fixed GUIDs for mouse as virtual joystick
        public static readonly Guid ProductGuid = new Guid("00000000-0000-0000-0000-000000000001");
        public static readonly Guid InstanceGuid = new Guid("00000000-0000-0000-0000-000000000002");

        public static ControllerId Mouse { get; } = new ControllerId(
            ProductGuid,
            InstanceGuid//,
                        //
        );

        public const string ControllerName = "System Mouse";
    }
}

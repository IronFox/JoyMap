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
}

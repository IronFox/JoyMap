namespace JoyMap.ControllerTracking
{
    public readonly record struct ControllerInputId
    (
        ControllerId ControllerId,
        string ControllerName,
        Input Axis,
        bool AxisNegated
    )
    {
        public bool AxisSigned => Axis > Input.None && Axis < Input.Button0;

    }
}

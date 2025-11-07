namespace JoyMap.ControllerTracking
{
    public readonly record struct ControllerInputId
    (
        ControllerId ControllerId,
        string ControllerName,
        InputAxis Axis,
        bool AxisNegated
    )
    {
        public bool AxisSigned => Axis > InputAxis.None && Axis < InputAxis.Button0;

        public string AxisName =>
             $"{Axis}{(AxisSigned ? (AxisNegated ? " Negative" : " Positive") : "")}";
    }
}

using JoyMap.Util;
using System.Text.Json.Serialization;

namespace JoyMap.ControllerTracking
{
    public readonly record struct ControllerInputId
    (
        ControllerId ControllerId,
        string ControllerName,
        InputAxis Axis,
        bool AxisNegated
    ) : IJsonCompatible
    {
        [JsonIgnore]
        public bool AxisSigned => Axis > InputAxis.None && Axis < InputAxis.Button0;

        [JsonIgnore]
        public string ControllerAxisName =>
             $"{ControllerName}:{AxisName}";

        [JsonIgnore]
        public string AxisName =>
             $"{Axis}{(AxisSigned ? (AxisNegated ? " Negative" : " Positive") : "")}";
    }

    // Mouse axes appear as standard joystick axes
    public enum MouseAxisType
    {
        X,  // Horizontal delta
        Y   // Vertical delta
    }

    // Extension method to create mouse input IDs
    public static class MouseInputIdFactory
    {
        public static ControllerInputId CreateMouseAxis(MouseAxisType axis, bool negated = false)
        {
            InputAxis axisName = axis switch
            {
                MouseAxisType.X => InputAxis.X,
                MouseAxisType.Y => InputAxis.Y,
                _ => throw new ArgumentException(nameof(axis))
            };

            return new ControllerInputId(
                MouseControllerId.Mouse,
                MouseControllerId.ControllerName,
                axisName,
                negated
            );
        }
    }
}
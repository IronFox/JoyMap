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
}

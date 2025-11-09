using JoyMap.Extensions;
using System.Text.Json.Serialization;

namespace JoyMap.Profile
{
    public record EventAction(
        string Name,
        float DelayMs,
        SimpleInputEffect? SimpleInputEffect
        )
    {
        [JsonIgnore]
        public string TypeName => SimpleInputEffect is not null
            ? "Simple"
            : "empty";
        [JsonIgnore]
        public string Action => SimpleInputEffect is not null
            ? SimpleInputEffect.Action
            : "No Action";
    }


    public record SimpleInputEffect(
        Keys Keys,
        float? AutoTriggerFrequency,
        int? AutoTriggerLimit
        )
    {
        [JsonIgnore]
        public string Action =>
                $"Key: {PickKeyForm.KeysToString(Keys)}{(AutoTriggerFrequency is not null ? $", f={AutoTriggerFrequency.Value.ToStr()}" : "")}";
    }
}

using JoyMap.Extensions;
using JoyMap.Windows;
using System.Text.Json.Serialization;

namespace JoyMap.Profile
{
    public record EventAction(
        string Name,
        float DelayMs,
        SimpleInputEffect? SimpleInputEffect = null,
        ChangeTriggerInputEffect? ChangeTriggeredInputEffect = null

        )
    {
        [JsonIgnore]
        public string TypeName => SimpleInputEffect is not null
            ? "Simple"
            : ChangeTriggeredInputEffect is not null
                ? "OnChange"
                : "empty";

        [JsonIgnore]
        public string Action => SimpleInputEffect is not null
            ? SimpleInputEffect.Action
            : ChangeTriggeredInputEffect is not null
                ? ChangeTriggeredInputEffect.Action
                : "No Action";
    }


    public record SimpleInputEffect(
        KeyOrButton Keys,
        float? AutoTriggerDelayMs,
        float? AutoTriggerFrequency,
        int? AutoTriggerLimit
        )
    {
        [JsonIgnore]
        public string Action =>
                $"Key: {Keys}{(AutoTriggerFrequency is not null ? $", f={AutoTriggerFrequency.Value.ToStr()}" : "")}";
    }

    public record ChangeTriggerInputEffect(
        KeyOrButton Keys
        )
    {
        [JsonIgnore]
        public string Action =>
                $"On Change: {Keys}";
    }
}

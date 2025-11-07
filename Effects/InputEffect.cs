using JoyMap.Extensions;

namespace JoyMap.KeyEffects
{
    public abstract record InputEffect(
        string Name,
        float DelayMs
        )
    {

        public abstract string TypeName { get; }
        public abstract string Action { get; }
    }


    public record SimpleInputEffect(
        string Name,
        float DelayMs,
        Keys Keys,
        float? ReAssertIntervalFrequency,
        float? AutoTriggerFrequency
        ) : InputEffect(Name, DelayMs)
    {
        public override string TypeName => "Simple";
        public override string Action => $"Key: {PickKeyForm.KeysToString(Keys)}{(AutoTriggerFrequency is not null ? $", f={AutoTriggerFrequency.Value.ToStr()}" : "")}";
    }
}

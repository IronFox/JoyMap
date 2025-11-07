using JoyMap.ControllerTracking;

namespace JoyMap.Profile
{
    public readonly record struct Trigger(
        ControllerInputId InputId,
        float MinValue,
        float MaxValue
        );


    public record TriggerInstance(
        Trigger Trigger,
        Func<float?> GetCurrentValue
        )
    {
        public float? CurrentValue
        {
            get
            {
                var v = GetCurrentValue();
                if (v is null)
                    return null;
                return Trigger.InputId.AxisNegated ? -v.Value : v.Value;
            }
        }


        public bool IsTriggered
        {
            get
            {
                var v = CurrentValue;
                if (v is null)
                    return false;
                return v.Value >= Trigger.MinValue && v.Value <= Trigger.MaxValue;
            }
        }

        internal static TriggerInstance Load(InputMonitor monitor, Trigger t)
        {
            return new TriggerInstance(
                Trigger: t,
                GetCurrentValue: monitor.GetFunction(t.InputId)
                );
        }
    }
}

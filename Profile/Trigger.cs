using JoyMap.ControllerTracking;

namespace JoyMap.Profile
{
    public readonly record struct Trigger(
        ControllerInputId InputId,
        float MinValue,
        float MaxValue,
        float? AutoOffAfterMs,
        float? DelayReleaseMs
        );


    public record TriggerInstance(
        Trigger Trigger,
        Func<float?> GetCurrentValue,
        Func<bool> IsTriggered
        )
    {
        //public float? CurrentValue
        //{
        //    get
        //    {
        //        var v = GetCurrentValue();
        //        if (v is null)
        //            return null;
        //        return Trigger.InputId.AxisNegated ? -v.Value : v.Value;
        //    }
        //}


        //public bool IsTriggered
        //{
        //    get
        //    {
        //        var v = CurrentValue;
        //        if (v is null)
        //            return false;
        //        return v.Value >= Trigger.MinValue && v.Value <= Trigger.MaxValue;
        //    }
        //}

        internal static TriggerInstance Build(Func<float?> getCurrentValue, Trigger t)
        {

            var isTriggered = new Func<bool>(() =>
            {
                var v = getCurrentValue();
                if (v is null)
                    return false;
                if (t.InputId.AxisNegated)
                    v = -v.Value;

                return v.Value >= t.MinValue && v.Value <= t.MaxValue;
            });
            if (t.AutoOffAfterMs is not null)
            {
                var ao = new TriggerAutoOffRelay(isTriggered, TimeSpan.FromMilliseconds(t.AutoOffAfterMs.Value));
                isTriggered = new Func<bool>(() => ao.Poll());
            }
            if (t.DelayReleaseMs is not null)
            {
                var dr = new TriggerDelayReleaseRelay(isTriggered, TimeSpan.FromMilliseconds(t.DelayReleaseMs.Value));
                isTriggered = new Func<bool>(() => dr.Poll());
            }


            return new TriggerInstance(
                Trigger: t,
                GetCurrentValue: getCurrentValue,
                IsTriggered: isTriggered
                );
        }

        internal static TriggerInstance Load(InputMonitor monitor, Trigger t)
        {
            var f = monitor.GetFunction(t.InputId);
            return Build(f, t);
        }
    }
}

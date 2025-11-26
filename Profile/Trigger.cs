using JoyMap.ControllerTracking;
using JoyMap.Profile.Processing;

namespace JoyMap.Profile
{
    public readonly record struct Trigger(
        ControllerInputId InputId,
        RangeConfig? Range = null,
        DitherConfig? Dither = null
        );

    public readonly record struct RangeConfig(
        float MinValue,
        float MaxValue,
        float? AutoOffAfterMs,
        float? DelayReleaseMs
        );

    public readonly record struct DitherConfig(
        float RampStart,
        float RampMax,
        float Frequency
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
            Func<bool> isTriggered;
            if (t.Range is not null)
            {
                isTriggered = new Func<bool>(() =>
                {
                    var v = getCurrentValue();
                    if (v is null)
                        return false;
                    if (t.InputId.AxisNegated)
                        v = -v.Value;

                    return v.Value >= t.Range.Value.MinValue && v.Value <= t.Range.Value.MaxValue;
                });
                if (t.Range.Value.AutoOffAfterMs is not null)
                {
                    var ao = new TriggerAutoOffRelay(isTriggered, TimeSpan.FromMilliseconds(t.Range.Value.AutoOffAfterMs.Value));
                    isTriggered = new Func<bool>(() => ao.Poll());
                }
                if (t.Range.Value.DelayReleaseMs is not null)
                {
                    var dr = new TriggerDelayReleaseRelay(isTriggered, TimeSpan.FromMilliseconds(t.Range.Value.DelayReleaseMs.Value));
                    isTriggered = new Func<bool>(() => dr.Poll());
                }
            }
            else if (t.Dither is not null)
            {
                var generator = new DitherGenerator(t.Dither.Value, t.InputId.AxisNegated, getCurrentValue);
                isTriggered = generator.Sample;
            }
            else
                throw new InvalidOperationException("Trigger must have either Range or Dither defined.");

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

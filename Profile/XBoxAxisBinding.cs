using JoyMap.ControllerTracking;
using JoyMap.Util;
using JoyMap.XBox;

namespace JoyMap.Profile
{
    public enum XBoxAxisTranslation
    {
        Linear
    }

    public record XBoxInputAxis(
        ControllerInputId InputId,
        float DeadZone,
        XBoxAxisTranslation Translation
        ) : IJsonCompatible;

    public record XBoxAxisBinding(
        IReadOnlyList<XBoxInputAxis> InAxes,
        XBoxAxis OutAxis
        ) : IJsonCompatible;

    public readonly record struct AxisInput(
        XBoxInputAxis Input,
        Func<float?> GetValue
        )
    {
        public static Func<float?> BuildGetter(Func<float?> raw, XBoxInputAxis t)
        {
            return () =>
            {
                var val = raw();
                if (val == null)
                    return null;
                switch (t.Translation)
                {
                    case XBoxAxisTranslation.Linear:
                        float effectiveRange = 1f - t.DeadZone;
                        return Math.Abs(val.Value) <= t.DeadZone
                            ? 0f
                            : val.Value > 0
                                ? (val.Value - t.DeadZone) / effectiveRange
                                : (val.Value + t.DeadZone) / effectiveRange;
                    default:
                        return val;
                }
                ;
            };
        }
        public static AxisInput Load(InputMonitor monitor, XBoxInputAxis t)
        {
            var raw = monitor.GetFunction(t.InputId);
            return new AxisInput(
                Input: t,
                GetValue: BuildGetter(raw, t)
                );
        }
    }

    public record XBoxAxisBindingInstance(
        XBoxAxisBinding Binding,
        IReadOnlyList<AxisInput> InputInstances,
        Func<float?> GetValue
        )
    {
        public bool IsSuspended { get; set; }
        public XBoxAxis OutAxis => Binding.OutAxis;

        internal static XBoxAxisBindingInstance Load(InputMonitor monitor, XBoxAxisBinding e)
        {
            var triggerInstances = e.InAxes
                .Select(t => AxisInput.Load(monitor, t))
                .ToList();
            return new XBoxAxisBindingInstance(
                Binding: e,
                InputInstances: triggerInstances,
                GetValue: () =>
                {
                    float? max = null;
                    foreach (var input in triggerInstances)
                    {
                        var val = input.GetValue();
                        if (val != null)
                        {
                            if (max is null)
                                max = val;
                            else
                                max = Math.Sign(val.Value)
                                    * Math.Max(Math.Abs(val.Value), Math.Abs(max.Value));
                        }
                    }
                    if (max == null)
                        return null;
                    return max;
                }
                );
        }

    }
}

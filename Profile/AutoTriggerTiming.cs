using System.Globalization;

namespace JoyMap.Profile
{
    public readonly struct AutoTriggerTiming(float holdMs, float releaseMs)
    {
        public float HoldMs { get; } = holdMs;
        public float ReleaseMs { get; } = releaseMs;

        public override string ToString() =>
            $"t={HoldMs.ToString(CultureInfo.InvariantCulture)}/{ReleaseMs.ToString(CultureInfo.InvariantCulture)}";
    }
}

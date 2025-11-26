namespace JoyMap.Profile.Processing
{
    public record DitherGenerator(DitherConfig Config, bool IsNegated, Func<float?> GetValue)
    {
        private DateTime Started { get; } = DateTime.UtcNow;

        public bool Sample()
        {
            var elapsed = DateTime.UtcNow - Started;
            var frequency = Config.Frequency;
            var rampStart = Config.RampStart;
            var rampMax = Config.RampMax;
            var raw = GetValue();
            if (raw is null)
                return false;
            var value = IsNegated ? -raw.Value : raw.Value;

            // Value below ramp range - always false
            if (value < rampStart)
                return false;

            // Value at or above ramp range - always true
            if (value >= rampMax)
                return true;

            // Calculate position within current dither iteration
            var totalSeconds = elapsed.TotalSeconds;
            var iterationDuration = 1.0 / frequency;
            var iterationIndex = (long)Math.Floor(totalSeconds * frequency);
            var iterationProgress = (float)((totalSeconds - iterationIndex * iterationDuration) / iterationDuration);

            // Normalize value to [0,1] range and compare with iteration progress
            var normalizedValue = (value - rampStart) / (rampMax - rampStart);
            return iterationProgress > normalizedValue;
        }
    }
}

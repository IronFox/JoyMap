namespace JoyMap.Profile
{
    public record TriggerDelayReleaseRelay(Func<bool> Input, TimeSpan DelayBy)
    {
        private DateTime? LastActiveTime { get; set; }
        public bool Poll()
        {
            var current = Input();

            if (current)
            {
                LastActiveTime = DateTime.UtcNow;
                return true;
            }

            if (LastActiveTime.HasValue && DateTime.UtcNow - LastActiveTime.Value < DelayBy)
            {
                return true;
            }
            return false;
        }
    }
}

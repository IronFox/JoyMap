namespace JoyMap.Profile
{
    public record TriggerAutoOffRelay(Func<bool> Input, TimeSpan AutoOffAfter)
    {
        private DateTime? LastActiveTime { get; set; }
        public bool Poll()
        {
            var current = Input();

            if (LastActiveTime is null && current)
            {
                LastActiveTime = DateTime.UtcNow;
                return true;
            }
            if (current)
            {
                if (DateTime.UtcNow - LastActiveTime >= AutoOffAfter)
                {
                    return false;
                }
                return true;
            }
            else
            {
                LastActiveTime = null;
                return false;
            }

        }
    }
}

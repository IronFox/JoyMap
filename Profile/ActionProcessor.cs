
using JoyMap.Windows;

namespace JoyMap.Profile
{
    internal class ActionProcessor
    {
        public ActionProcessor(EventAction source)
        {
            Source = source;
            Effect = source.SimpleInputEffect ?? throw new InvalidOperationException("Unknown Effect");
            var reaf = Effect.ReAssertIntervalFrequency ?? 0;
            var retrig = Effect.AutoTriggerFrequency ?? 0;
            ReassertDelay = reaf > 0 ? TimeSpan.FromSeconds(1f / reaf) : TimeSpan.MaxValue;
            RetriggerDelay = retrig > 0 ? TimeSpan.FromSeconds(1f / retrig) / 2 : TimeSpan.MaxValue;
            TriggerLimit = Effect.AutoTriggerLimit ?? int.MaxValue;
        }

        private TimeSpan ReassertDelay { get; }
        private TimeSpan RetriggerDelay { get; }
        public EventAction Source { get; }
        public SimpleInputEffect Effect { get; }
        private bool IsDown { get; set; } = false;
        private DateTime Started { get; set; }
        private DateTime LastAction { get; set; }
        private DateTime LastTrigger { get; set; }
        private int TriggerCount { get; set; }
        private int TriggerLimit { get; }

        internal void SetTriggerStatus(bool triggerStatus)
        {
            if (triggerStatus)
            {
                LastAction = Started = DateTime.Now;
                LastTrigger = DateTime.MinValue;
                TriggerCount = 0;
            }
            else
            {
                if (IsDown)
                {
                    KeyDispatch.Up(Effect.Keys);
                    IsDown = false;
                }
            }
        }

        internal void UpdateTriggered()
        {
            var elapsed = DateTime.Now - Started;
            if (elapsed < TimeSpan.FromMilliseconds(Source.DelayMs))
                return;


            if (Effect.AutoTriggerFrequency is null || TriggerCount >= TriggerLimit)
            {
                if (!IsDown)
                {
                    KeyDispatch.Down(Effect.Keys);
                    IsDown = true;
                    LastAction = DateTime.Now;
                }
            }
            else
            {
                elapsed = DateTime.Now - LastTrigger;
                if (elapsed > RetriggerDelay)
                {
                    KeyDispatch.Change(Effect.Keys, !IsDown);
                    IsDown = !IsDown;
                    LastAction = LastTrigger = DateTime.Now;
                    if (IsDown)
                        TriggerCount++;
                }
            }

            elapsed = DateTime.Now - LastAction;
            if (elapsed > ReassertDelay)
            {
                KeyDispatch.Change(Effect.Keys, IsDown, reassert: true);
                LastAction = DateTime.Now;
            }
        }
    }
}

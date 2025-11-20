
using JoyMap.Profile.Processing;
using JoyMap.Windows;

namespace JoyMap.Profile
{
    internal class SimpleActionProcessor : IActionProcessor
    {
        public SimpleActionProcessor(EventAction source, SimpleInputEffect effect)
        {
            Source = source;
            Effect = effect;

            var retrig = Effect.AutoTriggerFrequency ?? 0;
            ToggleInterval = retrig > 0 ? TimeSpan.FromSeconds(1f / retrig) / 2 : TimeSpan.MaxValue;
            TriggerLimit = Effect.AutoTriggerLimit ?? int.MaxValue;
            AutoTriggerDelay = Effect.AutoTriggerDelayMs is not null
                ? TimeSpan.FromMilliseconds(Effect.AutoTriggerDelayMs.Value)
                : TimeSpan.Zero;
            Key = new KeyHandle(Effect.Keys, false);
        }

        private TimeSpan ReassertDelay { get; } = TimeSpan.FromSeconds(0.1f);
        private TimeSpan ToggleInterval { get; }
        private TimeSpan AutoTriggerDelay { get; }
        public KeyHandle Key { get; }
        public EventAction Source { get; }
        public SimpleInputEffect Effect { get; }
        private DateTime Started { get; set; }
        private int TriggerCount { get; set; }
        private int TriggerLimit { get; }

        public void Dispose()
        {
            Key.Dispose();
        }

        public void SetTriggerStatus(bool triggerStatus)
        {
            if (triggerStatus)
            {
                Started = DateTime.Now;
                TriggerCount = 0;
            }
            else
            {
                Key.Release();
            }
        }

        public void UpdateNonTriggered()
        { }

        public void UpdateTriggered()
        {
            var elapsed = DateTime.Now - Started;
            if (elapsed < TimeSpan.FromMilliseconds(Source.DelayMs))
                return;


            if (Effect.AutoTriggerFrequency is null || TriggerCount >= TriggerLimit || elapsed < AutoTriggerDelay)
            {
                Key.Press();
            }
            else
            {
                if (Key.TimeSinceLastChange > ToggleInterval)
                {
                    Key.Toggle();
                    if (Key.IsPressed)
                        TriggerCount++;
                }
            }

            if (Key.TimeSinceLastAction > ReassertDelay)
            {
                Key.Reassert();
            }
        }
    }
}

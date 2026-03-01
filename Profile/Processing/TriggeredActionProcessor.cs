using JoyMap.Windows;

namespace JoyMap.Profile.Processing
{
    internal class TriggeredActionProcessor : IActionProcessor
    {
        public TriggeredActionProcessor(EventAction action, ChangeTriggerInputEffect changeTriggeredInputEffect)
        {
            Action = action;
            ChangeTriggeredInputEffect = changeTriggeredInputEffect;
            Key = new KeyHandle(changeTriggeredInputEffect.Keys, false);
        }

        private TimeSpan HoldDelay { get; } = TimeSpan.FromMilliseconds(100);
        public EventAction Action { get; }
        public ChangeTriggerInputEffect ChangeTriggeredInputEffect { get; }
        public KeyHandle Key { get; }

        private bool HaveStarted { get; set; }

        private DateTime Started { get; set; }

        public void Dispose()
        {
            Key.Dispose();
        }

        public void SetTriggerStatus(bool triggerStatus)
        {
            if (!triggerStatus)
            {
                if (HaveStarted)
                {
                    Key.Press();
                }
            }
            else
            {
                Key.Release();
                Started = DateTime.UtcNow;
                HaveStarted = false;
            }
        }

        public void UpdateTriggered()
        {
            if (!HaveStarted)
            {
                var elapsed = DateTime.UtcNow - Started;
                if (elapsed.TotalMilliseconds >= Action.DelayMs)
                {
                    Key.Press();
                    HaveStarted = true;
                }
            }
            else if (Key.IsPressed)
            {
                var elapsed = Key.TimeSincePressed;
                if (elapsed >= HoldDelay)
                {
                    Key.Release();
                }
            }
        }
        public void UpdateNonTriggered()
        {
            if (Key.IsPressed)
            {
                var elapsed = Key.TimeSincePressed;
                if (elapsed >= HoldDelay)
                {
                    Key.Release();
                }
            }
        }
    }
}

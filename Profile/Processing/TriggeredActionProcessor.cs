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

        private readonly TimeSpan HoldDelay = TimeSpan.FromMilliseconds(100);
        public EventAction Action { get; }
        public ChangeTriggerInputEffect ChangeTriggeredInputEffect { get; }
        public KeyHandle Key { get; }

        private bool haveStarted;

        private DateTime started;

        public void Dispose()
        {
            Key.Dispose();
        }

        public void SetTriggerStatus(bool triggerStatus)
        {
            if (!triggerStatus)
            {
                if (haveStarted)
                {
                    Key.Press();
                }
            }
            else
            {
                Key.Release();
                started = DateTime.UtcNow;
                haveStarted = false;
            }
        }

        public void UpdateTriggered()
        {
            if (!haveStarted)
            {
                var elapsed = DateTime.UtcNow - started;
                if (elapsed.TotalMilliseconds >= Action.DelayMs)
                {
                    Key.Press();
                    haveStarted = true;
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

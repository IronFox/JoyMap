using JoyMap.Windows;

namespace JoyMap.Profile.Processing
{
    internal class TriggeredActionProcessor : IActionProcessor
    {
        public TriggeredActionProcessor(EventAction action, ChangeTriggerInputEffect changeTriggeredInputEffect)
        {
            Action = action;
            ChangeTriggeredInputEffect = changeTriggeredInputEffect;
        }

        private readonly TimeSpan HoldDelay = TimeSpan.FromMilliseconds(100);
        public EventAction Action { get; }
        public ChangeTriggerInputEffect ChangeTriggeredInputEffect { get; }
        private bool downEmitted;

        private DateTime started;
        private DateTime? initialPressed, initialReleased, finalPressed, finalReleased;
        private void Emit()
        {
            KeyDispatch.DownUp(ChangeTriggeredInputEffect.Keys);
            //KeyDispatch.Up(ChangeTriggeredInputEffect.Keys);
        }

        public void SetTriggerStatus(bool triggerStatus)
        {
            if (!triggerStatus)
            {
                if (downEmitted)
                {
                    KeyDispatch.Down(ChangeTriggeredInputEffect.Keys);
                    finalPressed = DateTime.UtcNow;
                }
            }
            else
            {
                downEmitted = false;
                started = DateTime.UtcNow;
                finalPressed = null;
                finalReleased = null;
                initialPressed = null;
                initialReleased = null;

            }


        }

        public void UpdateTriggered()
        {
            if (!downEmitted)
            {
                var elapsed = DateTime.UtcNow - started;
                if (elapsed.TotalMilliseconds >= Action.DelayMs)
                {
                    KeyDispatch.Down(ChangeTriggeredInputEffect.Keys);
                    initialPressed = DateTime.UtcNow;
                    downEmitted = true;
                }
            }
            else if (initialPressed.HasValue && !initialReleased.HasValue)
            {
                var elapsed = DateTime.UtcNow - initialPressed.Value;
                if (elapsed >= HoldDelay)
                {
                    KeyDispatch.Up(ChangeTriggeredInputEffect.Keys);
                    initialReleased = DateTime.UtcNow;
                }
            }
        }
        public void UpdateNonTriggered()
        {
            if (downEmitted && finalPressed.HasValue && !finalReleased.HasValue)
            {
                var elapsed = DateTime.UtcNow - finalPressed.Value;
                if (elapsed >= HoldDelay)
                {
                    KeyDispatch.Up(ChangeTriggeredInputEffect.Keys);
                    finalReleased = DateTime.UtcNow;
                }
            }
        }
    }
}

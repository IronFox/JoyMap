using JoyMap.Windows;

namespace JoyMap.Profile.Processing
{
    internal class TriggeredActionProcessor : IActionProcessor
    {
        private sealed class KeySlot : IDisposable
        {
            private enum SlotState { Idle, Pressing, Cooling }

            private KeyHandle? Handle { get; }
            private TimeSpan PressDuration { get; }
            private SlotState State { get; set; } = SlotState.Idle;
            private DateTime StateEnteredAt { get; set; }
            private bool PendingPress { get; set; }

            public KeySlot(KeyOrButton key, TimeSpan pressDuration)
            {
                PressDuration = pressDuration;
                if (key != KeyOrButton.None)
                    Handle = new KeyHandle(key, false);
            }

            public void RequestPress()
            {
                if (Handle is null)
                    return;
                if (State == SlotState.Idle)
                {
                    Handle.Press();
                    State = SlotState.Pressing;
                    StateEnteredAt = DateTime.UtcNow;
                    PendingPress = false;
                }
                else
                    PendingPress = true;
            }

            public void Update()
            {
                if (Handle is null)
                    return;
                var elapsed = DateTime.UtcNow - StateEnteredAt;
                if (State == SlotState.Pressing && elapsed >= PressDuration)
                {
                    Handle.Release();
                    State = SlotState.Cooling;
                    StateEnteredAt = DateTime.UtcNow;
                }
                else if (State == SlotState.Cooling && elapsed >= PressDuration)
                {
                    State = SlotState.Idle;
                    if (PendingPress)
                    {
                        PendingPress = false;
                        RequestPress();
                    }
                }
            }

            public void Dispose() => Handle?.Dispose();
        }

        public TriggeredActionProcessor(EventAction action, ChangeTriggerInputEffect effect)
        {
            Action = action;
            Effect = effect;
            var pressDuration = TimeSpan.FromMilliseconds(effect.PressDurationMs);
            RisingSlot = new KeySlot(effect.Keys, pressDuration);
            var fallingKey = effect.FallingKeys ?? effect.Keys;
            FallingSlot = fallingKey == effect.Keys ? RisingSlot : new KeySlot(fallingKey, pressDuration);
        }

        public EventAction Action { get; }
        public ChangeTriggerInputEffect Effect { get; }
        private KeySlot RisingSlot { get; }
        private KeySlot FallingSlot { get; }
        private bool HaveStarted { get; set; }
        private DateTime Started { get; set; }

        public void Dispose()
        {
            RisingSlot.Dispose();
            if (!ReferenceEquals(RisingSlot, FallingSlot))
                FallingSlot.Dispose();
        }

        public void SetTriggerStatus(bool triggerStatus)
        {
            if (triggerStatus)
            {
                Started = DateTime.UtcNow;
                HaveStarted = false;
            }
            else
            {
                if (HaveStarted)
                    FallingSlot.RequestPress();
            }
        }

        public void UpdateTriggered()
        {
            if (!HaveStarted)
            {
                var elapsed = DateTime.UtcNow - Started;
                if (elapsed.TotalMilliseconds >= Action.DelayMs)
                {
                    RisingSlot.RequestPress();
                    HaveStarted = true;
                }
            }
            RisingSlot.Update();
            if (!ReferenceEquals(RisingSlot, FallingSlot))
                FallingSlot.Update();
        }

        public void UpdateNonTriggered()
        {
            RisingSlot.Update();
            if (!ReferenceEquals(RisingSlot, FallingSlot))
                FallingSlot.Update();
        }
    }
}

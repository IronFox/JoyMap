namespace JoyMap.Profile.Processing
{
    internal interface IActionProcessor
    {
        /// <summary>
        /// Change notifier when the trigger status changes
        /// </summary>
        /// <param name="triggerStatus">New trigger status (true = active)</param>
        public void SetTriggerStatus(bool triggerStatus);
        /// <summary>
        /// Per-tick update called only if the trigger combination is currently considered active
        /// </summary>
        public void UpdateTriggered();

        public void UpdateNonTriggered();
    }
}
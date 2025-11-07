using JoyMap.ControllerTracking;

namespace JoyMap.Profile
{
    public record Event(string Name,
        IReadOnlyList<Trigger> Triggers,
        IReadOnlyList<EventAction> Actions
        )
    {
    }

    public record EventInstance(
        Event Event,
        IReadOnlyList<TriggerInstance> TriggerInstances
        )
    {
        public IReadOnlyList<EventAction> Actions => Event.Actions;

        internal static EventInstance Load(InputMonitor monitor, Event e)
        {
            var triggerInstances = e.Triggers
                .Select(t => TriggerInstance.Load(monitor, t))
                .ToList();
            return new EventInstance(
                Event: e,
                TriggerInstances: triggerInstances
                );
        }
    }
}

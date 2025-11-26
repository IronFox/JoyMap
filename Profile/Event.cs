using JoyMap.ControllerTracking;

namespace JoyMap.Profile
{
    public record Event(string Name,
        string TriggerCombiner,
        IReadOnlyList<Trigger> Triggers,
        IReadOnlyList<EventAction> Actions
        );

    public record EventInstance(
        Event Event,
        IReadOnlyList<TriggerInstance> TriggerInstances,
        Func<bool> IsTriggered
        )
    {
        public bool IsSuspended { get; set; }
        public IReadOnlyList<EventAction> Actions => Event.Actions;

        internal static EventInstance Load(InputMonitor monitor, Event e)
        {
            var triggerInstances = e.Triggers
                .Select(t => TriggerInstance.Load(monitor, t))
                .ToList();
            return new EventInstance(
                Event: e,
                TriggerInstances: triggerInstances,
                IsTriggered: EventProcessor.BuildTriggerCombiner(
                    e.TriggerCombiner,
                    triggerInstances
                    ) ?? (() => false)
                );
        }

        public EventProcessor? ToProcessor()
        {
            if (IsSuspended)
                return null;
            return new EventProcessor(this);
        }

    }
}

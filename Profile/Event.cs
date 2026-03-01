using JoyMap.ControllerTracking;
using JoyMap.Util;

namespace JoyMap.Profile
{
    public record Event(string Name,
        string TriggerCombiner,
        IReadOnlyList<Trigger> Triggers,
        IReadOnlyList<EventAction> Actions
        ) : IJsonCompatible;

    public record EventInstance(
        Event Event,
        IReadOnlyList<TriggerInstance> TriggerInstances,
        Func<bool> IsTriggered
        )
    {
        public bool IsSuspended { get; set; }
        public IReadOnlyList<EventAction> Actions => Event.Actions;

        internal static EventInstance Load(InputMonitor monitor, Event e, IReadOnlyDictionary<string, Func<bool>>? globalResolvers = null)
        {
            var triggerInstances = e.Triggers
                .Select(t => TriggerInstance.Load(monitor, t))
                .ToList();
            return new EventInstance(
                Event: e,
                TriggerInstances: triggerInstances,
                IsTriggered: EventProcessor.BuildTriggerCombiner(
                    e.TriggerCombiner,
                    triggerInstances,
                    globalResolvers
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

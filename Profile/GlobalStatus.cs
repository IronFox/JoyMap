using JoyMap.ControllerTracking;
using JoyMap.Profile.Processing;
using JoyMap.Util;

namespace JoyMap.Profile
{
    public enum GlobalStatusMode
    {
        AlwaysTrue,
        AlwaysFalse,
        TrueIfCombiner,
        FalseIfCombiner,
        ToggleOffInitially,
        ToggleOnInitially
    }

    public record GlobalStatus(
        string Id,
        string Name,
        GlobalStatusMode Mode,
        string TriggerCombiner,
        IReadOnlyList<Trigger> Triggers
    ) : IJsonCompatible;

    public class GlobalStatusInstance
    {
        public GlobalStatus Status { get; }
        public IReadOnlyList<TriggerInstance> TriggerInstances { get; }
        private Func<bool> CombinerResult { get; }
        public bool IsSuspended { get; set; }

        private bool ToggleState { get; set; }
        private bool LastCombiner { get; set; }

        public string Id => Status.Id;

        public bool CurrentValue => Status.Mode switch
        {
            GlobalStatusMode.AlwaysTrue => true,
            GlobalStatusMode.AlwaysFalse => false,
            GlobalStatusMode.TrueIfCombiner => CombinerResult(),
            GlobalStatusMode.FalseIfCombiner => !CombinerResult(),
            GlobalStatusMode.ToggleOffInitially or GlobalStatusMode.ToggleOnInitially => ToggleState,
            _ => false
        };

        public GlobalStatusInstance(GlobalStatus status, IReadOnlyList<TriggerInstance> triggerInstances, Func<bool> combinerResult)
        {
            Status = status;
            TriggerInstances = triggerInstances;
            CombinerResult = combinerResult;
            ToggleState = status.Mode == GlobalStatusMode.ToggleOnInitially;
        }

        public void Update()
        {
            if (Status.Mode is GlobalStatusMode.ToggleOffInitially or GlobalStatusMode.ToggleOnInitially)
            {
                var current = CombinerResult();
                if (current && !LastCombiner)
                    ToggleState = !ToggleState;
                LastCombiner = current;
            }
        }

        internal static GlobalStatusInstance Load(InputMonitor monitor, GlobalStatus gs)
        {
            var triggerInstances = gs.Triggers
                .Select(t => TriggerInstance.Load(monitor, t))
                .ToList();
            var combiner = EventProcessor.BuildTriggerCombiner(gs.TriggerCombiner, triggerInstances) ?? (() => false);
            return new GlobalStatusInstance(gs, triggerInstances, combiner);
        }
    }
}

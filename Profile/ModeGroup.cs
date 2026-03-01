using JoyMap.ControllerTracking;
using JoyMap.Profile.Processing;
using JoyMap.Util;

namespace JoyMap.Profile
{
    public record ModeEntry(
        string Id,
        string Name,
        string TriggerCombiner,
        IReadOnlyList<Trigger> Triggers
        ) : IJsonCompatible;

    public record ModeGroup(
        string Id,
        string Name,
        string DefaultModeId,
        IReadOnlyList<ModeEntry> Modes
        ) : IJsonCompatible;

    public record ModeEntryInstance(
        ModeEntry Entry,
        IReadOnlyList<TriggerInstance> TriggerInstances,
        Func<bool> IsTriggered
        )
    {
        public bool WasTriggered { get; set; }
        public string Id => Entry.Id;

        internal static ModeEntryInstance Load(InputMonitor monitor, ModeEntry entry, IReadOnlyDictionary<string, Func<bool>>? globalResolvers = null)
        {
            var triggerInstances = entry.Triggers
                .Select(t => TriggerInstance.Load(monitor, t))
                .ToList();
            var combiner = EventProcessor.BuildTriggerCombiner(entry.TriggerCombiner, triggerInstances, globalResolvers)
                ?? (() => false);
            return new ModeEntryInstance(entry, triggerInstances, combiner);
        }
    }

    public class ModeGroupInstance
    {
        public ModeGroup Group { get; }
        public IReadOnlyList<ModeEntryInstance> EntryInstances { get; }
        private string ActiveModeId { get; set; }

        public string Id => Group.Id;

        public string ActiveModeName =>
            EntryInstances.FirstOrDefault(e => e.Id == ActiveModeId)?.Entry.Name
            ?? ActiveModeId;

        public ModeGroupInstance(ModeGroup group, IReadOnlyList<ModeEntryInstance> entryInstances)
        {
            Group = group;
            EntryInstances = entryInstances;
            ActiveModeId = group.DefaultModeId;
        }

        public bool IsModeActive(string entryId) => ActiveModeId == entryId;

        public void Update()
        {
            foreach (var entry in EntryInstances)
            {
                var current = entry.IsTriggered();
                if (current && !entry.WasTriggered)
                    ActiveModeId = entry.Id;
                entry.WasTriggered = current;
            }
        }

        public IReadOnlyDictionary<string, Func<bool>> BuildResolvers() =>
            EntryInstances.ToDictionary(
                e => e.Id,
                e => (Func<bool>)(() => ActiveModeId == e.Id));

        internal static ModeGroupInstance Load(InputMonitor monitor, ModeGroup group, IReadOnlyDictionary<string, Func<bool>>? globalResolvers = null)
        {
            var entryInstances = group.Modes
                .Select(m => ModeEntryInstance.Load(monitor, m, globalResolvers))
                .ToList();
            return new ModeGroupInstance(group, entryInstances);
        }
    }
}

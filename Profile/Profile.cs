using JoyMap.ControllerTracking;
using JoyMap.Windows;

namespace JoyMap.Profile
{
    public record Profile(
        Guid Id,
        string Name,
        string? Notes,
        string? ProcessNameRegex,
        string? WindowNameRegex,
        IReadOnlyList<Event> Events,
        IReadOnlyList<XBoxAxisBinding>? XBoxAxisBindings,
        IReadOnlyList<GlobalStatus>? GlobalStatuses = null,
        int NextGlobalStatusId = 0,
        IReadOnlyList<ModeGroup>? ModeGroups = null,
        int NextModeGroupId = 0,
        int NextModeEntryId = 0,
        bool HideControllers = false
        );


    public record ProfileInstance(
        Profile Profile,
        ProcessRegex ProcessNameRegex,
        IReadOnlyList<EventInstance> EventInstances,
        IReadOnlyList<XBoxAxisBindingInstance> XBoxAxisBindingInstances,
        IReadOnlyList<GlobalStatusInstance> GlobalStatusInstances,
        IReadOnlyList<ModeGroupInstance> ModeGroupInstances
        )
    {
        public bool Is(WindowReference wr)
        {
            return ProcessNameRegex.IsMatch(wr);
        }
        public bool Is(FocusPredicate wr)
        {
            return wr.ProcessNameIsMatch(ProcessNameRegex);
        }

        public static ProfileInstance Load(InputMonitor monitor, Profile profile)
        {
            var globalResolvers = new Dictionary<string, Func<bool>>();

            var modeGroupInstances = (profile.ModeGroups ?? [])
                .Select(mg => ModeGroupInstance.Load(monitor, mg, globalResolvers))
                .ToList();

            foreach (var mg in modeGroupInstances)
                foreach (var kv in mg.BuildResolvers())
                    globalResolvers[kv.Key] = kv.Value;

            var globalStatusInstances = new List<GlobalStatusInstance>();
            foreach (var gs in profile.GlobalStatuses ?? [])
            {
                var inst = GlobalStatusInstance.Load(monitor, gs, globalResolvers);
                globalStatusInstances.Add(inst);
                globalResolvers[inst.Id] = () => inst.CurrentValue;
            }

            var eventInstances = profile.Events
                .Select(e => EventInstance.Load(monitor, e, globalResolvers))
                .ToList();
            var axisBindingInstances = (profile.XBoxAxisBindings ?? [])
                .Select(a => XBoxAxisBindingInstance.Load(monitor, a, globalResolvers))
                .ToList();
            return new ProfileInstance(
                Profile: profile,
                ProcessNameRegex: new(profile.ProcessNameRegex, profile.WindowNameRegex),
                EventInstances: eventInstances,
                XBoxAxisBindingInstances: axisBindingInstances,
                GlobalStatusInstances: globalStatusInstances,
                ModeGroupInstances: modeGroupInstances
                );
        }
    }
}

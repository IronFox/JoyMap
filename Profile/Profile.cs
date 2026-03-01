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
        int NextGlobalStatusId = 0
        );


    public record ProfileInstance(
        Profile Profile,
        ProcessRegex ProcessNameRegex,
        IReadOnlyList<EventInstance> EventInstances,
        IReadOnlyList<XBoxAxisBindingInstance> XBoxAxisBindingInstances,
        IReadOnlyList<GlobalStatusInstance> GlobalStatusInstances
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
            var globalStatusInstances = (profile.GlobalStatuses ?? [])
                .Select(g => GlobalStatusInstance.Load(monitor, g))
                .ToList();

            var globalResolvers = globalStatusInstances
                .ToDictionary(g => g.Id, g => (Func<bool>)(() => g.CurrentValue));

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
                GlobalStatusInstances: globalStatusInstances
                );
        }
    }
}

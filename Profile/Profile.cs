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
        IReadOnlyList<XBoxAxisBinding>? XBoxAxisBindings
        );


    public record ProfileInstance(
        Profile Profile,
        ProcessRegex ProcessNameRegex,
        IReadOnlyList<EventInstance> EventInstances,
        IReadOnlyList<XBoxAxisBindingInstance> XBoxAxisBindingInstances
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
            var eventInstances = profile.Events
                .Select(e => EventInstance.Load(monitor, e))
                .ToList();
            var axisBindingInstances = (profile.XBoxAxisBindings ?? [])
                .Select(a => XBoxAxisBindingInstance.Load(monitor, a))
                .ToList();
            return new ProfileInstance(
                Profile: profile,
                ProcessNameRegex: new(profile.ProcessNameRegex, profile.WindowNameRegex),
                EventInstances: eventInstances,
                XBoxAxisBindingInstances: axisBindingInstances
                );
        }
    }
}

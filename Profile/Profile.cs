using JoyMap.ControllerTracking;
using JoyMap.Windows;

namespace JoyMap.Profile
{
    public record Profile(
        Guid Id,
        string Name,
        string? ProcessNameRegex,
        string? WindowNameRegex,
        IReadOnlyList<Event> Events,
        IReadOnlyList<XBoxMapping>? XBoxMappings
        );


    public record ProfileInstance(
        Profile Profile,
        ProcessRegex ProcessNameRegex,
        IReadOnlyList<EventInstance> EventInstances,
        IReadOnlyList<XBoxMappingInstance> MapperInstances
        )
    {
        public bool Is(WindowReference wr)
        {
            return ProcessNameRegex.IsMatch(wr);
        }

        public static ProfileInstance Load(InputMonitor monitor, Profile profile)
        {
            var eventInstances = profile.Events
                .Select(e => EventInstance.Load(monitor, e))
                .ToList();
            var mapperInstances = (profile.XBoxMappings ?? [])
                .Select(m => XBoxMappingInstance.Load(monitor, m))
                .ToList();
            return new ProfileInstance(
                Profile: profile,
                ProcessNameRegex: new(profile.ProcessNameRegex, profile.WindowNameRegex),
                EventInstances: eventInstances,
                MapperInstances: mapperInstances
                );
        }
    }
}

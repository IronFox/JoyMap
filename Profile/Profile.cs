using JoyMap.ControllerTracking;

namespace JoyMap.Profile
{
    public record Profile(
        Guid Id,
        string Name,
        string WindowNameRegex,
        IReadOnlyList<Event> Events
        );


    public record ProfileInstance(
        Profile Profile,
        IReadOnlyList<EventInstance> EventInstances
        )
    {
        public static ProfileInstance Load(InputMonitor monitor, Profile profile)
        {
            var eventInstances = profile.Events
                .Select(e => EventInstance.Load(monitor, e))
                .ToList();
            return new ProfileInstance(
                Profile: profile,
                EventInstances: eventInstances
                );
        }
    }
}

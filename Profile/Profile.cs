using JoyMap.ControllerTracking;
using JoyMap.Windows;

namespace JoyMap.Profile
{
    public record Profile(
        Guid Id,
        string Name,
        string? ProcessNameRegex,
        string? WindowNameRegex,
        IReadOnlyList<Event> Events
        )
    {
        public Profile FixImport()
        {
            return this with
            {
                Events = [..Events
                    .Select(e => e.FixImport())]
            };

        }
    }


    public record ProfileInstance(
        Profile Profile,
        ProcessRegex ProcessNameRegex,
        IReadOnlyList<EventInstance> EventInstances
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
            return new ProfileInstance(
                Profile: profile,
                ProcessNameRegex: new(profile.ProcessNameRegex, profile.WindowNameRegex),
                EventInstances: eventInstances
                );
        }
    }
}

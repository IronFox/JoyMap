using JoyMap.ControllerTracking;
using System.Text.RegularExpressions;

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
        Regex WindowNameRegex,
        IReadOnlyList<EventInstance> EventInstances
        )
    {
        public bool Is(string windowName)
        {
            return WindowNameRegex.IsMatch(windowName);
        }

        public static ProfileInstance Load(InputMonitor monitor, Profile profile)
        {
            var eventInstances = profile.Events
                .Select(e => EventInstance.Load(monitor, e))
                .ToList();
            return new ProfileInstance(
                Profile: profile,
                WindowNameRegex: new Regex(profile.WindowNameRegex, RegexOptions.Compiled),
                EventInstances: eventInstances
                );
        }
    }
}

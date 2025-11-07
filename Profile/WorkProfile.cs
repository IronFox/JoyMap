namespace JoyMap.Profile
{
    public class WorkProfile
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string WindowRegex { get; set; } = "";
        public List<EventInstance> Events { get; init; } = [];

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        internal static WorkProfile? Load(ProfileInstance? profile)
        {
            if (profile is null)
                return null;
            var workProfile = new WorkProfile
            {
                Id = profile.Profile.Id,
                Name = profile.Profile.Name,
                WindowRegex = profile.Profile.WindowNameRegex,
                Events = [.. profile.EventInstances]
            };
            return workProfile;
        }

        public Profile ToProfile()
        {
            return new Profile
            (
                Id: Id,
                Name: Name,
                WindowNameRegex: WindowRegex,
                Events: Events.Select(x => x.Event).ToList()
            );

        }
    }
}

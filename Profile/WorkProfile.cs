using JoyMap.Undo;

namespace JoyMap.Profile
{
    public class WorkProfile : IProfileInstance
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string ProcessNameRegex { get; set; } = "";
        public string WindowNameRegex { get; set; } = "";
        public List<EventInstance> Events { get; init; } = [];

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        public UndoHistory History { get; } = new();

        public IReadOnlyList<EventInstance> EventInstances => Events;

        public ProfileInstance ToProfileInstance()
        {
            return new ProfileInstance
            (
                Profile: ToProfile(),
                ProcessNameRegex: new(ProcessNameRegex, WindowNameRegex),
                EventInstances: Events
            );
        }

        public Profile ToProfile()
        {
            return new Profile
            (
                Id: Id,
                Name: Name,
                ProcessNameRegex: ProcessNameRegex,
                WindowNameRegex: WindowNameRegex,
                Events: Events.Select(x => x.Event).ToList()
            );

        }

    }
}

using JoyMap.Undo;
using JoyMap.XBox;

namespace JoyMap.Profile
{
    public class WorkProfile : IProfileInstance
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string ProcessNameRegex { get; set; } = "";
        public string WindowNameRegex { get; set; } = "";
        public string? Notes { get; set; }
        public List<EventInstance> Events { get; init; } = [];
        public Dictionary<XBoxAxis, XBoxAxisBindingInstance> AxisBindings { get; init; } = [];

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        public UndoHistory History { get; } = new();

        public IReadOnlyList<EventInstance> EventInstances => Events;
        public IReadOnlyList<XBoxAxisBindingInstance> XBoxAxisBindings => [.. AxisBindings.Values];

        public ProfileInstance ToProfileInstance()
        {
            return new ProfileInstance
            (
                Profile: ToProfile(),
                ProcessNameRegex: new(ProcessNameRegex, WindowNameRegex),
                EventInstances: Events,
                XBoxAxisBindingInstances: XBoxAxisBindings
            );
        }

        public Profile ToProfile()
        {
            return new Profile
            (
                Id: Id,
                Name: Name,
                Notes: Notes,
                ProcessNameRegex: ProcessNameRegex,
                WindowNameRegex: WindowNameRegex,
                Events: Events.Select(x => x.Event).ToList(),
                XBoxAxisBindings: AxisBindings.Values.Select(x => x.Binding).ToList()
            );

        }

    }
}

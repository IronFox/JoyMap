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
        public List<GlobalStatusInstance> GlobalStatuses { get; init; } = [];
        public int NextGlobalStatusId { get; set; }

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        public UndoHistory History { get; } = new();

        public IReadOnlyList<EventInstance> EventInstances => Events;
        public IReadOnlyList<XBoxAxisBindingInstance> XBoxAxisBindings => [.. AxisBindings.Values];
        public IReadOnlyList<GlobalStatusInstance> GlobalStatusInstances => GlobalStatuses;

        public ProfileInstance ToProfileInstance()
        {
            return new ProfileInstance
            (
                Profile: ToProfile(),
                ProcessNameRegex: new(ProcessNameRegex, WindowNameRegex),
                EventInstances: Events,
                XBoxAxisBindingInstances: XBoxAxisBindings,
                GlobalStatusInstances: GlobalStatuses
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
                XBoxAxisBindings: AxisBindings.Values.Select(x => x.Binding).ToList(),
                GlobalStatuses: GlobalStatuses.Select(x => x.Status).ToList(),
                NextGlobalStatusId: NextGlobalStatusId
            );
        }

        public string AllocateNextGlobalStatusId()
        {
            while (GlobalStatuses.Any(g => g.Id == $"G{NextGlobalStatusId}"))
                NextGlobalStatusId++;
            return $"G{NextGlobalStatusId}";
        }

        public void CommitNextGlobalStatusId()
        {
            NextGlobalStatusId++;
        }
    }
}

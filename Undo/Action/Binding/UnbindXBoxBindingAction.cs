using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.XBox;

namespace JoyMap.Undo.Action.Binding
{
    internal class UnbindXBoxBindingAction : CommonAction, IUndoableAction
    {
        public UnbindXBoxBindingAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<XBoxAxis> axes)
            : base(mainForm, activeProfile)
        {
            Axes = axes;
            SingleName = axes
                .Select(i => mainForm.RequireRowOf(i).Axis.ToString())
                .SafeSingleOrDefault();
        }

        public IReadOnlyList<XBoxAxis> Axes { get; }
        public string? SingleName { get; }
        private List<(XBoxAxis Axis, XBoxAxisBindingInstance Binding)> Deleted { get; } = new();
        public string Name => SingleName is not null ? $"Delete '{SingleName}'" : "Delete Bindings";
        public void Execute()
        {
            if (!IsValid)
                return;
            Deleted.Clear();
            foreach (var axis in Axes)
            {
                var item = Form.RequireRowOf(axis);
                if (item.GetBound(out var bound))
                {
                    Deleted.Add((axis, bound));
                    item.Bind(null);
                }
            }
            Deleted.Reverse();
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            foreach (var (Axis, Binding) in Deleted)
            {
                var row = Form.RequireRowOf(Axis);
                row.Bind(Binding);
            }
            Registry.Persist(TargetProfile);
            Deleted.Clear();
        }
    }
}

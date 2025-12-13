using JoyMap.Profile;

namespace JoyMap.Undo.Action.Binding
{
    internal class PasteXBoxBindingsAction : CommonAction, IUndoableAction
    {
        public PasteXBoxBindingsAction(MainForm mainForm, WorkProfile activeProfile,
            IReadOnlyList<XBoxAxisBinding> copied)
        : base(mainForm, activeProfile)
        {
            Copied = copied;
        }

        private List<XBoxAxisBindingInstance> Old { get; } = [];
        public string Name => "Paste XBox Axes";

        public IReadOnlyList<XBoxAxisBinding> Copied { get; }

        public void Execute()
        {
            Old.Clear();

            foreach (var c in Copied)
            {
                var item = Form.RequireRowOf(c.OutAxis);
                if (item.GetBound(out var bound))
                    Old.Add(bound);
                var b = XBoxAxisBindingInstance.Load(Form.InputMonitor, c);
                item.Bind(b);
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            foreach (var b in Old)
            {
                var item = Form.RequireRowOf(b.OutAxis);
                item.Bind(b);
            }
            Registry.Persist(TargetProfile);
        }
    }
}

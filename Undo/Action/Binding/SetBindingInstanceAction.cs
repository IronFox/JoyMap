using JoyMap.Profile;

namespace JoyMap.Undo.Action.Binding
{
    internal class SetBindingInstanceAction : CommonAction, IUndoableAction
    {
        public SetBindingInstanceAction(MainForm mainForm, WorkProfile activeProfile, XBoxAxisBindingInstance newInstance)
            : base(mainForm, activeProfile)
        {
            NewInstance = newInstance;
        }

        public XBoxAxisBindingInstance NewInstance { get; }

        //private ListViewItem? OnUndoRemovedItem { get; set; }

        public string Name => "Set XBox Axis Binding Instance";

        public XBoxAxisBindingInstance? Old { get; private set; }

        public void Execute()
        {
            if (!IsValid)
                return;
            var row = Form.RequireRowOf(NewInstance.OutAxis);
            row.GetBound(out var old);
            Old = old;
            row.Bind(NewInstance);
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            var row = Form.RequireRowOf(NewInstance.OutAxis);
            row.Bind(Old);
            Registry.Persist(TargetProfile);
        }
    }
}

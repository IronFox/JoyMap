using JoyMap.Profile;

namespace JoyMap.Undo.Action.GlobalStatusAction
{
    internal class EditGlobalStatusAction : CommonAction, IUndoableAction
    {
        public EditGlobalStatusAction(MainForm mainForm, WorkProfile activeProfile, int rowIndex, GlobalStatusInstance old, GlobalStatusInstance updated)
            : base(mainForm, activeProfile)
        {
            RowIndex = rowIndex;
            Old = old;
            Updated = updated;
        }

        public int RowIndex { get; }
        public GlobalStatusInstance Old { get; }
        public GlobalStatusInstance Updated { get; }

        public string Name => $"Edit Global Status '{Old.Status.Name}'";

        public void Execute() => Apply(Updated);
        public void Undo() => Apply(Old);

        private void Apply(GlobalStatusInstance inst)
        {
            if (!IsValid)
                return;
            TargetProfile.GlobalStatuses[RowIndex] = inst;
            var row = Form.GlobalStatusListView.Items[RowIndex];
            row.Text = inst.Status.Name;
            row.SubItems[1].Text = inst.Id;
            row.SubItems[2].Text = inst.Status.Mode.ToString();
            row.Tag = inst;
            Registry.Persist(TargetProfile);
        }
    }
}

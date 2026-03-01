using JoyMap.Profile;

namespace JoyMap.Undo.Action.ModeGroupAction
{
    internal class EditModeGroupAction : CommonAction, IUndoableAction
    {
        public EditModeGroupAction(MainForm mainForm, WorkProfile activeProfile, int rowIndex, ModeGroupInstance old, ModeGroupInstance updated)
            : base(mainForm, activeProfile)
        {
            RowIndex = rowIndex;
            Old = old;
            Updated = updated;
        }

        private int RowIndex { get; }
        private ModeGroupInstance Old { get; }
        private ModeGroupInstance Updated { get; }

        public string Name => $"Edit Mode Group '{Old.Group.Name}'";

        public void Execute() => Apply(Updated);
        public void Undo() => Apply(Old);

        private void Apply(ModeGroupInstance inst)
        {
            if (!IsValid)
                return;
            TargetProfile.ModeGroups[RowIndex] = inst;
            var row = Form.ModeGroupListView.Items[RowIndex];
            row.Text = inst.Group.Name;
            row.SubItems[1].Text = inst.Id;
            row.SubItems[2].Text = inst.ActiveModeName;
            row.Tag = inst;
            Registry.Persist(TargetProfile);
        }
    }
}

using JoyMap.Profile;

namespace JoyMap.Undo.Action.ModeGroupAction
{
    internal class DeleteModeGroupAction : CommonAction, IUndoableAction
    {
        public DeleteModeGroupAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes)
            : base(mainForm, activeProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
        }

        private IReadOnlyList<int> SelectedRowIndexes { get; }
        private List<(int Index, ModeGroupInstance Instance)> Deleted { get; } = [];

        public string Name => SelectedRowIndexes.Count == 1
            ? $"Delete '{TargetProfile.ModeGroups.ElementAtOrDefault(SelectedRowIndexes[0])?.Group.Name}'"
            : "Delete Mode Groups";

        public void Execute()
        {
            if (!IsValid)
                return;
            Deleted.Clear();
            for (int i = SelectedRowIndexes.Count - 1; i >= 0; i--)
            {
                var idx = SelectedRowIndexes[i];
                var row = Form.ModeGroupListView.Items[idx];
                var inst = (ModeGroupInstance)row.Tag!;
                Deleted.Add((idx, inst));
                TargetProfile.ModeGroups.RemoveAt(idx);
                Form.ModeGroupListView.Items.RemoveAt(idx);
            }
            Deleted.Reverse();
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            foreach (var (index, inst) in Deleted)
            {
                TargetProfile.ModeGroups.Insert(index, inst);
                var row = Form.ModeGroupListView.Items.Insert(index, inst.Group.Name);
                row.SubItems.Add(inst.Id);
                row.SubItems.Add(inst.ActiveModeName);
                row.Tag = inst;
            }
            Registry.Persist(TargetProfile);
            Deleted.Clear();
        }
    }
}

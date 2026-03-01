using JoyMap.Profile;

namespace JoyMap.Undo.Action.GlobalStatusAction
{
    internal class DeleteGlobalStatusAction : CommonAction, IUndoableAction
    {
        public DeleteGlobalStatusAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes)
            : base(mainForm, activeProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
        }

        public IReadOnlyList<int> SelectedRowIndexes { get; }
        private List<(int Index, GlobalStatusInstance Instance)> Deleted { get; } = [];

        public string Name => SelectedRowIndexes.Count == 1
            ? $"Delete '{TargetProfile.GlobalStatuses.ElementAtOrDefault(SelectedRowIndexes[0])?.Status.Name}'"
            : "Delete Global Statuses";

        public void Execute()
        {
            if (!IsValid)
                return;
            Deleted.Clear();
            for (int i = SelectedRowIndexes.Count - 1; i >= 0; i--)
            {
                var idx = SelectedRowIndexes[i];
                var row = Form.GlobalStatusListView.Items[idx];
                var inst = (GlobalStatusInstance)row.Tag!;
                Deleted.Add((idx, inst));
                TargetProfile.GlobalStatuses.RemoveAt(idx);
                Form.GlobalStatusListView.Items.RemoveAt(idx);
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
                TargetProfile.GlobalStatuses.Insert(index, inst);
                var row = Form.GlobalStatusListView.Items.Insert(index, inst.Status.Name);
                row.SubItems.Add(inst.Id);
                row.SubItems.Add(inst.Status.Mode.ToString());
                row.SubItems.Add("");
                row.Tag = inst;
            }
            Registry.Persist(TargetProfile);
            Deleted.Clear();
        }
    }
}

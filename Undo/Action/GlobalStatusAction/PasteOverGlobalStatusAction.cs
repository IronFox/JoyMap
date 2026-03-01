using JoyMap.Profile;

namespace JoyMap.Undo.Action.GlobalStatusAction
{
    internal class PasteOverGlobalStatusAction : CommonAction, IUndoableAction
    {
        public PasteOverGlobalStatusAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes, IReadOnlyList<GlobalStatus> copiedStatuses)
            : base(mainForm, activeProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
            CopiedStatuses = copiedStatuses;
        }

        private List<GlobalStatusInstance> OldInstances { get; } = [];
        public string Name => "Paste Over Global Statuses";

        public IReadOnlyList<int> SelectedRowIndexes { get; }
        public IReadOnlyList<GlobalStatus> CopiedStatuses { get; }

        public void Execute()
        {
            OldInstances.Clear();

            for (int i = 0; i < CopiedStatuses.Count; i++)
            {
                var idx = SelectedRowIndexes[i];
                OldInstances.Add(TargetProfile.GlobalStatuses[idx]);
                var item = Form.GlobalStatusListView.Items[idx];
                var inst = GlobalStatusInstance.Load(Form.InputMonitor, CopiedStatuses[i]);
                item.Text = inst.Status.Name;
                item.SubItems[1].Text = inst.Id;
                item.SubItems[2].Text = inst.Status.Mode.ToString();
                item.Tag = inst;
                TargetProfile.GlobalStatuses[idx] = inst;
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            for (int i = 0; i < OldInstances.Count; i++)
            {
                var idx = SelectedRowIndexes[i];
                var item = Form.GlobalStatusListView.Items[idx];
                var inst = OldInstances[i];
                item.Text = inst.Status.Name;
                item.SubItems[1].Text = inst.Id;
                item.SubItems[2].Text = inst.Status.Mode.ToString();
                item.Tag = inst;
                TargetProfile.GlobalStatuses[idx] = inst;
            }
            Registry.Persist(TargetProfile);
        }
    }
}

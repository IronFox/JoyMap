using JoyMap.Profile;

namespace JoyMap.Undo.Action.GlobalStatusAction
{
    internal class PasteInsertGlobalStatusAction : CommonAction, IUndoableAction
    {
        public PasteInsertGlobalStatusAction(MainForm mainForm, WorkProfile activeProfile, int insertIndex, IReadOnlyList<GlobalStatus> copiedStatuses)
            : base(mainForm, activeProfile)
        {
            InsertIndex = insertIndex;
            CopiedStatuses = copiedStatuses;
        }

        public int InsertIndex { get; }
        public IReadOnlyList<GlobalStatus> CopiedStatuses { get; }

        private List<ListViewItem> CreatedRows { get; } = new();

        public string Name => "Paste Insert Global Statuses";

        public void Execute()
        {
            if (!IsValid)
                return;

            CreatedRows.Clear();
            for (int i = 0; i < CopiedStatuses.Count; i++)
            {
                var inst = GlobalStatusInstance.Load(Form.InputMonitor, CopiedStatuses[i]);
                var row = Form.GlobalStatusListView.Items.Insert(InsertIndex + i, inst.Status.Name);
                row.SubItems.Add(inst.Id);
                row.SubItems.Add(inst.Status.Mode.ToString());
                row.SubItems.Add("");
                row.Tag = inst;
                TargetProfile.GlobalStatuses.Insert(InsertIndex + i, inst);
                CreatedRows.Add(row);
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            for (int i = CreatedRows.Count - 1; i >= 0; i--)
            {
                var row = CreatedRows[i];
                TargetProfile.GlobalStatuses.RemoveAt(InsertIndex + i);
                Form.GlobalStatusListView.Items.Remove(row);
            }
            Registry.Persist(TargetProfile);
            CreatedRows.Clear();
        }
    }
}

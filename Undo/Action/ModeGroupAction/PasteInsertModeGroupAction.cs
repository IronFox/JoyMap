using JoyMap.Profile;

namespace JoyMap.Undo.Action.ModeGroupAction
{
    internal class PasteInsertModeGroupAction : CommonAction, IUndoableAction
    {
        public PasteInsertModeGroupAction(MainForm mainForm, WorkProfile activeProfile, int insertIndex, IReadOnlyList<ModeGroup> copiedGroups)
            : base(mainForm, activeProfile)
        {
            InsertIndex = insertIndex;
            CopiedGroups = copiedGroups;
        }

        public int InsertIndex { get; }
        public IReadOnlyList<ModeGroup> CopiedGroups { get; }

        private List<(ModeGroupInstance Instance, ListViewItem Row)> Created { get; } = [];

        public string Name => "Paste Insert Mode Groups";

        public void Execute()
        {
            if (!IsValid)
                return;

            Created.Clear();
            for (int i = 0; i < CopiedGroups.Count; i++)
            {
                var groupId = TargetProfile.AllocateNextModeGroupId();
                TargetProfile.CommitNextModeGroupId();

                // Re-allocate entry IDs to avoid collisions
                var newModes = CopiedGroups[i].Modes.Select(m =>
                {
                    var newEntryId = TargetProfile.AllocateNextModeEntryId();
                    TargetProfile.CommitNextModeEntryId();
                    return m with { Id = newEntryId };
                }).ToList();

                var oldDefaultId = CopiedGroups[i].DefaultModeId;
                var defaultIndex = CopiedGroups[i].Modes.Select((m, idx) => (m, idx))
                    .FirstOrDefault(x => x.m.Id == oldDefaultId).idx;
                var newDefaultId = newModes.Count > 0 ? newModes[Math.Clamp(defaultIndex, 0, newModes.Count - 1)].Id : groupId;

                var newGroup = CopiedGroups[i] with { Id = groupId, Modes = newModes, DefaultModeId = newDefaultId };
                var inst = ModeGroupInstance.Load(Form.InputMonitor, newGroup);

                TargetProfile.ModeGroups.Insert(InsertIndex + i, inst);
                var row = Form.ModeGroupListView.Items.Insert(InsertIndex + i, inst.Group.Name);
                row.SubItems.Add(inst.Id);
                row.SubItems.Add(inst.ActiveModeName);
                row.Tag = inst;
                Created.Add((inst, row));
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            for (int i = Created.Count - 1; i >= 0; i--)
            {
                TargetProfile.ModeGroups.RemoveAt(InsertIndex + i);
                Form.ModeGroupListView.Items.Remove(Created[i].Row);
            }
            Created.Clear();
            Registry.Persist(TargetProfile);
        }
    }
}

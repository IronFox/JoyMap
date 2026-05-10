using JoyMap.Profile;

namespace JoyMap.Undo.Action.ModeGroupAction
{
    internal class PasteOverModeGroupAction : CommonAction, IUndoableAction
    {
        public PasteOverModeGroupAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedIndexes, IReadOnlyList<ModeGroup> copiedGroups)
            : base(mainForm, activeProfile)
        {
            SelectedIndexes = selectedIndexes;
            CopiedGroups = copiedGroups;
        }

        public IReadOnlyList<int> SelectedIndexes { get; }
        public IReadOnlyList<ModeGroup> CopiedGroups { get; }
        private List<ModeGroupInstance> OldInstances { get; } = [];

        public string Name => "Paste Over Mode Groups";

        public void Execute()
        {
            OldInstances.Clear();

            for (int i = 0; i < CopiedGroups.Count; i++)
            {
                var idx = SelectedIndexes[i];
                OldInstances.Add(TargetProfile.ModeGroups[idx]);

                var existingId = TargetProfile.ModeGroups[idx].Id;

                // Re-allocate entry IDs mapped from old -> new
                var newModes = CopiedGroups[i].Modes.Select(m =>
                {
                    var newEntryId = TargetProfile.AllocateNextModeEntryId();
                    TargetProfile.CommitNextModeEntryId();
                    return m with { Id = newEntryId };
                }).ToList();

                var oldDefaultId = CopiedGroups[i].DefaultModeId;
                var defaultIndex = CopiedGroups[i].Modes.Select((m, di) => (m, di))
                    .FirstOrDefault(x => x.m.Id == oldDefaultId).di;
                var newDefaultId = newModes.Count > 0 ? newModes[Math.Clamp(defaultIndex, 0, newModes.Count - 1)].Id : existingId;

                var newGroup = CopiedGroups[i] with { Id = existingId, Modes = newModes, DefaultModeId = newDefaultId };
                var inst = ModeGroupInstance.Load(Form.InputMonitor, newGroup);

                TargetProfile.ModeGroups[idx] = inst;
                var row = Form.ModeGroupListView.Items[idx];
                row.Text = inst.Group.Name;
                row.SubItems[1].Text = inst.Id;
                row.SubItems[2].Text = inst.ActiveModeName;
                row.Tag = inst;
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            for (int i = 0; i < OldInstances.Count; i++)
            {
                var idx = SelectedIndexes[i];
                var inst = OldInstances[i];
                TargetProfile.ModeGroups[idx] = inst;
                var row = Form.ModeGroupListView.Items[idx];
                row.Text = inst.Group.Name;
                row.SubItems[1].Text = inst.Id;
                row.SubItems[2].Text = inst.ActiveModeName;
                row.Tag = inst;
            }
            Registry.Persist(TargetProfile);
        }
    }
}

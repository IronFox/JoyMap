using JoyMap.Profile;

namespace JoyMap.Undo.Action.ModeGroupAction
{
    internal class AddModeGroupAction : CommonAction, IUndoableAction
    {
        public AddModeGroupAction(MainForm mainForm, WorkProfile activeProfile, ModeGroupInstance newInstance)
            : base(mainForm, activeProfile)
        {
            NewInstance = newInstance;
        }

        private ModeGroupInstance NewInstance { get; }
        private ListViewItem? AddedRow { get; set; }

        public string Name => "Add Mode Group";

        public void Execute()
        {
            if (!IsValid)
                return;
            TargetProfile.NextModeGroupId = Math.Max(
                TargetProfile.NextModeGroupId,
                int.Parse(NewInstance.Id[2..]) + 1);
            foreach (var e in NewInstance.EntryInstances)
                if (e.Id.StartsWith('M') && int.TryParse(e.Id[1..], out var n))
                    TargetProfile.NextModeEntryId = Math.Max(TargetProfile.NextModeEntryId, n + 1);
            TargetProfile.ModeGroups.Add(NewInstance);
            AddedRow = Form.ModeGroupListView.Items.Add(NewInstance.Group.Name);
            AddedRow.SubItems.Add(NewInstance.Id);
            AddedRow.SubItems.Add(NewInstance.ActiveModeName);
            AddedRow.Tag = NewInstance;
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid || AddedRow is null)
                return;
            TargetProfile.ModeGroups.Remove(NewInstance);
            Form.ModeGroupListView.Items.Remove(AddedRow);
            AddedRow = null;
            Registry.Persist(TargetProfile);
        }
    }
}

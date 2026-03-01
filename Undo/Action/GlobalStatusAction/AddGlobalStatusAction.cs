using JoyMap.Profile;

namespace JoyMap.Undo.Action.GlobalStatusAction
{
    internal class AddGlobalStatusAction : CommonAction, IUndoableAction
    {
        public AddGlobalStatusAction(MainForm mainForm, WorkProfile activeProfile, GlobalStatusInstance newInstance)
            : base(mainForm, activeProfile)
        {
            NewInstance = newInstance;
        }

        public GlobalStatusInstance NewInstance { get; }
        private ListViewItem? AddedRow { get; set; }

        public string Name => "Add Global Status";

        public void Execute()
        {
            if (!IsValid)
                return;
            TargetProfile.NextGlobalStatusId = Math.Max(
                TargetProfile.NextGlobalStatusId,
                int.Parse(NewInstance.Id[1..]) + 1
            );
            TargetProfile.GlobalStatuses.Add(NewInstance);
            AddedRow = Form.GlobalStatusListView.Items.Add(NewInstance.Status.Name);
            AddedRow.SubItems.Add(NewInstance.Id);
            AddedRow.SubItems.Add(NewInstance.Status.Mode.ToString());
            AddedRow.SubItems.Add("");
            AddedRow.Tag = NewInstance;
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid || AddedRow is null)
                return;
            TargetProfile.GlobalStatuses.Remove(NewInstance);
            Form.GlobalStatusListView.Items.Remove(AddedRow);
            AddedRow = null;
            Registry.Persist(TargetProfile);
        }
    }
}

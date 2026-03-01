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
        private ListViewItem? _addedRow;

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
            _addedRow = Form.GlobalStatusListView.Items.Add(NewInstance.Status.Name);
            _addedRow.SubItems.Add(NewInstance.Id);
            _addedRow.SubItems.Add(NewInstance.Status.Mode.ToString());
            _addedRow.SubItems.Add("");
            _addedRow.Tag = NewInstance;
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid || _addedRow is null)
                return;
            TargetProfile.GlobalStatuses.Remove(NewInstance);
            Form.GlobalStatusListView.Items.Remove(_addedRow);
            _addedRow = null;
            Registry.Persist(TargetProfile);
        }
    }
}

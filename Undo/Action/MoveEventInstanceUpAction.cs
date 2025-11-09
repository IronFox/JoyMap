using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class MoveEventInstanceUpAction : CommonMoveInstanceAction, IUndoableAction
    {
        public MoveEventInstanceUpAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes)
            : base(mainForm, activeProfile, selectedRowIndexes)
        { }

        public void Execute()
        {
            if (!IsValid)
                return;
            Form.EventListView.BeginUpdate();
            try
            {
                foreach (var oldRowIndex in SelectedRowIndexes)
                {
                    if (oldRowIndex <= 0)
                        continue;
                    var eventInstance = TargetProfile.Events[oldRowIndex];
                    TargetProfile.Events.RemoveAt(oldRowIndex);
                    TargetProfile.Events.Insert(oldRowIndex - 1, eventInstance);
                    var row = Form.EventListView.Items[oldRowIndex];
                    Form.EventListView.Items.RemoveAt(oldRowIndex);
                    Form.EventListView.Items.Insert(oldRowIndex - 1, row);
                }
                Registry.Persist(TargetProfile);
            }
            finally
            {
                Form.EventListView.EndUpdate();
            }

        }

        public void Undo()
        {
            if (!IsValid)
                return;
            Form.EventListView.BeginUpdate();
            try
            {
                for (int i = SelectedRowIndexes.Count - 1; i >= 0; i--)
                {
                    var oldRowIndex = SelectedRowIndexes[i];
                    if (oldRowIndex <= 0)
                        continue;
                    var eventInstance = TargetProfile.Events[oldRowIndex - 1];
                    TargetProfile.Events.RemoveAt(oldRowIndex - 1);
                    TargetProfile.Events.Insert(oldRowIndex, eventInstance);
                    var row = Form.EventListView.Items[oldRowIndex - 1];
                    Form.EventListView.Items.RemoveAt(oldRowIndex - 1);
                    Form.EventListView.Items.Insert(oldRowIndex, row);
                }
                Registry.Persist(TargetProfile);
            }
            finally
            {
                Form.EventListView.EndUpdate();
            }
        }
    }
}

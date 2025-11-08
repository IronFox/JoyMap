using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class MoveEventInstanceDownAction : CommonAction, IUndoableAction
    {
        public MoveEventInstanceDownAction(MainForm mainForm, WorkProfile activeProfile, int oldRowIndex)
            : base(mainForm, activeProfile)
        {
            OldRowIndex = oldRowIndex;
            EventName = TargetProfile.Events[oldRowIndex].Event.Name;
        }

        public int OldRowIndex { get; }
        public string EventName { get; }

        public string Name => $"Move '{EventName}' Down";

        public void Execute()
        {
            if (!IsValid || OldRowIndex >= TargetProfile.Events.Count - 1)
                return;

            var eventInstance = TargetProfile.Events[OldRowIndex];
            TargetProfile.Events.RemoveAt(OldRowIndex);
            TargetProfile.Events.Insert(OldRowIndex + 1, eventInstance);
            Form.EventListView.BeginUpdate();
            try
            {
                var row = Form.EventListView.Items[OldRowIndex];
                Form.EventListView.Items.RemoveAt(OldRowIndex);
                Form.EventListView.Items.Insert(OldRowIndex + 1, row);
                Registry.Persist(TargetProfile);
            }
            finally
            {
                Form.EventListView.EndUpdate();
            }
        }

        public void Undo()
        {
            if (!IsValid || OldRowIndex >= TargetProfile.Events.Count - 1)
                return;

            var eventInstance = TargetProfile.Events[OldRowIndex + 1];
            TargetProfile.Events.RemoveAt(OldRowIndex + 1);
            TargetProfile.Events.Insert(OldRowIndex, eventInstance);
            Form.EventListView.BeginUpdate();
            try
            {
                var row = Form.EventListView.Items[OldRowIndex + 1];
                Form.EventListView.Items.RemoveAt(OldRowIndex + 1);
                Form.EventListView.Items.Insert(OldRowIndex, row);
                Registry.Persist(TargetProfile);
            }
            finally
            {
                Form.EventListView.EndUpdate();
            }
        }
    }
}

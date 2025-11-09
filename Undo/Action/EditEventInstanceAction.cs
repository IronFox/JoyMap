using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class EditEventInstanceAction : CommonAction, IUndoableAction
    {
        public EditEventInstanceAction(MainForm mainForm, WorkProfile activeProfile, int viewRowIndex, EventInstance old, EventInstance newEvent)
            : base(mainForm, activeProfile)
        {
            ViewRowIndex = viewRowIndex;
            OldInstance = old;
            NewInstance = newEvent;
        }

        public int ViewRowIndex { get; }
        public EventInstance OldInstance { get; }
        public EventInstance NewInstance { get; }

        public string Name => $"Edit '{OldInstance.Event.Name}'";
        public void Execute()
        {
            if (!IsValid)
                return;
            TargetProfile.Events.RemoveAt(ViewRowIndex);
            TargetProfile.Events.Insert(ViewRowIndex, NewInstance);
            var row = Form.EventListView.Items[ViewRowIndex];
            row.Text = NewInstance.Event.Name;
            row.SubItems[1].Text = string.Join(", ", NewInstance.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
            row.SubItems[2].Text = string.Join(", ", NewInstance.Actions.Select(x => x.Action));
            row.Tag = NewInstance;
            Registry.Persist(TargetProfile, Form);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            TargetProfile.Events.RemoveAt(ViewRowIndex);
            TargetProfile.Events.Insert(ViewRowIndex, OldInstance);
            var row = Form.EventListView.Items[ViewRowIndex];
            row.Text = OldInstance.Event.Name;
            row.SubItems[1].Text = string.Join(", ", OldInstance.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
            row.SubItems[2].Text = string.Join(", ", OldInstance.Actions.Select(x => x.Action));
            row.Tag = OldInstance;
            Registry.Persist(TargetProfile, Form);

        }
    }
}

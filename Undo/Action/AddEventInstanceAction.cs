using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class AddEventInstanceAction : CommonAction, IUndoableAction
    {
        public AddEventInstanceAction(MainForm mainForm, WorkProfile activeProfile, EventInstance newInstance)
            : base(mainForm, activeProfile)
        {
            NewInstance = newInstance;
        }

        public EventInstance NewInstance { get; }

        private ListViewItem? OnUndoRemovedItem { get; set; }

        public string Name => "Add Event Instance";

        public void Execute()
        {
            if (!IsValid)
                return;
            var row = OnUndoRemovedItem = Form.EventListView.Items.Add(NewInstance.Event.Name);
            row.Tag = NewInstance;
            row.SubItems.Add(string.Join(", ", NewInstance.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
            row.SubItems.Add(string.Join(", ", NewInstance.Actions.Select(x => x.Action)));
            row.SubItems.Add("");
            TargetProfile.Events.Add(NewInstance);
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            if (!IsValid || OnUndoRemovedItem is null)
                return;
            TargetProfile.Events.Remove(NewInstance);
            Form.EventListView.Items.Remove(OnUndoRemovedItem);
            Registry.Persist(TargetProfile);
            OnUndoRemovedItem = null;
        }
    }
}

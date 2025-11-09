using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class DeleteEventInstancesAction : CommonAction, IUndoableAction
    {
        public DeleteEventInstancesAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes)
            : base(mainForm, activeProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
            SingleEventName = SelectedRowIndexes.Count == 1
                ? TargetProfile.Events[SelectedRowIndexes[0]].Event.Name
                : null;
        }

        public IReadOnlyList<int> SelectedRowIndexes { get; }
        public string? SingleEventName { get; }
        private List<(int Index, EventInstance EventInstance)> DeletedEvents { get; } = new();
        public string Name => SingleEventName is not null ? $"Delete '{SingleEventName}'" : "Delete Event Instances";
        public void Execute()
        {
            if (!IsValid)
                return;
            DeletedEvents.Clear();
            for (int i = SelectedRowIndexes.Count - 1; i >= 0; i--)
            {
                var idx = SelectedRowIndexes[i];
                var item = Form.EventListView.Items[idx];
                var ev = (EventInstance)item.Tag!;
                DeletedEvents.Add((idx, ev));
                TargetProfile.Events.RemoveAt(idx);
                Form.EventListView.Items.RemoveAt(idx);
            }
            DeletedEvents.Reverse();
            Registry.Persist(TargetProfile, Form);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            foreach (var (Index, EventInstance) in DeletedEvents)
            {
                var row = Form.EventListView.Items.Insert(Index, EventInstance.Event.Name);
                row.Tag = EventInstance;
                row.SubItems.Add(string.Join(", ", EventInstance.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
                row.SubItems.Add(string.Join(", ", EventInstance.Actions.Select(x => x.Action)));
                row.SubItems.Add("");
                TargetProfile.Events.Insert(Index, EventInstance);
            }
            Registry.Persist(TargetProfile, Form);
            DeletedEvents.Clear();
        }
    }
}

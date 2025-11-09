using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class PasteOverEventInstancesAction : CommonAction, IUndoableAction
    {
        public PasteOverEventInstancesAction(MainForm mainForm, WorkProfile activeProfile, IReadOnlyList<int> selectedRowIndexes, IReadOnlyList<Event> copiedEvents)
            : base(mainForm, activeProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
            CopiedEvents = copiedEvents;
        }

        private List<EventInstance> OldEvents { get; } = [];
        public string Name => "Paste Over Event Instances";

        public IReadOnlyList<int> SelectedRowIndexes { get; }
        public IReadOnlyList<Event> CopiedEvents { get; }

        public void Execute()
        {
            OldEvents.Clear();

            for (int i = 0; i < CopiedEvents.Count; i++)
            {
                var idx = SelectedRowIndexes[i];
                OldEvents.Add(TargetProfile.Events[idx]);
                var item = Form.EventListView.Items[idx];
                var ev = EventInstance.Load(Form.InputMonitor, CopiedEvents[i]);
                item.SubItems[0].Text = ev.Event.Name;
                item.SubItems[1].Text = string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
                item.SubItems[2].Text = string.Join(", ", ev.Actions.Select(x => x.Action));
                item.Tag = ev;
                TargetProfile.Events[idx] = ev;
            }
            Registry.Persist(TargetProfile);
        }

        public void Undo()
        {
            for (int i = 0; i < OldEvents.Count; i++)
            {
                var idx = SelectedRowIndexes[i];
                var item = Form.EventListView.Items[idx];
                var ev = OldEvents[i];
                item.SubItems[0].Text = ev.Event.Name;
                item.SubItems[1].Text = string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
                item.SubItems[2].Text = string.Join(", ", ev.Actions.Select(x => x.Action));
                item.Tag = ev;
                TargetProfile.Events[idx] = ev;
            }
            Registry.Persist(TargetProfile);
        }
    }
}

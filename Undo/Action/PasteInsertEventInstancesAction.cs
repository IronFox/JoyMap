using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class PasteInsertEventInstancesAction : CommonAction, IUndoableAction
    {
        public PasteInsertEventInstancesAction(MainForm mainForm, WorkProfile activeProfile, int insertIndex, IReadOnlyList<Event> copiedEvents)
            : base(mainForm, activeProfile)
        {
            InsertIndex = insertIndex;
            CopiedEvents = copiedEvents;
        }

        public int InsertIndex { get; }
        public IReadOnlyList<Event> CopiedEvents { get; }

        private List<ListViewItem> CreatedRows { get; } = new();

        public string Name => "Paste Insert Event Instances";

        public void Execute()
        {
            if (!IsValid)
                return;

            CreatedRows.Clear();
            for (int i = 0; i < CopiedEvents.Count; i++)
            {
                var ev = EventInstance.Load(Form.InputMonitor, CopiedEvents[i]);
                var row = Form.EventListView.Items.Insert(InsertIndex + i, ev.Event.Name);
                row.Tag = ev;
                row.SubItems.Add(string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
                row.SubItems.Add(string.Join(", ", ev.Actions.Select(x => x.Action)));
                row.SubItems.Add("");
                TargetProfile.Events.Insert(InsertIndex + i, ev);
                CreatedRows.Add(row);
            }
            Registry.Persist(TargetProfile, Form);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            for (int i = CreatedRows.Count - 1; i >= 0; i--)
            {
                var row = CreatedRows[i];
                TargetProfile.Events.RemoveAt(InsertIndex + i);
                Form.EventListView.Items.Remove(row);
            }
            Registry.Persist(TargetProfile, Form);
            CreatedRows.Clear();
        }
    }
}

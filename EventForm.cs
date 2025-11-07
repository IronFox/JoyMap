using JoyMap.Extensions;
using JoyMap.Profile;

namespace JoyMap
{
    public partial class EventForm : Form
    {
        public EventForm(EventInstance? instance = null)
        {
            InitializeComponent();
            if (instance is not null)
            {
                textName.Text = instance.Event.Name;
                foreach (var t in instance.TriggerInstances)
                {
                    var idx = Triggers.Count;
                    var row = triggerListView.Items.Add($"T{idx}");
                    row.SubItems.Add(t.Trigger.InputId.ControllerName);
                    row.SubItems.Add(t.Trigger.InputId.AxisName);
                    row.SubItems.Add(t.IsTriggered ? "A" : "");
                    row.Tag = t;
                    Triggers.Add((t, row));
                }
                foreach (var a in instance.Event.Actions)
                {
                    var row = actionListView.Items.Add(a.Name);
                    row.SubItems.Add(a.TypeName);
                    row.SubItems.Add(a.Action);
                    row.Tag = a;
                }
                Rebuild();
            }
        }

        private List<(TriggerInstance Trigger, ListViewItem Row)> Triggers { get; } = [];


        private void pickAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TriggerForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                var idx = Triggers.Count;
                var t = form.Result;

                var row = triggerListView.Items.Add($"T{idx}");
                row.SubItems.Add(t.Trigger.InputId.ControllerName);
                row.SubItems.Add(t.Trigger.InputId.AxisName);
                row.SubItems.Add(t.IsTriggered ? "A" : "");
                row.Tag = t;
                Triggers.Add((t, row));
                Rebuild();
            }
        }

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            foreach (var (trigger, row) in Triggers)
            {
                row.SubItems[3].Text = trigger.IsTriggered ? "A" : "";
            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            statusUpdateTimer.Stop();
            base.OnFormClosing(e);
        }


        private void triggerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deleteToolStripMenuItem1.Enabled = triggerListView.SelectedItems.Count > 0;
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var query = "Delete selected triggers?";
            if (triggerListView.SelectedItems.Count == 1)
            {
                var item = triggerListView.SelectedItems[0];
                var trigger = item.Tag as TriggerInstance;
                query = "Delete selected trigger (" + trigger?.Trigger.InputId.ControllerName + " - " + trigger?.Trigger.InputId.AxisName + ")?";
            }
            if (MessageBox.Show(this, query, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem item in triggerListView.SelectedItems)
                {
                    if (item.Tag is TriggerInstance t)
                    {
                        var toRemove = Triggers.FirstOrDefault(x => x.Trigger == t);
                        if (toRemove.Trigger is not null)
                        {
                            Triggers.Remove(toRemove);
                        }
                    }
                    triggerListView.Items.Remove(item);
                }
                foreach (var t in Triggers)
                {
                    t.Row.SubItems[0].Text = $"T{t.Row.Index}";
                }
                Rebuild();
            }
        }

        private void triggerListView_DoubleClick(object sender, EventArgs e)
        {
            if (triggerListView.SelectedItems.Count == 1)
            {
                var item = triggerListView.SelectedItems[0];
                if (item.Tag is TriggerInstance t)
                {
                    var form = new TriggerForm(t);
                    var result = form.ShowDialog(this);
                    if (result == DialogResult.OK && form.Result is not null)
                    {
                        var updated = form.Result;
                        item.SubItems[1].Text = updated.Trigger.InputId.ControllerName;
                        item.SubItems[2].Text = updated.Trigger.InputId.AxisName;
                        item.Tag = updated;
                        var idx = Triggers.FindIndex(x => x.Trigger == t);
                        if (idx >= 0)
                        {
                            Triggers[idx] = (updated, item);
                        }
                        Rebuild();
                    }
                }
            }

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ActionForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                var action = form.Result;
                var row = actionListView.Items.Add(action.Name);
                row.SubItems.Add(action.TypeName);
                row.SubItems.Add(action.Action);
                row.Tag = action;
                Rebuild();

            }

        }

        private void actionMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deleteToolStripMenuItem.Enabled = actionListView.SelectedItems.Count > 0;

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var query = "Delete selected actions?";
            if (actionListView.SelectedItems.Count == 1)
            {
                var item = actionListView.SelectedItems[0];
                var action = item.Tag as EventAction;
                query = "Delete selected action (" + action?.Name + ")?";
            }
            if (MessageBox.Show(this, query, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem item in actionListView.SelectedItems)
                {
                    actionListView.Items.Remove(item);
                }
                Rebuild();

            }
        }

        private void actionListView_DoubleClick(object sender, EventArgs e)
        {
            if (actionListView.SelectedItems.Count == 1)
            {
                var item = actionListView.SelectedItems[0];
                if (item.Tag is EventAction action)
                {
                    var form = new ActionForm(action);
                    var result = form.ShowDialog(this);
                    if (result == DialogResult.OK && form.Result is not null)
                    {
                        var updated = form.Result;
                        item.SubItems[0].Text = updated.Name;
                        item.SubItems[1].Text = updated.TypeName;
                        item.SubItems[2].Text = updated.Action;
                        item.Tag = updated;
                        Rebuild();
                    }
                }
            }

        }


        public EventInstance? Result { get; private set; }


        private void Rebuild()
        {
            btnOk.Enabled = Triggers.Count > 0 && actionListView.Items.Count > 0 && !string.IsNullOrWhiteSpace(textName.Text);
            if (btnOk.Enabled)
            {
                var actions = actionListView.Items.ToEnumerable()
                        .Select(x => x.Tag as EventAction!)
                        .Where(x => x is not null).ToList();
                var eventObj = new EventInstance(
                    Event: new(
                        Name: textName.Text.Trim(),
                        Triggers: this.Triggers.Select(x => x.Trigger.Trigger).ToList(),
                        Actions: actions!
                    ),
                    TriggerInstances: this.Triggers.Select(x => x.Trigger).ToList()
                    );
                Result = eventObj;
            }
            else
            {
                Result = null;
            }
        }

        private void AnyChanged(object sender, EventArgs e)
        {
            Rebuild();
        }
    }
}

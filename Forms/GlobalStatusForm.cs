using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.Profile.Processing;

namespace JoyMap.Forms
{
    public partial class GlobalStatusForm : Form
    {
        public GlobalStatusForm(string id, GlobalStatusInstance? instance = null)
        {
            InitializeComponent();
            StatusId = id;
            lId.Text = $"ID: {id}";

            foreach (GlobalStatusMode mode in Enum.GetValues<GlobalStatusMode>())
                cbMode.Items.Add(mode);

            if (instance is not null)
            {
                textName.Text = instance.Status.Name;
                cbMode.SelectedItem = instance.Status.Mode;
                if (!triggerCombiner.Items.Contains(instance.Status.TriggerCombiner))
                    triggerCombiner.Items.Add(instance.Status.TriggerCombiner);
                triggerCombiner.Text = instance.Status.TriggerCombiner;
                foreach (var (t, idx) in instance.TriggerInstances.Select((t, i) => (t, i)))
                {
                    var row = triggerListView.Items.Add($"T{idx}");
                    row.SubItems.Add(t.Trigger.InputId.ControllerName);
                    row.SubItems.Add(t.Trigger.InputId.AxisName);
                    row.SubItems.Add(t.IsTriggered() ? "A" : "");
                    row.Tag = t;
                    Triggers.Add((t, row));
                }
            }
            else
            {
                cbMode.SelectedIndex = 0;
                triggerCombiner.Items.Add("Or");
                triggerCombiner.Items.Add("And");
                triggerCombiner.SelectedIndex = 0;
            }

            UpdateVisibility();
            Rebuild();
        }

        public string StatusId { get; }
        public GlobalStatusInstance? Result { get; private set; }

        private List<(TriggerInstance Trigger, ListViewItem Row)> Triggers { get; } = [];

        private GlobalStatusMode SelectedMode =>
            cbMode.SelectedItem is GlobalStatusMode m ? m : GlobalStatusMode.AlwaysTrue;

        private bool NeedsCombiner => SelectedMode is
            GlobalStatusMode.TrueIfCombiner or
            GlobalStatusMode.FalseIfCombiner or
            GlobalStatusMode.ToggleOffInitially or
            GlobalStatusMode.ToggleOnInitially;

        private void UpdateVisibility()
        {
            panelCombiner.Visible = NeedsCombiner;
            ClientSize = new Size(ClientSize.Width, NeedsCombiner ? 680 : 195);
            Rebuild();
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }

        private void Rebuild()
        {
            btnOk.Enabled = false;
            Result = null;

            if (string.IsNullOrWhiteSpace(textName.Text))
                return;

            if (NeedsCombiner)
            {
                if (Triggers.Count == 0)
                    return;

                var triggerInstances = Triggers.Select(x => x.Trigger).ToList();
                var combiner = EventProcessor.BuildTriggerCombiner(triggerCombiner.Text, triggerInstances);
                if (combiner is null)
                    return;

                var status = new GlobalStatus(
                    StatusId,
                    textName.Text.Trim(),
                    SelectedMode,
                    triggerCombiner.Text,
                    triggerInstances.Select(x => x.Trigger).ToList()
                );
                Result = new GlobalStatusInstance(status, triggerInstances, combiner);
            }
            else
            {
                var status = new GlobalStatus(StatusId, textName.Text.Trim(), SelectedMode, "", []);
                Result = new GlobalStatusInstance(status, [], () => false);
            }

            btnOk.Enabled = true;
        }

        private void AnyChanged(object sender, EventArgs e) => Rebuild();

        private void pickAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new TriggerForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                var idx = Triggers.Count;
                var t = form.Result;
                var row = triggerListView.Items.Add($"T{idx}");
                row.SubItems.Add(t.Trigger.InputId.ControllerName);
                row.SubItems.Add(t.Trigger.InputId.AxisName);
                row.SubItems.Add(t.IsTriggered() ? "A" : "");
                row.Tag = t;
                Triggers.Add((t, row));
                Rebuild();
            }
        }

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            foreach (var (trigger, row) in Triggers)
                row.SubItems[3].Text = trigger.IsTriggered() ? "A" : "";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            statusUpdateTimer.Stop();
            base.OnFormClosing(e);
        }

        private void triggerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deleteToolStripMenuItem.Enabled = triggerListView.SelectedItems.Count > 0;
            editSelectedToolStripMenuItem.Enabled = triggerListView.SelectedItems.Count == 1;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in triggerListView.SelectedItems)
            {
                if (item.Tag is TriggerInstance t)
                {
                    var toRemove = Triggers.FirstOrDefault(x => x.Trigger == t);
                    if (toRemove.Trigger is not null)
                        Triggers.Remove(toRemove);
                }
                triggerListView.Items.Remove(item);
            }
            foreach (var t in Triggers)
                t.Row.SubItems[0].Text = $"T{t.Row.Index}";
            Rebuild();
        }

        private void triggerListView_DoubleClick(object sender, EventArgs e)
        {
            if (triggerListView.SelectedItems.Count == 1)
            {
                var item = triggerListView.SelectedItems[0];
                if (item.Tag is TriggerInstance t)
                {
                    using var form = new TriggerForm(t);
                    if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
                    {
                        var updated = form.Result;
                        item.SubItems[1].Text = updated.Trigger.InputId.ControllerName;
                        item.SubItems[2].Text = updated.Trigger.InputId.AxisName;
                        item.Tag = updated;
                        var idx = Triggers.FindIndex(x => x.Trigger == t);
                        if (idx >= 0)
                            Triggers[idx] = (updated, item);
                        Rebuild();
                    }
                }
            }
        }

        private void editSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            triggerListView_DoubleClick(sender, e);
        }
    }
}

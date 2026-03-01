using JoyMap.Profile;
using JoyMap.Windows;

namespace JoyMap.Forms
{
    public partial class ModeEntryEditForm : Form
    {
        private IReadOnlyList<GlobalStatusRef> GlobalStatuses { get; }

        public ModeEntryEditForm(string entryId, ModeEntryInstance? existing = null, IReadOnlyList<GlobalStatusRef>? globalStatuses = null)
        {
            InitializeComponent();
            GlobalStatuses = globalStatuses ?? [];
            EntryId = entryId;
            lId.Text = $"ID: {entryId}";

            triggerCombiner.Items.Add("Or");
            triggerCombiner.Items.Add("And");

            if (existing is not null)
            {
                textName.Text = existing.Entry.Name;
                var savedCombiner = existing.Entry.TriggerCombiner;
                if (!string.IsNullOrEmpty(savedCombiner) && !triggerCombiner.Items.Contains(savedCombiner))
                    triggerCombiner.Items.Add(savedCombiner);
                triggerCombiner.SelectedItem = !string.IsNullOrEmpty(savedCombiner) ? savedCombiner : (object)"Or";

                foreach (var (t, idx) in existing.TriggerInstances.Select((t, i) => (t, i)))
                {
                    var row = triggerListView.Items.Add($"T{idx}");
                    var (device, input) = GetTriggerDisplay(t);
                    row.SubItems.Add(device);
                    row.SubItems.Add(input);
                    row.SubItems.Add(t.IsTriggered() ? "A" : "");
                    row.Tag = t;
                    Triggers.Add((t, row));
                }
            }
            else
            {
                triggerCombiner.SelectedIndex = 0;
            }

            Rebuild();
        }

        public string EntryId { get; }
        public ModeEntryInstance? Result { get; private set; }

        private List<(TriggerInstance Trigger, ListViewItem Row)> Triggers { get; } = [];

        private static (string device, string input) GetTriggerDisplay(TriggerInstance t)
        {
            if (t.Trigger.KeyInput is { } ki && ki != KeyOrButton.None)
                return ("Key", ki.ToString());
            return (t.Trigger.InputId.ControllerName, t.Trigger.InputId.AxisName);
        }

        private void Rebuild()
        {
            btnOk.Enabled = false;
            Result = null;

            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                SetStatus("A name is required.");
                return;
            }
            if (Triggers.Count == 0)
            {
                SetStatus("At least one trigger is required.");
                return;
            }

            var triggerInstances = Triggers.Select(x => x.Trigger).ToList();
            var globalResolvers = GlobalStatuses.Count > 0
                ? GlobalStatuses.ToDictionary(g => g.Id, g => g.IsActive)
                : null;
            var combiner = EventProcessor.BuildTriggerCombiner(triggerCombiner.Text, triggerInstances, globalResolvers, out var combinerError);
            if (combiner is null)
            {
                SetStatus(combinerError ?? "Invalid combiner expression.");
                return;
            }

            SetStatus("");
            var entry = new ModeEntry(
                EntryId,
                textName.Text.Trim(),
                triggerCombiner.Text,
                triggerInstances.Select(x => x.Trigger).ToList());
            Result = new ModeEntryInstance(entry, triggerInstances, combiner);
            btnOk.Enabled = true;
        }

        private void SetStatus(string message) => statusLabel.Text = message;
        private void AnyChanged(object sender, EventArgs e) => Rebuild();

        private void pickAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new TriggerForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                var t = form.Result;
                var idx = Triggers.Count;
                var row = triggerListView.Items.Add($"T{idx}");
                var (device, input) = GetTriggerDisplay(t);
                row.SubItems.Add(device);
                row.SubItems.Add(input);
                row.SubItems.Add(t.IsTriggered() ? "A" : "");
                row.Tag = t;
                Triggers.Add((t, row));
                Rebuild();
            }
        }

        private void addKeyTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new PickKeyForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result != Keys.None)
            {
                var keyInput = KeyOrButton.From(form.Result);
                var t = TriggerInstance.BuildKeyTrigger(keyInput);
                var idx = Triggers.Count;
                var row = triggerListView.Items.Add($"T{idx}");
                row.SubItems.Add("Key");
                row.SubItems.Add(keyInput.ToString());
                row.SubItems.Add(t.IsTriggered() ? "A" : "");
                row.Tag = t;
                Triggers.Add((t, row));
                Rebuild();
            }
        }

        private void editSelectedToolStripMenuItem_Click(object sender, EventArgs e) => triggerListView_DoubleClick(sender, e);

        private void triggerListView_DoubleClick(object sender, EventArgs e)
        {
            if (triggerListView.SelectedItems.Count != 1)
                return;
            var item = triggerListView.SelectedItems[0];
            if (item.Tag is not TriggerInstance t)
                return;

            if (t.Trigger.KeyInput is { } ki && ki != KeyOrButton.None)
            {
                using var form = new PickKeyForm();
                if (form.ShowDialog(this) == DialogResult.OK && form.Result != Keys.None)
                {
                    var keyInput = KeyOrButton.From(form.Result);
                    var updated = TriggerInstance.BuildKeyTrigger(keyInput);
                    item.SubItems[1].Text = "Key";
                    item.SubItems[2].Text = keyInput.ToString();
                    item.Tag = updated;
                    var idx = Triggers.FindIndex(x => x.Trigger == t);
                    if (idx >= 0) Triggers[idx] = (updated, item);
                    Rebuild();
                }
            }
            else
            {
                using var form = new TriggerForm(t);
                if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
                {
                    var updated = form.Result;
                    var (device, input) = GetTriggerDisplay(updated);
                    item.SubItems[1].Text = device;
                    item.SubItems[2].Text = input;
                    item.Tag = updated;
                    var idx = Triggers.FindIndex(x => x.Trigger == t);
                    if (idx >= 0) Triggers[idx] = (updated, item);
                    Rebuild();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in triggerListView.SelectedItems)
            {
                if (item.Tag is TriggerInstance t)
                    Triggers.RemoveAll(x => x.Trigger == t);
                triggerListView.Items.Remove(item);
            }
            for (int i = 0; i < triggerListView.Items.Count; i++)
                triggerListView.Items[i].Text = $"T{i}";
            Rebuild();
        }

        private void triggerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deleteToolStripMenuItem.Enabled = triggerListView.SelectedItems.Count > 0;
            editSelectedToolStripMenuItem.Enabled = triggerListView.SelectedItems.Count == 1;
        }

        private void btnCombinerHelp_Click(object sender, EventArgs e)
        {
            var localInputs = Triggers
                .Select(t => new CombinerHelpForm.LocalInputInfo(
                    t.Row.SubItems[0].Text,
                    t.Row.SubItems[1].Text,
                    t.Row.SubItems[2].Text,
                    t.Trigger.IsTriggered))
                .ToList();
            using var form = new CombinerHelpForm(triggerCombiner.Text, localInputs, GlobalStatuses);
            if (form.ShowDialog(this) == DialogResult.OK && form.ResultExpression is not null)
                triggerCombiner.Text = form.ResultExpression;
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
    }
}

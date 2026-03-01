using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.Profile.Processing;

namespace JoyMap.Forms
{
    public partial class GlobalStatusForm : Form
    {
        private readonly IReadOnlyList<GlobalStatusRef> _globalStatuses;

        public GlobalStatusForm(string id, GlobalStatusInstance? instance = null, IReadOnlyList<GlobalStatusRef>? globalStatuses = null)
        {
            InitializeComponent();
            _globalStatuses = globalStatuses ?? [];
            StatusId = id;
            lId.Text = $"ID: {id}";

            foreach (GlobalStatusMode mode in Enum.GetValues<GlobalStatusMode>())
                cbMode.Items.Add(new ModeItem(mode));

            triggerCombiner.Items.Add("Or");
            triggerCombiner.Items.Add("And");

            if (instance is not null)
            {
                textName.Text = instance.Status.Name;
                cbMode.SelectedItem = cbMode.Items.OfType<ModeItem>().First(x => x.Mode == instance.Status.Mode);
                var savedCombiner = instance.Status.TriggerCombiner;
                if (!string.IsNullOrEmpty(savedCombiner) && !triggerCombiner.Items.Contains(savedCombiner))
                    triggerCombiner.Items.Add(savedCombiner);
                if (!string.IsNullOrEmpty(savedCombiner))
                    triggerCombiner.SelectedItem = savedCombiner;
                else
                    triggerCombiner.SelectedIndex = 0;
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
                triggerCombiner.SelectedIndex = 0;
            }

            UpdateVisibility();
            Rebuild();
        }

        public string StatusId { get; }
        public GlobalStatusInstance? Result { get; private set; }

        private List<(TriggerInstance Trigger, ListViewItem Row)> Triggers { get; } = [];

        private GlobalStatusMode SelectedMode =>
            cbMode.SelectedItem is ModeItem { Mode: var m } ? m : GlobalStatusMode.AlwaysTrue;

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
            {
                labelCombinerError.Text = "";
                return;
            }

            if (NeedsCombiner)
            {
                if (Triggers.Count == 0)
                {
                    labelCombinerError.Text = "";
                    return;
                }

                var triggerInstances = Triggers.Select(x => x.Trigger).ToList();
                var globalResolvers = _globalStatuses.Count > 0
                    ? _globalStatuses.ToDictionary(g => g.Id, g => g.IsActive)
                    : null;
                var combiner = EventProcessor.BuildTriggerCombiner(triggerCombiner.Text, triggerInstances, globalResolvers, out var combinerError);
                if (combiner is null)
                {
                    labelCombinerError.Text = combinerError ?? "Invalid expression.";
                    return;
                }

                labelCombinerError.Text = "";
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
                labelCombinerError.Text = "";
                var status = new GlobalStatus(StatusId, textName.Text.Trim(), SelectedMode, "", []);
                Result = new GlobalStatusInstance(status, [], () => false);
            }

            btnOk.Enabled = true;
        }

        private void AnyChanged(object sender, EventArgs e) => Rebuild();

        private void btnCombinerHelp_Click(object sender, EventArgs e)
        {
            var localInputs = Triggers
                .Select(t => new CombinerHelpForm.LocalInputInfo(
                    t.Row.SubItems[0].Text,
                    t.Trigger.Trigger.InputId.ControllerName,
                    t.Trigger.Trigger.InputId.AxisName,
                    t.Trigger.IsTriggered))
                .ToList();

            using var form = new CombinerHelpForm(triggerCombiner.Text, localInputs, _globalStatuses);
            if (form.ShowDialog(this) == DialogResult.OK && form.ResultExpression is not null)
            {
                triggerCombiner.Text = form.ResultExpression;
            }
        }

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

        private sealed record ModeItem(GlobalStatusMode Mode)
        {
            public override string ToString() => Mode switch
            {
                GlobalStatusMode.AlwaysTrue => "Always True",
                GlobalStatusMode.AlwaysFalse => "Always False",
                GlobalStatusMode.TrueIfCombiner => "True if Combiner",
                GlobalStatusMode.FalseIfCombiner => "False if Combiner",
                GlobalStatusMode.ToggleOffInitially => "Toggle (Start Inactive)",
                GlobalStatusMode.ToggleOnInitially => "Toggle (Start Active)",
                _ => Mode.ToString()
            };
        }
    }
}

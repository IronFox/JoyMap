using JoyMap.Profile;
using JoyMap.Windows;

namespace JoyMap.Forms
{
    public partial class ModeGroupForm : Form
    {
        private IReadOnlyList<GlobalStatusRef> GlobalStatuses { get; }
        private Func<string> AllocateEntryId { get; }

        public ModeGroupForm(string groupId, Func<string> allocateEntryId, ModeGroupInstance? existing = null, IReadOnlyList<GlobalStatusRef>? globalStatuses = null)
        {
            InitializeComponent();
            GlobalStatuses = globalStatuses ?? [];
            AllocateEntryId = allocateEntryId;
            GroupId = groupId;
            lId.Text = $"ID: {groupId}";

            if (existing is not null)
            {
                textName.Text = existing.Group.Name;
                foreach (var e in existing.EntryInstances)
                {
                    var row = AddEntryRow(e);
                    Entries.Add((e, row));
                }
            }

            RebuildDefaultCombo(existing?.Group.DefaultModeId);
            Rebuild();
        }

        public string GroupId { get; }
        public ModeGroupInstance? Result { get; private set; }

        private List<(ModeEntryInstance Instance, ListViewItem Row)> Entries { get; } = [];

        private void Rebuild()
        {
            btnOk.Enabled = false;
            Result = null;

            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                SetStatus("A name is required.");
                return;
            }
            if (Entries.Count == 0)
            {
                SetStatus("At least one mode entry is required.");
                return;
            }
            if (cbDefaultMode.SelectedItem is not DefaultModeItem)
            {
                SetStatus("A default mode must be selected.");
                return;
            }

            SetStatus("");
            var defaultId = ((DefaultModeItem)cbDefaultMode.SelectedItem).EntryId;
            var group = new ModeGroup(
                GroupId,
                textName.Text.Trim(),
                defaultId,
                Entries.Select(e => e.Instance.Entry).ToList());
            Result = new ModeGroupInstance(group, Entries.Select(e => e.Instance).ToList());
            btnOk.Enabled = true;
        }

        private void SetStatus(string message) => statusLabel.Text = message;
        private void AnyChanged(object sender, EventArgs e) => Rebuild();

        private void RebuildDefaultCombo(string? selectId)
        {
            cbDefaultMode.Items.Clear();
            foreach (var (inst, _) in Entries)
                cbDefaultMode.Items.Add(new DefaultModeItem(inst.Id, inst.Entry.Name));

            if (selectId is not null)
            {
                var match = cbDefaultMode.Items.OfType<DefaultModeItem>().FirstOrDefault(x => x.EntryId == selectId);
                if (match is not null)
                    cbDefaultMode.SelectedItem = match;
            }
            if (cbDefaultMode.SelectedItem is null && cbDefaultMode.Items.Count > 0)
                cbDefaultMode.SelectedIndex = 0;
        }

        private ListViewItem AddEntryRow(ModeEntryInstance e)
        {
            var row = modeListView.Items.Add(e.Id);
            row.SubItems.Add(e.Entry.Name);
            row.SubItems.Add(TriggerSummary(e));
            row.Tag = e;
            return row;
        }

        private static string TriggerSummary(ModeEntryInstance e)
        {
            if (e.TriggerInstances.Count == 0)
                return "(none)";
            if (e.TriggerInstances.Count == 1)
            {
                var t = e.TriggerInstances[0];
                if (t.Trigger.KeyInput is { } ki && ki != KeyOrButton.None)
                    return $"Key: {ki}";
                return $"{t.Trigger.InputId.ControllerName}: {t.Trigger.InputId.AxisName}";
            }
            return $"{e.TriggerInstances.Count} triggers";
        }

        private void mgEntryNewMenuItem_Click(object sender, EventArgs e)
        {
            var entryId = AllocateEntryId();
            using var form = new ModeEntryEditForm(entryId, null, GlobalStatuses);
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                var inst = form.Result;
                var row = AddEntryRow(inst);
                Entries.Add((inst, row));
                RebuildDefaultCombo(((cbDefaultMode.SelectedItem as DefaultModeItem)?.EntryId));
                Rebuild();
            }
        }

        private void mgEntryEditMenuItem_Click(object sender, EventArgs e) => modeListView_DoubleClick(sender, e);

        private void modeListView_DoubleClick(object sender, EventArgs e)
        {
            if (modeListView.SelectedItems.Count != 1)
                return;
            var item = modeListView.SelectedItems[0];
            if (item.Tag is not ModeEntryInstance inst)
                return;

            using var form = new ModeEntryEditForm(inst.Id, inst, GlobalStatuses);
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                var updated = form.Result;
                item.SubItems[1].Text = updated.Entry.Name;
                item.SubItems[2].Text = TriggerSummary(updated);
                item.Tag = updated;
                var idx = Entries.FindIndex(x => x.Instance == inst);
                if (idx >= 0) Entries[idx] = (updated, item);
                var prevDefault = (cbDefaultMode.SelectedItem as DefaultModeItem)?.EntryId;
                RebuildDefaultCombo(prevDefault);
                Rebuild();
            }
        }

        private void mgEntryDeleteMenuItem_Click(object sender, EventArgs e)
        {
            var prevDefault = (cbDefaultMode.SelectedItem as DefaultModeItem)?.EntryId;
            foreach (ListViewItem item in modeListView.SelectedItems)
            {
                if (item.Tag is ModeEntryInstance inst)
                    Entries.RemoveAll(x => x.Instance == inst);
                modeListView.Items.Remove(item);
            }
            RebuildDefaultCombo(prevDefault);
            Rebuild();
        }

        private void mgSetDefaultMenuItem_Click(object sender, EventArgs e)
        {
            if (modeListView.SelectedItems.Count != 1)
                return;
            if (modeListView.SelectedItems[0].Tag is not ModeEntryInstance inst)
                return;
            var match = cbDefaultMode.Items.OfType<DefaultModeItem>().FirstOrDefault(x => x.EntryId == inst.Id);
            if (match is not null)
                cbDefaultMode.SelectedItem = match;
        }

        private void modeContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var hasSelection = modeListView.SelectedItems.Count > 0;
            mgEntryEditMenuItem.Enabled = modeListView.SelectedItems.Count == 1;
            mgEntryDeleteMenuItem.Enabled = hasSelection;
            mgSetDefaultMenuItem.Enabled = modeListView.SelectedItems.Count == 1;
        }

        private void modeListView_SelectedIndexChanged(object sender, EventArgs e) { }

        private sealed record DefaultModeItem(string EntryId, string EntryName)
        {
            public override string ToString() => EntryName;
        }
    }
}

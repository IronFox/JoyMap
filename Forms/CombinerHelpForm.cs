using JoyMap.Profile;
using JoyMap.Profile.Processing;

namespace JoyMap.Forms
{
    public partial class CombinerHelpForm : Form
    {
        public record LocalInputInfo(string Label, string Device, string Axis, Func<bool> IsActive);

        private readonly IReadOnlyList<LocalInputInfo> _localInputs;
        private readonly IReadOnlyList<GlobalStatusRef> _globalInputs;
        private readonly IReadOnlyDictionary<string, Func<bool>>? _globalResolvers;

        // Rows in the live list views — kept for fast timer updates
        private readonly List<(ListViewItem Row, Func<bool> IsActive)> _localRows = [];
        private readonly List<(ListViewItem Row, Func<bool> IsActive)> _globalRows = [];
        // Dynamic insert menu items rebuilt on each context-menu open
        private readonly List<ToolStripItem> _dynamicMenuItems = [];

        public string? ResultExpression { get; private set; }

        public CombinerHelpForm(
            string currentExpression,
            IReadOnlyList<LocalInputInfo> localInputs,
            IReadOnlyList<GlobalStatusRef>? globalInputs = null)
        {
            InitializeComponent();

            _localInputs = localInputs;
            _globalInputs = globalInputs ?? [];
            _globalResolvers = _globalInputs.Count > 0
                ? _globalInputs.ToDictionary(g => g.Id, g => g.IsActive)
                : null;

            txtExpression.Text = currentExpression;

            PopulateLocalList();
            PopulateGlobalList();

            if (_globalInputs.Count == 0)
            {
                labelGlobal.Text = "Global Inputs  (none declared)";
                globalInsertHeader.Visible = false;
                exprSep2.Visible = false;
            }

            Validate();
        }

        // ── List population ──────────────────────────────────────────────────

        private void PopulateLocalList()
        {
            localListView.Items.Clear();
            _localRows.Clear();
            foreach (var inp in _localInputs)
            {
                var row = localListView.Items.Add(inp.Label);
                row.SubItems.Add(inp.Device);
                row.SubItems.Add(inp.Axis);
                row.SubItems.Add(inp.IsActive() ? "●" : "");
                _localRows.Add((row, inp.IsActive));
            }
        }

        private void PopulateGlobalList()
        {
            globalListView.Items.Clear();
            _globalRows.Clear();
            foreach (var gs in _globalInputs)
            {
                var row = globalListView.Items.Add(gs.Id);
                row.SubItems.Add(gs.Name);
                row.SubItems.Add(gs.IsActive() ? "●" : "");
                _globalRows.Add((row, gs.IsActive));
            }
        }

        // ── Validation ───────────────────────────────────────────────────────

        private void Validate()
        {
            var text = txtExpression.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                labelError.Text = "Expression is empty.";
                btnOk.Enabled = false;
                ResultExpression = null;
                return;
            }

            // "Or" / "And" short-hands are always valid
            if (text.Equals("or", StringComparison.OrdinalIgnoreCase) ||
                text.Equals("and", StringComparison.OrdinalIgnoreCase))
            {
                labelError.Text = "";
                btnOk.Enabled = true;
                ResultExpression = text;
                return;
            }

            // Build resolver dictionary: local T0..Tn + optional globals
            var identifiers = _localInputs
                .Select((inp, i) => KeyValuePair.Create(inp.Label, inp.IsActive))
                .ToDictionary(StringComparer.OrdinalIgnoreCase);
            if (_globalResolvers is not null)
                foreach (var kv in _globalResolvers)
                    identifiers[kv.Key] = kv.Value;

            var func = EventProcessor.BuildTriggerCombiner(text, [], identifiers, out var error);
            if (func is null)
            {
                labelError.Text = error ?? "Invalid expression.";
                btnOk.Enabled = false;
                ResultExpression = null;
            }
            else
            {
                labelError.Text = "";
                btnOk.Enabled = true;
                ResultExpression = text;
            }
        }

        // ── Expression TextBox ───────────────────────────────────────────────

        private void txtExpression_TextChanged(object sender, EventArgs e) => Validate();

        private void InsertText(string text)
        {
            var pos = txtExpression.SelectionStart;
            var sel = txtExpression.SelectionLength;
            txtExpression.Text = txtExpression.Text.Remove(pos, sel).Insert(pos, text);
            txtExpression.SelectionStart = pos + text.Length;
            txtExpression.SelectionLength = 0;
            txtExpression.Focus();
        }

        private void InsertParens()
        {
            var pos = txtExpression.SelectionStart;
            var sel = txtExpression.SelectionLength;
            if (sel > 0)
            {
                var inner = txtExpression.SelectedText;
                var replacement = $"({inner})";
                txtExpression.Text = txtExpression.Text.Remove(pos, sel).Insert(pos, replacement);
                txtExpression.SelectionStart = pos + replacement.Length;
            }
            else
            {
                txtExpression.Text = txtExpression.Text.Insert(pos, "()");
                txtExpression.SelectionStart = pos + 1;
            }
            txtExpression.SelectionLength = 0;
            txtExpression.Focus();
        }

        // ── Context menu ─────────────────────────────────────────────────────

        private void exprContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Remove previously generated dynamic items
            foreach (var item in _dynamicMenuItems)
                exprContextMenu.Items.Remove(item);
            _dynamicMenuItems.Clear();

            // Find insertion indices (just after the static header items)
            int localHeaderIdx = exprContextMenu.Items.IndexOf(localInsertHeader);
            int globalHeaderIdx = exprContextMenu.Items.IndexOf(globalInsertHeader);

            // Insert local trigger items after localInsertHeader
            int insertAt = localHeaderIdx + 1;
            foreach (var inp in _localInputs)
            {
                var label = inp.Label;
                var item = new ToolStripMenuItem($"Insert {label}  ({inp.Device} / {inp.Axis})");
                item.Click += (s, ev) => InsertText(label);
                exprContextMenu.Items.Insert(insertAt++, item);
                _dynamicMenuItems.Add(item);
                globalHeaderIdx++; // shift index as we insert before it
            }

            // Insert global status items after globalInsertHeader
            if (_globalInputs.Count > 0)
            {
                insertAt = globalHeaderIdx + 1;
                foreach (var gs in _globalInputs)
                {
                    var id = gs.Id;
                    var item = new ToolStripMenuItem($"Insert {id}  ({gs.Name})");
                    item.Click += (s, ev) => InsertText(id);
                    exprContextMenu.Items.Insert(insertAt++, item);
                    _dynamicMenuItems.Add(item);
                }
            }

            localInsertHeader.Visible = _localInputs.Count > 0;
            exprSep1.Visible = _localInputs.Count > 0;
            globalInsertHeader.Visible = _globalInputs.Count > 0;
            exprSep2.Visible = _globalInputs.Count > 0;
        }

        // ── Live status timer ────────────────────────────────────────────────

        private void liveTimer_Tick(object sender, EventArgs e)
        {
            foreach (var (row, isActive) in _localRows)
                row.SubItems[3].Text = isActive() ? "●" : "";
            foreach (var (row, isActive) in _globalRows)
                row.SubItems[2].Text = isActive() ? "●" : "";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            liveTimer.Stop();
            base.OnFormClosing(e);
        }
    }
}

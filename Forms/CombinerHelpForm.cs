using JoyMap.Profile;

namespace JoyMap.Forms
{
    public partial class CombinerHelpForm : Form
    {
        public record LocalInputInfo(string Label, string Device, string Axis, Func<bool> IsActive);

        private IReadOnlyList<LocalInputInfo> LocalInputs { get; }
        private IReadOnlyList<GlobalStatusRef> GlobalInputs { get; }
        private IReadOnlyDictionary<string, Func<bool>>? GlobalResolvers { get; }

        // Rows in the live list views — kept for fast timer updates
        private List<(ListViewItem Row, Func<bool> IsActive)> LocalRows { get; } = [];
        private List<(ListViewItem Row, Func<bool> IsActive)> GlobalRows { get; } = [];
        // Dynamic insert menu items rebuilt on each context-menu open
        private List<ToolStripItem> DynamicMenuItems { get; } = [];

        public string? ResultExpression { get; private set; }

        public CombinerHelpForm(
            string currentExpression,
            IReadOnlyList<LocalInputInfo> localInputs,
            IReadOnlyList<GlobalStatusRef>? globalInputs = null)
        {
            InitializeComponent();

            txtHelpText.Text =
                "EXPRESSION LANGUAGE\r\n" +
                "\r\n" +
                "Operators\r\n" +
                "  &&   Both conditions active simultaneously  (e.g. T0 && T1)\r\n" +
                "  ||   Either condition active  (e.g. T0 || T1)\r\n" +
                "  !    Condition must NOT be active  (e.g. !T0)\r\n" +
                "  ()   Group to control evaluation order  (e.g. (T0 || T1) && T2)\r\n" +
                "\r\n" +
                "Identifiers\r\n" +
                "  T0, T1, \u2026   Local trigger inputs (see table above)\r\n" +
                "  G0, G1, \u2026   Global status inputs (see table above)\r\n" +
                "\r\n" +
                "Shorthand modes\r\n" +
                "  Or    Any trigger fires  \u2014  equivalent to T0 || T1 || T2 || \u2026\r\n" +
                "  And   All triggers fire  \u2014  equivalent to T0 && T1 && T2 && \u2026\r\n" +
                "\r\n" +
                "Examples\r\n" +
                "  T0                       Active when trigger 0 fires\r\n" +
                "  T0 || T1                 Active when T0 OR T1 fires\r\n" +
                "  T0 && T1                 Active when T0 AND T1 fire simultaneously\r\n" +
                "  !T0                      Active when T0 is NOT firing\r\n" +
                "  (T0 || T1) && !T2        Active when (T0 or T1) fires but T2 does not\r\n" +
                "  G0 && (T0 || T1)         Active when status G0 is on and T0 or T1 fires\r\n" +
                "\r\n" +
                "Tip: double-click any row in the input lists to insert that identifier.";

            LocalInputs = localInputs;
            GlobalInputs = globalInputs ?? [];
            GlobalResolvers = GlobalInputs.Count > 0
                ? GlobalInputs.ToDictionary(g => g.Id, g => g.IsActive)
                : null;

            txtExpression.Text = currentExpression;

            PopulateLocalList();
            PopulateGlobalList();

            if (GlobalInputs.Count == 0)
            {
                labelGlobal.Text = "Global Inputs  (none declared)";
                globalInsertHeader.Visible = false;
                exprSep2.Visible = false;
            }

            ValidateExpression();
        }

        // ── List population ──────────────────────────────────────────────────

        private void PopulateLocalList()
        {
            localListView.Items.Clear();
            LocalRows.Clear();
            foreach (var inp in LocalInputs)
            {
                var row = localListView.Items.Add(inp.Label);
                row.SubItems.Add(inp.Device);
                row.SubItems.Add(inp.Axis);
                row.SubItems.Add(inp.IsActive() ? "●" : "");
                LocalRows.Add((row, inp.IsActive));
            }
        }

        private void PopulateGlobalList()
        {
            globalListView.Items.Clear();
            GlobalRows.Clear();
            foreach (var gs in GlobalInputs)
            {
                var row = globalListView.Items.Add(gs.Id);
                row.SubItems.Add(gs.Name);
                row.SubItems.Add(gs.IsActive() ? "●" : "");
                GlobalRows.Add((row, gs.IsActive));
            }
        }

        // ── Validation ───────────────────────────────────────────────────────

        private void ValidateExpression()
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
            var identifiers = LocalInputs
                .Select((inp, i) => KeyValuePair.Create(inp.Label, inp.IsActive))
                .ToDictionary(StringComparer.OrdinalIgnoreCase);
            if (GlobalResolvers is not null)
                foreach (var kv in GlobalResolvers)
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

        private void txtExpression_TextChanged(object sender, EventArgs e) => ValidateExpression();

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

        private void insertAndMenuItem_Click(object sender, EventArgs e) => InsertText(" && ");
        private void insertOrMenuItem_Click(object sender, EventArgs e) => InsertText(" || ");
        private void insertNotMenuItem_Click(object sender, EventArgs e) => InsertText("!");
        private void insertParensMenuItem_Click(object sender, EventArgs e) => InsertParens();
        private void setOrMenuItem_Click(object sender, EventArgs e) => txtExpression.Text = "Or";
        private void setAndMenuItem_Click(object sender, EventArgs e) => txtExpression.Text = "And";

        private void exprContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Remove previously generated dynamic items
            foreach (var item in DynamicMenuItems)
                exprContextMenu.Items.Remove(item);
            DynamicMenuItems.Clear();

            // Find insertion indices (just after the static header items)
            int localHeaderIdx = exprContextMenu.Items.IndexOf(localInsertHeader);
            int globalHeaderIdx = exprContextMenu.Items.IndexOf(globalInsertHeader);

            // Insert local trigger items after localInsertHeader
            int insertAt = localHeaderIdx + 1;
            foreach (var inp in LocalInputs)
            {
                var label = inp.Label;
                var item = new ToolStripMenuItem($"Insert '{label}'  —  {inp.Device} / {inp.Axis}");
                item.Click += (s, ev) => InsertText(label);
                exprContextMenu.Items.Insert(insertAt++, item);
                DynamicMenuItems.Add(item);
                globalHeaderIdx++; // shift index as we insert before it
            }

            // Insert global status items after globalInsertHeader
            if (GlobalInputs.Count > 0)
            {
                insertAt = globalHeaderIdx + 1;
                foreach (var gs in GlobalInputs)
                {
                    var id = gs.Id;
                    var item = new ToolStripMenuItem($"Insert '{id}'  —  {gs.Name}");
                    item.Click += (s, ev) => InsertText(id);
                    exprContextMenu.Items.Insert(insertAt++, item);
                    DynamicMenuItems.Add(item);
                }
            }

            localInsertHeader.Visible = LocalInputs.Count > 0;
            exprSep1.Visible = LocalInputs.Count > 0;
            globalInsertHeader.Visible = GlobalInputs.Count > 0;
            exprSep2.Visible = GlobalInputs.Count > 0;
        }

        // ── Live status timer ────────────────────────────────────────────────

        private void liveTimer_Tick(object sender, EventArgs e)
        {
            foreach (var (row, isActive) in LocalRows)
                row.SubItems[3].Text = isActive() ? "●" : "";
            foreach (var (row, isActive) in GlobalRows)
                row.SubItems[2].Text = isActive() ? "●" : "";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FitColumns();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (IsHandleCreated)
                BeginInvoke(FitColumns);
        }

        private void FitColumns()
        {
            AutoFitColumn(localListView, chLocalDevice);
            AutoFitColumn(globalListView, chGlobalName);
        }

        private static void AutoFitColumn(ListView lv, ColumnHeader stretchCol)
        {
            if (lv.ClientSize.Width <= 0)
                return;
            var totalFixed = lv.Columns.Cast<ColumnHeader>().Where(c => c != stretchCol).Sum(c => c.Width);
            var available = lv.ClientSize.Width - totalFixed;
            if (available > 0)
                stretchCol.Width = available;
        }

        private void localListView_DoubleClick(object sender, EventArgs e)
        {
            if (localListView.SelectedItems.Count == 1)
                InsertText(localListView.SelectedItems[0].Text);
        }

        private void globalListView_DoubleClick(object sender, EventArgs e)
        {
            if (globalListView.SelectedItems.Count == 1)
                InsertText(globalListView.SelectedItems[0].Text);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            liveTimer.Stop();
            base.OnFormClosing(e);
        }
    }
}

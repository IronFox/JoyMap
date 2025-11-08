using JoyMap.Windows;

namespace JoyMap
{
    public partial class PickWindowForm : Form
    {
        public PickWindowForm()
        {
            InitializeComponent();
            this.Load += (_, __) => RefreshWindowList();
        }

        private Dictionary<string, ListViewItem> WindowRows { get; } = [];

        private void RefreshWindowList()
        {
            try
            {
                windowListView.BeginUpdate();

                // Capture previous selection by stable keys (titles) and primary index
                var prevSelectedTitles = new HashSet<string>(
                    windowListView.SelectedItems.Cast<ListViewItem>()
                        .Select(i => i.Tag as string)
                        .Where(s => !string.IsNullOrWhiteSpace(s))!
                );
                int prevPrimaryIndex = windowListView.SelectedIndices.Count > 0
                    ? windowListView.SelectedIndices[0]
                    : (windowListView.FocusedItem?.Index ?? -1);

                var windows = WindowReference.OfAll();
                var existing = new HashSet<string>(windows.Select(x => x.WindowTitle));
                foreach (var win in windows)
                {
                    var rect = win.Rect;
                    if (!WindowRows.TryGetValue(win.WindowTitle, out var item))
                    {
                        item = new ListViewItem(win.WindowTitle);
                        item.SubItems.Add($"{rect.Width} * {rect.Height}");
                        item.SubItems.Add(win.ProcessName);
                        item.Tag = win;
                        windowListView.Items.Add(item);
                        WindowRows[win.WindowTitle] = item;
                    }
                    else
                    {
                        // Update existing
                        item.SubItems[1].Text = $"{rect.Width} * {rect.Height}";
                        //item.SubItems[2].Text = win.GetProcessName();
                    }
                }
                // Remove closed windows
                var toRemove = WindowRows.Keys.Except(existing).ToList();
                foreach (var title in toRemove)
                {
                    if (WindowRows.TryGetValue(title, out var item))
                    {
                        windowListView.Items.Remove(item);
                        WindowRows.Remove(title);
                    }
                }

                // Restore selection: prefer titles, fallback to previous index
                if (windowListView.Items.Count > 0)
                {
                    windowListView.SelectedItems.Clear();

                    if (prevSelectedTitles.Count > 0)
                    {
                        foreach (ListViewItem item in windowListView.Items)
                        {
                            if (item.Tag is string title && prevSelectedTitles.Contains(title))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    if (windowListView.SelectedIndices.Count == 0 && prevPrimaryIndex >= 0)
                    {
                        int idx = Math.Min(prevPrimaryIndex, windowListView.Items.Count - 1);
                        if (idx >= 0)
                        {
                            windowListView.Items[idx].Selected = true;
                        }
                    }
                }
            }
            finally
            {
                windowListView.EndUpdate();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateWindowListTimer.Stop();
            base.OnFormClosing(e);
        }



        private void updateWindowListTimer_Tick(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        public WindowReference? Result;

        private void windowListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Result = windowListView.SelectedItems.Count > 0
                ? windowListView.SelectedItems[0].Tag as WindowReference
                : null;
            btnOk.Enabled = Result is not null;
        }

        private void windowListView_DoubleClick(object sender, EventArgs e)
        {
            if (btnOk.Enabled)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }


    }
}

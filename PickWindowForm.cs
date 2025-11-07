using System.Runtime.InteropServices;
using System.Text;

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

                var windows = EnumerateTopLevelWindows().ToList();
                var existing = new HashSet<string>(windows.Select(x => x.Title));
                foreach (var win in windows)
                {
                    if (!WindowRows.TryGetValue(win.Title, out var item))
                    {
                        item = new ListViewItem(win.Title);
                        item.SubItems.Add($"{win.Width} * {win.Height}");
                        item.Tag = win.Title;
                        windowListView.Items.Add(item);
                        WindowRows[win.Title] = item;
                    }
                    else
                    {
                        // Update existing
                        item.SubItems[1].Text = $"{win.Width} * {win.Height}";
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

        private static IEnumerable<WindowInfo> EnumerateTopLevelWindows()
        {
            var list = new List<WindowInfo>();
            EnumWindows((hWnd, lParam) =>
            {
                if (!IsWindowVisible(hWnd))
                    return true;

                int len = GetWindowTextLength(hWnd);
                if (len == 0)
                    return true;

                var sb = new StringBuilder(len + 1);
                GetWindowText(hWnd, sb, sb.Capacity);
                var title = sb.ToString();
                if (string.IsNullOrWhiteSpace(title))
                    return true;

                if (!GetWindowRect(hWnd, out var rect))
                    return true;

                var info = new WindowInfo(hWnd, rect, title);
                // Skip zero-sized or minimized windows
                if (info.Width <= 0 || info.Height <= 0)
                    return true;

                list.Add(info);
                return true;
            }, IntPtr.Zero);

            // Sort by title for easier browsing
            list.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.CurrentCultureIgnoreCase));
            return list;
        }

        private readonly struct WindowInfo
        {
            public IntPtr Hwnd { get; }
            public string Title { get; }
            public int Left => Rect.Left;
            public int Top => Rect.Top;
            public int Width => Rect.Right - Rect.Left;
            public int Height => Rect.Bottom - Rect.Top;

            private RECT Rect { get; }

            public WindowInfo(IntPtr hwnd, RECT rect, string title)
            {
                Hwnd = hwnd;
                Rect = rect;
                Title = title;
            }
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        private void updateWindowListTimer_Tick(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        public string? Result;

        private void windowListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Result = windowListView.SelectedItems.Count > 0
                ? windowListView.SelectedItems[0].Tag as string
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

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}

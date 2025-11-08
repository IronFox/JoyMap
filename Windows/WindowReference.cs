using System.Runtime.InteropServices;
using System.Text;

namespace JoyMap.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public record WindowReference(
        string Title,
        IntPtr Handle,
        RECT Rect)
    {
        private string? ProcessNameCache { get; set; } = null;

        public string? GetProcessName()
        {
            if (ProcessNameCache is not null)
                return ProcessNameCache;
            try
            {
                _ = GetWindowThreadProcessId(Handle, out uint pid);
                if (pid == 0)
                    return null;

                var process = System.Diagnostics.Process.GetProcessById((int)pid);
                ProcessNameCache = process.ProcessName;
                return ProcessNameCache;
            }
            catch
            {
                return null;
            }
        }


        public bool IsChildOf(IntPtr parentHandle)
        {
            IntPtr hWnd = Handle;
            while (hWnd != IntPtr.Zero)
            {
                if (hWnd == parentHandle)
                    return true;
                hWnd = GetParent(hWnd); // walk up parent chain
            }
            return false;
        }

        public int Width => Rect.Right - Rect.Left;
        public int Height => Rect.Bottom - Rect.Top;

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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // Added implementation for GetParent used in IsChildOf
        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr hWnd);

        // Added: Direct P/Invoke to avoid inaccessible NativeMethods and correctly retrieve PID
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        internal static WindowReference? OfFocused()
        {
            var hWnd = GetForegroundWindow();
            if (hWnd == IntPtr.Zero)
                return null;

            int len = GetWindowTextLength(hWnd);
            var sb = new StringBuilder(len + 1);
            if (len > 0)
                GetWindowText(hWnd, sb, sb.Capacity);
            var title = sb.ToString();

            if (!GetWindowRect(hWnd, out var rect))
                rect = new RECT();

            return new WindowReference(title, hWnd, rect);
        }

        public static IReadOnlyList<WindowReference> OfAll()
        {
            var list = new List<WindowReference>();
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

                var info = new WindowReference(title, hWnd, rect);
                list.Add(info);
                return true;
            }, IntPtr.Zero);

            list.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.CurrentCultureIgnoreCase));
            return list;
        }
    }
}

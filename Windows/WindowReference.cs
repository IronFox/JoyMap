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

        public readonly int Width => Right - Left;
        public readonly int Height => Bottom - Top;
    }

    public record WindowReference(
        string WindowTitle,
        IntPtr WindowHandle,
        System.Diagnostics.Process? Process)
    {

        public bool IsAlive
        {
            get
            {
                try
                {
                    return Process?.HasExited == false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string? ProcessName
        {
            get
            {
                try
                {
                    return Process?.ProcessName;
                }
                catch
                {
                    return null;
                }
            }
        }



        public bool IsChildOf(IntPtr parentHandle)
        {
            IntPtr hWnd = WindowHandle;
            while (hWnd != IntPtr.Zero)
            {
                if (hWnd == parentHandle)
                    return true;
                hWnd = GetParent(hWnd); // walk up parent chain
            }
            return false;
        }

        public RECT Rect
        {
            get
            {
                if (GetWindowRect(WindowHandle, out var rect))
                    return rect;
                return new RECT();
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

            System.Diagnostics.Process? process = null;
            try
            {
                GetWindowThreadProcessId(hWnd, out uint pid);
                int processId = (int)pid;
                process = processId != 0
                    ? System.Diagnostics.Process.GetProcessById(processId)
                    : null;
            }
            catch (Exception)
            {
                // ignored
            }

            return new WindowReference(
                WindowTitle: title,
                WindowHandle: hWnd,
                Process: process
            );
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

                System.Diagnostics.Process? process = null;
                try
                {
                    GetWindowThreadProcessId(hWnd, out uint pid);
                    int processId = (int)pid;
                    process = processId != 0
                        ? System.Diagnostics.Process.GetProcessById(processId)
                        : null;
                }
                catch
                {
                    // ignored
                }

                var info = new WindowReference(title, hWnd, process);
                list.Add(info);
                return true;
            }, IntPtr.Zero);

            list.Sort((a, b) => string.Compare(a.WindowTitle, b.WindowTitle, StringComparison.CurrentCultureIgnoreCase));
            return list;
        }
    }
}

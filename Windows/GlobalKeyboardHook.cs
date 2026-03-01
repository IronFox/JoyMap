using System.Runtime.InteropServices;

namespace JoyMap.Windows
{
    internal static class GlobalKeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const uint LLKHF_INJECTED = 0x10;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string? lpModuleName);

        private static volatile IntPtr _hookHandle = IntPtr.Zero;
        private static LowLevelKeyboardProc? _proc;
        private static readonly bool[] _keyDown = new bool[256];

        internal static void Install()
        {
            if (_hookHandle != IntPtr.Zero)
                return;
            _proc = HookCallback;
            _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(null), 0);
        }

        internal static void Uninstall()
        {
            if (_hookHandle == IntPtr.Zero)
                return;
            UnhookWindowsHookEx(_hookHandle);
            _hookHandle = IntPtr.Zero;
            _proc = null;
        }

        internal static bool IsKeyDown(Keys key)
        {
            int vk = (int)(key & Keys.KeyCode) & 0xFF;
            return vk > 0 && _keyDown[vk];
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var kb = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                if ((kb.flags & LLKHF_INJECTED) == 0)
                {
                    int vk = (int)kb.vkCode & 0xFF;
                    bool isDown = wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN;
                    _keyDown[vk] = isDown;
                }
            }
            return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }
    }
}

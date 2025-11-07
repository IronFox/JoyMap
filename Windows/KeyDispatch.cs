using System.Runtime.InteropServices;

namespace JoyMap.Windows
{
    // Pseudocode / Plan (inline to satisfy planning requirement):
    // - Detect mouse keys (already done via isMouse).
    // - For mouse keys, build a Win32.INPUT of type MOUSE instead of KEYBOARD.
    // - Map Keys.{LButton,RButton,MButton,XButton1,XButton2} to corresponding MOUSEEVENTF flags:
    //     LButton -> LEFTDOWN / LEFTUP
    //     RButton -> RIGHTDOWN / RIGHTUP
    //     MButton -> MIDDLEDOWN / MIDDLEUP
    //     XButton1/2 -> XDOWN / XUP + mouseData = 1 or 2
    // - Populate MOUSEINPUT with dx=0, dy=0 (no movement), wheel=0, time=0, extraInfo=IntPtr.Zero.
    // - Call SendInput with that single INPUT.
    // - Return early so keyboard path is skipped.
    // - Extend Win32 interop:
    //     * Add INPUT_MOUSE constant.
    //     * Add MOUSEEVENTF_* constants and XBUTTON* constants.
    //     * Add MOUSEINPUT struct.
    //     * Add MOUSEINPUT field to InputUnion.
    // - Keep existing behavior for keyboard unchanged.
    //
    // Additional plan for assigning KEYBDINPUT.wScan:
    // - Use Win32.MapVirtualKey with MAPVK_VK_TO_VSC to translate the virtual-key to a hardware scan code.
    // - Assign the computed scan code to KEYBDINPUT.wScan.
    // - Keep using wVk and do not set KEYEVENTF_SCANCODE to preserve existing behavior.
    //
    // Plan addition:
    // - For PageUp/PageDown keys (and their aliases Prior/Next), set KEYEVENTF_EXTENDEDKEY bit
    //   in KEYBDINPUT.dwFlags for both key down and key up events.

    internal static class KeyDispatch
    {
        private static readonly int[] keyStates = new int[1024];

        internal static void Change(Keys keys, bool nowDown, bool reassert = false)
        {
            int idx = (int)keys;
            if (idx >= keyStates.Length)
                return;

            bool isDown = keyStates[idx] > 0;
            if (!reassert)
            {
                bool wasDown = isDown;
                if (nowDown)
                    keyStates[idx]++;
                else
                    keyStates[idx]--;
                isDown = keyStates[idx] > 0;
                if (wasDown == isDown)
                    return;
            }

            bool isMouse = (keys >= Keys.LButton && keys <= Keys.XButton2);
            if (isMouse)
            {
                //if (reassert)
                //{
                //    Win32.INPUT[] inputs = [
                //        MouseEvent(keys, !nowDown),
                //        MouseEvent(keys, nowDown)
                //        ];
                //    Win32.SendInput(2, inputs, Win32.INPUT.Size);
                //}
                //else
                {
                    Win32.INPUT[] inputs = [MouseEvent(keys, nowDown)];
                    Win32.SendInput(1, inputs, Win32.INPUT.Size);
                }
                return;
            }
            //if (reassert)
            //{
            //    Win32.INPUT[] kInputs = [
            //        KeyEvent(keys, !nowDown),
            //        KeyEvent(keys, nowDown)
            //        ];
            //    Win32.SendInput(2, kInputs, Win32.INPUT.Size);
            //}
            //else
            {
                Win32.INPUT[] kInputs = [
                    KeyEvent(keys, nowDown)
                    ];
                Win32.SendInput(1, kInputs, Win32.INPUT.Size);
            }
        }


        private static Win32.INPUT KeyEvent(Keys keys, bool nowDown)
        {
            Win32.INPUT rs = new();
            rs.type = Win32.INPUT_KEYBOARD;
            rs.U.ki.wVk = (ushort)keys;
            rs.U.ki.wScan = (ushort)Win32.MapVirtualKey((uint)keys, Win32.MAPVK_VK_TO_VSC);

            uint flags = nowDown ? 0u : Win32.KEYEVENTF_KEYUP;

            // Set extended bit for PageUp/PageDown (and aliases Prior/Next)
            if (keys == Keys.PageUp || keys == Keys.PageDown || keys == Keys.Prior || keys == Keys.Next)
            {
                flags |= Win32.KEYEVENTF_EXTENDEDKEY;
            }

            rs.U.ki.dwFlags = flags;
            rs.U.ki.time = 0;
            rs.U.ki.dwExtraInfo = IntPtr.Zero;
            return rs;
        }

        private static Win32.INPUT MouseEvent(Keys keys, bool nowDown)
        {
            Win32.INPUT rs = new();
            rs.type = Win32.INPUT_MOUSE;
            uint flags = 0;
            uint mouseData = 0;

            switch (keys)
            {
                case Keys.LButton:
                    flags = nowDown ? Win32.MOUSEEVENTF_LEFTDOWN : Win32.MOUSEEVENTF_LEFTUP;
                    break;
                case Keys.RButton:
                    flags = nowDown ? Win32.MOUSEEVENTF_RIGHTDOWN : Win32.MOUSEEVENTF_RIGHTUP;
                    break;
                case Keys.MButton:
                    flags = nowDown ? Win32.MOUSEEVENTF_MIDDLEDOWN : Win32.MOUSEEVENTF_MIDDLEUP;
                    break;
                case Keys.XButton1:
                    flags = nowDown ? Win32.MOUSEEVENTF_XDOWN : Win32.MOUSEEVENTF_XUP;
                    mouseData = Win32.XBUTTON1;
                    break;
                case Keys.XButton2:
                    flags = nowDown ? Win32.MOUSEEVENTF_XDOWN : Win32.MOUSEEVENTF_XUP;
                    mouseData = Win32.XBUTTON2;
                    break;
                default:
                    return rs; // Unknown mouse key -> ignore.
            }

            rs.U.mi.dx = 0;
            rs.U.mi.dy = 0;
            rs.U.mi.mouseData = mouseData;
            rs.U.mi.dwFlags = flags;
            rs.U.mi.time = 0;
            rs.U.mi.dwExtraInfo = IntPtr.Zero;
            return rs;
        }

        internal static void Down(Keys keys, bool reassert = false)
            => Change(keys, true, reassert: reassert);

        internal static void Up(Keys keys, bool reassert = false)
            => Change(keys, false, reassert: reassert);
    }

    internal static class Win32
    {
        internal const uint INPUT_MOUSE = 0;
        internal const uint INPUT_KEYBOARD = 1;

        internal const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        internal const uint KEYEVENTF_KEYUP = 0x0002;

        internal const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const uint MOUSEEVENTF_LEFTUP = 0x0004;
        internal const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        internal const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        internal const uint MOUSEEVENTF_XDOWN = 0x0080;
        internal const uint MOUSEEVENTF_XUP = 0x0100;

        internal const uint XBUTTON1 = 0x0001;
        internal const uint XBUTTON2 = 0x0002;

        // MapVirtualKey constants
        internal const uint MAPVK_VK_TO_VSC = 0;

        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            public uint type;
            public InputUnion U;
            public static int Size => Marshal.SizeOf<INPUT>();
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);
    }
}

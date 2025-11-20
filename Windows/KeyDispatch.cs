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
                {
                    Win32.INPUT[] inputs = [MouseEvent(keys, nowDown)];
                    Win32.SendInput(1, inputs, Win32.INPUT.Size);
                }
                return;
            }
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
            if (keys == Keys.PageUp || keys == Keys.PageDown || keys == Keys.Prior || keys == Keys.Next || keys == Keys.Home)
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
            rs.U.mi.dwExtraInfo = Win32.GetMessageExtraInfo();
            return rs;
        }

        internal static void DownUp(Keys keys)
        {
            int idx = (int)keys;
            if (idx >= keyStates.Length)
                return;

            bool isDown = keyStates[idx] > 0;
            if (isDown)
                return;

            bool isMouse = (keys >= Keys.LButton && keys <= Keys.XButton2);
            if (isMouse)
            {
                {
                    Win32.INPUT[] inputs = [MouseEvent(keys, true), MouseEvent(keys, false)];
                    Win32.SendInput(2, inputs, Win32.INPUT.Size);
                }
                return;
            }
            {
                Win32.INPUT[] kInputs = [
                    KeyEvent(keys, true),
                    KeyEvent(keys, false)
                    ];
                Win32.SendInput(2, kInputs, Win32.INPUT.Size);
            }



        }


        internal static KeyHandle Press(Keys keys)
            => new(keys);

        internal static void Reassert(Keys keys)
            => Change(keys, true, reassert: true);

        //internal static void Up(Keys keys, bool reassert = false)
        //    => Change(keys, false, reassert: reassert);
    }

    /// <summary>
    /// Represents a handle for managing the pressed and released state of one or more keys or buttons, providing timing
    /// and state information as well as control over their simulated input state.
    /// </summary>
    /// <remarks>A KeyHandle instance tracks the state and timing of the specified keys, allowing callers to
    /// programmatically press, release, or reassert the key state. The handle updates timing properties such as when
    /// the key was last pressed, released, or acted upon. Disposing the handle automatically releases the keys if they
    /// are pressed. This class is not thread-safe; callers should ensure thread safety if accessed from multiple
    /// threads.</remarks>
    public class KeyHandle : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the KeyHandle class for the specified key, optionally setting its initial
        /// pressed state.
        /// </summary>
        /// <param name="keys">The key to associate with this handle.</param>
        /// <param name="startPressed">true to set the key as pressed upon creation; otherwise, false. The default is true.</param>
        public KeyHandle(Keys keys, bool startPressed = true)
        {
            Keys = keys;
            if (startPressed)
                Press();
        }
        /// <summary>
        /// Gets the key or button to be emitted to the WinApi when this handle is pressed or released.
        /// </summary>
        public Keys Keys { get; }

        /// <summary>
        /// Gets a value indicating whether the key/button is currently pressed.
        /// </summary>
        public bool IsPressed { get; private set; }
        /// <summary>
        /// Gets the date and time when the button was last pressed.
        /// DateTime.MinValue if never pressed.
        /// </summary>
        public DateTime LastPressedAt { get; private set; }
        /// <summary>
        /// Gets the date and time when the item was released.
        /// DateTime.MinValue if never released.
        /// </summary>
        public DateTime LastReleasedAt { get; private set; }

        /// <summary>
        /// Gets the date and time when the key or button was last pressed or released. Does not account for reasserts.
        /// DateTime.MinValue if never changed.
        /// </summary>
        public DateTime LastChangeAt { get; private set; }
        /// <summary>
        /// Gets the date and time when the most recent action occurred.
        /// An action is either a press, a release, or a reassert.
        /// DateTime.MinValue if no actions have occurred.
        /// </summary>
        public DateTime LastActionAt { get; private set; }
        /// <summary>
        /// Gets the amount of time that has elapsed since the key/button was last pressed.
        /// </summary>
        public TimeSpan TimeSincePressed => DateTime.UtcNow - LastPressedAt;
        /// <summary>
        /// Gets the amount of time that has elapsed since the key/button was released.
        /// </summary>
        public TimeSpan TimeSinceReleased => DateTime.UtcNow - LastReleasedAt;
        /// <summary>
        /// Gets the amount of time that has elapsed since the last recorded action.
        /// An action is either a press, a release, or a reassert.
        /// </summary>
        public TimeSpan TimeSinceLastAction => DateTime.UtcNow - LastActionAt;

        /// <summary>
        /// Gets the amount of time that has elapsed since the last time this key/button was pressed or released.
        /// </summary>
        public TimeSpan TimeSinceLastChange => DateTime.UtcNow - LastChangeAt;

        public void Dispose()
        {
            Release();
        }


        /// <summary>
        /// Reapplies the current key state, ensuring that the key press or release is re-sent to the underlying system.
        /// </summary>
        /// <remarks>Use this method to explicitly reassert the key state when it may have been lost or
        /// needs to be refreshed. This can be useful in scenarios where external factors may have interfered with the
        /// expected key state.</remarks>
        public void Reassert()
        {
            KeyDispatch.Change(Keys, IsPressed, reassert: true);
            LastActionAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Releases the key or button if they are currently pressed.
        /// </summary>
        /// <remarks>If the key/button is not currently pressed, this method performs no action and returns
        /// false. The release action updates the relevant state and timestamps.</remarks>
        /// <returns>true if the key or button was pressed before and has now been released; otherwise, false.</returns>
        public bool Release()
        {
            if (IsPressed)
            {
                KeyDispatch.Change(Keys, false, reassert: false);
                IsPressed = false;
                LastChangeAt = LastActionAt = LastReleasedAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to press the key or button associated with this instance.
        /// </summary>
        /// <remarks>If the key or button is already pressed, this method does nothing and returns false.
        /// The method updates the press state and timestamps only when a press action occurs.</remarks>
        /// <returns>true if the key or button was not already pressed and is now pressed; otherwise, false.</returns>
        public bool Press()
        {
            if (!IsPressed)
            {
                KeyDispatch.Change(Keys, true, reassert: false);
                IsPressed = true;
                LastChangeAt = LastActionAt = LastPressedAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Toggles the current pressed state. If the object is pressed, it is released; otherwise, it is pressed.
        /// </summary>
        internal void Toggle()
        {
            if (IsPressed)
                Release();
            else
                Press();
        }
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

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HardwareInput hi;
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

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMessageExtraInfo();
    }
}

using System.Runtime.InteropServices;

namespace JoyMap
{
    public partial class PickKeyForm : Form
    {
        public PickKeyForm()
        {
            InitializeComponent();
        }

        private Dictionary<Keys, bool> Status { get; } = [];
        private Dictionary<Keys, ListViewItem> Rows { get; } = [];



        public static string KeysToString(Keys k)
        {
            List<string> parts = [];
            if ((k & Keys.Control) == Keys.Control)
            {
                parts.Add("Ctrl");
            }
            if ((k & Keys.Alt) == Keys.Alt)
            {
                parts.Add("Alt");
            }
            if ((k & Keys.Shift) == Keys.Shift)
            {
                parts.Add("Shift");
            }
            var keyOnly = k & Keys.KeyCode;

            parts.Add(KeyName(keyOnly));
            return string.Join(" + ", parts);
        }

        private static string KeyName(Keys k)
        {
            return k switch
            {
                Keys.Oemcomma => ",",
                Keys.Oemplus => "+",
                Keys.OemPeriod => ".",
                Keys.Oem1 => ";",
                Keys.Oem2 => "/",
                Keys.Oem3 => "`",
                Keys.Oem4 => "[",
                Keys.Oem5 => "\\",
                Keys.Oem6 => "]",
                Keys.Oem7 => "'",
                Keys.OemMinus => "-",
                Keys.Menu => "Alt",
                Keys.LMenu => "LAlt",
                Keys.RMenu => "RAlt",
                Keys.Next => "Page Down",      // VK_NEXT alias
                Keys.Prior => "Page Up",       // VK_PRIOR alias
                Keys.Return => "Enter",        // VK_RETURN alias
                Keys.Capital => "Caps Lock",   // VK_CAPITAL alias
                Keys.Snapshot => "Print Screen", // VK_SNAPSHOT alias
                _ => k.ToString()
            };
        }

        private void UpdateList()
        {
            foreach (var kvp in Status)
            {
                if (Rows.TryGetValue(kvp.Key, out var item))
                {
                    item.SubItems[1].Text = kvp.Value ? "Pressed" : "Released";
                }
                else
                {
                    var newItem = inputList.Items.Add(KeysToString(kvp.Key));
                    newItem.SubItems.Add(kvp.Value ? "Pressed" : "Released");
                    newItem.Tag = kvp.Key;
                    Rows[kvp.Key] = newItem;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_SYSKEYDOWN = 0x0104;
            if (msg.Msg == WM_SYSKEYDOWN)
            {
                var vk = keyData & Keys.KeyCode;
                if (vk is Keys.Menu or Keys.LMenu or Keys.RMenu)
                    {
                        if (vk == Keys.RMenu)
                            CleanupCtrlGroup();
                        RelayKey(vk, true, singleKey: false);
                        return true;
                    }
            }
            if (msg.Msg == WM_KEYDOWN && (keyData & Keys.KeyCode) == Keys.LControlKey && IsAltGrSyntheticCtrl())
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_KEYMENU = 0xF100;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_KEYMENU)
                return;
            base.WndProc(ref m);
        }

        private void PickKeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey && IsAltGrSyntheticCtrl())
                return;
            if (e.KeyCode == Keys.RMenu)
                CleanupCtrlGroup();
            RelayKey(e.KeyData, true, singleKey: true);
        }


        private void RelayKey(Keys k, bool isPressed, bool singleKey = false)
        {
            if ((k & Keys.Control) != 0)
            {
                RelayKey(Keys.ControlKey, isPressed);
                var rest = k & ~Keys.Control;
                if (rest != Keys.ControlKey)
                    RelayKey(rest, isPressed);
                return;
            }
            if ((k & Keys.Shift) != 0)
            {
                RelayKey(Keys.ShiftKey, isPressed);
                var rest = k & ~Keys.Shift;
                if (rest != Keys.ShiftKey)
                    RelayKey(rest, isPressed);
                return;
            }
            if ((k & Keys.Alt) != 0)
            {
                RelayKey(Keys.Menu, isPressed);
                var rest = k & ~Keys.Alt;
                if (rest != Keys.Menu)
                    RelayKey(rest, isPressed);
                return;
            }

            if (k == Keys.ControlKey)
            {
                RelayKey(Keys.LControlKey, isPressed);
                RelayKey(Keys.RControlKey, isPressed);
            }
            if (k == Keys.ShiftKey)
            {
                RelayKey(Keys.LShiftKey, isPressed);
                RelayKey(Keys.RShiftKey, isPressed);
            }
            if (k == Keys.Menu)
            {
                RelayKey(Keys.LMenu, isPressed);
                RelayKey(Keys.RMenu, isPressed);
            }
            if (k == Keys.PageUp)
            {

            }

            Status[k] = isPressed;
            UpdateList();
            if (singleKey)
            {
                Result = k;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void PickKeyForm_MouseDown(object sender, MouseEventArgs e)
        {
            // There is no Keys.MouseButtons. To represent mouse buttons as keys, you can map them manually.
            Keys mouseKey = e.Button switch
            {
                MouseButtons.Left => Keys.LButton,
                MouseButtons.Right => Keys.RButton,
                MouseButtons.Middle => Keys.MButton,
                MouseButtons.XButton1 => Keys.XButton1,
                MouseButtons.XButton2 => Keys.XButton2,
                _ => Keys.None
            };
            if (mouseKey != Keys.None)
            {
                Status[mouseKey] = true;
            }
            UpdateList();
            Result = mouseKey;
            DialogResult = DialogResult.OK;
            Close();

        }

        public Keys Result { get; private set; } = Keys.None;

        private void PickKeyForm_MouseUp(object sender, MouseEventArgs e)
        {
            Keys mouseKey = e.Button switch
            {
                MouseButtons.Left => Keys.LButton,
                MouseButtons.Right => Keys.RButton,
                MouseButtons.Middle => Keys.MButton,
                MouseButtons.XButton1 => Keys.XButton1,
                MouseButtons.XButton2 => Keys.XButton2,
                _ => Keys.None
            };
            if (mouseKey != Keys.None)
            {
                Status[mouseKey] = false;
            }
            UpdateList();
        }

        private void PickKeyForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey && Status.TryGetValue(Keys.RMenu, out var rMenuActive) && rMenuActive)
                return;
            RelayKey(e.KeyData, false);
        }

        private void inputList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            if (inputList.SelectedItems.Count > 0)
            {
                var item = inputList.SelectedItems[0];
                if (item.Tag is Keys k)
                {
                    Result = k;
                    btnOk.Enabled = true;
                }
            }

        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private static bool IsAltGrSyntheticCtrl()
        {
            const int VK_RMENU = 0xA5;
            return (GetAsyncKeyState(VK_RMENU) & 0x8000) != 0;
        }

        private void CleanupCtrlGroup()
        {
            foreach (var k in new[] { Keys.LControlKey, Keys.RControlKey, Keys.ControlKey })
            {
                if (Rows.TryGetValue(k, out var row))
                {
                    inputList.Items.Remove(row);
                    Rows.Remove(k);
                }
                Status.Remove(k);
            }
        }
    }
}

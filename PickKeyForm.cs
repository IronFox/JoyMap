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
            parts.Add(keyOnly.ToString());
            return string.Join(" + ", parts);
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

        private void PickKeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            RelayKey(e.KeyData, true);
        }


        private void RelayKey(Keys k, bool isPressed)
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
            Status[k] = isPressed;
            UpdateList();
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
    }
}

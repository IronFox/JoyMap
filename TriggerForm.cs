using System.Globalization;

namespace JoyMap
{
    public partial class TriggerForm : Form
    {
        public TriggerForm()
        {
            InitializeComponent();
        }

        private Event? Event { get; set; }

        private string LastAutoText { get; set; } = "";

        private void btnPickDeviceInput_Click(object sender, EventArgs e)
        {
            var form = new PickDeviceInputForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                Event = form.Result.Value;
                textDevice.Text = Event.Value.DeviceName;
                textInput.Text = $"{Event.Value.Which}{(Event.Value.Signed ? (Event.Value.Positive ? " Positive" : " Negative") : "")}";
                if (textLabel.Text == LastAutoText)
                    textLabel.Text = LastAutoText = $"{textDevice.Text} - {textInput.Text}";
            }
        }

        private float? GetMin()
        {
            var s = textMin.Text.Replace(',', '.');

            if (float.TryParse(s, CultureInfo.InvariantCulture, out var val))
            {
                return val / 100;
            }
            return null;
        }

        private float? GetMax()
        {
            var s = textMax.Text.Replace(',', '.');
            if (float.TryParse(s, CultureInfo.InvariantCulture, out var val))
            {
                return val / 100;
            }
            return null;
        }

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (Event is not null)
            {
                var state = Event.Value.GetLatestStatus();
                if (!Event.Value.Positive)
                {
                    state = -state;
                    labelStatus.Text = PickDeviceInputForm.Status2Str(state) + " (flipped)";
                }
                else
                    labelStatus.Text = PickDeviceInputForm.Status2Str(state);
                var min = GetMin();
                var max = GetMax();
                if (min is not null && max is not null)
                {
                    if (state >= min && state <= max)
                        labelActive.Text = "Yes";
                    else
                        labelActive.Text = "No";
                }
                else
                {
                    labelActive.Text = "N/A";
                }
            }
            else
            {
                labelStatus.Text = "N/A";
                labelActive.Text = "N/A";
            }
        }
    }
}

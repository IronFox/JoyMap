using JoyMap.ControllerTracking;
using JoyMap.Extensions;

namespace JoyMap
{
    public partial class PickDeviceInputForm : Form
    {
        EventRecorder EventRecorder { get; }
        public PickDeviceInputForm()
        {
            InitializeComponent();
            EventRecorder = new EventRecorder();
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            EventRecorder.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateDeviceListTimer.Stop();
            base.OnFormClosing(e);
        }

        private void PickDeviceInputForm_Load(object sender, EventArgs e)
        {

        }

        private readonly HashSet<EventKey> includedInputs = new();

        private void updateDeviceListTimer_Tick(object sender, EventArgs e)
        {
            Dictionary<EventKey, float> status = new();
            foreach (var ev in EventRecorder.GetAll())
            {
                var key = new EventKey(ev);
                if (includedInputs.Add(key))
                {
                    var row = inputList.Items.Add(ev.DeviceName);
                    row.SubItems.Add(ev.InputId.Axis.ToString());
                    row.SubItems.Add(Status2Str(ev.Status));
                    row.Tag = ev;
                }
                status.Add(key, ev.Status);
            }

            foreach (ListViewItem item in inputList.Items)
            {
                if (item.Tag is DeviceEvent record)
                {
                    var key = new EventKey(record);
                    if (status.TryGetValue(key, out var stat))
                    {
                        item.SubItems[2].Text = Status2Str(stat);
                        continue;
                    }
                    else
                    {
                        item.SubItems[2].Text = "Gone";
                    }
                }
            }

            if (inputList.Items.Count == 1 && inputList.SelectedIndices.Count == 0)
            {
                inputList.SelectedIndices.Add(0);
            }

        }

        public DeviceEvent? Result { get; private set; } = null;

        public static string Status2Str(float? status)
        {
            if (status is null)
                return "N/A";
            return Math.Round(status.Value * 100, 2).ToStr() + " %";
        }

        private void inputList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputList.SelectedItems.Count > 0)
            {
                var item = inputList.SelectedItems[0];
                var okay = item.SubItems[2].Text != "Gone";

                if (okay)
                {
                    if (item.Tag is DeviceEvent ev)
                    {
                        Result = ev;
                    }
                }
                btnOk.Enabled = okay;
            }
            else
            {
                btnOk.Enabled = false;
            }
        }
    }
}

using JoyMap.ControllerTracking;
using JoyMap.Extensions;
using JoyMap.Profile;

namespace JoyMap
{
    public partial class TriggerForm : Form
    {
        public TriggerForm(TriggerInstance? t = null)
        {
            InitializeComponent();
            if (t is not null)
            {
                SuspendEvents = true;
                Event = new DeviceEvent(
                    InputId: t.Trigger.InputId,
                    Status: t.GetCurrentValue() ?? 0,
                    GetLatestStatus: t.GetCurrentValue
                    );
                textDevice.Text = Event.Value.DeviceName;
                textInput.Text = Event.Value.InputId.AxisName;
                cbDelayRelease.Checked = t.Trigger.DelayReleaseMs is not null;
                textDelayReleaseMs.Text = t.Trigger.DelayReleaseMs?.ToStr() ?? "567.8";
                cbAutoReleaseActive.Checked = t.Trigger.AutoOffAfterMs is not null;
                textAutoReleaseMs.Text = t.Trigger.AutoOffAfterMs?.ToStr() ?? "567.8";
                textMin.Text = (t.Trigger.MinValue * 100).ToStr();
                textMax.Text = (t.Trigger.MaxValue * 100).ToStr();
                SuspendEvents = false;
                RebuildResult();

            }
        }
        private bool SuspendEvents { get; set; }
        private DeviceEvent? Event { get; set; }


        private void btnPickDeviceInput_Click(object sender, EventArgs e)
        {
            using var form = new PickDeviceInputForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                Event = form.Result.Value;
                textDevice.Text = Event.Value.DeviceName;
                textInput.Text = Event.Value.InputId.AxisName;

                RebuildResult();
            }

        }

        private float? GetMin()
            => textMin.GetFloat(true);

        private float? GetMax()
            => textMax.GetFloat(true);

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (Event is not null)
            {
                var state = Event.Value.GetLatestStatus();
                labelStatus.Text = PickDeviceInputForm.Status2Str(state, Event.Value.InputId.AxisNegated);

                if (Event.Value.InputId.AxisNegated && state is not null)
                {
                    state = -state;
                }

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

        public TriggerInstance? Result { get; private set; } = null;


        private void RebuildResult()
        {
            if (SuspendEvents)
                return;
            var min = GetMin();
            var max = GetMax();
            var releaseTriggerAfterMs = textAutoReleaseMs.GetFloat(false);
            if (!cbAutoReleaseActive.Checked)
                releaseTriggerAfterMs = null;
            var delayReleaseMs = textDelayReleaseMs.GetFloat(false);
            if (!cbDelayRelease.Checked)
                delayReleaseMs = null;
            if (Event is not null && min is not null && max is not null && (!cbAutoReleaseActive.Checked || releaseTriggerAfterMs is not null))
            {

                Result = TriggerInstance.Build(
                    getCurrentValue: Event.Value.GetLatestStatus,
                    t: new(
                        Event.Value.InputId,
                        min.Value,
                        max.Value,
                        AutoOffAfterMs: releaseTriggerAfterMs,
                        DelayReleaseMs: delayReleaseMs
                        )
                    );

                btnOk.Enabled = true;
            }
            else
            {
                Result = null;
                btnOk.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void textMin_TextChanged(object sender, EventArgs e)
        {
            RebuildResult();
        }

        private void textMax_TextChanged(object sender, EventArgs e)
        {
            RebuildResult();
        }

        private void textAutoReleaseMs_TextChanged(object sender, EventArgs e)
        {
            if (SuspendEvents)
                return;
            cbAutoReleaseActive.Checked = true;
            RebuildResult();
        }

        private void cbAutoReleaseActive_CheckedChanged(object sender, EventArgs e)
        {
            RebuildResult();
        }

        private void textDelayReleaseMs_TextChanged(object sender, EventArgs e)
        {
            if (SuspendEvents)
                return;
            cbDelayRelease.Checked = true;
            RebuildResult();
        }

        private void cbDelayRelease_CheckedChanged(object sender, EventArgs e)
        {
            RebuildResult();
        }
    }
}

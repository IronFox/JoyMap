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
                if (t.Trigger.Range is not null)
                {
                    cbDelayRelease.Checked = t.Trigger.Range.Value.DelayReleaseMs is not null;
                    textDelayReleaseMs.Text = t.Trigger.Range.Value.DelayReleaseMs?.ToStr() ?? "567.8";
                    cbAutoReleaseActive.Checked = t.Trigger.Range.Value.AutoOffAfterMs is not null;
                    textAutoReleaseMs.Text = t.Trigger.Range.Value.AutoOffAfterMs?.ToStr() ?? "567.8";
                    textMin.Text = (t.Trigger.Range.Value.MinValue * 100).ToStr();
                    textMax.Text = (t.Trigger.Range.Value.MaxValue * 100).ToStr();
                    tabs.SelectedTab = tabRange;
                }
                else if (t.Trigger.Dither is not null)
                {
                    textRampStart.Text = (t.Trigger.Dither.Value.RampStart * 100).ToStr();
                    textRampMax.Text = (t.Trigger.Dither.Value.RampMax * 100).ToStr();
                    textDitherFrequency.Text = t.Trigger.Dither.Value.Frequency.ToStr();
                    tabs.SelectedTab = tabDither;
                }

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
            if (Event is not null)
            {
                if (tabs.SelectedTab == tabDither)
                {
                    var rampStart = textRampStart.GetFloat(true);
                    var rampMax = textRampMax.GetFloat(true);
                    var frequency = textDitherFrequency.GetFloat(false);
                    if (rampStart is not null && rampMax is not null && frequency is not null)
                    {
                        Result = TriggerInstance.Build(
                        getCurrentValue: Event.Value.GetLatestStatus,
                        t: new(
                            Event.Value.InputId,
                            Dither: new(
                                RampStart: rampStart.Value,
                                RampMax: rampMax.Value,
                                Frequency: frequency.Value
                                )
                            )
                        );
                        btnOk.Enabled = true;
                    }
                    else
                        btnOk.Enabled = false;

                    return;
                }
                else if (tabs.SelectedTab == tabRange)
                {
                    var min = GetMin();
                    var max = GetMax();
                    var releaseTriggerAfterMs = textAutoReleaseMs.GetFloat(false);
                    if (!cbAutoReleaseActive.Checked)
                        releaseTriggerAfterMs = null;
                    var delayReleaseMs = textDelayReleaseMs.GetFloat(false);
                    if (!cbDelayRelease.Checked)
                        delayReleaseMs = null;
                    if (min is not null && max is not null && (!cbAutoReleaseActive.Checked || releaseTriggerAfterMs is not null))
                    {
                        Result = TriggerInstance.Build(
                            getCurrentValue: Event.Value.GetLatestStatus,
                            t: new(
                                Event.Value.InputId,
                                    Range: new(
                                        min.Value,
                                        max.Value,
                                        AutoOffAfterMs: releaseTriggerAfterMs,
                                        DelayReleaseMs: delayReleaseMs
                                    )
                                )
                            );
                        btnOk.Enabled = true;
                    }
                    else
                        btnOk.Enabled = false;
                }
                else
                    btnOk.Enabled = false;


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

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildResult();
        }

        private void AnyChanged(object sender, EventArgs e)
        {
            RebuildResult();

        }
    }
}

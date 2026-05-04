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
            SetupTooltips();
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

        private void SetupTooltips()
        {
            var tt = new ToolTip { AutoPopDelay = 10000, InitialDelay = 400, ReshowDelay = 200 };
            tt.MakeDark();

            tt.SetToolTip(btnPickDeviceInput,
                "Pick the physical device and axis/button that will serve as the input source for this trigger.");

            tt.SetToolTip(textMin,
                "Minimum axis value (as a percentage) required to activate the trigger.\n" +
                "Use 10–15% deadzone for analog sticks to avoid accidental activation.");

            tt.SetToolTip(textMax,
                "Maximum axis value (as a percentage) that keeps the trigger active.\n" +
                "Values outside [Min%, Max%] deactivate the trigger.");

            tt.SetToolTip(label4, "Minimum axis percentage required to activate the trigger.");
            tt.SetToolTip(label5, "Maximum axis percentage that keeps the trigger active.");

            tt.SetToolTip(cbAutoReleaseActive,
                "Automatically deactivate the trigger after this many milliseconds, even if the physical input is still held.\n" +
                "Useful for sequential macros.");

            tt.SetToolTip(textAutoReleaseMs,
                "Duration in milliseconds after which the trigger auto-releases.\n" +
                "Example: use 200ms, 400ms delays for sequential macro steps.");

            tt.SetToolTip(cbDelayRelease,
                "Delay the release of the trigger by this many milliseconds after the physical input drops out of range.\n" +
                "Helps smooth out brief dropouts.");

            tt.SetToolTip(textDelayReleaseMs,
                "How long (ms) to wait before releasing the trigger after the input leaves the active range.");

            tt.SetToolTip(tabRange,
                "Range mode: the trigger fires while the axis value stays within [Min%, Max%].\n" +
                "Best for buttons and simple axis thresholds.");

            tt.SetToolTip(tabDither,
                "Dither mode: modulates the trigger using a PWM-style duty cycle derived from the axis value.\n" +
                "Use for analog-to-digital conversion (e.g., gradual throttle → repeated keypress).\n" +
                "Frequency is FPS-dependent — for 60 FPS games use 20–30 Hz; for 30 FPS games use 10–15 Hz.");

            tt.SetToolTip(label1,
                "Axis percentage at which the dither duty cycle starts (0% duty cycle below this value).");
            tt.SetToolTip(textRampStart,
                "Ramp Start %: axis value at which dithering begins.\n" +
                "Below this the action is never triggered.");

            tt.SetToolTip(label7,
                "Axis percentage at which the dither duty cycle reaches 100%.");
            tt.SetToolTip(textRampMax,
                "Ramp Max %: axis value at which dithering reaches full (100%) duty cycle.\n" +
                "Above this the action fires every cycle.");

            tt.SetToolTip(label9, "Dither frequency in Hz (cycles per second).");
            tt.SetToolTip(textDitherFrequency,
                "How many times per second the dither cycle repeats.\n" +
                "Match to game FPS: 30 FPS → 10–15 Hz, 60 FPS → 20–30 Hz, 120+ FPS → 30–60 Hz.\n" +
                "FPS-aware: lower frequency in low-FPS games to prevent missed inputs.\n" +
                "Timing mode is wall-clock based and unaffected by game FPS.");
        }

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

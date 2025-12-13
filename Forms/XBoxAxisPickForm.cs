using JoyMap.ControllerTracking;
using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.XBox;

namespace JoyMap
{
    public partial class XBoxAxisPickForm : Form
    {
        public XBoxAxisPickForm(XBoxAxis axis, AxisInput? input)
        {
            InitializeComponent();
            Axis = axis;
            Text = "XBox Axis Input - " + axis;
            if (input is not null)
            {
                Event = input.Value.OriginalInput;
                textDevice.Text = Event.Value.DeviceName;
                textInput.Text = Event.Value.InputId.AxisName;
                textDeadzone.Text = (input.Value.Input.DeadZone * 100).ToStr();
                textScale.Text = (input.Value.Input.Scale * 100).ToStr();
                Translation = input.Value.Input.Translation;

            }
            RebuildResult(this, EventArgs.Empty);
        }

        public DeviceEvent? Event { get; private set; }
        public AxisInput? Result { get; private set; }
        public XBoxAxis Axis { get; }
        public XBoxAxisTranslation Translation { get; private set; } = XBoxAxisTranslation.Linear;

        private void RebuildResult(object sender, EventArgs e)
        {
            Result = null;
            btnOk.Enabled = false;
            if (Event is null)
            {
                statusLabel.Text = "No input selected";
                return;
            }
            var deadZone = textDeadzone.GetFloat(true);
            if (deadZone == null)
            {
                statusLabel.Text = "Invalid deadzone";
                return;
            }
            var scale = textScale.GetFloat(true);
            if (scale == null)
            {
                statusLabel.Text = $"Invalid scale";
                return;
            }
            var input = new XBoxInputAxis(
                    InputId: Event.Value.InputId,
                    DeadZone: deadZone.Value,
                    Scale: scale.Value,
                    Translation: Translation
                    );
            Result = new AxisInput(
                Input: input,
                OriginalInput: Event.Value,
                GetValue: AxisInput.BuildGetter(Event.Value.GetLatestStatus, input)
                );
            btnOk.Enabled = true;
            statusLabel.Text = "Ok";
        }

        private void btnPickDeviceInput_Click(object sender, EventArgs e)
        {
            using var form = new PickDeviceInputForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                Event = form.Result.Value;
                textDevice.Text = Event.Value.DeviceName;
                textInput.Text = Event.Value.InputId.AxisName;

                RebuildResult(sender, e);
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (Result is not null)
            {
                lInput.Text = $"Input: {Event!.Value.GetLatestStatus().ToStr()}";
                lOutput.Text = $"Output: {Result.Value.GetValue().ToStr()}";
            }
        }
    }
}

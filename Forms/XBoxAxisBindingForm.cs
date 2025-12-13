using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.XBox;

namespace JoyMap.Forms
{
    public partial class XBoxAxisBindingForm : Form
    {
        public XBoxAxisBindingForm(XBoxAxis axis)
        {
            InitializeComponent();
            Axis = axis;
            lAxis.Text = $"Axis: {axis}";
        }

        //private List<AxisInput> AxisInputList { get; } = [];

        public XBoxAxisBindingInstance? Result { get; private set; }
        public XBoxAxis Axis { get; }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new XBoxAxisPickForm(Axis, null);
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                var item = axisListView.Items.Add(form.Result.Value.Input.InputId.AxisName);
                item.SubItems.Add(form.Result.Value.Input.Translation.ToString());
                item.SubItems.Add(form.Result.Value.GetValue().ToStr());
                item.Tag = form.Result.Value;
            }
        }
    }
}

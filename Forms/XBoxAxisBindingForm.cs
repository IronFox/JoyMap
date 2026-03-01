using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.XBox;

namespace JoyMap.Forms
{
    public partial class XBoxAxisBindingForm : Form
    {
        private record StatusItem(GlobalStatusInstance? Status)
        {
            public override string ToString() =>
                Status is null ? "(none)" : $"{Status.Id}: {Status.Status.Name}";
        }

        public XBoxAxisBindingForm(XBoxAxis axis, XBoxAxisBindingInstance? instance = null, IReadOnlyList<GlobalStatusInstance>? globalStatuses = null)
        {
            InitializeComponent();
            Axis = axis;
            Text = lAxis.Text = $"Axis: {axis}";

            cbEnableStatus.Items.Add(new StatusItem(null));
            foreach (var gs in globalStatuses ?? [])
                cbEnableStatus.Items.Add(new StatusItem(gs));
            cbEnableStatus.SelectedIndex = 0;

            if (instance != null)
            {
                foreach (var input in instance.InputInstances)
                    Add(input);

                if (instance.Binding.EnableStatusId is not null)
                {
                    var match = cbEnableStatus.Items.OfType<StatusItem>()
                        .FirstOrDefault(x => x.Status?.Id == instance.Binding.EnableStatusId);
                    if (match is not null)
                        cbEnableStatus.SelectedItem = match;
                }
            }
            RebuildResult(null, null);
        }

        //private List<AxisInput> AxisInputList { get; } = [];

        public XBoxAxisBindingInstance? Result { get; private set; }
        public XBoxAxis Axis { get; }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new XBoxAxisPickForm(Axis, null);
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                Add(form.Result.Value);
                RebuildResult(sender, e);
            }
        }

        private void Add(AxisInput input)
        {
            var item = axisListView.Items.Add(input.Input.InputId.AxisName);
            item.SubItems.Add(input.Input.Translation.ToString());
            item.SubItems.Add(input.GetValue().ToStr());
            item.Tag = input;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = axisListView.SelectedItems.ToEnumerable().SafeSingleOrDefault();
            if (item != null)
            {
                using var form = new XBoxAxisPickForm(Axis, (AxisInput)item.Tag!);
                if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
                {
                    item.SubItems[1].Text = form.Result.Value.Input.Translation.ToString();
                    item.SubItems[2].Text = form.Result.Value.GetValue().ToStr();
                    item.Tag = form.Result.Value;
                    RebuildResult(sender, e);
                }
            }
        }

        private void RebuildResult(object? sender, EventArgs? e)
        {
            btnOk.Enabled = false;
            Result = null;
            if (axisListView.Items.Count == 0)
            {
                statusLabel.Text = "Add at least one axis to continue";
                return;
            }
            var axes = axisListView.Items.ToEnumerable().Select(x => (AxisInput)x.Tag!).ToList();
            var enableStatusItem = cbEnableStatus.SelectedItem as StatusItem;
            var enableStatusId = enableStatusItem?.Status?.Id;

            Func<float?> baseGet = XBoxAxisBindingInstance.CombineAxisInputs(axes);
            Func<float?> getValueFn = baseGet;
            if (enableStatusItem?.Status is { } statusInst)
            {
                var cap = statusInst;
                getValueFn = () => cap.CurrentValue ? baseGet() : 0f;
            }

            Result = new(
                new XBoxAxisBinding(axes.Select(x => x.Input).ToList(), Axis, enableStatusId),
                axes,
                getValueFn);
            btnOk.Enabled = true;
            statusLabel.Text = "Ok";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var idxs = axisListView.SelectedIndices.ToEnumerable().Reverse().ToArray();
            foreach (var idx in idxs)
            {
                axisListView.Items.RemoveAt(idx);
            }
            RebuildResult(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var row in axisListView.Items.ToEnumerable())
            {
                var axis = (AxisInput)row.Tag!;
                row.SubItems[2].Text = axis.GetValue().ToStr();
            }

            if (Result is not null)
            {
                lOutput.Text = $"Output: {Result.GetValue().ToStr()}";
            }
            else
                lOutput.Text = $"Output: -";
        }

        private void axisListView_DoubleClick(object sender, EventArgs e)
        {
            editToolStripMenuItem_Click(sender, e);
        }
    }
}

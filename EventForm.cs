using JoyMap.Profile;

namespace JoyMap
{
    public partial class EventForm : Form
    {
        public EventForm()
        {
            InitializeComponent();
        }

        private List<(TriggerInstance Trigger, ListViewItem Row)> Triggers { get; } = [];


        private void pickAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TriggerForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                var idx = Triggers.Count;
                var t = form.Result;

                var row = triggerListView.Items.Add($"T{idx}");
                row.SubItems.Add(t.Trigger.InputId.ControllerName);
                row.SubItems.Add(t.Trigger.InputId.AxisName);
                row.SubItems.Add(t.IsTriggered ? "A" : "");
                row.Tag = t;
                Triggers.Add((t, row));
            }
        }

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            foreach (var (trigger, row) in Triggers)
            {
                row.SubItems[3].Text = trigger.IsTriggered ? "A" : "";
            }

        }
    }
}

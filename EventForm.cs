namespace JoyMap
{
    public partial class EventForm : Form
    {
        public EventForm()
        {
            InitializeComponent();
        }

        private void pickAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TriggerForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {

            }
        }
    }
}

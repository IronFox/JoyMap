using JoyMap.ControllerTracking;

namespace JoyMap
{
    public partial class MainForm : Form
    {
        InputMonitor InputMonitor { get; }

        public MainForm()
        {
            InitializeComponent();
            InputMonitor = new InputMonitor(Handle);
        }


        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            InputMonitor.Dispose();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new EventForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                //var evt = form.Event;
            }
        }
    }
}

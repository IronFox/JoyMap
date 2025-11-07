using JoyMap.ControllerTracking;
using JoyMap.Extensions;
using JoyMap.Profile;
using JoyMap.Windows;
using System.Text.RegularExpressions;

namespace JoyMap
{
    public partial class MainForm : Form
    {
        private record ProfileSelection(Profile.Profile Profile)
        {
            public override string ToString() => Profile.Name;
        }


        InputMonitor InputMonitor { get; }

        public MainForm()
        {
            InitializeComponent();
            InputMonitor = new InputMonitor(Handle);
            Registry.LoadAll();
            cbProfile.Items.AddRange(Registry.GetAllProfiles().Select(x => new ProfileSelection(x)).ToArray());
            cbProfile.SelectedIndex = 0;
            cbProfile_SelectedIndexChanged(this, EventArgs.Empty);
        }


        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            InputMonitor.Dispose();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            using var form = new EventForm();
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                var row = eventListView.Items.Add(form.Result.Event.Name);
                row.Tag = form.Result;
                row.SubItems.Add(string.Join(", ", form.Result.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
                row.SubItems.Add(string.Join(", ", form.Result.Actions.Select(x => x.Action)));
                row.SubItems.Add("");
                ActiveProfile.Events.Add(form.Result);
                Registry.Persist(ActiveProfile);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new PickWindowForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                WorkProfile p = new WorkProfile { Name = form.Result, WindowRegex = Regex.Escape(form.Result) };
                Registry.Persist(p);
                cbProfile.Items.Clear();
                cbProfile.Items.AddRange(Registry.GetAllProfiles().Select(x => new ProfileSelection(x)).ToArray());
                cbProfile.SelectedIndex = cbProfile.Items.ToEnumerable().ToList().FindIndex(x => (x as ProfileSelection)?.Profile.Id == p.Id);
            }
        }

        private WorkProfile? ActiveProfile { get; set; }

        private void LoadProfile(WorkProfile? profile)
        {

            if (profile is null || profile.Id == ActiveProfile?.Id)
                return;
            if (ActiveProfile is not null)
                ActiveProfile.Stop();
            ActiveProfile = profile;
            textWindowRegex.Text = profile.WindowRegex;
            textWindowRegex.Enabled = true;
            textProfileName.Text = profile.Name;
            textProfileName.Enabled = true;
            btnAddPickWindow.Enabled = true;
            eventListView.ContextMenuStrip = eventContextMenu;
            foreach (var ev in profile.Events)
            {
                var row = eventListView.Items.Add(ev.Event.Name);
                row.Tag = ev;
                row.SubItems.Add(string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
                row.SubItems.Add(string.Join(", ", ev.Actions.Select(x => x.Action)));
                row.SubItems.Add("");
            }
            profile.StartListen(this);
        }

        private void btnAddPickWindow_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            using var form = new PickWindowForm();
            if (form.ShowDialog(this) == DialogResult.OK && form.Result is not null)
            {
                if (Regex.Escape(ActiveProfile.Name) == ActiveProfile.WindowRegex)
                    ActiveProfile.Name = form.Result;
                ActiveProfile.WindowRegex = textWindowRegex.Text = Regex.Escape(form.Result);
                Registry.Persist(ActiveProfile);
            }

        }

        private void Flush()
        {
            if (ActiveProfile is not null)
                ActiveProfile.Stop();
            ActiveProfile = null;
            textWindowRegex.Text = "";
            textWindowRegex.Enabled = false;
            textProfileName.Text = "";
            textProfileName.Enabled = false;
            eventListView.Items.Clear();
            eventListView.ContextMenuStrip = null;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
        }

        private void eventListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUp.Enabled = eventListView.SelectedItems.Count > 0 && eventListView.SelectedItems[0].Index > 0;
            btnDown.Enabled = eventListView.SelectedItems.Count > 0 && eventListView.SelectedItems[0].Index < eventListView.Items.Count - 1;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
            {
                btnUp.Enabled = false;
                return;
            }

            if (eventListView.SelectedItems.Count == 0)
                return;
            var item = eventListView.SelectedItems[0];
            var idx = item.Index;
            if (idx == 0)
                return;
            eventListView.Items.RemoveAt(idx);
            eventListView.Items.Insert(idx - 1, item);
            var temp = ActiveProfile.Events[idx];
            ActiveProfile.Events.RemoveAt(idx);
            ActiveProfile.Events.Insert(idx - 1, temp);
            Registry.Persist(ActiveProfile);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
            {
                btnDown.Enabled = false;
                return;
            }
            if (eventListView.SelectedItems.Count == 0)
                return;
            var item = eventListView.SelectedItems[0];
            var idx = item.Index;
            if (idx >= eventListView.Items.Count - 1)
                return;
            eventListView.Items.RemoveAt(idx);
            eventListView.Items.Insert(idx + 1, item);
            var temp = ActiveProfile.Events[idx];
            ActiveProfile.Events.RemoveAt(idx);
            ActiveProfile.Events.Insert(idx + 1, temp);
            Registry.Persist(ActiveProfile);
        }

        private void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile = Registry.Instantiate(InputMonitor, (cbProfile.SelectedItem as ProfileSelection)?.Profile);
            if (profile is not null)
            {
                LoadProfile(WorkProfile.Load(profile));
            }
            else
                Flush();
        }

        private void eventListView_DoubleClick(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            if (eventListView.SelectedItems.Count == 0)
                return;
            var item = eventListView.SelectedItems[0];
            if (item.Tag is not EventInstance ev)
                return;
            using var form = new EventForm(ev);
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                item.SubItems[0].Text = form.Result.Event.Name;
                item.SubItems[1].Text = string.Join(", ", form.Result.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
                item.SubItems[2].Text = string.Join(", ", form.Result.Actions.Select(x => x.Action));
                item.Tag = form.Result;
                var idx = item.Index;
                ActiveProfile.Events[idx] = form.Result;
                Registry.Persist(ActiveProfile);
            }

        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (ActiveProfile is null) return;
            foreach (ListViewItem row in eventListView.Items)
            {
                if (row.Tag is not EventInstance ev) continue;
                bool any = false;
                foreach (var trigger in ev.TriggerInstances)
                {
                    if (trigger.IsTriggered)
                    {
                        any = true;
                        break;
                    }
                }
                row.SubItems[3].Text = any ? "A" : "";
            }


            WindowReference? focusedWindow = WindowReference.OfFocused();
            if (focusedWindow is not null)
            {

                var match = Registry.FindAndLoadForWindow(focusedWindow.Value.Title, InputMonitor);

                if (match != null && match.Profile.Id != ActiveProfile?.Id)
                {
                    LoadProfile(WorkProfile.Load(match));
                }
            }
        }
    }
}

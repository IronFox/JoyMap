using JoyMap.ControllerTracking;
using JoyMap.Extensions;
using JoyMap.Forms;
using JoyMap.Profile;
using JoyMap.Undo.Action;
using JoyMap.Undo.Action.Binding;
using JoyMap.Undo.Action.EventAction;
using JoyMap.Util;
using JoyMap.Windows;
using JoyMap.XBox;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace JoyMap
{
    public partial class MainForm : Form
    {
        private record ProfileSelection(Profile.Profile Profile)
        {
            public override string ToString() => Profile.Name;
        }


        internal InputMonitor InputMonitor { get; }


        public void RefreshProfileList()
        {
            var selectedProfileId = ActiveProfile?.Id;
            var allProfiles = Registry.GetAllProfiles().Select(x => new ProfileSelection(x)).ToArray();
            cbProfile.Items.Clear();
            cbProfile.Items.AddRange(allProfiles);
            var previousIndexInNewList = allProfiles.IndexOf(x => x.Profile.Id == selectedProfileId);
            if (previousIndexInNewList < 0)
            {
                if (cbProfile.Items.Count > 0)
                {
                    cbProfile.SelectedIndex = 0;
                    cbProfile_SelectedIndexChanged(this, EventArgs.Empty);
                }
                return;
            }

            cbProfile.SelectedIndex = previousIndexInNewList;
            cbProfile_SelectedIndexChanged(this, EventArgs.Empty);
        }

        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            var families = Registry.LoadAll();
            InputMonitor = new InputMonitor(Handle, families);
            RefreshProfileList();
#if !DEBUG
            saveDebugOnlyToolStripMenuItem.Enabled = false;
#endif
            Log("Ready");
        }

        public bool SuppressIfGameIsNotFocused { get; private set; } = true;


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
                ActiveProfile.History.ExecuteAction(new AddEventInstanceAction(this, ActiveProfile, form.Result));
            }
        }

        private void CreateProfile(WorkProfile p)
        {
            Registry.Persist(p);
            LoadProfile(p);
            RefreshProfileList();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new PickWindowForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var procName = form.Result?.ProcessName;
                if (procName is null)
                    return;
                WorkProfile p = new WorkProfile
                {
                    Name = procName,
                    ProcessNameRegex = Regex.Escape(procName),
                    WindowNameRegex = Regex.Escape(form.Result!.WindowTitle)
                };
                CreateProfile(p);
            }
        }


        private void newEmptyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProfile(new WorkProfile
            {
                Name = "New Profile",
                ProcessNameRegex = "",
                WindowNameRegex = ""
            });
        }

        public WorkProfile? ActiveProfile { get; private set; }

        private void LoadProfile(WorkProfile? profile)
        {
            ProfileExecution.Stop();

            if (profile is null || profile == ActiveProfile)
                return;
            ActiveProfile = profile;
            WithNoEvent(() =>
            {
                textNotes.Text = profile.Notes;
                textNotes.Enabled = true;
                textProcessNameRegex.Text = profile.ProcessNameRegex;
                textProcessNameRegex.Enabled = true;
                textWindowNameRegex.Text = profile.WindowNameRegex;
                textWindowNameRegex.Enabled = true;
                textProfileName.Text = profile.Name;
                textProfileName.Enabled = true;
                btnAddPickWindow.Enabled = true;
                eventListView.ContextMenuStrip = eventContextMenu;
                btnDeleteCurrentProfile.Enabled = true;
                eventListView.Items.Clear();
                foreach (var ev in profile.Events)
                {
                    var row = eventListView.Items.Add(ev.Event.Name);
                    row.Tag = ev;
                    row.SubItems.Add(string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName)));
                    row.SubItems.Add(string.Join(", ", ev.Actions.Select(x => x.Action)));
                    row.SubItems.Add("");
                }
                bindingListView.Items.Clear();
                foreach (var axis in Enum.GetValues<XBoxAxis>())
                {
                    profile.AxisBindings.TryGetValue(axis, out var bound);
                    var row = new AxisRowHandle(this, axis, bindingListView.Items.Add(axis.ToString()));
                    row.Row.SubItems.Add("");
                    row.Row.SubItems.Add("");

                    row.Bind(bound, false);
                }
            });
        }

        private void btnAddPickWindow_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            using var form = new PickWindowForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var procName = form.Result?.ProcessName;
                if (procName is null)
                    return;
                ActiveProfile.History.ExecuteAction(
                    new SetAllRegexAction(
                        this,
                        ActiveProfile,
                        processName: Regex.Escape(procName),
                        windowName: Regex.Escape(form.Result!.WindowTitle),
                        textProcessNameRegex,
                        textWindowNameRegex
                        )
                    );
                WithNoEvent(() =>
                {
                    textProcessNameRegex.Text = Regex.Escape(procName);
                    textWindowNameRegex.Text = Regex.Escape(form.Result!.WindowTitle);
                });
            }

        }

        public ListView EventListView => eventListView;
        public ListView BindingListView => bindingListView;
        public AxisRowHandle RequireRowOf(XBoxAxis axis)
        {
            return new AxisRowHandle(this, axis, bindingListView.Items.ToEnumerable().First(x => AxisOf(x) == axis));
        }

        public record AxisRowHandle(MainForm Form, XBoxAxis Axis, ListViewItem Row)
        {
            public bool GetBound([NotNullWhen(true)] out XBoxAxisBindingInstance? bound)
            {
                bound = Row.Tag as XBoxAxisBindingInstance;
                return bound is not null;
            }

            public void Bind(XBoxAxisBindingInstance? b, bool updateCurrentProfile = true)
            {
                AxisUpdateItemTo(Row, Axis, b);
                if (updateCurrentProfile && Form.ActiveProfile is not null)
                {
                    if (b is not null)
                        Form.ActiveProfile.AxisBindings[Axis] = b;
                    else
                        Form.ActiveProfile.AxisBindings.Remove(Axis);
                }
            }
        }

        private void Flush()
        {
            if (ActiveProfile is not null)
                ProfileExecution.Stop();
            ActiveProfile = null;
            textNotes.Enabled = false;
            textNotes.Text = "";
            textProcessNameRegex.Text = "";
            textProcessNameRegex.Enabled = false;
            textProfileName.Text = "";
            textProfileName.Enabled = false;
            eventListView.Items.Clear();
            eventListView.ContextMenuStrip = null;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            btnDeleteCurrentProfile.Enabled = false;
        }

        private void eventListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUp.Enabled = eventListView.SelectedItems.Count > 0 && eventListView.SelectedItems[0].Index > 0;
            btnDown.Enabled = eventListView.SelectedItems.Count > 0 && eventListView.SelectedItems[eventListView.SelectedItems.Count - 1].Index < eventListView.Items.Count - 1;
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
            ActiveProfile.History.ExecuteAction(new MoveEventInstanceUpAction(this, ActiveProfile, eventListView.SelectedIndices.ToArray()));
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
            ActiveProfile.History.ExecuteAction(new MoveEventInstanceDownAction(this, ActiveProfile, eventListView.SelectedIndices.ToArray()));

        }

        private void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cbProfile.SelectedItem as ProfileSelection;
            if (selected is null)
            {
                Flush();
                return;
            }
            if (selected.Profile.Id == ActiveProfile?.Id)
                return;

            var profile = Registry.Instantiate(InputMonitor, (cbProfile.SelectedItem as ProfileSelection)?.Profile);
            if (profile is not null)
            {
                LoadProfile(Registry.ToWorkProfile(profile));
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
                ActiveProfile.History.ExecuteAction(new EditEventInstanceAction(this, ActiveProfile, item.Index, ev, form.Result));
            }

        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var lastOpenedForm = Application.OpenForms.Cast<Form>().Last();
            JoyMapIsFocused = lastOpenedForm.ContainsFocus;

            if (JoyMapIsFocused)
            {
                foreach (ListViewItem row in eventListView.Items)
                {
                    if (row.Tag is not EventInstance ev) continue;
                    row.SubItems[3].Text = ev.IsSuspended ? "Suspended" : ev.IsTriggered() ? "A" : "";
                }
                foreach (ListViewItem row in bindingListView.Items)
                {
                    if (row.Tag is not XBoxAxisBindingInstance map) continue;
                    row.SubItems[2].Text = map.IsSuspended ? "Suspended" : map.GetValue().ToStr();
                }
            }


            var focusedWindow = FocusPredicate.Get();
            if (focusedWindow.Any)
            {

                var match = Registry.FindAndLoadForWindow(focusedWindow, InputMonitor);

                if (match != null && match.Profile.Id != ActiveProfile.Id)
                {
                    cbProfile.SelectedIndex = cbProfile.Items.ToEnumerable().ToList().FindIndex(x => (x as ProfileSelection)?.Profile.Id == match.Profile.Id);
                    //LoadProfile(WorkProfile.Load(match));
                }
                GameNotFocused =
                    SuppressIfGameIsNotFocused && (match is null || !match.Is(focusedWindow));
            }
            else
                GameNotFocused = SuppressIfGameIsNotFocused;

            if (!JoyMapIsFocused && !GameNotFocused)
            {
                //focusedWindow.TopMost?.Focus();
                ProfileExecution.Start(ActiveProfile);
            }
            else
                if (ProfileExecution.IsActive)
                    ProfileExecution.Stop();
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveProfile?.Events.Select(ev => ev.Event).ToList().CopyToClipboard();
        }

        private void eventContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            copySelectedToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = eventListView.SelectedItems.Count > 0;
            var copied = ClipboardUtil.GetCopied<Event>();
            pasteOverToolStripMenuItem.Enabled = copied?.Count == eventListView.SelectedItems.Count;
            pasteInsertToolStripMenuItem.Enabled = copied is not null;
            moveSelectedUpToolStripMenuItem.Enabled = btnUp.Enabled;
            moveSelectedDownToolStripMenuItem.Enabled = btnDown.Enabled;
            selectAllToolStripMenuItem.Enabled = eventListView.Items.Count > 0;
            editSelectedToolStripMenuItem.Enabled = eventListView.SelectedItems.Count == 1;
        }

        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedEvents = eventListView.SelectedItems.ToEnumerable()
                .Select(item => item.Tag)
                .OfType<EventInstance>()
                .Select(ev => ev.Event)
                .ToList();
            selectedEvents.CopyToClipboard();
        }

        private void pasteOverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var copiedEvents = ClipboardUtil.GetCopied<Event>();
            if (copiedEvents is null)
                return;
            if (eventListView.SelectedItems.Count != copiedEvents.Count)
                return;

            ActiveProfile.History.ExecuteAction(new PasteOverEventInstancesAction(this, ActiveProfile, eventListView.SelectedItems.ToEnumerable().Select(item => item.Index).ToList(), copiedEvents));

        }

        private void pasteInsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var copiedEvents = ClipboardUtil.GetCopied<Event>();
            if (copiedEvents is null)
                return;
            int insertIndex = eventListView.SelectedItems.Count > 0 ? eventListView.SelectedItems[0].Index : eventListView.Items.Count;
            ActiveProfile.History.ExecuteAction(new PasteInsertEventInstancesAction(this, ActiveProfile, insertIndex, copiedEvents));

        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
            {
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;
                return;
            }
            {
                undoToolStripMenuItem.Enabled = ActiveProfile.History.CanUndo;
                redoToolStripMenuItem.Enabled = ActiveProfile.History.CanRedo;
                undoToolStripMenuItem.Text = ActiveProfile.History.NextUndoName is not null ? $"Undo {ActiveProfile.History.NextUndoName}" : "Undo";
                redoToolStripMenuItem.Text = ActiveProfile.History.NextRedoName is not null ? $"Redo {ActiveProfile.History.NextRedoName}" : "Redo";
            }
        }

        private bool NoEventFlag { get; set; } = false;
        public bool JoyMapIsFocused { get; private set; }
        public bool GameNotFocused { get; private set; }
        public static MainForm? Instance { get; private set; }

        private void textWindowRegex_TextChanged(object sender, EventArgs e)
        {
            if (NoEventFlag)
                return;
            if (ActiveProfile is null)
            {
                textWindowNameRegex.Enabled = false;
                return;
            }

            var lastUndo = ActiveProfile.History.NextUndoAction;
            if (lastUndo is SetWindowNameRegexAction swra)
            {
                swra.UpdateNewValue(textWindowNameRegex.Text);
            }
            else
            {
                ActiveProfile.History.ExecuteAction(new SetWindowNameRegexAction(this, ActiveProfile, textWindowNameRegex));
            }
        }

        private void textProcessNameRegex_TextChanged(object sender, EventArgs e)
        {
            if (NoEventFlag)
                return;
            if (ActiveProfile is null)
            {
                textProcessNameRegex.Enabled = false;
                return;
            }

            var lastUndo = ActiveProfile.History.NextUndoAction;
            if (lastUndo is SetProcessNameRegexAction swra)
            {
                swra.UpdateNewValue(textProcessNameRegex.Text);
            }
            else
            {
                ActiveProfile.History.ExecuteAction(new SetProcessNameRegexAction(this, ActiveProfile, textProcessNameRegex));
            }
        }


        internal void WithNoEvent(Action update)
        {
            if (ActiveProfile is null)
                return;
            NoEventFlag = true;
            try
            {
                update();
            }
            finally
            {
                NoEventFlag = false;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            ActiveProfile.History.Undo();

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            ActiveProfile.History.Redo();
        }

        private void shouldBeSuppressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuppressIfGameIsNotFocused = !SuppressIfGameIsNotFocused;
        }

        private void GlobalShortcuts(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && e.KeyCode == Keys.Z)
            {
                undoToolStripMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }

            if (e.Control && ((e.KeyCode == Keys.Y) || (e.Shift && e.KeyCode == Keys.Z)))
            {
                redoToolStripMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }


            if (tabControl.SelectedTab == tabXBox)
            {

                if (e.Control && e.KeyCode == Keys.C)
                {
                    tsmCopyBinding_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.V)
                {

                    tsmPasteBinding_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.KeyCode == Keys.Delete)
                {
                    tsmUnbind_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }


                if (e.Control && e.KeyCode == Keys.A)
                {
                    tsmSelectAllBindings_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

            }
            else if (tabControl.SelectedTab == tabEvents)
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    copySelectedToolStripMenuItem_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.V)
                {
                    pasteInsertToolStripMenuItem_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.KeyCode == Keys.Delete)
                {
                    deleteToolStripMenuItem_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.N)
                {
                    newToolStripMenuItem1_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.Up)
                {
                    btnUp_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.Down)
                {
                    btnDown_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }

                if (e.Control && e.KeyCode == Keys.A)
                {
                    selectAllToolStripMenuItem_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedIndexes = eventListView.SelectedItems.ToEnumerable().Select(item => item.Index).ToList();
            if (selectedIndexes.Count == 0)
                return;
            ActiveProfile.History.ExecuteAction(new DeleteEventInstancesAction(this, ActiveProfile, selectedIndexes));

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < eventListView.Items.Count; i++)
                eventListView.SelectedIndices.Add(i);
        }

        private void btnDeleteCurrentProfile_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            if (MessageBox.Show(this, $"Are you sure you want to delete profile '{ActiveProfile.Name}'? This operation cannot be undone!", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var profileId = ActiveProfile.Id;
                Flush();
                Registry.DeleteProfile(profileId);
                RefreshProfileList();
            }

        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void runOnlyWhenGameIsFocusedToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveProfile is null) return;
            SuppressIfGameIsNotFocused = runOnlyWhenGameIsFocusedToolStripMenuItem.Checked;
        }

        private void saveDebugOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (ActiveProfile is null)
                return;
            Registry.Persist(ActiveProfile, true);
#endif
        }

        private void editSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventListView_DoubleClick(this, EventArgs.Empty);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new AboutForm();
            form.ShowDialog(this);
        }

        private void textProfileName_TextChanged(object sender, EventArgs e)
        {
            if (NoEventFlag)
                return;
            if (ActiveProfile is null)
            {
                textProfileName.Enabled = false;
                return;
            }

            var lastUndo = ActiveProfile.History.NextUndoAction;
            if (lastUndo is SetProfileNameAction swra)
            {
                swra.UpdateNewValue(textProfileName.Text);
            }
            else
            {
                ActiveProfile.History.ExecuteAction(new SetProfileNameAction(this, ActiveProfile, textProfileName));
            }
        }

        private void suspendSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedIndexes = eventListView.SelectedIndices;
            if (selectedIndexes.Count == 0)
                return;
            ActiveProfile.History.ExecuteAction(new ToggleSuspendEventInstancesAction(this, ActiveProfile, selectedIndexes.ToArray()));

        }

        private void editControllerFamiliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new ControllerFamiliesForm(InputMonitor);
            form.ShowDialog(this);
        }

        internal static void Log(string status, Exception? ex = null)
        {
            if (Instance is null)
                return;

            if (Instance.InvokeRequired)
            {
                Instance.Invoke(() => Log(status, ex));
                return;
            }

            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            if (ex is not null)
                Instance.toolStripStatusLabel1.Text = $"[{timestamp}] {status}: {ex.Message}";
            else
                Instance.toolStripStatusLabel1.Text = $"[{timestamp}] {status}";
        }

        private void tsmEditBinding_Click(object sender, EventArgs e)
        {

            if (ActiveProfile is null)
                return;
            var axis = AxisOf(bindingListView.SelectedItems[0]);
            ActiveProfile.AxisBindings.TryGetValue(axis, out var binding);
            using var form = new XBoxAxisBindingForm(axis, binding);
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK && form.Result is not null)
            {
                ActiveProfile.History.ExecuteAction(new SetBindingInstanceAction(this, ActiveProfile, form.Result));
            }
        }

        private void tsmCopyBinding_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedEvents = bindingListView.SelectedItems.ToEnumerable()
                .Select(item => item.Tag)
                .OfType<XBoxAxisBindingInstance>()
                .Select(ev => ev.Binding)
                .ToList();
            selectedEvents.CopyToClipboard();

        }

        private void tsmCopyAllBindings_Click(object sender, EventArgs e)
        {
            ActiveProfile?.AxisBindings.Values.Select(ev => ev.Binding).ToList().CopyToClipboard();
        }

        private void bindingContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmCopyBinding.Enabled = tsmUnbind.Enabled = bindingListView.SelectedItems.Count > 0;
            var copied = ClipboardUtil.GetCopied<XBoxAxisBinding>();
            tsmPasteBinding.Enabled = copied is not null;

            tsmSuspendBinding.Enabled = tsmUnbind.Enabled = tsmSelectAllBindings.Enabled = bindingListView.Items.Count > 0;
            tsmEditBinding.Enabled = bindingListView.SelectedItems.Count == 1;

        }

        private void tsmSelectAllBindings_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bindingListView.Items.Count; i++)
                bindingListView.SelectedIndices.Add(i);
        }

        private void tsmPasteBinding_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var copied = ClipboardUtil.GetCopied<XBoxAxisBinding>();
            if (copied is null)
                return;

            ActiveProfile.History.ExecuteAction(new PasteXBoxBindingsAction(
                this,
                ActiveProfile,
                copied));
        }

        private IReadOnlyList<XBoxAxis> GetSelectedAxes()
        {
            return bindingListView.SelectedItems.ToEnumerable().Select(AxisOf).ToList();
        }

        private void tsmUnbind_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedIndexes = GetSelectedAxes();
            if (selectedIndexes.Count == 0)
                return;
            ActiveProfile.History.ExecuteAction(new UnbindXBoxBindingAction(this, ActiveProfile, selectedIndexes));
        }

        private void tsmSuspendBinding_Click(object sender, EventArgs e)
        {
            if (ActiveProfile is null)
                return;
            var selectedIndexes = bindingListView.SelectedIndices;
            if (selectedIndexes.Count == 0)
                return;
            ActiveProfile.History.ExecuteAction(new ToggleSuspendXBoxBindingInstancesAction(this, ActiveProfile, selectedIndexes.ToArray()));
        }

        public static XBoxAxis AxisOf(ListViewItem item)
        {
            var tag = item.Tag;
            if (tag is XBoxAxis axis)
                return axis;
            if (tag is XBoxAxisBindingInstance axisBindingInstance)
                return axisBindingInstance.Binding.OutAxis;
            throw new InvalidOperationException($"Row has neither axis nor binding as tag");
        }

        internal static void AxisUpdateItemTo(ListViewItem item, XBoxAxis outAxis, XBoxAxisBindingInstance? b)
        {
            if (b is null)
            {
                item.Tag = outAxis;
                item.SubItems[1].Text = "";
                item.SubItems[2].Text = "";
            }
            else
            {
                item.SubItems[1].Text = string.Join(", ", b.InputInstances.Select(x => x.Input.InputId.ControllerAxisName));
                item.SubItems[2].Text = b.GetValue().ToStr();
                item.Tag = b;
            }
        }

        private void bindingListView_DoubleClick(object sender, EventArgs e)
        {
            tsmEditBinding_Click(sender, e);
        }

        private void textNotes_TextChanged(object sender, EventArgs e)
        {
            if (NoEventFlag)
                return;
            if (ActiveProfile is null)
            {
                textNotes.Enabled = false;
                return;
            }

            var lastUndo = ActiveProfile.History.NextUndoAction;
            if (lastUndo is SetProfileNotesAction swra)
            {
                swra.UpdateNewValue(textNotes.Text);
            }
            else
            {
                ActiveProfile.History.ExecuteAction(new SetProfileNotesAction(this, ActiveProfile, textNotes));
            }
        }
    }
}

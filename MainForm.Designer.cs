namespace JoyMap
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveDebugOnlyToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripSeparator();
            editControllerFamiliesToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            profilesToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            newEmptyToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            runOnlyWhenGameIsFocusedToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            cbProfile = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            textProfileName = new TextBox();
            label3 = new Label();
            textProcessNameRegex = new TextBox();
            btnAddPickWindow = new Button();
            eventListView = new ListView();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            eventContextMenu = new ContextMenuStrip(components);
            newToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            moveSelectedDownToolStripMenuItem = new ToolStripMenuItem();
            moveSelectedUpToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            editSelectedToolStripMenuItem = new ToolStripMenuItem();
            suspendSelectedToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripSeparator();
            copySelectedToolStripMenuItem = new ToolStripMenuItem();
            copyAllToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            pasteOverToolStripMenuItem = new ToolStripMenuItem();
            pasteInsertToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            label4 = new Label();
            btnUp = new Button();
            btnDown = new Button();
            notifyIcon1 = new NotifyIcon(components);
            statusTimer = new System.Windows.Forms.Timer(components);
            btnDeleteCurrentProfile = new Button();
            textWindowNameRegex = new TextBox();
            label5 = new Label();
            mainMenu.SuspendLayout();
            eventContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(24, 24);
            mainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, profilesToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(1223, 33);
            mainMenu.TabIndex = 2;
            mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveDebugOnlyToolStripMenuItem, toolStripMenuItem6, editControllerFamiliesToolStripMenuItem, toolStripMenuItem8, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveDebugOnlyToolStripMenuItem
            // 
            saveDebugOnlyToolStripMenuItem.Name = "saveDebugOnlyToolStripMenuItem";
            saveDebugOnlyToolStripMenuItem.Size = new Size(311, 34);
            saveDebugOnlyToolStripMenuItem.Text = "Save (Debug Only)";
            saveDebugOnlyToolStripMenuItem.Click += saveDebugOnlyToolStripMenuItem_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(308, 6);
            // 
            // editControllerFamiliesToolStripMenuItem
            // 
            editControllerFamiliesToolStripMenuItem.Name = "editControllerFamiliesToolStripMenuItem";
            editControllerFamiliesToolStripMenuItem.Size = new Size(311, 34);
            editControllerFamiliesToolStripMenuItem.Text = "Edit Controller Families ...";
            editControllerFamiliesToolStripMenuItem.Click += editControllerFamiliesToolStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(308, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(311, 34);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // profilesToolStripMenuItem
            // 
            profilesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, newEmptyToolStripMenuItem, toolStripMenuItem5, runOnlyWhenGameIsFocusedToolStripMenuItem });
            profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            profilesToolStripMenuItem.Size = new Size(86, 29);
            profilesToolStripMenuItem.Text = "Profiles";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(373, 34);
            newToolStripMenuItem.Text = "New from Window ...";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // newEmptyToolStripMenuItem
            // 
            newEmptyToolStripMenuItem.Name = "newEmptyToolStripMenuItem";
            newEmptyToolStripMenuItem.Size = new Size(373, 34);
            newEmptyToolStripMenuItem.Text = "New Empty";
            newEmptyToolStripMenuItem.Click += newEmptyToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(370, 6);
            // 
            // runOnlyWhenGameIsFocusedToolStripMenuItem
            // 
            runOnlyWhenGameIsFocusedToolStripMenuItem.Checked = true;
            runOnlyWhenGameIsFocusedToolStripMenuItem.CheckOnClick = true;
            runOnlyWhenGameIsFocusedToolStripMenuItem.CheckState = CheckState.Checked;
            runOnlyWhenGameIsFocusedToolStripMenuItem.Name = "runOnlyWhenGameIsFocusedToolStripMenuItem";
            runOnlyWhenGameIsFocusedToolStripMenuItem.Size = new Size(373, 34);
            runOnlyWhenGameIsFocusedToolStripMenuItem.Text = "Run Only when Game is Focused";
            runOnlyWhenGameIsFocusedToolStripMenuItem.CheckedChanged += runOnlyWhenGameIsFocusedToolStripMenuItem_CheckedChanged;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(58, 29);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.DropDownOpening += editToolStripMenuItem_DropDownOpening;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(270, 34);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(270, 34);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(65, 29);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(181, 34);
            aboutToolStripMenuItem.Text = "About ...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // cbProfile
            // 
            cbProfile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProfile.FormattingEnabled = true;
            cbProfile.Location = new Point(84, 36);
            cbProfile.Name = "cbProfile";
            cbProfile.Size = new Size(1127, 33);
            cbProfile.TabIndex = 3;
            cbProfile.SelectedIndexChanged += cbProfile_SelectedIndexChanged;
            cbProfile.KeyDown += GlobalShortcuts;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 4;
            label1.Text = "Profile:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 5;
            label2.Text = "Name:";
            // 
            // textProfileName
            // 
            textProfileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textProfileName.Enabled = false;
            textProfileName.Location = new Point(81, 92);
            textProfileName.Name = "textProfileName";
            textProfileName.Size = new Size(1012, 31);
            textProfileName.TabIndex = 6;
            textProfileName.TextChanged += textProfileName_TextChanged;
            textProfileName.KeyDown += GlobalShortcuts;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 131);
            label3.Name = "label3";
            label3.Size = new Size(173, 25);
            label3.TabIndex = 7;
            label3.Text = "Process name regex:";
            // 
            // textProcessNameRegex
            // 
            textProcessNameRegex.Enabled = false;
            textProcessNameRegex.Location = new Point(191, 128);
            textProcessNameRegex.Name = "textProcessNameRegex";
            textProcessNameRegex.Size = new Size(377, 31);
            textProcessNameRegex.TabIndex = 8;
            textProcessNameRegex.TextChanged += textProcessNameRegex_TextChanged;
            textProcessNameRegex.KeyDown += GlobalShortcuts;
            // 
            // btnAddPickWindow
            // 
            btnAddPickWindow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPickWindow.Enabled = false;
            btnAddPickWindow.Location = new Point(1099, 126);
            btnAddPickWindow.Name = "btnAddPickWindow";
            btnAddPickWindow.Size = new Size(112, 34);
            btnAddPickWindow.TabIndex = 9;
            btnAddPickWindow.Text = "Pick ...";
            btnAddPickWindow.UseVisualStyleBackColor = true;
            btnAddPickWindow.Click += btnAddPickWindow_Click;
            btnAddPickWindow.KeyDown += GlobalShortcuts;
            // 
            // eventListView
            // 
            eventListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eventListView.Columns.AddRange(new ColumnHeader[] { columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            eventListView.FullRowSelect = true;
            eventListView.Location = new Point(12, 212);
            eventListView.Name = "eventListView";
            eventListView.Size = new Size(1123, 613);
            eventListView.TabIndex = 10;
            eventListView.UseCompatibleStateImageBehavior = false;
            eventListView.View = View.Details;
            eventListView.SelectedIndexChanged += eventListView_SelectedIndexChanged;
            eventListView.DoubleClick += eventListView_DoubleClick;
            eventListView.KeyDown += GlobalShortcuts;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Trigger(s)";
            columnHeader3.Width = 300;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Action(s)";
            columnHeader4.Width = 300;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Active";
            columnHeader5.Width = 110;
            // 
            // eventContextMenu
            // 
            eventContextMenu.ImageScalingSize = new Size(24, 24);
            eventContextMenu.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem1, toolStripMenuItem4, selectAllToolStripMenuItem, moveSelectedDownToolStripMenuItem, moveSelectedUpToolStripMenuItem, toolStripMenuItem2, editSelectedToolStripMenuItem, suspendSelectedToolStripMenuItem, toolStripMenuItem7, copySelectedToolStripMenuItem, copyAllToolStripMenuItem, toolStripMenuItem1, pasteOverToolStripMenuItem, pasteInsertToolStripMenuItem, toolStripMenuItem3, deleteToolStripMenuItem });
            eventContextMenu.Name = "eventContextMenu";
            eventContextMenu.Size = new Size(351, 386);
            eventContextMenu.Opening += eventContextMenu_Opening;
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new Size(350, 32);
            newToolStripMenuItem1.Text = "New ...";
            newToolStripMenuItem1.Click += newToolStripMenuItem1_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(347, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            selectAllToolStripMenuItem.Size = new Size(350, 32);
            selectAllToolStripMenuItem.Text = "Select All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // moveSelectedDownToolStripMenuItem
            // 
            moveSelectedDownToolStripMenuItem.Name = "moveSelectedDownToolStripMenuItem";
            moveSelectedDownToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Down;
            moveSelectedDownToolStripMenuItem.Size = new Size(350, 32);
            moveSelectedDownToolStripMenuItem.Text = "Move Selected Down";
            moveSelectedDownToolStripMenuItem.Click += btnDown_Click;
            // 
            // moveSelectedUpToolStripMenuItem
            // 
            moveSelectedUpToolStripMenuItem.Name = "moveSelectedUpToolStripMenuItem";
            moveSelectedUpToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Up;
            moveSelectedUpToolStripMenuItem.Size = new Size(350, 32);
            moveSelectedUpToolStripMenuItem.Text = "Move Selected Up";
            moveSelectedUpToolStripMenuItem.Click += btnUp_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(347, 6);
            // 
            // editSelectedToolStripMenuItem
            // 
            editSelectedToolStripMenuItem.Name = "editSelectedToolStripMenuItem";
            editSelectedToolStripMenuItem.Size = new Size(350, 32);
            editSelectedToolStripMenuItem.Text = "Edit Selected (double click) ...";
            editSelectedToolStripMenuItem.Click += editSelectedToolStripMenuItem_Click;
            // 
            // suspendSelectedToolStripMenuItem
            // 
            suspendSelectedToolStripMenuItem.Name = "suspendSelectedToolStripMenuItem";
            suspendSelectedToolStripMenuItem.Size = new Size(350, 32);
            suspendSelectedToolStripMenuItem.Text = "(Un)Suspend Selected";
            suspendSelectedToolStripMenuItem.Click += suspendSelectedToolStripMenuItem_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(347, 6);
            // 
            // copySelectedToolStripMenuItem
            // 
            copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            copySelectedToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copySelectedToolStripMenuItem.Size = new Size(350, 32);
            copySelectedToolStripMenuItem.Text = "Copy Selected";
            copySelectedToolStripMenuItem.Click += copySelectedToolStripMenuItem_Click;
            // 
            // copyAllToolStripMenuItem
            // 
            copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            copyAllToolStripMenuItem.Size = new Size(350, 32);
            copyAllToolStripMenuItem.Text = "Copy All";
            copyAllToolStripMenuItem.Click += copyAllToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(347, 6);
            // 
            // pasteOverToolStripMenuItem
            // 
            pasteOverToolStripMenuItem.Name = "pasteOverToolStripMenuItem";
            pasteOverToolStripMenuItem.Size = new Size(350, 32);
            pasteOverToolStripMenuItem.Text = "Paste Over";
            pasteOverToolStripMenuItem.Click += pasteOverToolStripMenuItem_Click;
            // 
            // pasteInsertToolStripMenuItem
            // 
            pasteInsertToolStripMenuItem.Name = "pasteInsertToolStripMenuItem";
            pasteInsertToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteInsertToolStripMenuItem.Size = new Size(350, 32);
            pasteInsertToolStripMenuItem.Text = "Paste Insert";
            pasteInsertToolStripMenuItem.Click += pasteInsertToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(347, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteToolStripMenuItem.Size = new Size(350, 32);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 184);
            label4.Name = "label4";
            label4.Size = new Size(67, 25);
            label4.TabIndex = 11;
            label4.Text = "Events:";
            // 
            // btnUp
            // 
            btnUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUp.Enabled = false;
            btnUp.Font = new Font("Segoe UI", 30F);
            btnUp.Location = new Point(1141, 212);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(70, 264);
            btnUp.TabIndex = 12;
            btnUp.Text = "↑";
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += btnUp_Click;
            btnUp.KeyDown += GlobalShortcuts;
            // 
            // btnDown
            // 
            btnDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDown.Enabled = false;
            btnDown.Font = new Font("Segoe UI", 30F);
            btnDown.Location = new Point(1141, 561);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(70, 264);
            btnDown.TabIndex = 13;
            btnDown.Text = "↓";
            btnDown.UseVisualStyleBackColor = true;
            btnDown.Click += btnDown_Click;
            btnDown.KeyDown += GlobalShortcuts;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // statusTimer
            // 
            statusTimer.Enabled = true;
            statusTimer.Interval = 200;
            statusTimer.Tick += statusTimer_Tick;
            // 
            // btnDeleteCurrentProfile
            // 
            btnDeleteCurrentProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteCurrentProfile.Enabled = false;
            btnDeleteCurrentProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteCurrentProfile.Location = new Point(1099, 92);
            btnDeleteCurrentProfile.Name = "btnDeleteCurrentProfile";
            btnDeleteCurrentProfile.Size = new Size(112, 34);
            btnDeleteCurrentProfile.TabIndex = 14;
            btnDeleteCurrentProfile.Text = "Delete";
            btnDeleteCurrentProfile.UseVisualStyleBackColor = true;
            btnDeleteCurrentProfile.Click += btnDeleteCurrentProfile_Click;
            // 
            // textWindowNameRegex
            // 
            textWindowNameRegex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textWindowNameRegex.Enabled = false;
            textWindowNameRegex.Location = new Point(753, 128);
            textWindowNameRegex.Name = "textWindowNameRegex";
            textWindowNameRegex.Size = new Size(340, 31);
            textWindowNameRegex.TabIndex = 16;
            textWindowNameRegex.TextChanged += textWindowRegex_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(574, 131);
            label5.Name = "label5";
            label5.Size = new Size(179, 25);
            label5.TabIndex = 15;
            label5.Text = "Window name regex:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 837);
            Controls.Add(textWindowNameRegex);
            Controls.Add(label5);
            Controls.Add(btnDeleteCurrentProfile);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(label4);
            Controls.Add(eventListView);
            Controls.Add(btnAddPickWindow);
            Controls.Add(textProcessNameRegex);
            Controls.Add(label3);
            Controls.Add(textProfileName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbProfile);
            Controls.Add(mainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenu;
            MinimumSize = new Size(1245, 893);
            Name = "MainForm";
            Text = "JoyMap";
            KeyDown += GlobalShortcuts;
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            eventContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem profilesToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ComboBox cbProfile;
        private Label label1;
        private Label label2;
        private TextBox textProfileName;
        private Label label3;
        private TextBox textProcessNameRegex;
        private Button btnAddPickWindow;
        private ListView eventListView;
        private Label label4;
        private ContextMenuStrip eventContextMenu;
        private ToolStripMenuItem newToolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Button btnUp;
        private Button btnDown;
        private NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer statusTimer;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem copySelectedToolStripMenuItem;
        private ToolStripMenuItem copyAllToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem pasteOverToolStripMenuItem;
        private ToolStripMenuItem pasteInsertToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem moveSelectedDownToolStripMenuItem;
        private ToolStripMenuItem moveSelectedUpToolStripMenuItem;
        private Button btnDeleteCurrentProfile;
        private TextBox textWindowNameRegex;
        private Label label5;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem runOnlyWhenGameIsFocusedToolStripMenuItem;
        private ToolStripMenuItem saveDebugOnlyToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem6;
        private ToolStripMenuItem editSelectedToolStripMenuItem;
        private ToolStripMenuItem newEmptyToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem suspendSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem7;
        private ToolStripMenuItem editControllerFamiliesToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem8;
    }
}

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
            mainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            profilesToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            cbProfile = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            textProfileName = new TextBox();
            label3 = new Label();
            textWindowRegex = new TextBox();
            btnAddPickWindow = new Button();
            eventListView = new ListView();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            eventContextMenu = new ContextMenuStrip(components);
            newToolStripMenuItem1 = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            label4 = new Label();
            btnUp = new Button();
            btnDown = new Button();
            notifyIcon1 = new NotifyIcon(components);
            statusTimer = new System.Windows.Forms.Timer(components);
            mainMenu.SuspendLayout();
            eventContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(24, 24);
            mainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, profilesToolStripMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(1223, 33);
            mainMenu.TabIndex = 2;
            mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(148, 34);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // profilesToolStripMenuItem
            // 
            profilesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem });
            profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            profilesToolStripMenuItem.Size = new Size(86, 29);
            profilesToolStripMenuItem.Text = "Profiles";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(166, 34);
            newToolStripMenuItem.Text = "New ...";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
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
            label2.Location = new Point(12, 128);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 5;
            label2.Text = "Name:";
            // 
            // textProfileName
            // 
            textProfileName.Enabled = false;
            textProfileName.Location = new Point(81, 125);
            textProfileName.Name = "textProfileName";
            textProfileName.Size = new Size(390, 31);
            textProfileName.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(489, 128);
            label3.Name = "label3";
            label3.Size = new Size(179, 25);
            label3.TabIndex = 7;
            label3.Text = "Window name regex:";
            // 
            // textWindowRegex
            // 
            textWindowRegex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textWindowRegex.Enabled = false;
            textWindowRegex.Location = new Point(674, 125);
            textWindowRegex.Name = "textWindowRegex";
            textWindowRegex.Size = new Size(419, 31);
            textWindowRegex.TabIndex = 8;
            // 
            // btnAddPickWindow
            // 
            btnAddPickWindow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPickWindow.Enabled = false;
            btnAddPickWindow.Location = new Point(1099, 122);
            btnAddPickWindow.Name = "btnAddPickWindow";
            btnAddPickWindow.Size = new Size(112, 34);
            btnAddPickWindow.TabIndex = 9;
            btnAddPickWindow.Text = "Pick...";
            btnAddPickWindow.UseVisualStyleBackColor = true;
            btnAddPickWindow.Click += btnAddPickWindow_Click;
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
            // 
            // eventContextMenu
            // 
            eventContextMenu.ImageScalingSize = new Size(24, 24);
            eventContextMenu.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem1, deleteToolStripMenuItem });
            eventContextMenu.Name = "eventContextMenu";
            eventContextMenu.Size = new Size(137, 68);
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new Size(136, 32);
            newToolStripMenuItem1.Text = "New ...";
            newToolStripMenuItem1.Click += newToolStripMenuItem1_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(136, 32);
            deleteToolStripMenuItem.Text = "Delete";
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 837);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(label4);
            Controls.Add(eventListView);
            Controls.Add(btnAddPickWindow);
            Controls.Add(textWindowRegex);
            Controls.Add(label3);
            Controls.Add(textProfileName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbProfile);
            Controls.Add(mainMenu);
            MainMenuStrip = mainMenu;
            MinimumSize = new Size(1245, 893);
            Name = "MainForm";
            Text = "JoyMap";
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
        private TextBox textWindowRegex;
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
    }
}

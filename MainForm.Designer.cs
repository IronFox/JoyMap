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
            selectedProfile = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            profileNameInput = new TextBox();
            label3 = new Label();
            windowRegexInput = new TextBox();
            addPickWindowName = new Button();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            eventContextMenu = new ContextMenuStrip(components);
            newToolStripMenuItem1 = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            label4 = new Label();
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
            // 
            // selectedProfile
            // 
            selectedProfile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selectedProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            selectedProfile.FormattingEnabled = true;
            selectedProfile.Location = new Point(84, 36);
            selectedProfile.Name = "selectedProfile";
            selectedProfile.Size = new Size(1127, 33);
            selectedProfile.TabIndex = 3;
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
            // profileNameInput
            // 
            profileNameInput.Location = new Point(81, 125);
            profileNameInput.Name = "profileNameInput";
            profileNameInput.Size = new Size(390, 31);
            profileNameInput.TabIndex = 6;
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
            // windowRegexInput
            // 
            windowRegexInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            windowRegexInput.Location = new Point(674, 125);
            windowRegexInput.Name = "windowRegexInput";
            windowRegexInput.Size = new Size(419, 31);
            windowRegexInput.TabIndex = 8;
            // 
            // addPickWindowName
            // 
            addPickWindowName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addPickWindowName.Location = new Point(1099, 122);
            addPickWindowName.Name = "addPickWindowName";
            addPickWindowName.Size = new Size(112, 34);
            addPickWindowName.TabIndex = 9;
            addPickWindowName.Text = "Pick...";
            addPickWindowName.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listView1.ContextMenuStrip = eventContextMenu;
            listView1.Location = new Point(12, 212);
            listView1.Name = "listView1";
            listView1.Size = new Size(1199, 613);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Enabled";
            columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Source(s)";
            columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Active";
            // 
            // eventContextMenu
            // 
            eventContextMenu.ImageScalingSize = new Size(24, 24);
            eventContextMenu.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem1, deleteToolStripMenuItem });
            eventContextMenu.Name = "eventContextMenu";
            eventContextMenu.Size = new Size(241, 101);
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new Size(240, 32);
            newToolStripMenuItem1.Text = "New ...";
            newToolStripMenuItem1.Click += newToolStripMenuItem1_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(240, 32);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 837);
            Controls.Add(label4);
            Controls.Add(listView1);
            Controls.Add(addPickWindowName);
            Controls.Add(windowRegexInput);
            Controls.Add(label3);
            Controls.Add(profileNameInput);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(selectedProfile);
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
        private ComboBox selectedProfile;
        private Label label1;
        private Label label2;
        private TextBox profileNameInput;
        private Label label3;
        private TextBox windowRegexInput;
        private Button addPickWindowName;
        private ListView listView1;
        private Label label4;
        private ContextMenuStrip eventContextMenu;
        private ToolStripMenuItem newToolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
    }
}

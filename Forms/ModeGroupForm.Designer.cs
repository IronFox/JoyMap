namespace JoyMap.Forms
{
    partial class ModeGroupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lId = new Label();
            label1 = new Label();
            textName = new TextBox();
            label2 = new Label();
            modeListView = new ListView();
            columnHeaderId = new ColumnHeader();
            columnHeaderName = new ColumnHeader();
            columnHeaderTriggers = new ColumnHeader();
            modeContextMenu = new ContextMenuStrip(components);
            mgEntryNewMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mgEntryEditMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            mgEntryDeleteMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            mgSetDefaultMenuItem = new ToolStripMenuItem();
            label3 = new Label();
            cbDefaultMode = new ComboBox();
            btnOk = new Button();
            btnCancel = new Button();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            modeContextMenu.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lId
            // 
            lId.AutoSize = true;
            lId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lId.Location = new Point(12, 12);
            lId.Name = "lId";
            lId.Size = new Size(64, 25);
            lId.TabIndex = 0;
            lId.Text = "ID: MG0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 1;
            label1.Text = "Name:";
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(85, 47);
            textName.Name = "textName";
            textName.Size = new Size(827, 31);
            textName.TabIndex = 2;
            textName.TextChanged += AnyChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 92);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 3;
            label2.Text = "Modes:";
            // 
            // modeListView
            // 
            modeListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            modeListView.Columns.AddRange(new ColumnHeader[] { columnHeaderId, columnHeaderName, columnHeaderTriggers });
            modeListView.ContextMenuStrip = modeContextMenu;
            modeListView.FullRowSelect = true;
            modeListView.Location = new Point(6, 120);
            modeListView.Name = "modeListView";
            modeListView.Size = new Size(912, 455);
            modeListView.TabIndex = 4;
            modeListView.UseCompatibleStateImageBehavior = false;
            modeListView.View = View.Details;
            modeListView.DoubleClick += modeListView_DoubleClick;
            modeListView.SelectedIndexChanged += modeListView_SelectedIndexChanged;
            // 
            // columnHeaderId
            // 
            columnHeaderId.Text = "ID";
            columnHeaderId.Width = 80;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 300;
            // 
            // columnHeaderTriggers
            // 
            columnHeaderTriggers.Text = "Triggers";
            columnHeaderTriggers.Width = 450;
            // 
            // modeContextMenu
            // 
            modeContextMenu.ImageScalingSize = new Size(24, 24);
            modeContextMenu.Items.AddRange(new ToolStripItem[] { mgEntryNewMenuItem, toolStripSeparator1, mgEntryEditMenuItem, toolStripSeparator2, mgEntryDeleteMenuItem, toolStripSeparator3, mgSetDefaultMenuItem });
            modeContextMenu.Name = "modeContextMenu";
            modeContextMenu.Size = new Size(280, 142);
            modeContextMenu.Opening += modeContextMenu_Opening;
            // 
            // mgEntryNewMenuItem
            // 
            mgEntryNewMenuItem.Name = "mgEntryNewMenuItem";
            mgEntryNewMenuItem.Size = new Size(279, 32);
            mgEntryNewMenuItem.Text = "New Entry ...";
            mgEntryNewMenuItem.Click += mgEntryNewMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(276, 6);
            // 
            // mgEntryEditMenuItem
            // 
            mgEntryEditMenuItem.Name = "mgEntryEditMenuItem";
            mgEntryEditMenuItem.Size = new Size(279, 32);
            mgEntryEditMenuItem.Text = "Edit Entry (double click) ...";
            mgEntryEditMenuItem.Click += mgEntryEditMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(276, 6);
            // 
            // mgEntryDeleteMenuItem
            // 
            mgEntryDeleteMenuItem.Name = "mgEntryDeleteMenuItem";
            mgEntryDeleteMenuItem.ShortcutKeys = Keys.Delete;
            mgEntryDeleteMenuItem.Size = new Size(279, 32);
            mgEntryDeleteMenuItem.Text = "Delete";
            mgEntryDeleteMenuItem.Click += mgEntryDeleteMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(276, 6);
            // 
            // mgSetDefaultMenuItem
            // 
            mgSetDefaultMenuItem.Name = "mgSetDefaultMenuItem";
            mgSetDefaultMenuItem.Size = new Size(279, 32);
            mgSetDefaultMenuItem.Text = "Set as Default";
            mgSetDefaultMenuItem.Click += mgSetDefaultMenuItem_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 588);
            label3.Name = "label3";
            label3.Size = new Size(116, 25);
            label3.TabIndex = 5;
            label3.Text = "Default Mode:";
            // 
            // cbDefaultMode
            // 
            cbDefaultMode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbDefaultMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDefaultMode.FormattingEnabled = true;
            cbDefaultMode.Location = new Point(140, 585);
            cbDefaultMode.Name = "cbDefaultMode";
            cbDefaultMode.Size = new Size(350, 33);
            cbDefaultMode.TabIndex = 6;
            cbDefaultMode.SelectedIndexChanged += AnyChanged;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(342, 616);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 7;
            btnOk.Text = "Update / Create";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(630, 616);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 658);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(924, 22);
            statusStrip.TabIndex = 9;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            // 
            // ModeGroupForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(924, 680);
            Controls.Add(lId);
            Controls.Add(label1);
            Controls.Add(textName);
            Controls.Add(label2);
            Controls.Add(modeListView);
            Controls.Add(label3);
            Controls.Add(cbDefaultMode);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(statusStrip);
            MinimumSize = new Size(750, 500);
            Name = "ModeGroupForm";
            Text = "Mode Group";
            modeContextMenu.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lId;
        private Label label1;
        private TextBox textName;
        private Label label2;
        private ListView modeListView;
        private ColumnHeader columnHeaderId;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderTriggers;
        private ContextMenuStrip modeContextMenu;
        private ToolStripMenuItem mgEntryNewMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mgEntryEditMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mgEntryDeleteMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mgSetDefaultMenuItem;
        private Label label3;
        private ComboBox cbDefaultMode;
        private Button btnOk;
        private Button btnCancel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
    }
}

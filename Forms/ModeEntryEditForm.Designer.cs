namespace JoyMap.Forms
{
    partial class ModeEntryEditForm
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
            triggerCombiner = new ComboBox();
            btnCombinerHelp = new Button();
            label3 = new Label();
            triggerListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            triggerMenu = new ContextMenuStrip(components);
            pickAddToolStripMenuItem = new ToolStripMenuItem();
            addKeyTriggerToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editSelectedToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            btnOk = new Button();
            btnCancel = new Button();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusUpdateTimer = new System.Windows.Forms.Timer(components);
            triggerMenu.SuspendLayout();
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
            lId.Text = "ID: M0";
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
            label2.Size = new Size(94, 25);
            label2.TabIndex = 3;
            label2.Text = "Combiner:";
            // 
            // triggerCombiner
            // 
            triggerCombiner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            triggerCombiner.FormattingEnabled = true;
            triggerCombiner.Location = new Point(115, 89);
            triggerCombiner.Name = "triggerCombiner";
            triggerCombiner.Size = new Size(751, 33);
            triggerCombiner.TabIndex = 4;
            triggerCombiner.TextChanged += AnyChanged;
            // 
            // btnCombinerHelp
            // 
            btnCombinerHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCombinerHelp.Location = new Point(874, 89);
            btnCombinerHelp.Name = "btnCombinerHelp";
            btnCombinerHelp.Size = new Size(38, 33);
            btnCombinerHelp.TabIndex = 5;
            btnCombinerHelp.Text = "?";
            btnCombinerHelp.UseVisualStyleBackColor = true;
            btnCombinerHelp.Click += btnCombinerHelp_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 136);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 6;
            label3.Text = "Triggers:";
            // 
            // triggerListView
            // 
            triggerListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            triggerListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            triggerListView.ContextMenuStrip = triggerMenu;
            triggerListView.FullRowSelect = true;
            triggerListView.Location = new Point(6, 162);
            triggerListView.Name = "triggerListView";
            triggerListView.Size = new Size(912, 416);
            triggerListView.TabIndex = 7;
            triggerListView.UseCompatibleStateImageBehavior = false;
            triggerListView.View = View.Details;
            triggerListView.DoubleClick += triggerListView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Label";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Device";
            columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Input";
            columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Status";
            columnHeader4.Width = 100;
            // 
            // triggerMenu
            // 
            triggerMenu.ImageScalingSize = new Size(24, 24);
            triggerMenu.Items.AddRange(new ToolStripItem[] { pickAddToolStripMenuItem, addKeyTriggerToolStripMenuItem, toolStripSeparator1, editSelectedToolStripMenuItem, toolStripSeparator2, deleteToolStripMenuItem });
            triggerMenu.Name = "triggerMenu";
            triggerMenu.Size = new Size(312, 142);
            triggerMenu.Opening += triggerMenu_Opening;
            // 
            // pickAddToolStripMenuItem
            // 
            pickAddToolStripMenuItem.Name = "pickAddToolStripMenuItem";
            pickAddToolStripMenuItem.Size = new Size(311, 32);
            pickAddToolStripMenuItem.Text = "Pick/Add ...";
            pickAddToolStripMenuItem.Click += pickAddToolStripMenuItem_Click;
            // 
            // addKeyTriggerToolStripMenuItem
            // 
            addKeyTriggerToolStripMenuItem.Name = "addKeyTriggerToolStripMenuItem";
            addKeyTriggerToolStripMenuItem.Size = new Size(311, 32);
            addKeyTriggerToolStripMenuItem.Text = "Add Key Trigger ...";
            addKeyTriggerToolStripMenuItem.Click += addKeyTriggerToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(308, 6);
            // 
            // editSelectedToolStripMenuItem
            // 
            editSelectedToolStripMenuItem.Name = "editSelectedToolStripMenuItem";
            editSelectedToolStripMenuItem.Size = new Size(311, 32);
            editSelectedToolStripMenuItem.Text = "Edit Selected (double click) ...";
            editSelectedToolStripMenuItem.Click += editSelectedToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(308, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(311, 32);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(342, 616);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 8;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(630, 616);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 9;
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
            statusStrip.TabIndex = 10;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            // 
            // statusUpdateTimer
            // 
            statusUpdateTimer.Enabled = true;
            statusUpdateTimer.Interval = 200;
            statusUpdateTimer.Tick += statusUpdateTimer_Tick;
            // 
            // ModeEntryEditForm
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
            Controls.Add(triggerCombiner);
            Controls.Add(btnCombinerHelp);
            Controls.Add(label3);
            Controls.Add(triggerListView);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(statusStrip);
            MinimumSize = new Size(750, 400);
            Name = "ModeEntryEditForm";
            Text = "Mode Entry";
            triggerMenu.ResumeLayout(false);
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
        private ComboBox triggerCombiner;
        private Button btnCombinerHelp;
        private Label label3;
        private ListView triggerListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ContextMenuStrip triggerMenu;
        private ToolStripMenuItem pickAddToolStripMenuItem;
        private ToolStripMenuItem addKeyTriggerToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem editSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Button btnOk;
        private Button btnCancel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer statusUpdateTimer;
    }
}

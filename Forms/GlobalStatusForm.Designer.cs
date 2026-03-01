namespace JoyMap.Forms
{
    partial class GlobalStatusForm
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
            cbMode = new ComboBox();
            panelCombiner = new Panel();
            label3 = new Label();
            triggerCombiner = new ComboBox();
            btnCombinerHelp = new Button();
            labelCombinerError = new Label();
            label4 = new Label();
            triggerListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            triggerMenu = new ContextMenuStrip(components);
            pickAddToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editSelectedToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            btnOk = new Button();
            btnCancel = new Button();
            statusUpdateTimer = new System.Windows.Forms.Timer(components);
            panelCombiner.SuspendLayout();
            triggerMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lId
            // 
            lId.AutoSize = true;
            lId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lId.Location = new Point(12, 12);
            lId.Name = "lId";
            lId.Size = new Size(60, 25);
            lId.TabIndex = 0;
            lId.Text = "ID: G0";
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
            label2.Size = new Size(60, 25);
            label2.TabIndex = 3;
            label2.Text = "Mode:";
            // 
            // cbMode
            // 
            cbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMode.FormattingEnabled = true;
            cbMode.Location = new Point(85, 89);
            cbMode.Name = "cbMode";
            cbMode.Size = new Size(350, 33);
            cbMode.TabIndex = 4;
            cbMode.SelectedIndexChanged += cbMode_SelectedIndexChanged;
            // 
            // panelCombiner
            // 
            panelCombiner.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelCombiner.Controls.Add(label3);
            panelCombiner.Controls.Add(triggerCombiner);
            panelCombiner.Controls.Add(btnCombinerHelp);
            panelCombiner.Controls.Add(labelCombinerError);
            panelCombiner.Controls.Add(label4);
            panelCombiner.Controls.Add(triggerListView);
            panelCombiner.Location = new Point(0, 135);
            panelCombiner.Name = "panelCombiner";
            panelCombiner.Size = new Size(924, 490);
            panelCombiner.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 12);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 0;
            label3.Text = "Combiner:";
            // 
            // triggerCombiner
            // 
            triggerCombiner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            triggerCombiner.FormattingEnabled = true;
            triggerCombiner.Location = new Point(115, 9);
            triggerCombiner.Name = "triggerCombiner";
            triggerCombiner.Size = new Size(751, 33);
            triggerCombiner.TabIndex = 1;
            triggerCombiner.TextChanged += AnyChanged;
            // 
            // btnCombinerHelp
            // 
            btnCombinerHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCombinerHelp.Location = new Point(874, 9);
            btnCombinerHelp.Name = "btnCombinerHelp";
            btnCombinerHelp.Size = new Size(38, 33);
            btnCombinerHelp.TabIndex = 5;
            btnCombinerHelp.Text = "?";
            btnCombinerHelp.UseVisualStyleBackColor = true;
            btnCombinerHelp.Click += btnCombinerHelp_Click;
            // 
            // labelCombinerError
            // 
            labelCombinerError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelCombinerError.ForeColor = Color.Firebrick;
            labelCombinerError.Location = new Point(115, 46);
            labelCombinerError.Name = "labelCombinerError";
            labelCombinerError.Size = new Size(797, 22);
            labelCombinerError.TabIndex = 6;
            labelCombinerError.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 74);
            label4.Name = "label4";
            label4.Size = new Size(74, 25);
            label4.TabIndex = 2;
            label4.Text = "Triggers";
            // 
            // triggerListView
            // 
            triggerListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            triggerListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            triggerListView.ContextMenuStrip = triggerMenu;
            triggerListView.FullRowSelect = true;
            triggerListView.Location = new Point(12, 102);
            triggerListView.Name = "triggerListView";
            triggerListView.Size = new Size(898, 373);
            triggerListView.TabIndex = 3;
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
            triggerMenu.Items.AddRange(new ToolStripItem[] { pickAddToolStripMenuItem, toolStripSeparator1, editSelectedToolStripMenuItem, toolStripSeparator2, deleteToolStripMenuItem });
            triggerMenu.Name = "triggerMenu";
            triggerMenu.Size = new Size(312, 112);
            triggerMenu.Opening += triggerMenu_Opening;
            // 
            // pickAddToolStripMenuItem
            // 
            pickAddToolStripMenuItem.Name = "pickAddToolStripMenuItem";
            pickAddToolStripMenuItem.Size = new Size(311, 32);
            pickAddToolStripMenuItem.Text = "Pick/Add ...";
            pickAddToolStripMenuItem.Click += pickAddToolStripMenuItem_Click;
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
            btnOk.Location = new Point(342, 638);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 6;
            btnOk.Text = "Update / Create";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(630, 638);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusUpdateTimer
            // 
            statusUpdateTimer.Enabled = true;
            statusUpdateTimer.Interval = 200;
            statusUpdateTimer.Tick += statusUpdateTimer_Tick;
            // 
            // GlobalStatusForm
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
            Controls.Add(cbMode);
            Controls.Add(panelCombiner);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            MinimumSize = new Size(750, 250);
            Name = "GlobalStatusForm";
            Text = "Global Status";
            panelCombiner.ResumeLayout(false);
            panelCombiner.PerformLayout();
            triggerMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lId;
        private Label label1;
        private TextBox textName;
        private Label label2;
        private ComboBox cbMode;
        private Panel panelCombiner;
        private Label label3;
        private ComboBox triggerCombiner;
        private Button btnCombinerHelp;
        private Label labelCombinerError;
        private Label label4;
        private ListView triggerListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ContextMenuStrip triggerMenu;
        private ToolStripMenuItem pickAddToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem editSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Button btnOk;
        private Button btnCancel;
        private System.Windows.Forms.Timer statusUpdateTimer;
    }
}

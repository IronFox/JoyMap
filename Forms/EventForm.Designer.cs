namespace JoyMap
{
    partial class EventForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventForm));
            label1 = new Label();
            textName = new TextBox();
            triggerMenu = new ContextMenuStrip(components);
            pickAddToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            editSelectedToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            label3 = new Label();
            actionMenu = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            editSelectedToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            btnCancel = new Button();
            btnOk = new Button();
            statusUpdateTimer = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            label4 = new Label();
            triggerCombiner = new ComboBox();
            btnCombinerHelp = new Button();
            labelCombinerError = new Label();
            triggerListView = new ListView();
            columnHeader6 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            label2 = new Label();
            actionListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            triggerMenu.SuspendLayout();
            actionMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(81, 12);
            textName.Name = "textName";
            textName.Size = new Size(831, 31);
            textName.TabIndex = 1;
            textName.TextChanged += AnyChanged;
            // 
            // triggerMenu
            // 
            triggerMenu.ImageScalingSize = new Size(24, 24);
            triggerMenu.Items.AddRange(new ToolStripItem[] { pickAddToolStripMenuItem, toolStripMenuItem1, editSelectedToolStripMenuItem, toolStripMenuItem2, deleteToolStripMenuItem1 });
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
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(308, 6);
            // 
            // editSelectedToolStripMenuItem
            // 
            editSelectedToolStripMenuItem.Name = "editSelectedToolStripMenuItem";
            editSelectedToolStripMenuItem.Size = new Size(311, 32);
            editSelectedToolStripMenuItem.Text = "Edit Selected (double click) ...";
            editSelectedToolStripMenuItem.Click += editSelectedToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(308, 6);
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(311, 32);
            deleteToolStripMenuItem1.Text = "Delete";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 394);
            label3.Name = "label3";
            label3.Size = new Size(71, 25);
            label3.TabIndex = 4;
            label3.Text = "Actions";
            // 
            // actionMenu
            // 
            actionMenu.ImageScalingSize = new Size(24, 24);
            actionMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, toolStripMenuItem3, editSelectedToolStripMenuItem1, toolStripMenuItem4, deleteToolStripMenuItem });
            actionMenu.Name = "actionMenu";
            actionMenu.Size = new Size(312, 112);
            actionMenu.Opening += actionMenu_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(311, 32);
            addToolStripMenuItem.Text = "Add ...";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(308, 6);
            // 
            // editSelectedToolStripMenuItem1
            // 
            editSelectedToolStripMenuItem1.Name = "editSelectedToolStripMenuItem1";
            editSelectedToolStripMenuItem1.Size = new Size(311, 32);
            editSelectedToolStripMenuItem1.Text = "Edit Selected (double click) ...";
            editSelectedToolStripMenuItem1.Click += editSelectedToolStripMenuItem1_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(308, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(311, 32);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(630, 724);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(342, 724);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 7;
            btnOk.Text = "Update / Create";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // statusUpdateTimer
            // 
            statusUpdateTimer.Enabled = true;
            statusUpdateTimer.Interval = 200;
            statusUpdateTimer.Tick += statusUpdateTimer_Tick;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 49);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(triggerCombiner);
            splitContainer1.Panel1.Controls.Add(btnCombinerHelp);
            splitContainer1.Panel1.Controls.Add(labelCombinerError);
            splitContainer1.Panel1.Controls.Add(triggerListView);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1MinSize = 75;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(actionListView);
            splitContainer1.Panel2MinSize = 75;
            splitContainer1.Size = new Size(900, 669);
            splitContainer1.SplitterDistance = 297;
            splitContainer1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(262, 10);
            label4.Name = "label4";
            label4.Size = new Size(94, 25);
            label4.TabIndex = 13;
            label4.Text = "Combiner:";
            // 
            // triggerCombiner
            // 
            triggerCombiner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            triggerCombiner.FormattingEnabled = true;
            triggerCombiner.Items.AddRange(new object[] { "Or", "And" });
            triggerCombiner.Location = new Point(362, 7);
            triggerCombiner.Name = "triggerCombiner";
            triggerCombiner.Size = new Size(491, 33);
            triggerCombiner.TabIndex = 12;
            triggerCombiner.Text = "Or";
            triggerCombiner.TextChanged += AnyChanged;
            // 
            // btnCombinerHelp
            // 
            btnCombinerHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCombinerHelp.Location = new Point(861, 7);
            btnCombinerHelp.Name = "btnCombinerHelp";
            btnCombinerHelp.Size = new Size(36, 33);
            btnCombinerHelp.TabIndex = 14;
            btnCombinerHelp.Text = "?";
            btnCombinerHelp.UseVisualStyleBackColor = true;
            btnCombinerHelp.Click += btnCombinerHelp_Click;
            // 
            // labelCombinerError
            // 
            labelCombinerError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelCombinerError.ForeColor = Color.Firebrick;
            labelCombinerError.Location = new Point(362, 44);
            labelCombinerError.Name = "labelCombinerError";
            labelCombinerError.Size = new Size(535, 22);
            labelCombinerError.TabIndex = 15;
            labelCombinerError.Text = "";
            // 
            // triggerListView
            // 
            triggerListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            triggerListView.Columns.AddRange(new ColumnHeader[] { columnHeader6, columnHeader1, columnHeader2, columnHeader3 });
            triggerListView.ContextMenuStrip = triggerMenu;
            triggerListView.FullRowSelect = true;
            triggerListView.Location = new Point(3, 70);
            triggerListView.Name = "triggerListView";
            triggerListView.Size = new Size(894, 224);
            triggerListView.TabIndex = 11;
            triggerListView.UseCompatibleStateImageBehavior = false;
            triggerListView.View = View.Details;
            triggerListView.DoubleClick += triggerListView_DoubleClick;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Label";
            columnHeader6.Width = 100;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Device";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Input";
            columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Status";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 18);
            label2.Name = "label2";
            label2.Size = new Size(74, 25);
            label2.TabIndex = 10;
            label2.Text = "Triggers";
            // 
            // actionListView
            // 
            actionListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            actionListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader7 });
            actionListView.ContextMenuStrip = actionMenu;
            actionListView.FullRowSelect = true;
            actionListView.Location = new Point(3, 3);
            actionListView.Name = "actionListView";
            actionListView.Size = new Size(894, 362);
            actionListView.TabIndex = 6;
            actionListView.UseCompatibleStateImageBehavior = false;
            actionListView.View = View.Details;
            actionListView.DoubleClick += actionListView_DoubleClick;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Name";
            columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Type";
            columnHeader5.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Action";
            columnHeader7.Width = 200;
            // 
            // EventForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(924, 770);
            Controls.Add(splitContainer1);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(label3);
            Controls.Add(textName);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(946, 826);
            Name = "EventForm";
            Text = "Event";
            triggerMenu.ResumeLayout(false);
            actionMenu.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textName;
        private Label label3;
        private Button btnCancel;
        private Button btnOk;
        private ContextMenuStrip triggerMenu;
        private ContextMenuStrip actionMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem pickAddToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.Timer statusUpdateTimer;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem editSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem editSelectedToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem4;
        private SplitContainer splitContainer1;
        private Label label4;
        private ComboBox triggerCombiner;
        private Button btnCombinerHelp;
        private Label labelCombinerError;
        private ListView triggerListView;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label2;
        private ListView actionListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader7;
    }
}
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            triggerListView = new ListView();
            columnHeader6 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            triggerMenu = new ContextMenuStrip(components);
            pickAddToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            label3 = new Label();
            actionListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            actionMenu = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            btnCancel = new Button();
            btnOk = new Button();
            label4 = new Label();
            triggerCombiner = new ComboBox();
            statusUpdateTimer = new System.Windows.Forms.Timer(components);
            triggerMenu.SuspendLayout();
            actionMenu.SuspendLayout();
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
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(81, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(831, 31);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 75);
            label2.Name = "label2";
            label2.Size = new Size(74, 25);
            label2.TabIndex = 2;
            label2.Text = "Triggers";
            // 
            // triggerListView
            // 
            triggerListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            triggerListView.Columns.AddRange(new ColumnHeader[] { columnHeader6, columnHeader1, columnHeader2, columnHeader3 });
            triggerListView.ContextMenuStrip = triggerMenu;
            triggerListView.FullRowSelect = true;
            triggerListView.Location = new Point(12, 103);
            triggerListView.Name = "triggerListView";
            triggerListView.Size = new Size(900, 265);
            triggerListView.TabIndex = 3;
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
            columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Status";
            // 
            // triggerMenu
            // 
            triggerMenu.ImageScalingSize = new Size(24, 24);
            triggerMenu.Items.AddRange(new ToolStripItem[] { pickAddToolStripMenuItem, deleteToolStripMenuItem1 });
            triggerMenu.Name = "triggerMenu";
            triggerMenu.Size = new Size(174, 68);
            triggerMenu.Opening += triggerMenu_Opening;
            // 
            // pickAddToolStripMenuItem
            // 
            pickAddToolStripMenuItem.Name = "pickAddToolStripMenuItem";
            pickAddToolStripMenuItem.Size = new Size(173, 32);
            pickAddToolStripMenuItem.Text = "Pick/Add ...";
            pickAddToolStripMenuItem.Click += pickAddToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(173, 32);
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
            // actionListView
            // 
            actionListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            actionListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader7 });
            actionListView.ContextMenuStrip = actionMenu;
            actionListView.FullRowSelect = true;
            actionListView.Location = new Point(12, 422);
            actionListView.Name = "actionListView";
            actionListView.Size = new Size(900, 296);
            actionListView.TabIndex = 5;
            actionListView.UseCompatibleStateImageBehavior = false;
            actionListView.View = View.Details;
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
            // actionMenu
            // 
            actionMenu.ImageScalingSize = new Size(24, 24);
            actionMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            actionMenu.Name = "actionMenu";
            actionMenu.Size = new Size(241, 101);
            actionMenu.Opening += actionMenu_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(240, 32);
            addToolStripMenuItem.Text = "Add ...";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(240, 32);
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
            btnOk.Location = new Point(342, 724);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 7;
            btnOk.Text = "Update / Create";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(556, 67);
            label4.Name = "label4";
            label4.Size = new Size(94, 25);
            label4.TabIndex = 8;
            label4.Text = "Combiner:";
            // 
            // triggerCombiner
            // 
            triggerCombiner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            triggerCombiner.FormattingEnabled = true;
            triggerCombiner.Items.AddRange(new object[] { "Or", "And" });
            triggerCombiner.Location = new Point(656, 64);
            triggerCombiner.Name = "triggerCombiner";
            triggerCombiner.Size = new Size(256, 33);
            triggerCombiner.TabIndex = 9;
            triggerCombiner.Text = "Or";
            // 
            // statusUpdateTimer
            // 
            statusUpdateTimer.Enabled = true;
            statusUpdateTimer.Interval = 200;
            statusUpdateTimer.Tick += statusUpdateTimer_Tick;
            // 
            // EventForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(924, 770);
            Controls.Add(triggerCombiner);
            Controls.Add(label4);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(actionListView);
            Controls.Add(label3);
            Controls.Add(triggerListView);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "EventForm";
            Text = "Event";
            triggerMenu.ResumeLayout(false);
            actionMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private ListView triggerListView;
        private Label label3;
        private ListView actionListView;
        private Button btnCancel;
        private Button btnOk;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Label label4;
        private ComboBox triggerCombiner;
        private ColumnHeader columnHeader6;
        private ContextMenuStrip triggerMenu;
        private ContextMenuStrip actionMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem pickAddToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.Timer statusUpdateTimer;
        private ColumnHeader columnHeader7;
    }
}
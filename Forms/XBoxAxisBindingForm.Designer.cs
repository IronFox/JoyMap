namespace JoyMap.Forms
{
    partial class XBoxAxisBindingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XBoxAxisBindingForm));
            lAxis = new Label();
            axisListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            btnCancel = new Button();
            btnOk = new Button();
            lOutput = new Label();
            lEnableStatus = new Label();
            cbEnableStatus = new ComboBox();
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lAxis
            // 
            lAxis.AutoSize = true;
            lAxis.Location = new Point(12, 9);
            lAxis.Name = "lAxis";
            lAxis.Size = new Size(48, 25);
            lAxis.TabIndex = 0;
            lAxis.Text = "Axis:";
            // 
            // axisListView
            // 
            axisListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            axisListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3, columnHeader4 });
            axisListView.ContextMenuStrip = contextMenuStrip1;
            axisListView.FullRowSelect = true;
            axisListView.Location = new Point(12, 37);
            axisListView.Name = "axisListView";
            axisListView.Size = new Size(776, 295);
            axisListView.TabIndex = 1;
            axisListView.UseCompatibleStateImageBehavior = false;
            axisListView.View = View.Details;
            axisListView.DoubleClick += axisListView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Device";
            columnHeader1.Width = 300;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Transform";
            columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Current";
            columnHeader4.Width = 100;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, toolStripMenuItem1, deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(241, 106);
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(240, 32);
            addToolStripMenuItem.Text = "Add ...";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(240, 32);
            editToolStripMenuItem.Text = "Edit ... (double click)";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(237, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(240, 32);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 418);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 32);
            statusStrip1.TabIndex = 23;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(107, 25);
            statusLabel.Text = "                   ";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(627, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(161, 34);
            btnCancel.TabIndex = 22;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(460, 381);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(161, 34);
            btnOk.TabIndex = 21;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // lOutput
            // 
            lOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lOutput.AutoSize = true;
            lOutput.Location = new Point(12, 386);
            lOutput.Name = "lOutput";
            lOutput.Size = new Size(88, 25);
            lOutput.TabIndex = 20;
            lOutput.Text = "Output: 0";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // lEnableStatus
            // 
            lEnableStatus.AutoSize = true;
            lEnableStatus.Location = new Point(12, 345);
            lEnableStatus.Name = "lEnableStatus";
            lEnableStatus.Size = new Size(130, 25);
            lEnableStatus.TabIndex = 24;
            lEnableStatus.Text = "Enable Status:";
            // 
            // cbEnableStatus
            // 
            cbEnableStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cbEnableStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEnableStatus.FormattingEnabled = true;
            cbEnableStatus.Location = new Point(148, 342);
            cbEnableStatus.Name = "cbEnableStatus";
            cbEnableStatus.Size = new Size(640, 33);
            cbEnableStatus.TabIndex = 25;
            cbEnableStatus.SelectedIndexChanged += RebuildResult;
            // 
            // XBoxAxisBindingForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStrip1);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lOutput);
            Controls.Add(cbEnableStatus);
            Controls.Add(lEnableStatus);
            Controls.Add(axisListView);
            Controls.Add(lAxis);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(822, 506);
            Name = "XBoxAxisBindingForm";
            Text = "XBoxAxisBinding";
            contextMenuStrip1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lAxis;
        private ListView axisListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private Button btnCancel;
        private Button btnOk;
        private Label lOutput;
        private Label lEnableStatus;
        private ComboBox cbEnableStatus;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}
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
            listView1 = new ListView();
            columnHeader6 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            triggerMenu = new ContextMenuStrip(components);
            pickAddToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            label3 = new Label();
            listView2 = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            actionMenu = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            btnCancel = new Button();
            btnOk = new Button();
            label4 = new Label();
            triggerCombiner = new ComboBox();
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
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader6, columnHeader1, columnHeader2, columnHeader3 });
            listView1.ContextMenuStrip = triggerMenu;
            listView1.FullRowSelect = true;
            listView1.Location = new Point(12, 103);
            listView1.Name = "listView1";
            listView1.Size = new Size(900, 265);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
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
            // listView2
            // 
            listView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5 });
            listView2.ContextMenuStrip = actionMenu;
            listView2.FullRowSelect = true;
            listView2.Location = new Point(12, 422);
            listView2.Name = "listView2";
            listView2.Size = new Size(900, 296);
            listView2.TabIndex = 5;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Name";
            columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Type";
            // 
            // actionMenu
            // 
            actionMenu.ImageScalingSize = new Size(24, 24);
            actionMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            actionMenu.Name = "actionMenu";
            actionMenu.Size = new Size(136, 68);
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(135, 32);
            addToolStripMenuItem.Text = "Add ...";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(135, 32);
            deleteToolStripMenuItem.Text = "Delete";
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
            Controls.Add(listView2);
            Controls.Add(label3);
            Controls.Add(listView1);
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
        private ListView listView1;
        private Label label3;
        private ListView listView2;
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
    }
}
namespace JoyMap
{
    partial class ControllerFamiliesForm
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
            listFamilies = new ListView();
            columnHeader1 = new ColumnHeader();
            contextFamilies = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            contextMembers = new ContextMenuStrip(components);
            addToolStripMenuItem1 = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            columnHeader4 = new ColumnHeader();
            contextFamilies.SuspendLayout();
            contextMembers.SuspendLayout();
            SuspendLayout();
            // 
            // listFamilies
            // 
            listFamilies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listFamilies.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader4 });
            listFamilies.ContextMenuStrip = contextFamilies;
            listFamilies.Location = new Point(12, 12);
            listFamilies.Name = "listFamilies";
            listFamilies.Size = new Size(868, 526);
            listFamilies.TabIndex = 1;
            listFamilies.UseCompatibleStateImageBehavior = false;
            listFamilies.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 500;
            // 
            // contextFamilies
            // 
            contextFamilies.ImageScalingSize = new Size(24, 24);
            contextFamilies.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem });
            contextFamilies.Name = "contextFamilies";
            contextFamilies.Size = new Size(136, 100);
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(135, 32);
            addToolStripMenuItem.Text = "Add ...";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(135, 32);
            editToolStripMenuItem.Text = "Edit ...";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(135, 32);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // contextMembers
            // 
            contextMembers.ImageScalingSize = new Size(24, 24);
            contextMembers.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, removeToolStripMenuItem });
            contextMembers.Name = "contextMembers";
            contextMembers.Size = new Size(166, 68);
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            addToolStripMenuItem1.Size = new Size(165, 32);
            addToolStripMenuItem1.Text = "Add ...";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(165, 32);
            removeToolStripMenuItem.Text = "Remove ...";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "# Controllers";
            columnHeader4.Width = 120;
            // 
            // ControllerFamiliesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 550);
            Controls.Add(listFamilies);
            Name = "ControllerFamiliesForm";
            Text = "ControllerFamiliesForm";
            contextFamilies.ResumeLayout(false);
            contextMembers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ListView listFamilies;
        private ColumnHeader columnHeader1;
        private ContextMenuStrip contextFamilies;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip contextMembers;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ColumnHeader columnHeader4;
    }
}
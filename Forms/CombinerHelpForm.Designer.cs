namespace JoyMap.Forms
{
    partial class CombinerHelpForm
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
            labelExpr = new Label();
            txtExpression = new TextBox();
            exprContextMenu = new ContextMenuStrip(components);
            insertAndMenuItem = new ToolStripMenuItem();
            insertOrMenuItem = new ToolStripMenuItem();
            insertNotMenuItem = new ToolStripMenuItem();
            insertParensMenuItem = new ToolStripMenuItem();
            exprSep1 = new ToolStripSeparator();
            localInsertHeader = new ToolStripMenuItem();
            exprSep2 = new ToolStripSeparator();
            globalInsertHeader = new ToolStripMenuItem();
            exprSep3 = new ToolStripSeparator();
            setOrMenuItem = new ToolStripMenuItem();
            setAndMenuItem = new ToolStripMenuItem();
            txtHelpText = new TextBox();
            labelError = new Label();
            splitMain = new SplitContainer();
            panelLocal = new Panel();
            labelLocal = new Label();
            localListView = new ListView();
            chLocalLabel = new ColumnHeader();
            chLocalDevice = new ColumnHeader();
            chLocalAxis = new ColumnHeader();
            chLocalStatus = new ColumnHeader();
            panelGlobal = new Panel();
            labelGlobal = new Label();
            globalListView = new ListView();
            chGlobalId = new ColumnHeader();
            chGlobalName = new ColumnHeader();
            chGlobalStatus = new ColumnHeader();
            btnOk = new Button();
            btnCancel = new Button();
            liveTimer = new System.Windows.Forms.Timer(components);
            exprContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelLocal.SuspendLayout();
            panelGlobal.SuspendLayout();
            SuspendLayout();
            // 
            // labelExpr
            // 
            labelExpr.AutoSize = true;
            labelExpr.Location = new Point(12, 14);
            labelExpr.Name = "labelExpr";
            labelExpr.Size = new Size(100, 25);
            labelExpr.TabIndex = 0;
            labelExpr.Text = "Expression:";
            // 
            // txtExpression
            // 
            txtExpression.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtExpression.ContextMenuStrip = exprContextMenu;
            txtExpression.Font = new Font("Consolas", 10F);
            txtExpression.Location = new Point(112, 11);
            txtExpression.Name = "txtExpression";
            txtExpression.Size = new Size(826, 31);
            txtExpression.TabIndex = 1;
            txtExpression.TextChanged += txtExpression_TextChanged;
            // 
            // exprContextMenu
            // 
            exprContextMenu.ImageScalingSize = new Size(24, 24);
            exprContextMenu.Items.AddRange(new ToolStripItem[] { insertAndMenuItem, insertOrMenuItem, insertNotMenuItem, insertParensMenuItem, exprSep1, localInsertHeader, exprSep2, globalInsertHeader, exprSep3, setOrMenuItem, setAndMenuItem });
            exprContextMenu.Name = "exprContextMenu";
            exprContextMenu.Size = new Size(316, 278);
            exprContextMenu.Opening += exprContextMenu_Opening;
            // 
            // insertAndMenuItem
            // 
            insertAndMenuItem.Name = "insertAndMenuItem";
            insertAndMenuItem.Size = new Size(315, 32);
            insertAndMenuItem.Text = "Insert: && (all must match)";
            insertAndMenuItem.Click += insertAndMenuItem_Click;
            // 
            // insertOrMenuItem
            // 
            insertOrMenuItem.Name = "insertOrMenuItem";
            insertOrMenuItem.Size = new Size(315, 32);
            insertOrMenuItem.Text = "Insert: || (any must match)";
            insertOrMenuItem.Click += insertOrMenuItem_Click;
            // 
            // insertNotMenuItem
            // 
            insertNotMenuItem.Name = "insertNotMenuItem";
            insertNotMenuItem.Size = new Size(315, 32);
            insertNotMenuItem.Text = "Insert: ! (negate)";
            insertNotMenuItem.Click += insertNotMenuItem_Click;
            // 
            // insertParensMenuItem
            // 
            insertParensMenuItem.Name = "insertParensMenuItem";
            insertParensMenuItem.Size = new Size(315, 32);
            insertParensMenuItem.Text = "Insert: ( ) group";
            insertParensMenuItem.Click += insertParensMenuItem_Click;
            // 
            // exprSep1
            // 
            exprSep1.Name = "exprSep1";
            exprSep1.Size = new Size(312, 6);
            // 
            // localInsertHeader
            // 
            localInsertHeader.Enabled = false;
            localInsertHeader.Name = "localInsertHeader";
            localInsertHeader.Size = new Size(315, 32);
            localInsertHeader.Text = "— Local Inputs —";
            // 
            // exprSep2
            // 
            exprSep2.Name = "exprSep2";
            exprSep2.Size = new Size(312, 6);
            // 
            // globalInsertHeader
            // 
            globalInsertHeader.Enabled = false;
            globalInsertHeader.Name = "globalInsertHeader";
            globalInsertHeader.Size = new Size(315, 32);
            globalInsertHeader.Text = "— Global Inputs —";
            // 
            // exprSep3
            // 
            exprSep3.Name = "exprSep3";
            exprSep3.Size = new Size(312, 6);
            // 
            // setOrMenuItem
            // 
            setOrMenuItem.Name = "setOrMenuItem";
            setOrMenuItem.Size = new Size(315, 32);
            setOrMenuItem.Text = "Set: any trigger matches  (Or)";
            setOrMenuItem.Click += setOrMenuItem_Click;
            // 
            // setAndMenuItem
            // 
            setAndMenuItem.Name = "setAndMenuItem";
            setAndMenuItem.Size = new Size(315, 32);
            setAndMenuItem.Text = "Set: all triggers match  (And)";
            setAndMenuItem.Click += setAndMenuItem_Click;
            // 
            // txtHelpText
            // 
            txtHelpText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtHelpText.BackColor = SystemColors.Control;
            txtHelpText.BorderStyle = BorderStyle.None;
            txtHelpText.Font = new Font("Segoe UI", 8.5F);
            txtHelpText.ForeColor = SystemColors.ControlText;
            txtHelpText.Location = new Point(12, 332);
            txtHelpText.Multiline = true;
            txtHelpText.Name = "txtHelpText";
            txtHelpText.ReadOnly = true;
            txtHelpText.ScrollBars = ScrollBars.Vertical;
            txtHelpText.Size = new Size(926, 222);
            txtHelpText.TabIndex = 7;
            txtHelpText.TabStop = false;
            // 
            // labelError
            // 
            labelError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelError.ForeColor = Color.Firebrick;
            labelError.Location = new Point(112, 46);
            labelError.Name = "labelError";
            labelError.Size = new Size(826, 22);
            labelError.TabIndex = 2;
            // 
            // splitMain
            // 
            splitMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            splitMain.Location = new Point(12, 72);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(panelLocal);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(panelGlobal);
            splitMain.Size = new Size(926, 254);
            splitMain.SplitterDistance = 459;
            splitMain.TabIndex = 3;
            // 
            // panelLocal
            // 
            panelLocal.Controls.Add(labelLocal);
            panelLocal.Controls.Add(localListView);
            panelLocal.Dock = DockStyle.Fill;
            panelLocal.Location = new Point(0, 0);
            panelLocal.Name = "panelLocal";
            panelLocal.Size = new Size(459, 254);
            panelLocal.TabIndex = 0;
            // 
            // labelLocal
            // 
            labelLocal.AutoSize = true;
            labelLocal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelLocal.Location = new Point(0, 0);
            labelLocal.Name = "labelLocal";
            labelLocal.Size = new Size(217, 25);
            labelLocal.TabIndex = 0;
            labelLocal.Text = "Local Inputs  (T0, T1, …)";
            // 
            // localListView
            // 
            localListView.Columns.AddRange(new ColumnHeader[] { chLocalLabel, chLocalDevice, chLocalAxis, chLocalStatus });
            localListView.Dock = DockStyle.Bottom;
            localListView.FullRowSelect = true;
            localListView.Location = new Point(0, 28);
            localListView.Name = "localListView";
            localListView.Size = new Size(459, 226);
            localListView.TabIndex = 1;
            localListView.UseCompatibleStateImageBehavior = false;
            localListView.View = View.Details;
            localListView.DoubleClick += localListView_DoubleClick;
            // 
            // chLocalLabel
            // 
            chLocalLabel.Text = "Label";
            // 
            // chLocalDevice
            // 
            chLocalDevice.Text = "Device";
            chLocalDevice.Width = 160;
            // 
            // chLocalAxis
            // 
            chLocalAxis.Text = "Input";
            chLocalAxis.Width = 160;
            // 
            // chLocalStatus
            // 
            chLocalStatus.Text = "Active";
            // 
            // panelGlobal
            // 
            panelGlobal.Controls.Add(labelGlobal);
            panelGlobal.Controls.Add(globalListView);
            panelGlobal.Dock = DockStyle.Fill;
            panelGlobal.Location = new Point(0, 0);
            panelGlobal.Name = "panelGlobal";
            panelGlobal.Size = new Size(463, 254);
            panelGlobal.TabIndex = 0;
            // 
            // labelGlobal
            // 
            labelGlobal.AutoSize = true;
            labelGlobal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelGlobal.Location = new Point(0, 0);
            labelGlobal.Name = "labelGlobal";
            labelGlobal.Size = new Size(232, 25);
            labelGlobal.TabIndex = 0;
            labelGlobal.Text = "Global Inputs  (G0, G1, …)";
            // 
            // globalListView
            // 
            globalListView.Columns.AddRange(new ColumnHeader[] { chGlobalId, chGlobalName, chGlobalStatus });
            globalListView.Dock = DockStyle.Bottom;
            globalListView.FullRowSelect = true;
            globalListView.Location = new Point(0, 28);
            globalListView.Name = "globalListView";
            globalListView.Size = new Size(463, 226);
            globalListView.TabIndex = 1;
            globalListView.UseCompatibleStateImageBehavior = false;
            globalListView.View = View.Details;
            globalListView.DoubleClick += globalListView_DoubleClick;
            // 
            // chGlobalId
            // 
            chGlobalId.Text = "ID";
            // 
            // chGlobalName
            // 
            chGlobalName.Text = "Name";
            chGlobalName.Width = 300;
            // 
            // chGlobalStatus
            // 
            chGlobalStatus.Text = "Active";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(362, 560);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 4;
            btnOk.Text = "Apply";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(656, 560);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // liveTimer
            // 
            liveTimer.Enabled = true;
            liveTimer.Interval = 200;
            liveTimer.Tick += liveTimer_Tick;
            // 
            // CombinerHelpForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(950, 606);
            Controls.Add(labelExpr);
            Controls.Add(txtExpression);
            Controls.Add(labelError);
            Controls.Add(splitMain);
            Controls.Add(txtHelpText);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            MinimumSize = new Size(750, 500);
            Name = "CombinerHelpForm";
            Text = "Combiner Expression";
            exprContextMenu.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelLocal.ResumeLayout(false);
            panelLocal.PerformLayout();
            panelGlobal.ResumeLayout(false);
            panelGlobal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelExpr;
        private TextBox txtExpression;
        private ContextMenuStrip exprContextMenu;
        private ToolStripMenuItem insertAndMenuItem;
        private ToolStripMenuItem insertOrMenuItem;
        private ToolStripMenuItem insertNotMenuItem;
        private ToolStripMenuItem insertParensMenuItem;
        private ToolStripSeparator exprSep1;
        private ToolStripMenuItem localInsertHeader;
        private ToolStripSeparator exprSep2;
        private ToolStripMenuItem globalInsertHeader;
        private ToolStripSeparator exprSep3;
        private ToolStripMenuItem setOrMenuItem;
        private ToolStripMenuItem setAndMenuItem;
        private Label labelError;
        private TextBox txtHelpText;
        private SplitContainer splitMain;
        private Panel panelLocal;
        private Label labelLocal;
        private ListView localListView;
        private ColumnHeader chLocalLabel;
        private ColumnHeader chLocalDevice;
        private ColumnHeader chLocalAxis;
        private ColumnHeader chLocalStatus;
        private Panel panelGlobal;
        private Label labelGlobal;
        private ListView globalListView;
        private ColumnHeader chGlobalId;
        private ColumnHeader chGlobalName;
        private ColumnHeader chGlobalStatus;
        private Button btnOk;
        private Button btnCancel;
        private System.Windows.Forms.Timer liveTimer;
    }
}

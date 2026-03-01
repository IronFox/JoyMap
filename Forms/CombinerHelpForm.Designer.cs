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
            labelExpr.Size = new Size(90, 25);
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
            exprContextMenu.Items.AddRange(new ToolStripItem[]
            {
                insertAndMenuItem, insertOrMenuItem, insertNotMenuItem, insertParensMenuItem,
                exprSep1,
                localInsertHeader,
                exprSep2,
                globalInsertHeader,
                exprSep3,
                setOrMenuItem, setAndMenuItem
            });
            exprContextMenu.Name = "exprContextMenu";
            exprContextMenu.Size = new Size(220, 270);
            exprContextMenu.Opening += exprContextMenu_Opening;
            // 
            // insertAndMenuItem
            // 
            insertAndMenuItem.Name = "insertAndMenuItem";
            insertAndMenuItem.Size = new Size(219, 32);
            insertAndMenuItem.Text = "Insert: AND";
            insertAndMenuItem.Click += (s, e) => InsertText(" AND ");
            // 
            // insertOrMenuItem
            // 
            insertOrMenuItem.Name = "insertOrMenuItem";
            insertOrMenuItem.Size = new Size(219, 32);
            insertOrMenuItem.Text = "Insert: OR";
            insertOrMenuItem.Click += (s, e) => InsertText(" OR ");
            // 
            // insertNotMenuItem
            // 
            insertNotMenuItem.Name = "insertNotMenuItem";
            insertNotMenuItem.Size = new Size(219, 32);
            insertNotMenuItem.Text = "Insert: NOT";
            insertNotMenuItem.Click += (s, e) => InsertText("NOT ");
            // 
            // insertParensMenuItem
            // 
            insertParensMenuItem.Name = "insertParensMenuItem";
            insertParensMenuItem.Size = new Size(219, 32);
            insertParensMenuItem.Text = "Insert: ( )";
            insertParensMenuItem.Click += (s, e) => InsertParens();
            // 
            // exprSep1
            // 
            exprSep1.Name = "exprSep1";
            exprSep1.Size = new Size(216, 6);
            // 
            // localInsertHeader
            // 
            localInsertHeader.Enabled = false;
            localInsertHeader.Name = "localInsertHeader";
            localInsertHeader.Size = new Size(219, 32);
            localInsertHeader.Text = "— Local Inputs —";
            // 
            // exprSep2
            // 
            exprSep2.Name = "exprSep2";
            exprSep2.Size = new Size(216, 6);
            // 
            // globalInsertHeader
            // 
            globalInsertHeader.Enabled = false;
            globalInsertHeader.Name = "globalInsertHeader";
            globalInsertHeader.Size = new Size(219, 32);
            globalInsertHeader.Text = "— Global Inputs —";
            // 
            // exprSep3
            // 
            exprSep3.Name = "exprSep3";
            exprSep3.Size = new Size(216, 6);
            // 
            // setOrMenuItem
            // 
            setOrMenuItem.Name = "setOrMenuItem";
            setOrMenuItem.Size = new Size(219, 32);
            setOrMenuItem.Text = "Set to \"Or\"";
            setOrMenuItem.Click += (s, e) => { txtExpression.Text = "Or"; };
            // 
            // setAndMenuItem
            // 
            setAndMenuItem.Name = "setAndMenuItem";
            setAndMenuItem.Size = new Size(219, 32);
            setAndMenuItem.Text = "Set to \"And\"";
            setAndMenuItem.Click += (s, e) => { txtExpression.Text = "And"; };
            // 
            // labelError
            // 
            labelError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelError.ForeColor = Color.Firebrick;
            labelError.Location = new Point(112, 46);
            labelError.Name = "labelError";
            labelError.Size = new Size(826, 22);
            labelError.TabIndex = 2;
            labelError.Text = "";
            // 
            // splitMain
            // 
            splitMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitMain.Location = new Point(12, 74);
            splitMain.Name = "splitMain";
            splitMain.Size = new Size(926, 530);
            splitMain.SplitterDistance = 460;
            splitMain.TabIndex = 3;
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(panelLocal);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(panelGlobal);
            // 
            // panelLocal
            // 
            panelLocal.Dock = DockStyle.Fill;
            panelLocal.Controls.Add(labelLocal);
            panelLocal.Controls.Add(localListView);
            panelLocal.Name = "panelLocal";
            panelLocal.TabIndex = 0;
            // 
            // labelLocal
            // 
            labelLocal.AutoSize = true;
            labelLocal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelLocal.Location = new Point(0, 0);
            labelLocal.Name = "labelLocal";
            labelLocal.Size = new Size(180, 25);
            labelLocal.TabIndex = 0;
            labelLocal.Text = "Local Inputs  (T0, T1, …)";
            // 
            // localListView
            // 
            localListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            localListView.Columns.AddRange(new ColumnHeader[] { chLocalLabel, chLocalDevice, chLocalAxis, chLocalStatus });
            localListView.FullRowSelect = true;
            localListView.Location = new Point(0, 28);
            localListView.Name = "localListView";
            localListView.Size = new Size(456, 498);
            localListView.TabIndex = 1;
            localListView.UseCompatibleStateImageBehavior = false;
            localListView.View = View.Details;
            // 
            // chLocalLabel
            // 
            chLocalLabel.Text = "Label";
            chLocalLabel.Width = 60;
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
            chLocalStatus.Width = 60;
            // 
            // panelGlobal
            // 
            panelGlobal.Dock = DockStyle.Fill;
            panelGlobal.Controls.Add(labelGlobal);
            panelGlobal.Controls.Add(globalListView);
            panelGlobal.Name = "panelGlobal";
            panelGlobal.TabIndex = 0;
            // 
            // labelGlobal
            // 
            labelGlobal.AutoSize = true;
            labelGlobal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelGlobal.Location = new Point(0, 0);
            labelGlobal.Name = "labelGlobal";
            labelGlobal.Size = new Size(200, 25);
            labelGlobal.TabIndex = 0;
            labelGlobal.Text = "Global Inputs  (G0, G1, …)";
            // 
            // globalListView
            // 
            globalListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            globalListView.Columns.AddRange(new ColumnHeader[] { chGlobalId, chGlobalName, chGlobalStatus });
            globalListView.FullRowSelect = true;
            globalListView.Location = new Point(0, 28);
            globalListView.Name = "globalListView";
            globalListView.Size = new Size(458, 498);
            globalListView.TabIndex = 1;
            globalListView.UseCompatibleStateImageBehavior = false;
            globalListView.View = View.Details;
            // 
            // chGlobalId
            // 
            chGlobalId.Text = "ID";
            chGlobalId.Width = 60;
            // 
            // chGlobalName
            // 
            chGlobalName.Text = "Name";
            chGlobalName.Width = 300;
            // 
            // chGlobalStatus
            // 
            chGlobalStatus.Text = "Active";
            chGlobalStatus.Width = 60;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(362, 616);
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
            btnCancel.Location = new Point(656, 616);
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
            ClientSize = new Size(950, 662);
            Controls.Add(labelExpr);
            Controls.Add(txtExpression);
            Controls.Add(labelError);
            Controls.Add(splitMain);
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

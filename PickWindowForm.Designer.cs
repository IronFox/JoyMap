namespace JoyMap
{
    partial class PickWindowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickWindowForm));
            windowListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            btnOk = new Button();
            btnCancel = new Button();
            updateWindowListTimer = new System.Windows.Forms.Timer(components);
            columnHeader3 = new ColumnHeader();
            SuspendLayout();
            // 
            // windowListView
            // 
            windowListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            windowListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            windowListView.FullRowSelect = true;
            windowListView.Location = new Point(12, 12);
            windowListView.Name = "windowListView";
            windowListView.Size = new Size(776, 386);
            windowListView.TabIndex = 20;
            windowListView.UseCompatibleStateImageBehavior = false;
            windowListView.View = View.Details;
            windowListView.SelectedIndexChanged += windowListView_SelectedIndexChanged;
            windowListView.DoubleClick += windowListView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Window Name";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Window Size";
            columnHeader2.Width = 200;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(219, 404);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 22;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(507, 404);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // updateWindowListTimer
            // 
            updateWindowListTimer.Enabled = true;
            updateWindowListTimer.Interval = 500;
            updateWindowListTimer.Tick += updateWindowListTimer_Tick;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Process Name";
            columnHeader3.Width = 200;
            // 
            // PickWindowForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(windowListView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(822, 506);
            Name = "PickWindowForm";
            Text = "PickWindowForm";
            ResumeLayout(false);
        }

        #endregion

        private ListView windowListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button btnOk;
        private Button btnCancel;
        private System.Windows.Forms.Timer updateWindowListTimer;
        private ColumnHeader columnHeader3;
    }
}
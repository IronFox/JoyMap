namespace JoyMap
{
    partial class PickDeviceInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickDeviceInputForm));
            btnOk = new Button();
            btnCancel = new Button();
            inputList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            updateDeviceListTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(196, 386);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 18;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(484, 386);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // inputList
            // 
            inputList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            inputList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            inputList.FullRowSelect = true;
            inputList.Location = new Point(12, 12);
            inputList.Name = "inputList";
            inputList.Size = new Size(754, 368);
            inputList.TabIndex = 19;
            inputList.UseCompatibleStateImageBehavior = false;
            inputList.View = View.Details;
            inputList.SelectedIndexChanged += inputList_SelectedIndexChanged;
            inputList.DoubleClick += inputList_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Device";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Input";
            columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Status";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Direction";
            columnHeader4.Width = 120;
            // 
            // updateDeviceListTimer
            // 
            updateDeviceListTimer.Enabled = true;
            updateDeviceListTimer.Interval = 200;
            updateDeviceListTimer.Tick += updateDeviceListTimer_Tick;
            // 
            // PickDeviceInputForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(778, 432);
            Controls.Add(inputList);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 488);
            Name = "PickDeviceInputForm";
            Text = "Pick Axis";
            Load += PickDeviceInputForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private ListView inputList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private System.Windows.Forms.Timer updateDeviceListTimer;
        private ColumnHeader columnHeader4;
    }
}
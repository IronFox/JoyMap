namespace JoyMap
{
    partial class AddControllerFamily
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
                DevicePoller.Dispose();
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
            textFamilyName = new TextBox();
            btnCancel = new Button();
            btnOK = new Button();
            label1 = new Label();
            listDevices = new CheckedListBox();
            timerRedetectDevices = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // textFamilyName
            // 
            textFamilyName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textFamilyName.Location = new Point(81, 12);
            textFamilyName.Name = "textFamilyName";
            textFamilyName.Size = new Size(583, 31);
            textFamilyName.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(552, 235);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(434, 235);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 3;
            label1.Text = "Name:";
            // 
            // listDevices
            // 
            listDevices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listDevices.FormattingEnabled = true;
            listDevices.Location = new Point(12, 49);
            listDevices.Name = "listDevices";
            listDevices.Size = new Size(652, 172);
            listDevices.TabIndex = 4;
            // 
            // timerRedetectDevices
            // 
            timerRedetectDevices.Enabled = true;
            timerRedetectDevices.Interval = 1000;
            timerRedetectDevices.Tick += timerRedetectDevices_Tick;
            // 
            // AddControllerFamily
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(676, 281);
            Controls.Add(listDevices);
            Controls.Add(label1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(textFamilyName);
            Name = "AddControllerFamily";
            Text = "AddControllerFamily";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textFamilyName;
        private Button btnCancel;
        private Button btnOK;
        private Label label1;
        private CheckedListBox listDevices;
        private System.Windows.Forms.Timer timerRedetectDevices;
    }
}
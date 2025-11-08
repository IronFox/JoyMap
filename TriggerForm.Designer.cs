namespace JoyMap
{
    partial class TriggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerForm));
            textDevice = new TextBox();
            label2 = new Label();
            btnPickDeviceInput = new Button();
            textInput = new TextBox();
            label3 = new Label();
            textMin = new TextBox();
            label4 = new Label();
            textMax = new TextBox();
            label5 = new Label();
            label6 = new Label();
            labelStatus = new Label();
            label8 = new Label();
            labelActive = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            statusUpdateTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // textDevice
            // 
            textDevice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDevice.Location = new Point(80, 12);
            textDevice.Name = "textDevice";
            textDevice.ReadOnly = true;
            textDevice.Size = new Size(396, 31);
            textDevice.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 15);
            label2.Name = "label2";
            label2.Size = new Size(64, 25);
            label2.TabIndex = 2;
            label2.Text = "Device";
            // 
            // btnPickDeviceInput
            // 
            btnPickDeviceInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPickDeviceInput.Location = new Point(482, 12);
            btnPickDeviceInput.Name = "btnPickDeviceInput";
            btnPickDeviceInput.Size = new Size(112, 68);
            btnPickDeviceInput.TabIndex = 4;
            btnPickDeviceInput.Text = "Pick ...";
            btnPickDeviceInput.UseVisualStyleBackColor = true;
            btnPickDeviceInput.Click += btnPickDeviceInput_Click;
            // 
            // textInput
            // 
            textInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textInput.Location = new Point(80, 49);
            textInput.Name = "textInput";
            textInput.ReadOnly = true;
            textInput.Size = new Size(396, 31);
            textInput.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 52);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
            label3.TabIndex = 5;
            label3.Text = "Input";
            // 
            // textMin
            // 
            textMin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textMin.Location = new Point(80, 95);
            textMin.Name = "textMin";
            textMin.Size = new Size(514, 31);
            textMin.TabIndex = 8;
            textMin.Text = "50";
            textMin.TextChanged += textMin_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 98);
            label4.Name = "label4";
            label4.Size = new Size(62, 25);
            label4.TabIndex = 7;
            label4.Text = "Min %";
            // 
            // textMax
            // 
            textMax.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textMax.Location = new Point(80, 132);
            textMax.Name = "textMax";
            textMax.Size = new Size(514, 31);
            textMax.TabIndex = 10;
            textMax.Text = "100";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 135);
            label5.Name = "label5";
            label5.Size = new Size(65, 25);
            label5.TabIndex = 9;
            label5.Text = "Max %";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 206);
            label6.Name = "label6";
            label6.Size = new Size(74, 25);
            label6.TabIndex = 11;
            label6.Text = "Current:";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(160, 206);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(61, 25);
            labelStatus.TabIndex = 12;
            labelStatus.Text = "12.4%";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(332, 206);
            label8.Name = "label8";
            label8.Size = new Size(161, 25);
            label8.TabIndex = 13;
            label8.Text = "In Min/Max Range:";
            // 
            // labelActive
            // 
            labelActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelActive.AutoSize = true;
            labelActive.Location = new Point(556, 206);
            labelActive.Name = "labelActive";
            labelActive.Size = new Size(37, 25);
            labelActive.TabIndex = 14;
            labelActive.Text = "Yes";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(26, 248);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 16;
            btnOk.Text = "Update / Create";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(314, 248);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusUpdateTimer
            // 
            statusUpdateTimer.Enabled = true;
            statusUpdateTimer.Interval = 200;
            statusUpdateTimer.Tick += statusUpdateTimer_Tick;
            // 
            // TriggerForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(608, 294);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(labelActive);
            Controls.Add(label8);
            Controls.Add(labelStatus);
            Controls.Add(label6);
            Controls.Add(textMax);
            Controls.Add(label5);
            Controls.Add(textMin);
            Controls.Add(label4);
            Controls.Add(textInput);
            Controls.Add(label3);
            Controls.Add(btnPickDeviceInput);
            Controls.Add(textDevice);
            Controls.Add(label2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(10000000, 350);
            MinimumSize = new Size(618, 350);
            Name = "TriggerForm";
            Text = "Trigger";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textDevice;
        private Label label2;
        private Button btnPickDeviceInput;
        private TextBox textInput;
        private Label label3;
        private TextBox textMin;
        private Label label4;
        private TextBox textMax;
        private Label label5;
        private Label label6;
        private Label labelStatus;
        private Label label8;
        private Label labelActive;
        private Button btnOk;
        private Button btnCancel;
        private System.Windows.Forms.Timer statusUpdateTimer;
    }
}
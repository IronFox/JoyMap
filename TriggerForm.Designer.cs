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
            textAutoReleaseMs = new TextBox();
            cbAutoReleaseActive = new CheckBox();
            cbDelayRelease = new CheckBox();
            textDelayReleaseMs = new TextBox();
            tabs = new TabControl();
            tabRange = new TabPage();
            tabDither = new TabPage();
            label9 = new Label();
            textDitherFrequency = new TextBox();
            label1 = new Label();
            textRampStart = new TextBox();
            label7 = new Label();
            textRampMax = new TextBox();
            tabs.SuspendLayout();
            tabRange.SuspendLayout();
            tabDither.SuspendLayout();
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
            textMin.Location = new Point(76, 10);
            textMin.Name = "textMin";
            textMin.Size = new Size(498, 31);
            textMin.TabIndex = 8;
            textMin.Text = "50";
            textMin.TextChanged += textMin_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 13);
            label4.Name = "label4";
            label4.Size = new Size(62, 25);
            label4.TabIndex = 7;
            label4.Text = "Min %";
            // 
            // textMax
            // 
            textMax.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textMax.Location = new Point(76, 47);
            textMax.Name = "textMax";
            textMax.Size = new Size(498, 31);
            textMax.TabIndex = 10;
            textMax.Text = "100";
            textMax.TextChanged += textMax_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 50);
            label5.Name = "label5";
            label5.Size = new Size(65, 25);
            label5.TabIndex = 9;
            label5.Text = "Max %";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(10, 303);
            label6.Name = "label6";
            label6.Size = new Size(74, 25);
            label6.TabIndex = 11;
            label6.Text = "Current:";
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(160, 303);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(61, 25);
            labelStatus.TabIndex = 12;
            labelStatus.Text = "12.4%";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(332, 303);
            label8.Name = "label8";
            label8.Size = new Size(161, 25);
            label8.TabIndex = 13;
            label8.Text = "In Min/Max Range:";
            // 
            // labelActive
            // 
            labelActive.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelActive.AutoSize = true;
            labelActive.Location = new Point(556, 303);
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
            btnOk.Location = new Point(26, 345);
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
            btnCancel.Location = new Point(314, 345);
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
            // textAutoReleaseMs
            // 
            textAutoReleaseMs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textAutoReleaseMs.Location = new Point(236, 84);
            textAutoReleaseMs.Name = "textAutoReleaseMs";
            textAutoReleaseMs.Size = new Size(338, 31);
            textAutoReleaseMs.TabIndex = 18;
            textAutoReleaseMs.Text = "567.8";
            textAutoReleaseMs.TextChanged += textAutoReleaseMs_TextChanged;
            // 
            // cbAutoReleaseActive
            // 
            cbAutoReleaseActive.AutoSize = true;
            cbAutoReleaseActive.Location = new Point(6, 86);
            cbAutoReleaseActive.Name = "cbAutoReleaseActive";
            cbAutoReleaseActive.Size = new Size(224, 29);
            cbAutoReleaseActive.TabIndex = 19;
            cbAutoReleaseActive.Text = "Auto Release after (ms):";
            cbAutoReleaseActive.UseVisualStyleBackColor = true;
            cbAutoReleaseActive.CheckedChanged += cbAutoReleaseActive_CheckedChanged;
            // 
            // cbDelayRelease
            // 
            cbDelayRelease.AutoSize = true;
            cbDelayRelease.Location = new Point(6, 123);
            cbDelayRelease.Name = "cbDelayRelease";
            cbDelayRelease.Size = new Size(216, 29);
            cbDelayRelease.TabIndex = 21;
            cbDelayRelease.Text = "Delay Release for (ms):";
            cbDelayRelease.UseVisualStyleBackColor = true;
            cbDelayRelease.CheckedChanged += cbDelayRelease_CheckedChanged;
            // 
            // textDelayReleaseMs
            // 
            textDelayReleaseMs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDelayReleaseMs.Location = new Point(236, 121);
            textDelayReleaseMs.Name = "textDelayReleaseMs";
            textDelayReleaseMs.Size = new Size(338, 31);
            textDelayReleaseMs.TabIndex = 20;
            textDelayReleaseMs.Text = "567.8";
            textDelayReleaseMs.TextChanged += textDelayReleaseMs_TextChanged;
            // 
            // tabs
            // 
            tabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabs.Controls.Add(tabRange);
            tabs.Controls.Add(tabDither);
            tabs.Location = new Point(8, 86);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(588, 208);
            tabs.TabIndex = 22;
            tabs.SelectedIndexChanged += tabs_SelectedIndexChanged;
            // 
            // tabRange
            // 
            tabRange.BackColor = SystemColors.Control;
            tabRange.Controls.Add(label4);
            tabRange.Controls.Add(cbDelayRelease);
            tabRange.Controls.Add(textMin);
            tabRange.Controls.Add(textDelayReleaseMs);
            tabRange.Controls.Add(label5);
            tabRange.Controls.Add(cbAutoReleaseActive);
            tabRange.Controls.Add(textMax);
            tabRange.Controls.Add(textAutoReleaseMs);
            tabRange.Location = new Point(4, 34);
            tabRange.Name = "tabRange";
            tabRange.Padding = new Padding(3);
            tabRange.Size = new Size(580, 170);
            tabRange.TabIndex = 0;
            tabRange.Text = "Range";
            // 
            // tabDither
            // 
            tabDither.BackColor = SystemColors.Control;
            tabDither.Controls.Add(label9);
            tabDither.Controls.Add(textDitherFrequency);
            tabDither.Controls.Add(label1);
            tabDither.Controls.Add(textRampStart);
            tabDither.Controls.Add(label7);
            tabDither.Controls.Add(textRampMax);
            tabDither.Location = new Point(4, 34);
            tabDither.Name = "tabDither";
            tabDither.Padding = new Padding(3);
            tabDither.Size = new Size(580, 170);
            tabDither.TabIndex = 1;
            tabDither.Text = "Dither";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 91);
            label9.Name = "label9";
            label9.Size = new Size(93, 25);
            label9.TabIndex = 15;
            label9.Text = "Frequency";
            // 
            // textDitherFrequency
            // 
            textDitherFrequency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDitherFrequency.Location = new Point(129, 88);
            textDitherFrequency.Name = "textDitherFrequency";
            textDitherFrequency.Size = new Size(445, 31);
            textDitherFrequency.TabIndex = 16;
            textDitherFrequency.Text = "10.0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 17);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 11;
            label1.Text = "Ramp Start %";
            // 
            // textRampStart
            // 
            textRampStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textRampStart.Location = new Point(129, 14);
            textRampStart.Name = "textRampStart";
            textRampStart.Size = new Size(445, 31);
            textRampStart.TabIndex = 12;
            textRampStart.Text = "20";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 54);
            label7.Name = "label7";
            label7.Size = new Size(117, 25);
            label7.TabIndex = 13;
            label7.Text = "Ramp Max %";
            // 
            // textRampMax
            // 
            textRampMax.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textRampMax.Location = new Point(129, 51);
            textRampMax.Name = "textRampMax";
            textRampMax.Size = new Size(445, 31);
            textRampMax.TabIndex = 14;
            textRampMax.Text = "80";
            // 
            // TriggerForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(608, 391);
            Controls.Add(tabs);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(labelActive);
            Controls.Add(label8);
            Controls.Add(labelStatus);
            Controls.Add(label6);
            Controls.Add(textInput);
            Controls.Add(label3);
            Controls.Add(btnPickDeviceInput);
            Controls.Add(textDevice);
            Controls.Add(label2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(630, 447);
            Name = "TriggerForm";
            Text = "Trigger";
            tabs.ResumeLayout(false);
            tabRange.ResumeLayout(false);
            tabRange.PerformLayout();
            tabDither.ResumeLayout(false);
            tabDither.PerformLayout();
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
        private TextBox textAutoReleaseMs;
        private CheckBox cbAutoReleaseActive;
        private CheckBox cbDelayRelease;
        private TextBox textDelayReleaseMs;
        private TabControl tabs;
        private TabPage tabRange;
        private TabPage tabDither;
        private Label label9;
        private TextBox textDitherFrequency;
        private Label label1;
        private TextBox textRampStart;
        private Label label7;
        private TextBox textRampMax;
    }
}
namespace JoyMap
{
    partial class XBoxAxisPickForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XBoxAxisPickForm));
            textInput = new TextBox();
            label3 = new Label();
            btnPickDeviceInput = new Button();
            textDevice = new TextBox();
            label2 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label4 = new Label();
            label5 = new Label();
            textDeadzone = new TextBox();
            lInput = new Label();
            lOutput = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            updateTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            textScale = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textInput
            // 
            textInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textInput.Location = new Point(82, 49);
            textInput.Name = "textInput";
            textInput.ReadOnly = true;
            textInput.Size = new Size(613, 31);
            textInput.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 52);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
            label3.TabIndex = 10;
            label3.Text = "Input";
            // 
            // btnPickDeviceInput
            // 
            btnPickDeviceInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPickDeviceInput.Location = new Point(701, 12);
            btnPickDeviceInput.Name = "btnPickDeviceInput";
            btnPickDeviceInput.Size = new Size(112, 68);
            btnPickDeviceInput.TabIndex = 9;
            btnPickDeviceInput.Text = "Pick ...";
            btnPickDeviceInput.UseVisualStyleBackColor = true;
            btnPickDeviceInput.Click += btnPickDeviceInput_Click;
            // 
            // textDevice
            // 
            textDevice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDevice.Location = new Point(82, 12);
            textDevice.Name = "textDevice";
            textDevice.ReadOnly = true;
            textDevice.Size = new Size(613, 31);
            textDevice.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(64, 25);
            label2.TabIndex = 7;
            label2.Text = "Device";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 160);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(801, 109);
            tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label4);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(793, 71);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Linear";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 3);
            label4.Name = "label4";
            label4.Size = new Size(564, 25);
            label4.TabIndex = 0;
            label4.Text = "Maps +/- values to the corresponding XBox-Axis. Deadzone around 0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 89);
            label5.Name = "label5";
            label5.Size = new Size(112, 25);
            label5.TabIndex = 15;
            label5.Text = "Deadzone %";
            // 
            // textDeadzone
            // 
            textDeadzone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDeadzone.Location = new Point(135, 86);
            textDeadzone.Name = "textDeadzone";
            textDeadzone.Size = new Size(678, 31);
            textDeadzone.TabIndex = 1;
            textDeadzone.Text = "5";
            textDeadzone.TextChanged += RebuildResult;
            // 
            // lInput
            // 
            lInput.Anchor = AnchorStyles.Bottom;
            lInput.AutoSize = true;
            lInput.Location = new Point(16, 280);
            lInput.Name = "lInput";
            lInput.Size = new Size(73, 25);
            lInput.TabIndex = 15;
            lInput.Text = "Input: 0";
            // 
            // lOutput
            // 
            lOutput.Anchor = AnchorStyles.Bottom;
            lOutput.AutoSize = true;
            lOutput.Location = new Point(166, 280);
            lOutput.Name = "lOutput";
            lOutput.Size = new Size(88, 25);
            lOutput.TabIndex = 16;
            lOutput.Text = "Output: 0";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(481, 275);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(161, 34);
            btnOk.TabIndex = 17;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(648, 275);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(161, 34);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 312);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(825, 32);
            statusStrip1.TabIndex = 19;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(67, 25);
            statusLabel.Text = "           ";
            // 
            // updateTimer
            // 
            updateTimer.Enabled = true;
            updateTimer.Tick += updateTimer_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 126);
            label1.Name = "label1";
            label1.Size = new Size(72, 25);
            label1.TabIndex = 21;
            label1.Text = "Scale %";
            // 
            // textScale
            // 
            textScale.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textScale.Location = new Point(135, 123);
            textScale.Name = "textScale";
            textScale.Size = new Size(678, 31);
            textScale.TabIndex = 20;
            textScale.Text = "100";
            textScale.TextChanged += RebuildResult;
            // 
            // XBoxAxisPickForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(825, 344);
            Controls.Add(label1);
            Controls.Add(textScale);
            Controls.Add(label5);
            Controls.Add(statusStrip1);
            Controls.Add(textDeadzone);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lOutput);
            Controls.Add(lInput);
            Controls.Add(tabControl1);
            Controls.Add(textInput);
            Controls.Add(label3);
            Controls.Add(btnPickDeviceInput);
            Controls.Add(textDevice);
            Controls.Add(label2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(847, 400);
            Name = "XBoxAxisPickForm";
            Text = "XboxAxisMapping";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textInput;
        private Label label3;
        private Button btnPickDeviceInput;
        private TextBox textDevice;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label5;
        private TextBox textDeadzone;
        private Label label4;
        private Label lInput;
        private Label lOutput;
        private Button btnOk;
        private Button btnCancel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer updateTimer;
        private Label label1;
        private TextBox textScale;
    }
}
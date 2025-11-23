namespace JoyMap
{
    partial class ActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionForm));
            btnOk = new Button();
            btnCancel = new Button();
            tabControl = new TabControl();
            tpSimple = new TabPage();
            textSimpleAutoTriggerDelayedStartMs = new TextBox();
            cbSimpleAutoTriggerDelayedStart = new CheckBox();
            textSimpleAutoTriggerLimit = new TextBox();
            cbSimpleAutoTriggerLimit = new CheckBox();
            textSimpleAutoTriggerFrequency = new TextBox();
            cbSimpleAutoTriggerFrequency = new CheckBox();
            btnSimplePickKey = new Button();
            textSimpleKey = new TextBox();
            label3 = new Label();
            tpTrigger = new TabPage();
            btnChangeTriggerKeySelect = new Button();
            textTriggerKey = new TextBox();
            label4 = new Label();
            label1 = new Label();
            textDelay = new TextBox();
            textName = new TextBox();
            label2 = new Label();
            tabControl.SuspendLayout();
            tpSimple.SuspendLayout();
            tpTrigger.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(218, 404);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 20;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(506, 404);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tpSimple);
            tabControl.Controls.Add(tpTrigger);
            tabControl.Location = new Point(12, 90);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(776, 299);
            tabControl.TabIndex = 21;
            // 
            // tpSimple
            // 
            tpSimple.Controls.Add(textSimpleAutoTriggerDelayedStartMs);
            tpSimple.Controls.Add(cbSimpleAutoTriggerDelayedStart);
            tpSimple.Controls.Add(textSimpleAutoTriggerLimit);
            tpSimple.Controls.Add(cbSimpleAutoTriggerLimit);
            tpSimple.Controls.Add(textSimpleAutoTriggerFrequency);
            tpSimple.Controls.Add(cbSimpleAutoTriggerFrequency);
            tpSimple.Controls.Add(btnSimplePickKey);
            tpSimple.Controls.Add(textSimpleKey);
            tpSimple.Controls.Add(label3);
            tpSimple.Location = new Point(4, 34);
            tpSimple.Name = "tpSimple";
            tpSimple.Padding = new Padding(3);
            tpSimple.Size = new Size(768, 261);
            tpSimple.TabIndex = 0;
            tpSimple.Text = "Simple Trigger";
            // 
            // textSimpleAutoTriggerDelayedStartMs
            // 
            textSimpleAutoTriggerDelayedStartMs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textSimpleAutoTriggerDelayedStartMs.Enabled = false;
            textSimpleAutoTriggerDelayedStartMs.Location = new Point(245, 81);
            textSimpleAutoTriggerDelayedStartMs.Name = "textSimpleAutoTriggerDelayedStartMs";
            textSimpleAutoTriggerDelayedStartMs.Size = new Size(517, 31);
            textSimpleAutoTriggerDelayedStartMs.TabIndex = 35;
            textSimpleAutoTriggerDelayedStartMs.Text = "512.345";
            textSimpleAutoTriggerDelayedStartMs.TextChanged += textSimpleAutoTriggerDelayedStartMs_TextChanged;
            // 
            // cbSimpleAutoTriggerDelayedStart
            // 
            cbSimpleAutoTriggerDelayedStart.AutoSize = true;
            cbSimpleAutoTriggerDelayedStart.Enabled = false;
            cbSimpleAutoTriggerDelayedStart.Location = new Point(6, 83);
            cbSimpleAutoTriggerDelayedStart.Name = "cbSimpleAutoTriggerDelayedStart";
            cbSimpleAutoTriggerDelayedStart.Size = new Size(166, 29);
            cbSimpleAutoTriggerDelayedStart.TabIndex = 34;
            cbSimpleAutoTriggerDelayedStart.Text = "Delay Start (ms):";
            cbSimpleAutoTriggerDelayedStart.UseVisualStyleBackColor = true;
            cbSimpleAutoTriggerDelayedStart.CheckedChanged += cbSimpleAutoTriggerDelayedStart_CheckedChanged;
            // 
            // textSimpleAutoTriggerLimit
            // 
            textSimpleAutoTriggerLimit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textSimpleAutoTriggerLimit.Enabled = false;
            textSimpleAutoTriggerLimit.Location = new Point(245, 118);
            textSimpleAutoTriggerLimit.Name = "textSimpleAutoTriggerLimit";
            textSimpleAutoTriggerLimit.Size = new Size(517, 31);
            textSimpleAutoTriggerLimit.TabIndex = 33;
            textSimpleAutoTriggerLimit.Text = "3";
            textSimpleAutoTriggerLimit.TextChanged += textAutoTriggerLimit_TextChanged;
            // 
            // cbSimpleAutoTriggerLimit
            // 
            cbSimpleAutoTriggerLimit.AutoSize = true;
            cbSimpleAutoTriggerLimit.Enabled = false;
            cbSimpleAutoTriggerLimit.Location = new Point(6, 120);
            cbSimpleAutoTriggerLimit.Name = "cbSimpleAutoTriggerLimit";
            cbSimpleAutoTriggerLimit.Size = new Size(193, 29);
            cbSimpleAutoTriggerLimit.TabIndex = 32;
            cbSimpleAutoTriggerLimit.Text = "Limit Auto-Triggers:";
            cbSimpleAutoTriggerLimit.UseVisualStyleBackColor = true;
            cbSimpleAutoTriggerLimit.CheckedChanged += cbSimpleAutoTriggerLimit_CheckedChanged;
            // 
            // textSimpleAutoTriggerFrequency
            // 
            textSimpleAutoTriggerFrequency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textSimpleAutoTriggerFrequency.Location = new Point(245, 44);
            textSimpleAutoTriggerFrequency.Name = "textSimpleAutoTriggerFrequency";
            textSimpleAutoTriggerFrequency.Size = new Size(517, 31);
            textSimpleAutoTriggerFrequency.TabIndex = 31;
            textSimpleAutoTriggerFrequency.Text = "3.12";
            textSimpleAutoTriggerFrequency.TextChanged += textSimpleAutoTriggerFrequency_TextChanged;
            // 
            // cbSimpleAutoTriggerFrequency
            // 
            cbSimpleAutoTriggerFrequency.AutoSize = true;
            cbSimpleAutoTriggerFrequency.Location = new Point(6, 46);
            cbSimpleAutoTriggerFrequency.Name = "cbSimpleAutoTriggerFrequency";
            cbSimpleAutoTriggerFrequency.Size = new Size(228, 29);
            cbSimpleAutoTriggerFrequency.TabIndex = 30;
            cbSimpleAutoTriggerFrequency.Text = "Auto-Trigger Frequency:";
            cbSimpleAutoTriggerFrequency.UseVisualStyleBackColor = true;
            cbSimpleAutoTriggerFrequency.CheckedChanged += cbSimpleAutoTriggerFrequency_CheckedChanged;
            // 
            // btnSimplePickKey
            // 
            btnSimplePickKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSimplePickKey.Location = new Point(653, 4);
            btnSimplePickKey.Name = "btnSimplePickKey";
            btnSimplePickKey.Size = new Size(112, 34);
            btnSimplePickKey.TabIndex = 28;
            btnSimplePickKey.Text = "Pick ...";
            btnSimplePickKey.UseVisualStyleBackColor = true;
            btnSimplePickKey.Click += btnSimplePickKey_Click;
            // 
            // textSimpleKey
            // 
            textSimpleKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textSimpleKey.Location = new Point(148, 6);
            textSimpleKey.Name = "textSimpleKey";
            textSimpleKey.ReadOnly = true;
            textSimpleKey.Size = new Size(499, 31);
            textSimpleKey.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 9);
            label3.Name = "label3";
            label3.Size = new Size(104, 25);
            label3.TabIndex = 26;
            label3.Text = "Key/Button:";
            // 
            // tpTrigger
            // 
            tpTrigger.Controls.Add(btnChangeTriggerKeySelect);
            tpTrigger.Controls.Add(textTriggerKey);
            tpTrigger.Controls.Add(label4);
            tpTrigger.Location = new Point(4, 34);
            tpTrigger.Name = "tpTrigger";
            tpTrigger.Padding = new Padding(3);
            tpTrigger.Size = new Size(768, 261);
            tpTrigger.TabIndex = 1;
            tpTrigger.Text = "On Change Trigger";
            // 
            // btnChangeTriggerKeySelect
            // 
            btnChangeTriggerKeySelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChangeTriggerKeySelect.Location = new Point(653, 8);
            btnChangeTriggerKeySelect.Name = "btnChangeTriggerKeySelect";
            btnChangeTriggerKeySelect.Size = new Size(112, 34);
            btnChangeTriggerKeySelect.TabIndex = 31;
            btnChangeTriggerKeySelect.Text = "Pick ...";
            btnChangeTriggerKeySelect.UseVisualStyleBackColor = true;
            btnChangeTriggerKeySelect.Click += btnChangeTriggerKeySelect_Click;
            // 
            // textTriggerKey
            // 
            textTriggerKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textTriggerKey.Location = new Point(148, 10);
            textTriggerKey.Name = "textTriggerKey";
            textTriggerKey.ReadOnly = true;
            textTriggerKey.Size = new Size(499, 31);
            textTriggerKey.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 13);
            label4.Name = "label4";
            label4.Size = new Size(104, 25);
            label4.TabIndex = 29;
            label4.Text = "Key/Button:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 56);
            label1.Name = "label1";
            label1.Size = new Size(146, 25);
            label1.TabIndex = 22;
            label1.Text = "Initial Delay (ms):";
            // 
            // textDelay
            // 
            textDelay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textDelay.Location = new Point(164, 53);
            textDelay.Name = "textDelay";
            textDelay.Size = new Size(620, 31);
            textDelay.TabIndex = 23;
            textDelay.Text = "0";
            textDelay.TextChanged += AnyInputChanged;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(164, 12);
            textName.Name = "textName";
            textName.Size = new Size(620, 31);
            textName.TabIndex = 25;
            textName.TextChanged += AnyInputChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 24;
            label2.Text = "Name:";
            // 
            // ActionForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(800, 450);
            Controls.Add(textName);
            Controls.Add(label2);
            Controls.Add(textDelay);
            Controls.Add(label1);
            Controls.Add(tabControl);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(822, 506);
            Name = "ActionForm";
            Text = "Action";
            tabControl.ResumeLayout(false);
            tpSimple.ResumeLayout(false);
            tpSimple.PerformLayout();
            tpTrigger.ResumeLayout(false);
            tpTrigger.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private TabControl tabControl;
        private TabPage tpSimple;
        private Button btnSimplePickKey;
        private TextBox textSimpleKey;
        private Label label3;
        private Label label1;
        private TextBox textDelay;
        private TextBox textName;
        private Label label2;
        private TextBox textSimpleAutoTriggerFrequency;
        private CheckBox cbSimpleAutoTriggerFrequency;
        private TextBox textSimpleAutoTriggerLimit;
        private CheckBox cbSimpleAutoTriggerLimit;
        private TextBox textSimpleAutoTriggerDelayedStartMs;
        private CheckBox cbSimpleAutoTriggerDelayedStart;
        private TabPage tpTrigger;
        private Button btnChangeTriggerKeySelect;
        private TextBox textTriggerKey;
        private Label label4;
    }
}
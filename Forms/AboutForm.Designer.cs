namespace JoyMap
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            label1 = new Label();
            lVersion = new Label();
            textBox1 = new TextBox();
            btnOk = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(74, 25);
            label1.TabIndex = 0;
            label1.Text = "JoyMap";
            // 
            // lVersion
            // 
            lVersion.AutoSize = true;
            lVersion.Location = new Point(92, 9);
            lVersion.Name = "lVersion";
            lVersion.Size = new Size(59, 25);
            lVersion.TabIndex = 1;
            lVersion.Text = "v0.0.0";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(12, 51);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(776, 331);
            textBox1.TabIndex = 2;
            textBox1.TabStop = false;
            textBox1.Text = "JoyMap is a Joystick to Keyboard mapping program.\r\n\r\nhttps://github.com/IronFox/JoyMap\r\nWritten by Stefan Elsen 2025 - 2026\r\n";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(12, 388);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(776, 50);
            btnOk.TabIndex = 3;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnOk;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOk);
            Controls.Add(textBox1);
            Controls.Add(lVersion);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AboutForm";
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lVersion;
        private TextBox textBox1;
        private Button btnOk;
    }
}
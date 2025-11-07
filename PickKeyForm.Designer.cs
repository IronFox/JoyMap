namespace JoyMap
{
    partial class PickKeyForm
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
            inputList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // inputList
            // 
            inputList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            inputList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3 });
            inputList.FullRowSelect = true;
            inputList.Location = new Point(12, 12);
            inputList.Name = "inputList";
            inputList.Size = new Size(776, 386);
            inputList.TabIndex = 20;
            inputList.UseCompatibleStateImageBehavior = false;
            inputList.View = View.Details;
            inputList.SelectedIndexChanged += inputList_SelectedIndexChanged;
            inputList.KeyDown += PickKeyForm_KeyDown;
            inputList.KeyUp += PickKeyForm_KeyUp;
            inputList.MouseDown += PickKeyForm_MouseDown;
            inputList.MouseUp += PickKeyForm_MouseUp;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Device";
            columnHeader1.Width = 300;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Status";
            columnHeader3.Width = 120;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(218, 404);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(282, 34);
            btnOk.TabIndex = 22;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.KeyDown += PickKeyForm_KeyDown;
            btnOk.KeyUp += PickKeyForm_KeyUp;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(506, 404);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(282, 34);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.KeyDown += PickKeyForm_KeyUp;
            btnCancel.KeyUp += PickKeyForm_KeyUp;
            // 
            // PickKeyForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(inputList);
            Name = "PickKeyForm";
            Text = "PickKeyForm";
            KeyDown += PickKeyForm_KeyDown;
            KeyUp += PickKeyForm_KeyUp;
            MouseDown += PickKeyForm_MouseDown;
            MouseUp += PickKeyForm_MouseUp;
            ResumeLayout(false);
        }

        #endregion

        private ListView inputList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader3;
        private Button btnOk;
        private Button btnCancel;
    }
}
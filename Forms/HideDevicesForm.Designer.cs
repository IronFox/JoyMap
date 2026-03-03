namespace JoyMap
{
    partial class HideDevicesForm
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
            labelStatus = new Label();
            linkLabelDownload = new LinkLabel();
            contextMenuUrlLink = new ContextMenuStrip(components);
            copyUrlMenuItem = new ToolStripMenuItem();
            labelInfo = new Label();
            listDevices = new CheckedListBox();
            labelNote = new Label();
            btnApply = new Button();
            btnClose = new Button();
            timerRefresh = new System.Windows.Forms.Timer(components);
            contextMenuUrlLink.SuspendLayout();
            SuspendLayout();
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelStatus.Location = new Point(12, 12);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(658, 22);
            labelStatus.TabIndex = 0;
            labelStatus.Text = "Checking HidHide ...";
            // 
            // linkLabelDownload
            // 
            linkLabelDownload.ActiveLinkColor = Color.White;
            linkLabelDownload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabelDownload.ContextMenuStrip = contextMenuUrlLink;
            linkLabelDownload.LinkColor = Color.DeepSkyBlue;
            linkLabelDownload.Location = new Point(12, 38);
            linkLabelDownload.Name = "linkLabelDownload";
            linkLabelDownload.Size = new Size(658, 22);
            linkLabelDownload.TabIndex = 6;
            linkLabelDownload.TabStop = true;
            linkLabelDownload.Text = "https://github.com/nefarius/HidHide/releases";
            linkLabelDownload.VisitedLinkColor = Color.DeepSkyBlue;
            linkLabelDownload.Visible = false;
            linkLabelDownload.LinkClicked += linkLabelDownload_LinkClicked;
            // 
            // contextMenuUrlLink
            // 
            contextMenuUrlLink.ImageScalingSize = new Size(24, 24);
            contextMenuUrlLink.Items.AddRange(new ToolStripItem[] { copyUrlMenuItem });
            contextMenuUrlLink.Name = "contextMenuUrlLink";
            contextMenuUrlLink.Size = new Size(181, 34);
            // 
            // copyUrlMenuItem
            // 
            copyUrlMenuItem.Name = "copyUrlMenuItem";
            copyUrlMenuItem.Size = new Size(180, 30);
            copyUrlMenuItem.Text = "Copy URL";
            copyUrlMenuItem.Click += copyUrlMenuItem_Click;
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(12, 68);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(250, 25);
            labelInfo.TabIndex = 1;
            labelInfo.Text = "Select devices to hide from games:";
            // 
            // listDevices
            // 
            listDevices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listDevices.FormattingEnabled = true;
            listDevices.Location = new Point(12, 99);
            listDevices.Name = "listDevices";
            listDevices.Size = new Size(658, 160);
            listDevices.TabIndex = 2;
            // 
            // labelNote
            // 
            labelNote.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelNote.Location = new Point(12, 269);
            labelNote.Name = "labelNote";
            labelNote.Size = new Size(658, 25);
            labelNote.TabIndex = 3;
            labelNote.Text = "Checked devices are hidden from all other applications. JoyMap can always see them.";
            // 
            // btnApply
            // 
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApply.Location = new Point(446, 304);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(112, 34);
            btnApply.TabIndex = 4;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(564, 304);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(106, 34);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // timerRefresh
            // 
            timerRefresh.Enabled = true;
            timerRefresh.Interval = 1000;
            timerRefresh.Tick += timerRefresh_Tick;
            contextMenuUrlLink.ResumeLayout(false);
            // 
            // HideDevicesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 350);
            Controls.Add(labelNote);
            Controls.Add(listDevices);
            Controls.Add(labelInfo);
            Controls.Add(linkLabelDownload);
            Controls.Add(labelStatus);
            Controls.Add(btnApply);
            Controls.Add(btnClose);
            Name = "HideDevicesForm";
            Text = "Hide Input Devices";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStatus;
        private LinkLabel linkLabelDownload;
        private ContextMenuStrip contextMenuUrlLink;
        private ToolStripMenuItem copyUrlMenuItem;
        private Label labelInfo;
        private CheckedListBox listDevices;
        private Label labelNote;
        private Button btnApply;
        private Button btnClose;
        private System.Windows.Forms.Timer timerRefresh;
    }
}

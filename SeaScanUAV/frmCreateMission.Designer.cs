namespace SeaScanUAV
{
    partial class frmCreateMission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateMission));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.lblVideoFile = new System.Windows.Forms.Label();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.cmdBrowseVideo = new System.Windows.Forms.Button();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmdBrowseLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLiveMission = new System.Windows.Forms.CheckBox();
            this.chkUploadPoints = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(535, 542);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(67, 28);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Tag = "renderTimer != null && renderTimer.Paused";
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(608, 542);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(67, 28);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lblVideoFile
            // 
            this.lblVideoFile.AutoSize = true;
            this.lblVideoFile.Location = new System.Drawing.Point(23, 35);
            this.lblVideoFile.Name = "lblVideoFile";
            this.lblVideoFile.Size = new System.Drawing.Size(56, 13);
            this.lblVideoFile.TabIndex = 0;
            this.lblVideoFile.Text = "Video File:";
            // 
            // txtVideo
            // 
            this.txtVideo.Location = new System.Drawing.Point(87, 31);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.Size = new System.Drawing.Size(427, 20);
            this.txtVideo.TabIndex = 1;
            // 
            // cmdBrowseVideo
            // 
            this.cmdBrowseVideo.Location = new System.Drawing.Point(523, 31);
            this.cmdBrowseVideo.Name = "cmdBrowseVideo";
            this.cmdBrowseVideo.Size = new System.Drawing.Size(82, 21);
            this.cmdBrowseVideo.TabIndex = 2;
            this.cmdBrowseVideo.Text = "Browse...";
            this.cmdBrowseVideo.UseVisualStyleBackColor = true;
            this.cmdBrowseVideo.Click += new System.EventHandler(this.cmdBrowseVideo_Click);
            // 
            // lblLogFile
            // 
            this.lblLogFile.AutoSize = true;
            this.lblLogFile.Location = new System.Drawing.Point(32, 99);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(47, 13);
            this.lblLogFile.TabIndex = 3;
            this.lblLogFile.Text = "Log File:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(87, 95);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(427, 20);
            this.txtLog.TabIndex = 4;
            // 
            // cmdBrowseLog
            // 
            this.cmdBrowseLog.Location = new System.Drawing.Point(523, 95);
            this.cmdBrowseLog.Name = "cmdBrowseLog";
            this.cmdBrowseLog.Size = new System.Drawing.Size(82, 21);
            this.cmdBrowseLog.TabIndex = 5;
            this.cmdBrowseLog.Text = "Browse...";
            this.cmdBrowseLog.UseVisualStyleBackColor = true;
            this.cmdBrowseLog.Click += new System.EventHandler(this.cmdBrowseLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdBrowseLog);
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Controls.Add(this.lblLogFile);
            this.groupBox1.Controls.Add(this.cmdBrowseVideo);
            this.groupBox1.Controls.Add(this.txtVideo);
            this.groupBox1.Controls.Add(this.lblVideoFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saved Mission";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 19);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(651, 213);
            this.txtDescription.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Location = new System.Drawing.Point(12, 286);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(663, 250);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // chkLiveMission
            // 
            this.chkLiveMission.AutoSize = true;
            this.chkLiveMission.Location = new System.Drawing.Point(26, 21);
            this.chkLiveMission.Name = "chkLiveMission";
            this.chkLiveMission.Size = new System.Drawing.Size(194, 17);
            this.chkLiveMission.TabIndex = 11;
            this.chkLiveMission.Text = "Connect to live mission (via Socket)";
            this.chkLiveMission.UseVisualStyleBackColor = true;
            this.chkLiveMission.Click += new System.EventHandler(this.chkLiveMission_Click);
            // 
            // chkUploadPoints
            // 
            this.chkUploadPoints.AutoSize = true;
            this.chkUploadPoints.Checked = true;
            this.chkUploadPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUploadPoints.Enabled = false;
            this.chkUploadPoints.Location = new System.Drawing.Point(26, 53);
            this.chkUploadPoints.Name = "chkUploadPoints";
            this.chkUploadPoints.Size = new System.Drawing.Size(254, 17);
            this.chkUploadPoints.TabIndex = 14;
            this.chkUploadPoints.Text = "Upload points to webservice as they are created";
            this.chkUploadPoints.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkUploadPoints);
            this.groupBox3.Controls.Add(this.chkLiveMission);
            this.groupBox3.Location = new System.Drawing.Point(12, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(662, 99);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Live Mission";
            // 
            // frmCreateMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(687, 582);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateMission";
            this.ShowInTaskbar = false;
            this.Text = "Create Mission";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.Label lblVideoFile;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Button cmdBrowseVideo;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button cmdBrowseLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkLiveMission;
        private System.Windows.Forms.CheckBox chkUploadPoints;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}
namespace SeaScanUAV
{
    partial class frmUploadMission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadMission));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdUpload = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.grdMissionSummary = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdMissionSummary);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 451);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload mission to webservice or save to disk";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(367, 466);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(67, 28);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdUpload
            // 
            this.cmdUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdUpload.Location = new System.Drawing.Point(234, 466);
            this.cmdUpload.Name = "cmdUpload";
            this.cmdUpload.Size = new System.Drawing.Size(127, 28);
            this.cmdUpload.TabIndex = 2;
            this.cmdUpload.Text = "Upload to Webservice";
            this.cmdUpload.UseVisualStyleBackColor = true;
            this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSave.Location = new System.Drawing.Point(126, 466);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(102, 28);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "Save To Disk";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // grdMissionSummary
            // 
            this.grdMissionSummary.Location = new System.Drawing.Point(6, 19);
            this.grdMissionSummary.Name = "grdMissionSummary";
            this.grdMissionSummary.Size = new System.Drawing.Size(402, 416);
            this.grdMissionSummary.TabIndex = 0;
            // 
            // frmUploadMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(441, 497);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdUpload);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadMission";
            this.ShowInTaskbar = false;
            this.Text = "Save Mission";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdUpload;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.PropertyGrid grdMissionSummary;
    }
}
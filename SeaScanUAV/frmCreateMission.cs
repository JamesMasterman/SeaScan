using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SeaScanUAV
{
    public partial class frmCreateMission : Form
    {        
        public String Description { get; set; }
        public String VideoFile { get; set; }
        public String LogFile { get; set; }
        public bool IsLive { get; set; }

        public frmCreateMission()
        {
            InitializeComponent();

            VideoFile = txtVideo.Text = Properties.Settings.Default.lastMissionMovie;
            LogFile = txtLog.Text = Properties.Settings.Default.lastMissionLog;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Description = txtDescription.Text;
            VideoFile = txtVideo.Text;
            LogFile = txtLog.Text;
        }

        private void cmdBrowseVideo_Click(object sender, EventArgs e)
        {
            dlgFileOpen.AddExtension = true;
            dlgFileOpen.Filter = "All Files (*.*)|*.*|MPEG Files (*.mpg)|*.mpg|Avi Files (*.avi)|*.avi";
            dlgFileOpen.FilterIndex = 2;
            dlgFileOpen.DefaultExt = "*.mpg";

            if(dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                txtVideo.Text = dlgFileOpen.FileName;
                Properties.Settings.Default.lastMissionMovie = txtVideo.Text;
                Properties.Settings.Default.Save();
            }

        }

        private void cmdBrowseLog_Click(object sender, EventArgs e)
        {
            dlgFileOpen.AddExtension = true;
            dlgFileOpen.Filter = "All Files (*.*)|*.*|Ardupilot Telemetry log (*.tlog)|*.tlog";
            dlgFileOpen.FilterIndex = 2;
            dlgFileOpen.DefaultExt = "*.tlog";
            

            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                txtLog.Text = dlgFileOpen.FileName;
                Properties.Settings.Default.lastMissionLog = txtLog.Text;
                Properties.Settings.Default.Save();
            }
        }        

        private void chkLiveMission_Click(object sender, EventArgs e)
        {
            txtLog.Enabled = !chkLiveMission.Checked;
            lblVideoFile.Enabled = !chkLiveMission.Checked;
            lblLogFile.Enabled = !chkLiveMission.Checked;
            cmdBrowseLog.Enabled = !chkLiveMission.Checked;
            cmdBrowseVideo.Enabled = !chkLiveMission.Checked;
            txtVideo.Enabled = !chkLiveMission.Checked;
            chkUploadPoints.Enabled = chkLiveMission.Checked;
            IsLive = chkLiveMission.Checked;

            if (IsLive)
            {
                txtLog.Text = "";
                txtVideo.Text = "";
            }
            else
            {
                txtLog.Text = Properties.Settings.Default.lastMissionLog;
                txtVideo.Text = Properties.Settings.Default.lastMissionMovie;
            }
        }


    }
}

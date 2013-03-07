using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeaScanUAV
{
    public enum UploadType
    {
        Cancel = 0,
        SaveMission = 1,
        UploadMission = 2
    };

    public partial class frmUploadMission : Form
    {
        protected Mission mission;        
        public UploadType MissionUploadResult { get; set; }

        public frmUploadMission(Mission mission)
        {
            this.mission = mission;
            InitializeComponent();
            grdMissionSummary.SelectedObject = mission;
            MissionUploadResult = UploadType.Cancel;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            MissionUploadResult = UploadType.SaveMission;
        }

        private void cmdUpload_Click(object sender, EventArgs e)
        {
            MissionUploadResult = UploadType.UploadMission;
        }       

    }
}

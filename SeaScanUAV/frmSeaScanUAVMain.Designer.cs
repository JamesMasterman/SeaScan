namespace SeaScanUAV
{
    partial class frmSeaScanUAVMain
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
            System.Windows.Forms.RibbonTab rtVideo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeaScanUAVMain));
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.cmdRewind = new System.Windows.Forms.RibbonButton();
            this.cmdPause = new System.Windows.Forms.RibbonButton();
            this.cmdStop = new System.Windows.Forms.RibbonButton();
            this.cmdPlay = new System.Windows.Forms.RibbonButton();
            this.cmdFwd = new System.Windows.Forms.RibbonButton();
            this.cmdRecord = new System.Windows.Forms.RibbonButton();
            this.udFrameRate = new System.Windows.Forms.RibbonUpDown();
            this.cmdDeinterlace = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel6 = new System.Windows.Forms.RibbonPanel();
            this.udZoom = new System.Windows.Forms.RibbonUpDown();
            this.spBackground = new System.Windows.Forms.SplitContainer();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.rtMission = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.cmdStartMission = new System.Windows.Forms.RibbonButton();
            this.cmdResetStart = new System.Windows.Forms.RibbonButton();
            this.cmdPauseMission = new System.Windows.Forms.RibbonButton();
            this.cmdStopMission = new System.Windows.Forms.RibbonButton();
            this.airLabel = new System.Windows.Forms.RibbonLabel();
            this.airHost = new System.Windows.Forms.RibbonHost();
            this.cbAircraft = new System.Windows.Forms.ComboBox();
            this.camLabel = new System.Windows.Forms.RibbonLabel();
            this.camHost = new System.Windows.Forms.RibbonHost();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.locLabel = new System.Windows.Forms.RibbonLabel();
            this.locHost = new System.Windows.Forms.RibbonHost();
            this.cbLocations = new System.Windows.Forms.ComboBox();
            this.usrLabel = new System.Windows.Forms.RibbonLabel();
            this.usrHost = new System.Windows.Forms.RibbonHost();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.cmdSharkDetected = new System.Windows.Forms.RibbonButton();
            this.cmdWhaleDetected = new System.Windows.Forms.RibbonButton();
            this.cmdDolphinDetected = new System.Windows.Forms.RibbonButton();
            this.cmdSealDetected = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel8 = new System.Windows.Forms.RibbonPanel();
            this.rtTraining = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.cmdAddTrainingFrame = new System.Windows.Forms.RibbonButton();
            this.cmdRemoveTrainingFrame = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel7 = new System.Windows.Forms.RibbonPanel();
            this.rtSettings = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel9 = new System.Windows.Forms.RibbonPanel();
            this.cmdSettings = new System.Windows.Forms.RibbonButton();
            this.spImageCapture = new System.Windows.Forms.SplitContainer();
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tbVideo = new System.Windows.Forms.TabPage();
            this.imgCapture = new Emgu.CV.UI.ImageBox();
            this.mpMissionMap = new GMap.NET.WindowsForms.GMapControl();
            this.tbTraining = new System.Windows.Forms.TabPage();
            this.lblMavLinkPosition = new System.Windows.Forms.Label();
            this.tbMavLink = new System.Windows.Forms.TrackBar();
            this.chkSynchroniseVideo = new System.Windows.Forms.CheckBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.pictRecording = new System.Windows.Forms.PictureBox();
            this.lblMovieProgress = new System.Windows.Forms.Label();
            this.gbTrainingSet = new System.Windows.Forms.GroupBox();
            this.cmdPreviousFrame = new System.Windows.Forms.Button();
            this.cmdNextFrame = new System.Windows.Forms.Button();
            this.imgTrainingFrames = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrameCount = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.cboSource = new System.Windows.Forms.ComboBox();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblMissionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMovieStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLblBufferSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.tbFramePosition = new System.Windows.Forms.TrackBar();
            this.lblVideoTime = new System.Windows.Forms.Label();
            this.imgTraining = new SeaScanUAV.ROISelectorImageBox();
            rtVideo = new System.Windows.Forms.RibbonTab();
            ((System.ComponentModel.ISupportInitialize)(this.spBackground)).BeginInit();
            this.spBackground.Panel1.SuspendLayout();
            this.spBackground.Panel2.SuspendLayout();
            this.spBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spImageCapture)).BeginInit();
            this.spImageCapture.Panel1.SuspendLayout();
            this.spImageCapture.Panel2.SuspendLayout();
            this.spImageCapture.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.tbVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).BeginInit();
            this.tbTraining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMavLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictRecording)).BeginInit();
            this.gbTrainingSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgTrainingFrames)).BeginInit();
            this.ssMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFramePosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTraining)).BeginInit();
            this.SuspendLayout();
            // 
            // rtVideo
            // 
            rtVideo.Panels.Add(this.ribbonPanel1);
            rtVideo.Tag = null;
            rtVideo.Text = "Video";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.cmdRewind);
            this.ribbonPanel1.Items.Add(this.cmdPause);
            this.ribbonPanel1.Items.Add(this.cmdStop);
            this.ribbonPanel1.Items.Add(this.cmdPlay);
            this.ribbonPanel1.Items.Add(this.cmdFwd);
            this.ribbonPanel1.Items.Add(this.cmdRecord);
            this.ribbonPanel1.Items.Add(this.udFrameRate);
            this.ribbonPanel1.Items.Add(this.cmdDeinterlace);
            this.ribbonPanel1.Tag = null;
            this.ribbonPanel1.Text = "Video Control";
            // 
            // cmdRewind
            // 
            this.cmdRewind.AltKey = null;
            this.cmdRewind.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdRewind.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdRewind.Enabled = false;
            this.cmdRewind.Image = ((System.Drawing.Image)(resources.GetObject("cmdRewind.Image")));
            this.cmdRewind.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdRewind.SmallImage")));
            this.cmdRewind.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdRewind.Tag = null;
            this.cmdRewind.Text = null;
            this.cmdRewind.ToolTip = null;
            this.cmdRewind.ToolTipImage = null;
            this.cmdRewind.ToolTipTitle = null;
            this.cmdRewind.Value = null;
            this.cmdRewind.Click += new System.EventHandler(this.cmdRewind_Click);
            // 
            // cmdPause
            // 
            this.cmdPause.AltKey = null;
            this.cmdPause.CheckOnClick = true;
            this.cmdPause.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdPause.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdPause.Enabled = false;
            this.cmdPause.Image = ((System.Drawing.Image)(resources.GetObject("cmdPause.Image")));
            this.cmdPause.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdPause.SmallImage")));
            this.cmdPause.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdPause.Tag = null;
            this.cmdPause.Text = null;
            this.cmdPause.ToolTip = null;
            this.cmdPause.ToolTipImage = null;
            this.cmdPause.ToolTipTitle = null;
            this.cmdPause.Value = null;
            this.cmdPause.Click += new System.EventHandler(this.cmdPause_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.AltKey = null;
            this.cmdStop.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdStop.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdStop.Enabled = false;
            this.cmdStop.Image = ((System.Drawing.Image)(resources.GetObject("cmdStop.Image")));
            this.cmdStop.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStop.SmallImage")));
            this.cmdStop.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdStop.Tag = null;
            this.cmdStop.Text = null;
            this.cmdStop.ToolTip = null;
            this.cmdStop.ToolTipImage = null;
            this.cmdStop.ToolTipTitle = null;
            this.cmdStop.Value = null;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdPlay
            // 
            this.cmdPlay.AltKey = null;
            this.cmdPlay.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdPlay.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdPlay.Enabled = false;
            this.cmdPlay.Image = ((System.Drawing.Image)(resources.GetObject("cmdPlay.Image")));
            this.cmdPlay.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdPlay.SmallImage")));
            this.cmdPlay.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdPlay.Tag = null;
            this.cmdPlay.Text = null;
            this.cmdPlay.ToolTip = null;
            this.cmdPlay.ToolTipImage = null;
            this.cmdPlay.ToolTipTitle = null;
            this.cmdPlay.Value = null;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdFwd
            // 
            this.cmdFwd.AltKey = null;
            this.cmdFwd.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdFwd.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdFwd.Enabled = false;
            this.cmdFwd.Image = ((System.Drawing.Image)(resources.GetObject("cmdFwd.Image")));
            this.cmdFwd.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdFwd.SmallImage")));
            this.cmdFwd.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdFwd.Tag = null;
            this.cmdFwd.Text = null;
            this.cmdFwd.ToolTip = null;
            this.cmdFwd.ToolTipImage = null;
            this.cmdFwd.ToolTipTitle = null;
            this.cmdFwd.Value = null;
            this.cmdFwd.Click += new System.EventHandler(this.cmdFwd_Click);
            // 
            // cmdRecord
            // 
            this.cmdRecord.AltKey = null;
            this.cmdRecord.CheckOnClick = true;
            this.cmdRecord.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdRecord.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdRecord.Enabled = false;
            this.cmdRecord.Image = ((System.Drawing.Image)(resources.GetObject("cmdRecord.Image")));
            this.cmdRecord.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdRecord.SmallImage")));
            this.cmdRecord.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdRecord.Tag = null;
            this.cmdRecord.Text = null;
            this.cmdRecord.ToolTip = null;
            this.cmdRecord.ToolTipImage = null;
            this.cmdRecord.ToolTipTitle = null;
            this.cmdRecord.Value = null;
            this.cmdRecord.Click += new System.EventHandler(this.cmdRecord_Click);
            // 
            // udFrameRate
            // 
            this.udFrameRate.AltKey = null;
            this.udFrameRate.Image = null;
            this.udFrameRate.Tag = null;
            this.udFrameRate.Text = "Frame Rate";
            this.udFrameRate.TextBoxText = "25";
            this.udFrameRate.TextBoxWidth = 50;
            this.udFrameRate.ToolTip = null;
            this.udFrameRate.ToolTipImage = null;
            this.udFrameRate.ToolTipTitle = null;
            this.udFrameRate.Value = null;
            this.udFrameRate.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.udFrameRate_UpButtonClicked);
            this.udFrameRate.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.udFrameRate_DownButtonClicked);
            this.udFrameRate.TextBoxTextChanged += new System.EventHandler(this.udFrameRate_TextBoxTextChanged);
            // 
            // cmdDeinterlace
            // 
            this.cmdDeinterlace.AltKey = null;
            this.cmdDeinterlace.Checked = true;
            this.cmdDeinterlace.CheckOnClick = true;
            this.cmdDeinterlace.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdDeinterlace.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdDeinterlace.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeinterlace.Image")));
            this.cmdDeinterlace.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdDeinterlace.SmallImage")));
            this.cmdDeinterlace.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdDeinterlace.Tag = null;
            this.cmdDeinterlace.Text = "Deinterlace";
            this.cmdDeinterlace.ToolTip = null;
            this.cmdDeinterlace.ToolTipImage = null;
            this.cmdDeinterlace.ToolTipTitle = null;
            this.cmdDeinterlace.Value = null;
            this.cmdDeinterlace.Click += new System.EventHandler(this.cmdDeinterlace_Click);
            // 
            // ribbonPanel6
            // 
            this.ribbonPanel6.Items.Add(this.udZoom);
            this.ribbonPanel6.Tag = null;
            this.ribbonPanel6.Text = "Map Control";
            // 
            // udZoom
            // 
            this.udZoom.AltKey = null;
            this.udZoom.Image = ((System.Drawing.Image)(resources.GetObject("udZoom.Image")));
            this.udZoom.Tag = null;
            this.udZoom.Text = "Zoom";
            this.udZoom.TextBoxText = "2";
            this.udZoom.TextBoxWidth = 50;
            this.udZoom.ToolTip = null;
            this.udZoom.ToolTipImage = null;
            this.udZoom.ToolTipTitle = null;
            this.udZoom.Value = null;
            this.udZoom.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.udZoom_UpButtonClicked);
            this.udZoom.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.udZoom_DownButtonClicked);
            this.udZoom.TextBoxTextChanged += new System.EventHandler(this.udZoom_TextBoxTextChanged);
            // 
            // spBackground
            // 
            this.spBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spBackground.IsSplitterFixed = true;
            this.spBackground.Location = new System.Drawing.Point(0, 0);
            this.spBackground.Name = "spBackground";
            this.spBackground.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spBackground.Panel1
            // 
            this.spBackground.Panel1.Controls.Add(this.ribbon1);
            // 
            // spBackground.Panel2
            // 
            this.spBackground.Panel2.Controls.Add(this.spImageCapture);
            this.spBackground.Size = new System.Drawing.Size(1179, 699);
            this.spBackground.SplitterDistance = 141;
            this.spBackground.TabIndex = 0;
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.AltKey = null;
            this.ribbon1.QuickAcessToolbar.Image = null;
            this.ribbon1.QuickAcessToolbar.Tag = null;
            this.ribbon1.QuickAcessToolbar.Text = null;
            this.ribbon1.QuickAcessToolbar.ToolTip = null;
            this.ribbon1.QuickAcessToolbar.ToolTipImage = null;
            this.ribbon1.QuickAcessToolbar.ToolTipTitle = null;
            this.ribbon1.QuickAcessToolbar.Value = null;
            this.ribbon1.Size = new System.Drawing.Size(1179, 125);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(rtVideo);
            this.ribbon1.Tabs.Add(this.rtMission);
            this.ribbon1.Tabs.Add(this.rtTraining);
            this.ribbon1.Tabs.Add(this.rtSettings);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon1.TabSpacing = 6;
            this.ribbon1.Text = "ribbon1";
            // 
            // rtMission
            // 
            this.rtMission.Panels.Add(this.ribbonPanel4);
            this.rtMission.Panels.Add(this.ribbonPanel5);
            this.rtMission.Panels.Add(this.ribbonPanel8);
            this.rtMission.Panels.Add(this.ribbonPanel6);
            this.rtMission.Tag = null;
            this.rtMission.Text = "Mission";
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.Items.Add(this.cmdStartMission);
            this.ribbonPanel4.Items.Add(this.cmdResetStart);
            this.ribbonPanel4.Items.Add(this.cmdPauseMission);
            this.ribbonPanel4.Items.Add(this.cmdStopMission);
            this.ribbonPanel4.Items.Add(this.airLabel);
            this.ribbonPanel4.Items.Add(this.airHost);
            this.ribbonPanel4.Items.Add(this.camLabel);
            this.ribbonPanel4.Items.Add(this.camHost);
            this.ribbonPanel4.Items.Add(this.locLabel);
            this.ribbonPanel4.Items.Add(this.locHost);
            this.ribbonPanel4.Items.Add(this.usrLabel);
            this.ribbonPanel4.Items.Add(this.usrHost);
            this.ribbonPanel4.Tag = null;
            this.ribbonPanel4.Text = "Mission Control";
            // 
            // cmdStartMission
            // 
            this.cmdStartMission.AltKey = null;
            this.cmdStartMission.CheckOnClick = true;
            this.cmdStartMission.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdStartMission.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdStartMission.Image = ((System.Drawing.Image)(resources.GetObject("cmdStartMission.Image")));
            this.cmdStartMission.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStartMission.SmallImage")));
            this.cmdStartMission.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdStartMission.Tag = null;
            this.cmdStartMission.Text = "Start";
            this.cmdStartMission.ToolTip = "Start Mission";
            this.cmdStartMission.ToolTipImage = null;
            this.cmdStartMission.ToolTipTitle = null;
            this.cmdStartMission.Value = null;
            this.cmdStartMission.Click += new System.EventHandler(this.cmdStartMission_Click);
            // 
            // cmdResetStart
            // 
            this.cmdResetStart.AltKey = null;
            this.cmdResetStart.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdResetStart.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdResetStart.Enabled = false;
            this.cmdResetStart.Image = ((System.Drawing.Image)(resources.GetObject("cmdResetStart.Image")));
            this.cmdResetStart.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdResetStart.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdResetStart.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdResetStart.SmallImage")));
            this.cmdResetStart.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdResetStart.Tag = null;
            this.cmdResetStart.Text = "Reset";
            this.cmdResetStart.ToolTip = null;
            this.cmdResetStart.ToolTipImage = null;
            this.cmdResetStart.ToolTipTitle = null;
            this.cmdResetStart.Value = null;
            this.cmdResetStart.Click += new System.EventHandler(this.cmdResetStart_Click);
            // 
            // cmdPauseMission
            // 
            this.cmdPauseMission.AltKey = null;
            this.cmdPauseMission.CheckOnClick = true;
            this.cmdPauseMission.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdPauseMission.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdPauseMission.Enabled = false;
            this.cmdPauseMission.Image = ((System.Drawing.Image)(resources.GetObject("cmdPauseMission.Image")));
            this.cmdPauseMission.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdPauseMission.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdPauseMission.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdPauseMission.SmallImage")));
            this.cmdPauseMission.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdPauseMission.Tag = null;
            this.cmdPauseMission.Text = "Pause";
            this.cmdPauseMission.ToolTip = null;
            this.cmdPauseMission.ToolTipImage = null;
            this.cmdPauseMission.ToolTipTitle = null;
            this.cmdPauseMission.Value = null;
            this.cmdPauseMission.Click += new System.EventHandler(this.cmdPauseMission_Click);
            // 
            // cmdStopMission
            // 
            this.cmdStopMission.AltKey = null;
            this.cmdStopMission.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdStopMission.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdStopMission.Enabled = false;
            this.cmdStopMission.Image = ((System.Drawing.Image)(resources.GetObject("cmdStopMission.Image")));
            this.cmdStopMission.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStopMission.SmallImage")));
            this.cmdStopMission.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdStopMission.Tag = null;
            this.cmdStopMission.Text = "Stop";
            this.cmdStopMission.ToolTip = "Complete Mission";
            this.cmdStopMission.ToolTipImage = null;
            this.cmdStopMission.ToolTipTitle = null;
            this.cmdStopMission.Value = null;
            this.cmdStopMission.Click += new System.EventHandler(this.cmdStopMission_Click);
            // 
            // airLabel
            // 
            this.airLabel.AltKey = null;
            this.airLabel.Image = null;
            this.airLabel.Tag = null;
            this.airLabel.Text = "Aircraft";
            this.airLabel.ToolTip = null;
            this.airLabel.ToolTipImage = null;
            this.airLabel.ToolTipTitle = null;
            this.airLabel.Value = null;
            // 
            // airHost
            // 
            this.airHost.AltKey = null;
            this.airHost.HostedControl = this.cbAircraft;
            this.airHost.Image = null;
            this.airHost.Tag = null;
            this.airHost.Text = null;
            this.airHost.ToolTip = null;
            this.airHost.ToolTipImage = null;
            this.airHost.ToolTipTitle = null;
            this.airHost.Value = null;
            // 
            // cbAircraft
            // 
            this.cbAircraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAircraft.Location = new System.Drawing.Point(0, 0);
            this.cbAircraft.Name = "cbAircraft";
            this.cbAircraft.Size = new System.Drawing.Size(121, 21);
            this.cbAircraft.TabIndex = 1;
            // 
            // camLabel
            // 
            this.camLabel.AltKey = null;
            this.camLabel.Image = null;
            this.camLabel.Tag = null;
            this.camLabel.Text = "Camera";
            this.camLabel.ToolTip = null;
            this.camLabel.ToolTipImage = null;
            this.camLabel.ToolTipTitle = null;
            this.camLabel.Value = null;
            // 
            // camHost
            // 
            this.camHost.AltKey = null;
            this.camHost.HostedControl = this.cbCameras;
            this.camHost.Image = null;
            this.camHost.Tag = null;
            this.camHost.Text = null;
            this.camHost.ToolTip = null;
            this.camHost.ToolTipImage = null;
            this.camHost.ToolTipTitle = null;
            this.camHost.Value = null;
            // 
            // cbCameras
            // 
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.Location = new System.Drawing.Point(0, 0);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(121, 21);
            this.cbCameras.TabIndex = 2;
            // 
            // locLabel
            // 
            this.locLabel.AltKey = null;
            this.locLabel.Image = null;
            this.locLabel.Tag = null;
            this.locLabel.Text = "Location";
            this.locLabel.ToolTip = null;
            this.locLabel.ToolTipImage = null;
            this.locLabel.ToolTipTitle = null;
            this.locLabel.Value = null;
            // 
            // locHost
            // 
            this.locHost.AltKey = null;
            this.locHost.HostedControl = this.cbLocations;
            this.locHost.Image = null;
            this.locHost.Tag = null;
            this.locHost.Text = "Location: ";
            this.locHost.ToolTip = null;
            this.locHost.ToolTipImage = null;
            this.locHost.ToolTipTitle = null;
            this.locHost.Value = null;
            // 
            // cbLocations
            // 
            this.cbLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocations.Location = new System.Drawing.Point(0, 0);
            this.cbLocations.Name = "cbLocations";
            this.cbLocations.Size = new System.Drawing.Size(121, 21);
            this.cbLocations.TabIndex = 3;
            this.cbLocations.SelectedIndexChanged += new System.EventHandler(this.cbLocations_SelectedIndexChanged);
            // 
            // usrLabel
            // 
            this.usrLabel.AltKey = null;
            this.usrLabel.Image = null;
            this.usrLabel.Tag = null;
            this.usrLabel.Text = "Pilot";
            this.usrLabel.ToolTip = null;
            this.usrLabel.ToolTipImage = null;
            this.usrLabel.ToolTipTitle = null;
            this.usrLabel.Value = null;
            // 
            // usrHost
            // 
            this.usrHost.AltKey = null;
            this.usrHost.HostedControl = this.cbUsers;
            this.usrHost.Image = null;
            this.usrHost.Tag = null;
            this.usrHost.Text = "User";
            this.usrHost.ToolTip = null;
            this.usrHost.ToolTipImage = null;
            this.usrHost.ToolTipTitle = null;
            this.usrHost.Value = null;
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Location = new System.Drawing.Point(0, 0);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(121, 21);
            this.cbUsers.TabIndex = 4;
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.Items.Add(this.cmdSharkDetected);
            this.ribbonPanel5.Items.Add(this.cmdWhaleDetected);
            this.ribbonPanel5.Items.Add(this.cmdDolphinDetected);
            this.ribbonPanel5.Items.Add(this.cmdSealDetected);
            this.ribbonPanel5.Tag = null;
            this.ribbonPanel5.Text = "Target Detection";
            // 
            // cmdSharkDetected
            // 
            this.cmdSharkDetected.AltKey = null;
            this.cmdSharkDetected.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdSharkDetected.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdSharkDetected.Enabled = false;
            this.cmdSharkDetected.Image = ((System.Drawing.Image)(resources.GetObject("cmdSharkDetected.Image")));
            this.cmdSharkDetected.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdSharkDetected.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdSharkDetected.SmallImage")));
            this.cmdSharkDetected.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdSharkDetected.Tag = null;
            this.cmdSharkDetected.Text = "Shark";
            this.cmdSharkDetected.ToolTip = null;
            this.cmdSharkDetected.ToolTipImage = null;
            this.cmdSharkDetected.ToolTipTitle = null;
            this.cmdSharkDetected.Value = null;
            this.cmdSharkDetected.Click += new System.EventHandler(this.cmdSharkDetected_Click);
            // 
            // cmdWhaleDetected
            // 
            this.cmdWhaleDetected.AltKey = null;
            this.cmdWhaleDetected.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdWhaleDetected.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdWhaleDetected.Enabled = false;
            this.cmdWhaleDetected.Image = ((System.Drawing.Image)(resources.GetObject("cmdWhaleDetected.Image")));
            this.cmdWhaleDetected.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdWhaleDetected.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdWhaleDetected.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdWhaleDetected.SmallImage")));
            this.cmdWhaleDetected.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdWhaleDetected.Tag = null;
            this.cmdWhaleDetected.Text = "Whale";
            this.cmdWhaleDetected.ToolTip = null;
            this.cmdWhaleDetected.ToolTipImage = null;
            this.cmdWhaleDetected.ToolTipTitle = null;
            this.cmdWhaleDetected.Value = null;
            this.cmdWhaleDetected.Click += new System.EventHandler(this.cmdWhaleDetected_Click);
            // 
            // cmdDolphinDetected
            // 
            this.cmdDolphinDetected.AltKey = null;
            this.cmdDolphinDetected.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdDolphinDetected.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdDolphinDetected.Enabled = false;
            this.cmdDolphinDetected.Image = ((System.Drawing.Image)(resources.GetObject("cmdDolphinDetected.Image")));
            this.cmdDolphinDetected.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdDolphinDetected.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdDolphinDetected.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdDolphinDetected.SmallImage")));
            this.cmdDolphinDetected.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdDolphinDetected.Tag = null;
            this.cmdDolphinDetected.Text = "Dolphin";
            this.cmdDolphinDetected.ToolTip = null;
            this.cmdDolphinDetected.ToolTipImage = null;
            this.cmdDolphinDetected.ToolTipTitle = null;
            this.cmdDolphinDetected.Value = null;
            this.cmdDolphinDetected.Click += new System.EventHandler(this.cmdDolphinDetected_Click);
            // 
            // cmdSealDetected
            // 
            this.cmdSealDetected.AltKey = null;
            this.cmdSealDetected.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdSealDetected.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdSealDetected.Enabled = false;
            this.cmdSealDetected.Image = ((System.Drawing.Image)(resources.GetObject("cmdSealDetected.Image")));
            this.cmdSealDetected.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdSealDetected.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdSealDetected.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdSealDetected.SmallImage")));
            this.cmdSealDetected.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdSealDetected.Tag = null;
            this.cmdSealDetected.Text = "Seal";
            this.cmdSealDetected.ToolTip = null;
            this.cmdSealDetected.ToolTipImage = null;
            this.cmdSealDetected.ToolTipTitle = null;
            this.cmdSealDetected.Value = null;
            this.cmdSealDetected.Click += new System.EventHandler(this.cmdSealDetected_Click);
            // 
            // ribbonPanel8
            // 
            this.ribbonPanel8.Tag = null;
            this.ribbonPanel8.Text = "Maintain Database";
            // 
            // rtTraining
            // 
            this.rtTraining.Panels.Add(this.ribbonPanel2);
            this.rtTraining.Panels.Add(this.ribbonPanel7);
            this.rtTraining.Tag = null;
            this.rtTraining.Text = "Training";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.cmdAddTrainingFrame);
            this.ribbonPanel2.Items.Add(this.cmdRemoveTrainingFrame);
            this.ribbonPanel2.Tag = null;
            this.ribbonPanel2.Text = "Training Image Set";
            // 
            // cmdAddTrainingFrame
            // 
            this.cmdAddTrainingFrame.AltKey = null;
            this.cmdAddTrainingFrame.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdAddTrainingFrame.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdAddTrainingFrame.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddTrainingFrame.Image")));
            this.cmdAddTrainingFrame.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdAddTrainingFrame.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdAddTrainingFrame.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdAddTrainingFrame.SmallImage")));
            this.cmdAddTrainingFrame.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdAddTrainingFrame.Tag = null;
            this.cmdAddTrainingFrame.Text = null;
            this.cmdAddTrainingFrame.ToolTip = null;
            this.cmdAddTrainingFrame.ToolTipImage = null;
            this.cmdAddTrainingFrame.ToolTipTitle = null;
            this.cmdAddTrainingFrame.Value = null;
            this.cmdAddTrainingFrame.Click += new System.EventHandler(this.cmdAddTrainingFrame_Click);
            // 
            // cmdRemoveTrainingFrame
            // 
            this.cmdRemoveTrainingFrame.AltKey = null;
            this.cmdRemoveTrainingFrame.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdRemoveTrainingFrame.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdRemoveTrainingFrame.Image = ((System.Drawing.Image)(resources.GetObject("cmdRemoveTrainingFrame.Image")));
            this.cmdRemoveTrainingFrame.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdRemoveTrainingFrame.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.cmdRemoveTrainingFrame.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdRemoveTrainingFrame.SmallImage")));
            this.cmdRemoveTrainingFrame.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdRemoveTrainingFrame.Tag = null;
            this.cmdRemoveTrainingFrame.Text = null;
            this.cmdRemoveTrainingFrame.ToolTip = null;
            this.cmdRemoveTrainingFrame.ToolTipImage = null;
            this.cmdRemoveTrainingFrame.ToolTipTitle = null;
            this.cmdRemoveTrainingFrame.Value = null;
            this.cmdRemoveTrainingFrame.Click += new System.EventHandler(this.cmdRemoveTrainingFrame_Click);
            // 
            // ribbonPanel7
            // 
            this.ribbonPanel7.Tag = null;
            this.ribbonPanel7.Text = "Training";
            // 
            // rtSettings
            // 
            this.rtSettings.Panels.Add(this.ribbonPanel9);
            this.rtSettings.Tag = null;
            this.rtSettings.Text = "Settings";
            // 
            // ribbonPanel9
            // 
            this.ribbonPanel9.Items.Add(this.cmdSettings);
            this.ribbonPanel9.Tag = null;
            this.ribbonPanel9.Text = "Settings";
            // 
            // cmdSettings
            // 
            this.cmdSettings.AltKey = null;
            this.cmdSettings.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.cmdSettings.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.cmdSettings.Image = ((System.Drawing.Image)(resources.GetObject("cmdSettings.Image")));
            this.cmdSettings.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdSettings.SmallImage")));
            this.cmdSettings.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.cmdSettings.Tag = null;
            this.cmdSettings.Text = null;
            this.cmdSettings.ToolTip = "Shark Scan Settings";
            this.cmdSettings.ToolTipImage = null;
            this.cmdSettings.ToolTipTitle = null;
            this.cmdSettings.Value = null;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // spImageCapture
            // 
            this.spImageCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spImageCapture.Location = new System.Drawing.Point(0, 0);
            this.spImageCapture.Name = "spImageCapture";
            // 
            // spImageCapture.Panel1
            // 
            this.spImageCapture.Panel1.Controls.Add(this.tbMain);
            // 
            // spImageCapture.Panel2
            // 
            this.spImageCapture.Panel2.Controls.Add(this.lblMovieProgress);
            this.spImageCapture.Panel2.Controls.Add(this.lblVideoTime);
            this.spImageCapture.Panel2.Controls.Add(this.tbFramePosition);
            this.spImageCapture.Panel2.Controls.Add(this.lblMavLinkPosition);
            this.spImageCapture.Panel2.Controls.Add(this.tbMavLink);
            this.spImageCapture.Panel2.Controls.Add(this.chkSynchroniseVideo);
            this.spImageCapture.Panel2.Controls.Add(this.lblWidth);
            this.spImageCapture.Panel2.Controls.Add(this.lblHeight);
            this.spImageCapture.Panel2.Controls.Add(this.lblElapsedTime);
            this.spImageCapture.Panel2.Controls.Add(this.pictRecording);
            this.spImageCapture.Panel2.Controls.Add(this.gbTrainingSet);
            this.spImageCapture.Panel2.Controls.Add(this.lblSource);
            this.spImageCapture.Panel2.Controls.Add(this.cboSource);
            this.spImageCapture.Size = new System.Drawing.Size(1179, 554);
            this.spImageCapture.SplitterDistance = 979;
            this.spImageCapture.TabIndex = 0;
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.tbVideo);
            this.tbMain.Controls.Add(this.tbTraining);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(979, 554);
            this.tbMain.TabIndex = 0;
            this.tbMain.SelectedIndexChanged += new System.EventHandler(this.tbMain_SelectedIndexChanged);
            // 
            // tbVideo
            // 
            this.tbVideo.Controls.Add(this.imgCapture);
            this.tbVideo.Controls.Add(this.mpMissionMap);
            this.tbVideo.Location = new System.Drawing.Point(4, 22);
            this.tbVideo.Name = "tbVideo";
            this.tbVideo.Padding = new System.Windows.Forms.Padding(3);
            this.tbVideo.Size = new System.Drawing.Size(971, 528);
            this.tbVideo.TabIndex = 0;
            this.tbVideo.Text = "Mission";
            this.tbVideo.UseVisualStyleBackColor = true;
            // 
            // imgCapture
            // 
            this.imgCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCapture.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgCapture.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imgCapture.Location = new System.Drawing.Point(1, 1);
            this.imgCapture.Name = "imgCapture";
            this.imgCapture.Size = new System.Drawing.Size(188, 141);
            this.imgCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCapture.TabIndex = 2;
            this.imgCapture.TabStop = false;
            // 
            // mpMissionMap
            // 
            this.mpMissionMap.Bearing = 0F;
            this.mpMissionMap.CanDragMap = true;
            this.mpMissionMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMissionMap.GrayScaleMode = false;
            this.mpMissionMap.LevelsKeepInMemmory = 5;
            this.mpMissionMap.Location = new System.Drawing.Point(3, 3);
            this.mpMissionMap.MarkersEnabled = true;
            this.mpMissionMap.MaxZoom = 20;
            this.mpMissionMap.MinZoom = 0;
            this.mpMissionMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.ViewCenter;
            this.mpMissionMap.Name = "mpMissionMap";
            this.mpMissionMap.NegativeMode = false;
            this.mpMissionMap.PolygonsEnabled = true;
            this.mpMissionMap.RetryLoadTile = 0;
            this.mpMissionMap.RoutesEnabled = true;
            this.mpMissionMap.ShowTileGridLines = false;
            this.mpMissionMap.Size = new System.Drawing.Size(965, 522);
            this.mpMissionMap.TabIndex = 3;
            this.mpMissionMap.Zoom = 2D;
            this.mpMissionMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.mpMissionMap_OnMapZoomChanged);
            // 
            // tbTraining
            // 
            this.tbTraining.Controls.Add(this.imgTraining);
            this.tbTraining.Location = new System.Drawing.Point(4, 22);
            this.tbTraining.Name = "tbTraining";
            this.tbTraining.Padding = new System.Windows.Forms.Padding(3);
            this.tbTraining.Size = new System.Drawing.Size(971, 528);
            this.tbTraining.TabIndex = 1;
            this.tbTraining.Text = "Image Training";
            this.tbTraining.UseVisualStyleBackColor = true;
            // 
            // lblMavLinkPosition
            // 
            this.lblMavLinkPosition.AutoSize = true;
            this.lblMavLinkPosition.Location = new System.Drawing.Point(12, 163);
            this.lblMavLinkPosition.Name = "lblMavLinkPosition";
            this.lblMavLinkPosition.Size = new System.Drawing.Size(70, 13);
            this.lblMavLinkPosition.TabIndex = 28;
            this.lblMavLinkPosition.Text = "Mavlink Time";
            // 
            // tbMavLink
            // 
            this.tbMavLink.Location = new System.Drawing.Point(5, 140);
            this.tbMavLink.Name = "tbMavLink";
            this.tbMavLink.Size = new System.Drawing.Size(161, 45);
            this.tbMavLink.TabIndex = 27;
            this.tbMavLink.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMavLink.ValueChanged += new System.EventHandler(this.tbMavLink_ValueChanged);
            this.tbMavLink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbMavLink_MouseDown);
            this.tbMavLink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbMavLink_MouseUp);
            // 
            // chkSynchroniseVideo
            // 
            this.chkSynchroniseVideo.AutoSize = true;
            this.chkSynchroniseVideo.Checked = true;
            this.chkSynchroniseVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSynchroniseVideo.Enabled = false;
            this.chkSynchroniseVideo.Location = new System.Drawing.Point(5, 199);
            this.chkSynchroniseVideo.Name = "chkSynchroniseVideo";
            this.chkSynchroniseVideo.Size = new System.Drawing.Size(180, 17);
            this.chkSynchroniseVideo.TabIndex = 24;
            this.chkSynchroniseVideo.Text = "Synchronise Video with MavLink";
            this.chkSynchroniseVideo.UseVisualStyleBackColor = true;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(12, 505);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(70, 13);
            this.lblWidth.TabIndex = 21;
            this.lblWidth.Text = "Image Width:";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 484);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(73, 13);
            this.lblHeight.TabIndex = 20;
            this.lblHeight.Text = "Image Height:";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Location = new System.Drawing.Point(12, 19);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(33, 13);
            this.lblElapsedTime.TabIndex = 19;
            this.lblElapsedTime.Text = "Time:";
            // 
            // pictRecording
            // 
            this.pictRecording.Image = ((System.Drawing.Image)(resources.GetObject("pictRecording.Image")));
            this.pictRecording.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictRecording.InitialImage")));
            this.pictRecording.Location = new System.Drawing.Point(162, 0);
            this.pictRecording.Name = "pictRecording";
            this.pictRecording.Size = new System.Drawing.Size(32, 32);
            this.pictRecording.TabIndex = 18;
            this.pictRecording.TabStop = false;
            this.pictRecording.Visible = false;
            // 
            // lblMovieProgress
            // 
            this.lblMovieProgress.AutoSize = true;
            this.lblMovieProgress.Enabled = false;
            this.lblMovieProgress.Location = new System.Drawing.Point(78, 105);
            this.lblMovieProgress.Name = "lblMovieProgress";
            this.lblMovieProgress.Size = new System.Drawing.Size(0, 13);
            this.lblMovieProgress.TabIndex = 17;
            // 
            // gbTrainingSet
            // 
            this.gbTrainingSet.Controls.Add(this.cmdPreviousFrame);
            this.gbTrainingSet.Controls.Add(this.cmdNextFrame);
            this.gbTrainingSet.Controls.Add(this.imgTrainingFrames);
            this.gbTrainingSet.Controls.Add(this.label1);
            this.gbTrainingSet.Controls.Add(this.txtFrameCount);
            this.gbTrainingSet.Location = new System.Drawing.Point(3, 222);
            this.gbTrainingSet.Name = "gbTrainingSet";
            this.gbTrainingSet.Size = new System.Drawing.Size(182, 259);
            this.gbTrainingSet.TabIndex = 16;
            this.gbTrainingSet.TabStop = false;
            this.gbTrainingSet.Text = "Training Set";
            // 
            // cmdPreviousFrame
            // 
            this.cmdPreviousFrame.Location = new System.Drawing.Point(10, 231);
            this.cmdPreviousFrame.Name = "cmdPreviousFrame";
            this.cmdPreviousFrame.Size = new System.Drawing.Size(26, 25);
            this.cmdPreviousFrame.TabIndex = 21;
            this.cmdPreviousFrame.Text = "<";
            this.cmdPreviousFrame.UseVisualStyleBackColor = true;
            this.cmdPreviousFrame.Click += new System.EventHandler(this.cmdPreviousFrame_Click);
            // 
            // cmdNextFrame
            // 
            this.cmdNextFrame.Location = new System.Drawing.Point(141, 231);
            this.cmdNextFrame.Name = "cmdNextFrame";
            this.cmdNextFrame.Size = new System.Drawing.Size(26, 25);
            this.cmdNextFrame.TabIndex = 20;
            this.cmdNextFrame.Text = ">";
            this.cmdNextFrame.UseVisualStyleBackColor = true;
            this.cmdNextFrame.Click += new System.EventHandler(this.cmdNextFrame_Click);
            // 
            // imgTrainingFrames
            // 
            this.imgTrainingFrames.Location = new System.Drawing.Point(9, 30);
            this.imgTrainingFrames.Name = "imgTrainingFrames";
            this.imgTrainingFrames.Size = new System.Drawing.Size(167, 189);
            this.imgTrainingFrames.TabIndex = 19;
            this.imgTrainingFrames.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Frames";
            // 
            // txtFrameCount
            // 
            this.txtFrameCount.Location = new System.Drawing.Point(86, 234);
            this.txtFrameCount.Name = "txtFrameCount";
            this.txtFrameCount.Size = new System.Drawing.Size(39, 20);
            this.txtFrameCount.TabIndex = 17;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(12, 39);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(44, 13);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "Source:";
            // 
            // cboSource
            // 
            this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSource.FormattingEnabled = true;
            this.cboSource.Items.AddRange(new object[] {
            "Browse for file...",
            "Video Device"});
            this.cboSource.Location = new System.Drawing.Point(13, 55);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(164, 21);
            this.cboSource.TabIndex = 3;
            this.cboSource.SelectedIndexChanged += new System.EventHandler(this.cboSource_SelectedIndexChanged);
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.FileName = "*.mpg";
            this.dlgFileOpen.Filter = "All Files|*.*|MPEG Files (*.mpg)|*.mpg|Avi Files (*.avi)|*.avi\"";
            this.dlgFileOpen.FilterIndex = 2;
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.FileName = "*.mpg";
            this.dlgFileSave.Filter = "Video Files|*.mpg";
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMissionStatus,
            this.lblMovieStatus,
            this.tsLblBufferSize});
            this.ssMain.Location = new System.Drawing.Point(0, 677);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(1179, 22);
            this.ssMain.TabIndex = 5;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblMissionStatus
            // 
            this.lblMissionStatus.Name = "lblMissionStatus";
            this.lblMissionStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lblMovieStatus
            // 
            this.lblMovieStatus.Name = "lblMovieStatus";
            this.lblMovieStatus.Size = new System.Drawing.Size(29, 17);
            this.lblMovieStatus.Text = "FPS:";
            // 
            // tsLblBufferSize
            // 
            this.tsLblBufferSize.Name = "tsLblBufferSize";
            this.tsLblBufferSize.Size = new System.Drawing.Size(0, 17);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Tag = null;
            this.ribbonPanel3.Text = null;
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.AltKey = null;
            this.ribbonButton1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down;
            this.ribbonButton1.DropDownArrowSize = new System.Drawing.Size(5, 3);
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Style = System.Windows.Forms.RibbonButtonStyle.Normal;
            this.ribbonButton1.Tag = null;
            this.ribbonButton1.Text = null;
            this.ribbonButton1.ToolTip = null;
            this.ribbonButton1.ToolTipImage = null;
            this.ribbonButton1.ToolTipTitle = null;
            this.ribbonButton1.Value = null;
            // 
            // tbFramePosition
            // 
            this.tbFramePosition.Location = new System.Drawing.Point(5, 82);
            this.tbFramePosition.Name = "tbFramePosition";
            this.tbFramePosition.Size = new System.Drawing.Size(161, 45);
            this.tbFramePosition.TabIndex = 29;
            this.tbFramePosition.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbFramePosition.Scroll += new System.EventHandler(this.tbFramePosition_Scroll);
            this.tbFramePosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbFramePosition_MouseDown);
            this.tbFramePosition.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbFramePosition_MouseUp);
            // 
            // lblVideoTime
            // 
            this.lblVideoTime.AutoSize = true;
            this.lblVideoTime.Location = new System.Drawing.Point(12, 105);
            this.lblVideoTime.Name = "lblVideoTime";
            this.lblVideoTime.Size = new System.Drawing.Size(60, 13);
            this.lblVideoTime.TabIndex = 30;
            this.lblVideoTime.Text = "Video Time";
            // 
            // imgTraining
            // 
            this.imgTraining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgTraining.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgTraining.Location = new System.Drawing.Point(3, 3);
            this.imgTraining.Name = "imgTraining";
            this.imgTraining.Size = new System.Drawing.Size(965, 522);
            this.imgTraining.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgTraining.TabIndex = 2;
            this.imgTraining.TabStop = false;
            this.imgTraining.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgTraining_MouseDown);
            this.imgTraining.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgTraining_MouseMove);
            this.imgTraining.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgTraining_MouseUp);
            // 
            // frmSeaScanUAVMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 699);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.spBackground);
            this.Controls.Add(this.cbAircraft);
            this.Controls.Add(this.cbCameras);
            this.Controls.Add(this.cbLocations);
            this.Controls.Add(this.cbUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSeaScanUAVMain";
            this.Text = "SeaScan - UAV";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUAVVisionMain_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmUAVVisionMain_SizeChanged);
            this.spBackground.Panel1.ResumeLayout(false);
            this.spBackground.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spBackground)).EndInit();
            this.spBackground.ResumeLayout(false);
            this.spImageCapture.Panel1.ResumeLayout(false);
            this.spImageCapture.Panel2.ResumeLayout(false);
            this.spImageCapture.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spImageCapture)).EndInit();
            this.spImageCapture.ResumeLayout(false);
            this.tbMain.ResumeLayout(false);
            this.tbVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).EndInit();
            this.tbTraining.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbMavLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictRecording)).EndInit();
            this.gbTrainingSet.ResumeLayout(false);
            this.gbTrainingSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgTrainingFrames)).EndInit();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFramePosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTraining)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spBackground;
        private System.Windows.Forms.SplitContainer spImageCapture;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonTab rtTraining;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton cmdRewind;
        private System.Windows.Forms.RibbonButton cmdFwd;
        private System.Windows.Forms.RibbonButton cmdPause;
        private System.Windows.Forms.RibbonButton cmdStop;
        private System.Windows.Forms.RibbonButton cmdPlay;
        private System.Windows.Forms.RibbonButton cmdRecord;
        private System.Windows.Forms.RibbonButton cmdAddTrainingFrame;
        private System.Windows.Forms.RibbonButton cmdRemoveTrainingFrame;
        private System.Windows.Forms.RibbonButton cmdDeinterlace;

        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tbVideo;
        private System.Windows.Forms.TabPage tbTraining;
        private Emgu.CV.UI.ImageBox imgCapture;
        private ROISelectorImageBox imgTraining;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.GroupBox gbTrainingSet;
        private System.Windows.Forms.Button cmdPreviousFrame;
        private System.Windows.Forms.Button cmdNextFrame;
        private Emgu.CV.UI.PanAndZoomPictureBox imgTrainingFrames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrameCount;
        private System.Windows.Forms.RibbonTab rtMission;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        private System.Windows.Forms.RibbonPanel ribbonPanel6;
        private GMap.NET.WindowsForms.GMapControl mpMissionMap;
        private System.Windows.Forms.RibbonPanel ribbonPanel7;
        private System.Windows.Forms.RibbonUpDown udZoom;
        private System.Windows.Forms.Label lblMovieProgress;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.RibbonPanel ribbonPanel8;
        private System.Windows.Forms.RibbonButton cmdStartMission;
        private System.Windows.Forms.RibbonButton cmdStopMission;
        private System.Windows.Forms.ComboBox cbLocations;
        private System.Windows.Forms.ComboBox cbAircraft;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.RibbonHost locHost;
        private System.Windows.Forms.RibbonHost airHost;
        private System.Windows.Forms.RibbonHost camHost;
        private System.Windows.Forms.RibbonHost usrHost;
        private System.Windows.Forms.RibbonLabel locLabel;
        private System.Windows.Forms.RibbonLabel airLabel;
        private System.Windows.Forms.RibbonLabel camLabel;
        private System.Windows.Forms.RibbonLabel usrLabel;
        private System.Windows.Forms.RibbonUpDown udFrameRate;
        private System.Windows.Forms.PictureBox pictRecording;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblMissionStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblMovieStatus;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.ToolStripStatusLabel tsLblBufferSize;
        private System.Windows.Forms.CheckBox chkSynchroniseVideo;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonTab rtSettings;
        private System.Windows.Forms.RibbonPanel ribbonPanel9;
        private System.Windows.Forms.RibbonButton cmdSettings;
        private System.Windows.Forms.RibbonButton cmdPauseMission;
        private System.Windows.Forms.RibbonButton cmdSharkDetected;
        private System.Windows.Forms.RibbonButton cmdWhaleDetected;
        private System.Windows.Forms.RibbonButton cmdDolphinDetected;
        private System.Windows.Forms.RibbonButton cmdSealDetected;
        private System.Windows.Forms.RibbonButton cmdResetStart;
        private System.Windows.Forms.TrackBar tbMavLink;
        private System.Windows.Forms.Label lblMavLinkPosition;
        private System.Windows.Forms.TrackBar tbFramePosition;
        private System.Windows.Forms.Label lblVideoTime;

     
    }
}


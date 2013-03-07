using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.IO;
using System.Diagnostics;

namespace SeaScanUAV
{
    public partial class frmSeaScanUAVMain : Form, IMavLinkListener, IPrecisionTimerListener, IVideoController
    {
        public const double MIN_ZOOM = 0.0;
       
        public const double MAX_ZOOM = 20.0;
        public const int MIN_FRAME_RATE = 0;
        public const int MAX_FRAME_RATE = 60;
        public const int FRAME_RATE = 25;
        public const int FRAME_HEIGHT = 480;
        public const int FRAME_WIDTH = 640;
        public const int FPS_COUNTER_FRAME_RESET_LIMIT = 10;

        protected ImageStreamController imageStream = null;
      
        protected MissionController missionControl = new MissionController();     

        protected ApplicationPropertyManager properties = new ApplicationPropertyManager();
   
        protected  Image<Bgr, Byte> currentImage = null;
        
        protected volatile bool isStopped = true;
        protected volatile bool isRecording = false;
        protected int selTrainingIndex = 1;
     
        protected int spf = 1000 / FRAME_RATE;
        protected int targetfps =  FRAME_RATE;

        protected Object lockObject = new Object();      
        
        protected DateTime videoStart= DateTime.Now;
        protected int blinkStart = 0;

        protected TargetType navTarget = null;

        protected GMapRoute route;
        protected GMapOverlay routes;
        protected GMapOverlay sampledPoints;
        protected GMapOverlay locationLimits; 
       
        protected List<PointLatLng> trackPoints = new List<PointLatLng>();
        protected List<TargetType> targetTypes = new List<TargetType>();
        protected Coordinate3D currentPosition = new Coordinate3D();

        protected PrecisionTimer renderTimer = null;
      
        protected Emgu.CV.UI.ImageBox activeImageBox=null;

        protected volatile string mavLinkText = "";
        
        protected List<Image<Gray, Byte>> trainingImageList = new List<Image<Gray, Byte>>();
        protected List<Image<Bgr, Byte>> targetImageList = new List<Image<Bgr, Byte>>();

        protected bool bReplayingSavedMission = false;

        protected delegate void UpdateImageDelegate(Image<Bgr, Byte> img, int frame, int frameCount, double fps, bool lastFrame);
        public delegate void MissionPointUpdateDelegate(LogInfo info, long currPos, long maxPos, DateTime loggedAt, TimeSpan span);

        protected int fpsFrameCounter = 0;
        protected int fpsTime = 0;

        protected float secsPerMavlinkPos = 0;

        protected long furthestMavlinkPos = 0;

        protected volatile bool mouseDown = false;
        public ImageStreamController ImageStream
        {
            get
            {
                return imageStream;
            }
        }
                    

        public frmSeaScanUAVMain()
        {
            InitializeComponent();
           
            activeImageBox = imgCapture;
            tbMain.SelectedIndex = 0;
            
            mpMissionMap.MapProvider = GMapProviders.GoogleSatelliteMap;
           
            mpMissionMap.CacheLocation = Application.StartupPath;
            mpMissionMap.Zoom = Properties.Settings.Default.initZoom;
            udZoom.TextBoxText = Properties.Settings.Default.initZoom.ToString();
                                       
            mpMissionMap.ZoomAndCenterMarkers(null);
            mpMissionMap.RoutesEnabled = true;
            routes = new GMapOverlay(mpMissionMap, "routes");

            sampledPoints = new GMapOverlay(mpMissionMap, "samples");
            locationLimits = new GMapOverlay(mpMissionMap, "location boundaries");
          

            route = new GMapRoute(trackPoints,"route");
           
            route.Stroke = new Pen(Color.FromArgb(144, Color.Blue));
            route.Stroke.Width = 4;
            route.Tag = "track";
         
            routes.Routes.Add(route);
            mpMissionMap.Overlays.Add(routes);
            mpMissionMap.Overlays.Add(sampledPoints);
            mpMissionMap.Overlays.Add(locationLimits);      


            WebServiceConsumer webService = WebServiceConsumer.GetInstance();

            List<Location> locations = webService.GetLocations("all", false, false);
            foreach(Location loc in locations)
            {
                cbLocations.Items.Add(loc);             

            }

            if (cbLocations.Items.Count > 0)
            {
                cbLocations.SelectedIndex = 0;
            }

            List<User> users = webService.GetUsers();
            foreach (User user in users)
            {
                cbUsers.Items.Add(user);
            }

            if (cbUsers.Items.Count > 0)
            {
                cbUsers.SelectedIndex = 0;
            }

            List<Camera> cameras = webService.GetCameras();
            foreach (Camera cam in cameras)
            {
                cbCameras.Items.Add(cam);
            }

            if (cbCameras.Items.Count > 0)
            {
                cbCameras.SelectedIndex = 0;
            }

            List<Airframe> planes = webService.GetAirframes();
            foreach (Airframe plane in planes)
            {
                cbAircraft.Items.Add(plane);
            }

            if (cbAircraft.Items.Count > 0)
            {
                cbAircraft.SelectedIndex = 0;
            }

            targetTypes = webService.GetTargetTypes();
            foreach (TargetType tt in targetTypes)
            {
                if (tt.ID == 1) //nav marker target
                {
                    navTarget = tt;
                    break;
                }
            }
                     
           
        }

        private void cboSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopImageStream();

            if (cboSource.SelectedIndex == 0) //browse
            {
                String fileName = "";
                if (dlgFileOpen.ShowDialog() == DialogResult.OK)
                {
                    fileName = dlgFileOpen.FileName;
                    OpenImageFile(fileName);                    
                }
            }
            else
            {
                String deviceId = cboSource.SelectedIndex.ToString();
                OpenImageCapture(int.Parse(deviceId));                    
            }

            StartImageStream();   
        }

        public void OpenImageCapture(int streamId)
        {  
            imageStream = new ImageStreamController(streamId, cmdDeinterlace.Checked);
            lblSource.Text = "Source: Streaming";
            lblElapsedTime.Visible = true;
                       
        }

        public void OpenImageFile(String fileName)
        {            
     
            lblSource.Text = "Source: " + System.IO.Path.GetFileName(fileName);
            imageStream = new ImageStreamController(fileName, cmdDeinterlace.Checked);
            lblElapsedTime.Visible = false;             
            
        }


        public bool StartImageStream()
        {
            bool retVal = false;
            if (imageStream != null && imageStream.IsEnabled)
            {
                tbFramePosition.Minimum = 0;               
                imageStream.CurrentFrame = 0;
                tbFramePosition.Maximum = imageStream.FrameCount;
                if (tbFramePosition.Maximum > 0)
                {
                    tbFramePosition.Value = 0;
                }
                              
                cmdPlay.Enabled = true;
                cmdPause.Enabled = true;
                cmdStop.Enabled = true;
                cmdRecord.Enabled = true;
                cmdFwd.Enabled = true;
                cmdRewind.Enabled = true;

                if (imageStream.FrameRate > 0)
                {
                    targetfps = (int)imageStream.FrameRate;
                    spf = 1000 / (int)imageStream.FrameRate;
                }
                else
                {
                    targetfps = FRAME_RATE;
                    spf = 1000 / FRAME_RATE;
                }

                udFrameRate.TextBoxText = targetfps.ToString("0");

                lblHeight.Text = "Image Height: " + imageStream.FrameHeight.ToString("0");
                lblWidth.Text = "Image Width: " + imageStream.FrameWidth.ToString("0");

                imageStream.StartReading();

                //wait until the reader buffer fills up
                while (imageStream.ReadBufferFullness < 99) //start with a 100% full buffer
                {
                    Thread.Sleep(100);
                }

                if(imageStream.FrameCount <= 0)
                {               
                    lblMovieProgress.Text = "Live streaming";
                    lblMovieProgress.Enabled = false;
                    tbFramePosition.Enabled = false;
                }
                else
                {                    
                    lblMovieProgress.Enabled = true;
                    tbFramePosition.Enabled = true;
                }
                lblMovieProgress.Invalidate();

                isStopped = false;
                
                cmdPause.Checked = false;
                cmdRecord.Checked = false;
                isRecording = false;
                tbFramePosition.Left = gbTrainingSet.Left;
                renderTimer = new PrecisionTimer(spf, this);
                renderTimer.Start();
               
                videoStart = DateTime.Now;

                retVal = imageStream.IsEnabled;
             
            }
            else
            {
                isStopped = true;
                
                cmdPause.Checked = false;
                cmdRecord.Checked = false;
                isRecording = true;

                cmdPlay.Enabled = false;
                cmdPause.Enabled = false;
                cmdStop.Enabled = false;
                cmdRecord.Enabled = false;
                cmdFwd.Enabled = false;
                cmdRewind.Enabled = false;
            }

            return retVal;

        }
               
        private void frmUAVVisionMain_SizeChanged(object sender, EventArgs e)
        {
            spBackground.SplitterDistance = ribbon1.Height;
            gbTrainingSet.Width = spImageCapture.Panel2.Width - 10;
            imgTrainingFrames.Width = spImageCapture.Panel2.Width - 20;          
            pictRecording.Left = spImageCapture.Left + spImageCapture.Panel2.Width - pictRecording.Width;

            tbMavLink.Width = spImageCapture.Panel2.Width - 10;
            tbFramePosition.Width = spImageCapture.Panel2.Width - 10;
        }

        private void cmdPause_Click(object sender, EventArgs e)
        {
            TogglePauseVideo(cmdPause.Checked);                        
        }

        public void TogglePauseVideo(bool pause)
        {
            if (renderTimer != null)
            {
                if (pause)
                {
                    cmdPause.Checked = true;
                    renderTimer.Paused = true;
                }
                else
                {
                    cmdPause.Checked = false;
                    renderTimer.Paused = false;
                }            
            }     
        }

        private void cmdRewind_Click(object sender, EventArgs e)
        {

        }

        private void cmdFwd_Click(object sender, EventArgs e)
        {

        }

        private void frmUAVVisionMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopImageStream();
            StopMission();
     
        }

        public void StopImageStream()
        {
            isStopped = true;            
           
            cmdPause.Checked = false;
            if (renderTimer != null)
            {
                renderTimer.Stop();
            }

            if (imageStream != null)
            {
                imageStream.StopReading();
            }

            imageStream = null;

        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            if (!isStopped)
            {
                isStopped = true;
                
                cmdPause.Checked = false;
                if (renderTimer != null)
                {
                    renderTimer.Stop();
                }

                if (imageStream != null)
                {
                    imageStream.StopReading();
                }               
            }
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {           
            if(imageStream != null)
            {
                if (isStopped && imageStream != null)
                {                    
                    StartImageStream();
                }
                else if (renderTimer != null && renderTimer.Paused)
                {
                    renderTimer.Paused = false;
                    cmdPause.Checked = false;
                }               
            }
            
        }

        private void tbFramePosition_Scroll(object sender, ScrollEventArgs e)
        {
            lblMovieProgress.Text = @"Frame " + tbFramePosition.Value.ToString() + @"/" + tbFramePosition.Maximum.ToString();
            lblMovieProgress.Invalidate();
        }


        private void tbFramePosition_MouseUp(object sender, MouseEventArgs e)
        {
            if (imageStream != null)
            {               
                imageStream.CurrentFrame = tbFramePosition.Value;
                renderTimer.Paused = false;
                cmdPause.Checked = false;        
            }
        }

        private void tbFramePosition_MouseDown(object sender, MouseEventArgs e)
        {
            if (!renderTimer.Paused && !isStopped)
            {
                renderTimer.Paused = true;
                cmdPause.Checked = true;
            }            
        }

        private void tbFramePosition_FlyOutInfo(ref string data)
        {
            data = GetTimeFromSeconds(tbFramePosition.Value / targetfps);
        }

            
     
        private void tbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbMain.SelectedIndex == 0)
            {
                activeImageBox = imgCapture;
            }
            else
            {
                activeImageBox = imgTraining;
            }
        
        }

        private String GetTimeFromSeconds(int seconds)
        {
            int mins = (seconds / 60);
            int secs = (seconds % 60);

            return mins.ToString() + ":" + secs.ToString("00");
        }
            
            
       
        private void cmdPreviousFrame_Click(object sender, EventArgs e)
        {
            if (selTrainingIndex > 1)
            {
                selTrainingIndex--;
            }

            Image<Gray,Byte> img = trainingImageList[selTrainingIndex-1];
            imgTrainingFrames.Image = new Bitmap(img.Bitmap, imgTrainingFrames.Width, imgTrainingFrames.Height);

            imgTrainingFrames.Update();
            txtFrameCount.Text = selTrainingIndex.ToString() + "/" + trainingImageList.Count.ToString();
        }

        private void cmdNextFrame_Click(object sender, EventArgs e)
        {
            if (selTrainingIndex < trainingImageList.Count)
            {
                selTrainingIndex++;
            }

            Image<Gray, Byte> img = trainingImageList[selTrainingIndex-1];
            imgTrainingFrames.Image = new Bitmap(img.Bitmap, imgTrainingFrames.Width, imgTrainingFrames.Height);

            imgTrainingFrames.Update();
            txtFrameCount.Text = selTrainingIndex.ToString() + "/" + trainingImageList.Count.ToString();
        }

        private void imgTraining_MouseDown(object sender, MouseEventArgs e)
        {
            if (renderTimer != null && renderTimer.Paused)
            {
                imgTraining.ROIMouseDown(sender, e);
            }
        }

        private void imgTraining_MouseMove(object sender, MouseEventArgs e)
        {
            if (renderTimer != null && renderTimer.Paused)
            {
                imgTraining.ROIMouseMove(sender, e);
            }
        }

        private void imgTraining_MouseUp(object sender, MouseEventArgs e)
        {
            if (renderTimer != null && renderTimer.Paused)
            {
                imgTraining.ROIMouseUp(sender, e);
            }
        }

        private void cmdAddTrainingFrame_Click(object sender, EventArgs e)
        {
            if (renderTimer != null && renderTimer.Paused)
            {
                if (currentImage != null)
                {
                    Image<Gray, Byte> grayImage = currentImage.Convert<Gray, Byte>();
                    if (imgTraining.HasValidROI)
                    {
                        float xScale = (float)currentImage.Width / (float)imgTraining.Width;
                        float yScale = (float)currentImage.Height / (float)imgTraining.Height;
                        
                        trainingImageList.Add(grayImage.Copy(imgTraining.GetScaledROI(xScale, yScale)));
                    }
                    else
                    {
                        trainingImageList.Add(grayImage.Copy());
                    }

                    imgTrainingFrames.Image = new Bitmap(trainingImageList[trainingImageList.Count - 1].Bitmap, imgTrainingFrames.Width, imgTrainingFrames.Height);

                    imgTrainingFrames.Update();
                    txtFrameCount.Text = trainingImageList.Count.ToString();
                    selTrainingIndex = trainingImageList.Count;
                }

            }
        }

        private void cmdRemoveTrainingFrame_Click(object sender, EventArgs e)
        {
            if(selTrainingIndex > 0 && selTrainingIndex < trainingImageList.Count)
            {
                trainingImageList.RemoveAt(selTrainingIndex);
                if(selTrainingIndex > trainingImageList.Count-1)
                {
                    selTrainingIndex = trainingImageList.Count - 1;
                }
            }
        }

        private void udZoom_TextBoxTextChanged(object sender, EventArgs e)
        {
            double result = mpMissionMap.Zoom;
            if (Double.TryParse(udZoom.TextBoxText, out result))
            {
                if (result >= MIN_ZOOM && result <= MAX_ZOOM)
                {
                    mpMissionMap.Zoom = result;

                }
                else if (result < MIN_ZOOM)
                {
                    udZoom.TextBoxText = MIN_ZOOM.ToString();

                }
                else if (result > MAX_ZOOM)
                {
                    udZoom.TextBoxText = MAX_ZOOM.ToString();
                }
            }
        }

        private void udZoom_DownButtonClicked(object sender, MouseEventArgs e)
        {
            double result = 0.0;
            if (Double.TryParse(udZoom.TextBoxText, out result))
            {
                if (result > (MIN_ZOOM+1.0))
                {
                    result -= 1.0;
                    udZoom.TextBoxText = result.ToString();
                }
            }
        }

        private void udZoom_UpButtonClicked(object sender, MouseEventArgs e)
        {
            double result = 0.0;
            if (Double.TryParse(udZoom.TextBoxText, out result))
            {
                if (result <= MAX_ZOOM)
                {
                    result += 1.0;
                    udZoom.TextBoxText = result.ToString();
                }
                
            }
        }

        private void cmdRecord_Click(object sender, EventArgs e)
        {
            if (imageStream != null)
            {                
                if (cmdRecord.Checked)
                {
                    string fileName = Properties.Settings.Default.moviedir + @"UAV Flight " + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
                    dlgFileSave.InitialDirectory = Properties.Settings.Default.moviedir;
                    dlgFileSave.FileName = fileName;
                    if (dlgFileSave.ShowDialog() == DialogResult.OK)
                    {

                        int nativeFPS = (int)imageStream.FrameRate;
                        if (nativeFPS == 0)
                        {
                            nativeFPS = FRAME_RATE;
                        }

                        int ht = imageStream.FrameHeight;
                        if (ht == 0)
                        {
                            ht = FRAME_HEIGHT;
                        }

                        int wd = imageStream.FrameWidth;
                        if (wd == 0)
                        {
                            wd = FRAME_WIDTH;
                        }
                                               

                        imageStream.OpenWriteStream(dlgFileSave.FileName, ImageStreamWriter.MPEG1_CODEC, nativeFPS, wd, ht, true);
                        isRecording = true;
                        blinkStart = Environment.TickCount;
                        videoStart = DateTime.Now;
                    }
                    else
                    {
                        lock(lockObject)
                        {
                            isRecording = false;
                            if (imageStream != null)
                            {
                                imageStream.StopWriting();                               
                            }
                            cmdRecord.Checked = false;
                            pictRecording.Visible = false;
                        }
                    }
                }
                else
                {
                    lock(lockObject)
                    {
                        isRecording = false;
                        if (imageStream != null)
                        {
                            imageStream.StopWriting();
                        }                        
                        pictRecording.Visible = false;
                    }
                }               

            }
            else
            {
                cmdRecord.Checked = false;
            }
        }

        private void mpMissionMap_OnMapZoomChanged()
        {
            udZoom.TextBoxTextChanged -= new EventHandler(this.udZoom_TextBoxTextChanged);
            udZoom.TextBoxText = mpMissionMap.Zoom.ToString();
            udZoom.TextBoxTextChanged += new EventHandler(this.udZoom_TextBoxTextChanged);
        }

       

        private void udFrameRate_DownButtonClicked(object sender, MouseEventArgs e)
        {
            int result = 0;
            if (int.TryParse(udFrameRate.TextBoxText, out result))
            {
                if (result > (MIN_FRAME_RATE + 1))
                {
                    result -= 1;
                    udFrameRate.TextBoxText = result.ToString();
                            
                    
                }
            }           
        }

        private void udFrameRate_UpButtonClicked(object sender, MouseEventArgs e)
        {
            int result=0;
            if (int.TryParse(udFrameRate.TextBoxText, out result))
            {
                if (result < (MAX_FRAME_RATE))
                {
                    result += 1;
                    udFrameRate.TextBoxText = result.ToString();
                                     
                }
            }         

        }

        private void udFrameRate_TextBoxTextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (int.TryParse(udFrameRate.TextBoxText, out result))
            {
                if (result < MIN_FRAME_RATE)
                {
                    udFrameRate.TextBoxText = MIN_FRAME_RATE.ToString();
                    result = MIN_FRAME_RATE;
                }
                else if (result > MAX_FRAME_RATE)
                {
                    udFrameRate.TextBoxText = MAX_FRAME_RATE.ToString();
                    result = MAX_FRAME_RATE;
                }

                setSPF(result);              
            }
        }

        private void setSPF(int frameRate)
        {
            targetfps = frameRate;
            spf = 1000 / frameRate;
            if (renderTimer != null)
            {
                renderTimer.Interval = spf;
            }
        }                    
      

        public void PrecisionTick(int interval)
        {
            bool lastFrame = false;          
            double fps = 0.0;           
            int frameNumber = 0;                     
                             
            if(!isStopped)
            {  
                currentImage = imageStream.GetNextFrame(out lastFrame, out frameNumber);
                fpsTime += interval;
                if (currentImage != null)
                {
                    fpsFrameCounter++;
                    
                    fps = ((double)fpsFrameCounter / (double)fpsTime)*1000.0;

                    UpdateImage(currentImage, frameNumber, imageStream.FrameCount, fps, lastFrame);

                    if (lastFrame)
                    {
                        isStopped = true;
                        renderTimer.Stop();
                        return;
                    }
                    else
                    {
                        //currentImage.Dispose();

                        if (fpsFrameCounter > FPS_COUNTER_FRAME_RESET_LIMIT)
                        {
                            fpsFrameCounter = 0;
                            fpsTime = 0;
                        }
                    }
                }                               
            }                       
        }

        private void UpdateImage(Image<Bgr, Byte> img, int frame, int frameCount, double fps, bool lastFrame)
        {
            if (!isStopped && this.InvokeRequired && img.Bytes != null)
            {
                UpdateImageDelegate UpdateImageVar = new UpdateImageDelegate(UpdateImage);
                Object[] args = new Object[5];
                args[0] = img;
                args[1] = frame;
                args[2] = frameCount;
                args[3] = fps;
                args[4] = lastFrame;

                Invoke(UpdateImageVar, args);
            }
            else
            {
                if (imageStream != null)
                {
                    activeImageBox.Image = img;
                    activeImageBox.Update();
                    if (frameCount > 0)
                    {
                        lblMovieProgress.Text = @"Frame " + frame + @"/" + frameCount;
                        lblMovieProgress.Invalidate();

                        if (lastFrame) //vid is finished.
                        {
                            return;
                        }
                    }

                    if (tbFramePosition.Enabled && frame <= tbFramePosition.Maximum)
                    {
                        tbFramePosition.Value = frame;
                    }

                    if (isRecording && (Environment.TickCount - blinkStart > 500))
                    {
                        pictRecording.Visible = !pictRecording.Visible;
                        blinkStart = Environment.TickCount;
                    }

                    if (lblElapsedTime.Visible)
                    {
                        TimeSpan span = DateTime.Now - videoStart;
                        if (span.Seconds > 1)
                        {
                            lblElapsedTime.Text = "Time: " + span.Hours.ToString("0") + ":" + span.Minutes.ToString("00") + ":" + span.Seconds.ToString("00");
                        }
                    }

                    lblMovieStatus.Text = "FPS: " + fps.ToString("0.0");
                    tsLblBufferSize.Text = "Buffer Fullness: " + imageStream.ReadBufferFullness + "%";
                }
            }

        }
               

        public void MissionPointUpdate(LogInfo info, long currPos, long maxPos, DateTime loggedAt, TimeSpan span)
        {
            if (this.InvokeRequired)
            {
                MissionPointUpdateDelegate UpdateMissionVar = new MissionPointUpdateDelegate(MissionPointUpdate);
                Object[] args = new Object[5];
                args[0] = info;              
                args[1] = currPos;
                args[2] = maxPos;
                args[3] = loggedAt;
                args[4] = span;

                Invoke(UpdateMissionVar, args);
            }
            else
            {
                if (currPos == maxPos)
                {
                    StopMission();
                }
                else
                {
                    if (missionControl != null && info != null)
                    {
                        if (info.GetType == LogInfo.InfoType.COORD)
                        {
                            Coordinate3D coord = info as Coordinate3D;
                            if (Math.Abs(coord.Lat) > 0 && Math.Abs(coord.Lon) > 0)
                            {
                                mpMissionMap.HoldInvalidation = true;

                                // if (bReplayingSavedMission)
                                {
                                    currentPosition = new Coordinate3D(coord.Lat, coord.Lon, coord.Alt);

                                    if (currPos > furthestMavlinkPos)
                                    {
                                        furthestMavlinkPos = currPos;
                                        route.Points.Add(currentPosition.GMapPoint);

                                        if (missionControl.IsTimeToPoll((long)span.TotalMilliseconds))
                                        {
                                            MissionPoint mp = missionControl.AddMissionPoint(currentPosition, navTarget, "");
                                            mp.WindBearing = coord.Wind.WindBearing;
                                            mp.WindSpeed = coord.Wind.WindSpeed;
                                            sampledPoints.Markers.Add(new GMapMarkerWind(currentPosition.GMapPoint, mpMissionMap, (float)coord.Wind.WindBearing, (float)coord.Wind.WindSpeed) { ToolTipText = "Wind Speed " + (coord.Wind.WindSpeed * 3.6).ToString("0.0") + "km/hr", ToolTipMode = MarkerTooltipMode.OnMouseOver });
                                        }
                                    }

                                    if (!mouseDown && (int)currPos < tbMavLink.Maximum)
                                    {
                                        tbMavLink.Value = (int)currPos;
                                    }

                                    secsPerMavlinkPos = ((float)span.TotalSeconds) / ((float)(currPos-missionControl.MissionStartPosition));
                                    mavLinkText = GetTimeFromSeconds((int)span.TotalSeconds);

                                    routes.Markers.Clear();
                                    routes.Markers.Add(new GMapMarkerPlane(currentPosition.GMapPoint, mpMissionMap) { ToolTipText = "current location", ToolTipMode = MarkerTooltipMode.OnMouseOver });

                                }

                                if (imageStream != null && chkSynchroniseVideo.Checked)
                                {
                                    imageStream.SyncFrames(loggedAt);
                                }                                                            

                                if (route.Points.Count == 1)
                                {
                                    mpMissionMap.Position = currentPosition.GMapPoint;
                                }
                                mpMissionMap.UpdateRouteLocalPosition(route);
                                mpMissionMap.HoldInvalidation = false;
                                mpMissionMap.Invalidate();
                            }
                        }                        
                    }
                }
            }
        }       
              

        private void cmdDeinterlace_Click(object sender, EventArgs e)
        {
            if (imageStream != null)
            {
                imageStream.IsInterlaced = cmdDeinterlace.Checked;
            }
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            frmApplicationProperties options = new frmApplicationProperties();
            options.SetProperties(new ApplicationPropertyManager());
            if (options.ShowDialog() == DialogResult.OK)
            {
                //UpdateAllProperties();
            }
        }
               
       

        private void cmdStartMission_Click(object sender, EventArgs e)
        {
            StartMission();
        }

        private void StartMission()
        {
            cmdStopMission.Enabled = true;
            cmdPauseMission.Enabled = true;
            cmdSharkDetected.Enabled = true;
            cmdWhaleDetected.Enabled = true;
            cmdDolphinDetected.Enabled = true;
            cmdSealDetected.Enabled = true;
            cmdResetStart.Enabled = true;

            route.Points.Clear();
            bReplayingSavedMission = true;
            missionControl.StopMission(false);

            chkSynchroniseVideo.Enabled = true;
            cmdPauseMission.Enabled = true;
            
            tbMavLink.Maximum = 1;
            missionControl.NewMission(cbLocations.SelectedItem as Location, cbAircraft.SelectedItem as Airframe,
                                      cbUsers.SelectedItem as User, cbCameras.SelectedItem as Camera, this, this);
            missionControl.StartMission();

            tbMavLink.Minimum = 0;
            tbMavLink.Maximum = (int)missionControl.MissionLength;

            furthestMavlinkPos = 0;
                     
            tbMavLink.Enabled = true;
                   
        }

        private void StopMission()
        {
            if (missionControl.MissionRunning)
            {
                cmdStartMission.Checked = false;
                cmdStopMission.Enabled = false;
                cmdPauseMission.Enabled = false;
                cmdSharkDetected.Enabled = false;
                cmdWhaleDetected.Enabled = false;
                cmdDolphinDetected.Enabled = false;
                cmdSealDetected.Enabled = false;
                cmdResetStart.Enabled = false;

                tbMavLink.Enabled = false;

                if (missionControl != null && missionControl.MissionRunning)
                {
                    missionControl.StopMission(true);                   
                }
            }
        }

        private void cmdStopMission_Click(object sender, EventArgs e)
        {
            StopMission();
        }             

        private void cmdPauseMission_Click(object sender, EventArgs e)
        {
            missionControl.Paused = cmdPauseMission.Checked;
        }

      
        private void tbMavLink_MouseDown(object sender, MouseEventArgs e)
        {
            missionControl.Paused = true;
            mouseDown = true;            
        }

        private void tbMavLink_MouseUp(object sender, MouseEventArgs e)
        {
            if (tbMavLink.Value >= missionControl.MissionStartPosition)
            {
                missionControl.MissionReadPosition = tbMavLink.Value;
            }
            else
            {
                missionControl.MissionReadPosition = missionControl.MissionStartPosition;
            }

            mouseDown = false;
            missionControl.Paused = false;
         
        }

        private void tbMavLink_FlyOutInfo(ref string data)
        {
            data = mavLinkText;           
        }            
            

        private void cbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Location selLoc = cbLocations.SelectedItem as Location;
                if (selLoc != null)
                {                
                    mpMissionMap.SetZoomToFitRect(new RectLatLng(selLoc.MaxY, selLoc.MinX,selLoc.MaxY - selLoc.MinY, selLoc.MaxX - selLoc.MinX));
                    mpMissionMap.Zoom--;
                    mpMissionMap.Position = new PointLatLng((selLoc.MaxY + selLoc.MinY) / 2.0, (selLoc.MaxX + selLoc.MinX) / 2.0);

                    locationLimits.Polygons.Clear();
                    GMapPolygon poly = new GMapPolygon(selLoc.BoundingPolygon, selLoc.LocationName);
                    poly.Fill = new SolidBrush(Color.Transparent);
          
                    locationLimits.Polygons.Add(poly);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to select a valid Location", "Selection failed",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSharkDetected_Click(object sender, EventArgs e)
        {
            targetDetected(TargetType.SHARK_ID, Properties.Resources.shark30);
        }

        private void cmdWhaleDetected_Click(object sender, EventArgs e)
        {
            targetDetected(TargetType.WHALE_ID, Properties.Resources.whale30);
        }

        private void cmdDolphinDetected_Click(object sender, EventArgs e)
        {
            targetDetected(TargetType.DOLPHIN_ID, Properties.Resources.dolphin30);
        }

        private void cmdSealDetected_Click(object sender, EventArgs e)
        {
            targetDetected(TargetType.SEAL_ID, Properties.Resources.seal30);
        }

        private void targetDetected(int id, Bitmap targetIcon)
        {
            missionControl.Paused = true;

            bReplayingSavedMission = true;
            Bitmap image = currentImage.Clone().Bitmap;
            string annotation = "";
            TargetType target = null;

            frmTargetSelection targetSelectionForm = new frmTargetSelection(image, targetTypes, id);
            if (targetSelectionForm.ShowDialog() == DialogResult.OK)
            {
                image = targetSelectionForm.SelectedImage.Clone() as Bitmap;
                annotation = targetSelectionForm.Annotation;
                target = targetSelectionForm.Target;

                MissionPoint mp = missionControl.AddMissionPoint(currentPosition, target, annotation);
                mp.Image = image;
                mp.ImageIndex = tbFramePosition.Value;
                sampledPoints.Markers.Add(new GMapMarkerTarget(currentPosition.GMapPoint, mpMissionMap, targetIcon) { ToolTipText = target.TargetName + ". " + annotation, ToolTipMode = MarkerTooltipMode.Always });
            }

            missionControl.Paused = false;

        }

        private void tbMavLink_ValueChanged(object sender, EventArgs e)
        {          
            if (mouseDown)
            {
                mavLinkText = GetTimeFromSeconds((int)(secsPerMavlinkPos * (tbMavLink.Value-missionControl.MissionStartPosition)));
                Debug.WriteLine("value = " + tbMavLink.Value.ToString());
            }
        }

        private void cmdResetStart_Click(object sender, EventArgs e)
        {
            missionControl.Paused = true;

            routes.Markers.Clear();
            sampledPoints.Markers.Clear();
            route.Points.Clear();
            mpMissionMap.Invalidate();

            missionControl.ResetMissionStartPoint();
            furthestMavlinkPos = missionControl.MissionReadPosition;

            missionControl.Paused = false;
        }

      
      

      
     
       

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace SeaScanUAV
{
    //TODO: Add way to control video feed
    public class MissionController
    {
        protected Mission mission = null;
        protected IVideoController videoController = null;
        protected int index = 0;
        protected long cumulativeTime = 0;
       
        protected MissionPlannerLogReader logReader = null;
        protected Coordinate3D lastCoord = null;

        public bool MissionRunning { get; set; }
        public bool IsLive { get; set; }

        public uint PollingInterval {get;set;}

        public long MissionStartPosition { get; set; }

        public MissionController(Mission mission)
        {
            this.mission = mission;
            PollingInterval = Properties.Settings.Default.missionPointPollingInterval;
                      
        }

        public MissionController()
        {
            PollingInterval = Properties.Settings.Default.missionPointPollingInterval;            
        }      


        public bool IsTimeToPoll(long elapsedMillis)
        {
            bool isTime = false;
            if (cumulativeTime == 0 || (elapsedMillis - cumulativeTime) >= PollingInterval)
            {
                isTime = true;
                cumulativeTime = elapsedMillis;
            }

            
            return isTime;
        }

        public MissionPoint AddMissionPoint(Coordinate3D coord, TargetType type, string annotation)
        {
            MissionPoint mp = null;
            if (mission != null)
            {     

                index++;
                mp = new MissionPoint(mission.ID, index, coord.Lat, coord.Lon, coord.Alt, type, CurrentMissionTime, annotation);
                mission.AddMissionPoint(mp);
                
                if (index > 1)
                {
                    mission.DistanceFlown += CalcDistance(lastCoord, coord);
                }

                lastCoord = new Coordinate3D(coord.Lat, coord.Lon, coord.Alt);               
            }

            return mp;


        }

        public void LoadMission(Mission mission)
        {
            this.mission = mission;
        }

        public void NewMission(Location loc, Airframe plane, User user, Camera camera, IMavLinkListener callback, IVideoController videoController)
        {
            frmCreateMission createMission = new frmCreateMission();
            if (createMission.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(createMission.LogFile) && File.Exists(createMission.VideoFile))
                {
                    mission = new Mission(loc, plane, user, camera, createMission.Description, createMission.VideoFile, createMission.LogFile);
                    logReader = new MissionPlannerLogReader(createMission.LogFile, false, callback);
                    this.videoController = videoController;
                    IsLive = createMission.IsLive;
                }
            }
        }

        public void StartMission()
        {
            if (!MissionRunning && logReader != null && videoController != null)
            {
                videoController.OpenImageFile(mission.MissionVideo);                
                MissionRunning = true;

                if (!logReader.Open(videoController.ImageStream.StartTime))
                {
                    MissionRunning = false;
                    throw new IOException("Error opening log file " + logReader.FileName);
                }

                MissionStartPosition = logReader.Position;

                if (!videoController.StartImageStream())
                {
                    MissionRunning = false;
                    throw new IOException("Error opening video file " + mission.MissionVideo);
                }

                mission.DateFlown = new DateTime(logReader.CurrentMissionTime.Ticks);
                
            }
        }

        public void ResetMissionStartPoint()
        {           
            mission.ClearMissionPoints();
        }

        public DateTime CurrentMissionTime
        {
            get
            {
                DateTime time = DateTime.Now;
                if (logReader != null && logReader.IsOpen)
                {
                    time = logReader.CurrentMissionTime;
                }
                return time;
            }
        }
        
        public long MissionReadPosition
        {
            set
            {
                if (logReader!= null && logReader.IsOpen)
                {
                    logReader.Position = value;
                }
            }

            get
            {
                if (logReader != null && logReader.IsOpen)
                {
                    return logReader.Position;
                }

                return 0;
            }

        }

        public long MissionLength
        {
            get
            {
                long len = 0;
                if (logReader != null && logReader.IsOpen)
                {
                    len =  logReader.MaximumPosition;
                }

                return len;
            }
        }

        public void StopMission(bool upload)
        {
            if (MissionRunning)
            {
                if (logReader != null && logReader.IsOpen)
                {
                    MissionRunning = false;
                    logReader.Close();
                    mission.Duration = logReader.Duration.TotalMinutes;
                }

                if (videoController != null && videoController.ImageStream.IsEnabled)
                {
                    videoController.StopImageStream();
                }

                if (upload)
                {
                    UploadMission();
                }
                mission = null;
            }
        }

        public bool Paused
        {
            get
            {
                if (logReader != null && logReader.IsOpen)
                {
                    return logReader.Paused;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (logReader != null && logReader.IsOpen)
                {
                    logReader.Paused = value;
                }

                if (videoController != null && videoController.ImageStream.IsEnabled)
                {
                    videoController.TogglePauseVideo(value);
                }
            }
        }

        public void UploadMission()
        {
            if (mission != null)
            {
                frmUploadMission dlgUploadMission = new frmUploadMission(mission);
                if (dlgUploadMission.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlgUploadMission.MissionUploadResult == UploadType.UploadMission)
                    {
                        WebServiceConsumer webService = WebServiceConsumer.GetInstance();
                        string retMessage = "";
                        HttpStatusCode retValue =  webService.PostMission(mission, ref retMessage);
                        if (retValue == HttpStatusCode.OK)
                        {
                            MessageBox.Show(@"Mission successfully uploaded", @"Mission uploaded", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show(retMessage, @"Error uploading mission (" + retValue.ToString() + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else //save to file
                    {


                    }
                }
            }
        }                

        private double Radians(double x)
        {
            return x * Math.PI / 180.0;
        }    

        private double CalcDistance(Coordinate3D coord1, Coordinate3D coord2)
        {
            const double RADIUS = 6378.16;
            double dlon = Radians(coord2.Lon - coord1.Lon);
            double dlat = Radians(coord2.Lat - coord1.Lat);

            double a = (Math.Sin(dlat / 2.0) * Math.Sin(dlat / 2.0)) + Math.Cos(Radians(coord1.Lat)) * Math.Cos(Radians(coord2.Lat)) * (Math.Sin(dlon / 2.0) * Math.Sin(dlon / 2.0));
            double angle = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return angle * RADIUS;
        }    
    }
}

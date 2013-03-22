using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace SeaScanUAV
{
    //TODO: Add way to control video feed
    public class MissionController
    {
        protected Mission mission = null;
        protected IVideoController videoController = null;
        protected int index = 0;
        protected long cumulativeTime = 0;
       
        protected IMissionPlannerReader missionReader = null;
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

        public Location MissionLocation
        {
            set
            {
                if (mission != null)
                {
                    mission.Location = value;
                }
            }

            get
            {
                Location loc = null;
                if (mission != null)
                {
                    loc = mission.Location;
                }

                return loc;
            }
        }
        public DateTime CurrentMissionTime
        {
            get
            {
                DateTime time = DateTime.Now;
                if (missionReader != null && missionReader.IsOpen)
                {
                    time = missionReader.CurrentMissionTime;
                }
                return time;
            }
        }

        public long MissionReadPosition
        {
            set
            {
                if (missionReader != null && missionReader.IsOpen)
                {
                    missionReader.Position = value;
                }
            }

            get
            {
                if (missionReader != null && missionReader.IsOpen)
                {
                    return missionReader.Position;
                }

                return 0;
            }

        }

        public long MissionLength
        {
            get
            {
                long len = 0;
                if (missionReader != null && missionReader.IsOpen)
                {
                    len = missionReader.MaximumPosition;
                }

                return len;
            }
        }

        public bool Paused
        {
            get
            {
                if (missionReader != null && missionReader.IsOpen)
                {
                    return missionReader.Paused;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (missionReader != null && missionReader.IsOpen)
                {
                    missionReader.Paused = value;
                }

                if (videoController != null && videoController.ImageStream.IsEnabled)
                {
                    videoController.TogglePauseVideo(value);
                }
            }
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
                IsLive = createMission.IsLive;
                if (createMission.IsLive)
                {
                    missionReader = new MissionPlannerLiveConnector("127.0.0.1", "56781", callback);
                    if (missionReader.Open(false, DateTime.Now)) 
                    {
                        mission = new Mission(loc, plane, user, camera, createMission.Description, createMission.VideoFile, createMission.LogFile);
                        this.videoController = videoController;
                    }
                }
                else
                {
                    if (File.Exists(createMission.LogFile) && File.Exists(createMission.VideoFile))
                    {
                        mission = new Mission(loc, plane, user, camera, createMission.Description, createMission.VideoFile, createMission.LogFile);
                        missionReader = new MissionPlannerLogReader(createMission.LogFile, false, callback);
                        this.videoController = videoController;                      
                    }
                }
            }
        }

        public void StartMission()
        {
            if (!MissionRunning && missionReader != null && videoController != null)
            {
                if (IsLive)
                {
                    videoController.OpenImageCapture(1);
                }
                else
                {
                    videoController.OpenImageFile(mission.MissionVideo);
                }

                MissionRunning = true;

                if (!missionReader.Open(true, videoController.ImageStream.StartTime))
                {
                    MissionRunning = false;
                    throw new IOException("Error opening log file " + missionReader.FileName);
                }

                MissionStartPosition = missionReader.Position;

                if (!videoController.StartImageStream())
                {
                    MissionRunning = false;
                    throw new IOException("Error opening video file " + mission.MissionVideo);
                }

                mission.DateFlown = new DateTime(missionReader.CurrentMissionTime.Ticks);
                
            }
        }

        public void ResetMissionStartPoint()
        {
            if (mission != null)
            {
                mission.ClearMissionPoints();
            }
        }

       
        public void StopMission(bool upload)
        {
            if (MissionRunning)
            {
                if (missionReader != null && missionReader.IsOpen)
                {
                    MissionRunning = false;
                    missionReader.Close();
                    mission.Duration = missionReader.Duration.TotalMinutes;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SeaScanUAV
{
    [DataContract]
    [Serializable()]
    public class Mission
    {
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "DateFlown")]
        public DateTime DateFlown{get;set;}

        [DataMember(Name = "LocationID")]
        public int LocationID { get; set;}

        [DataMember(Name = "PilotID")]
        public int PilotID { get; set;}

        [DataMember(Name = "AircraftID")]
        public int AircraftID { get; set; }

        [DataMember(Name = "CameraID")]
        public int CameraID { get; set; }

        [DataMember(Name = "Duration")]
        public double Duration { get; set; }

        [DataMember(Name = "DistanceFlown")]
        public double DistanceFlown { get; set; }

        [DataMember(Name = "TargetsDetected")]
        public int TargetsDetected { get; set; }

        [DataMember(Name = "PointCount")]
        public int PointCount { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "MissionVideo")]
        public string MissionVideo { get; set; }

        [DataMember(Name = "MissionLog")]
        public string MissionLog { get; set; }

        [DataMember(Name = "MissionPoints")]
        public List<MissionPoint> PointList
        {
            set
            {
                missionPoints = value;
                PointCount = missionPoints.Count;
            }

            get
            {
                return missionPoints;
            }
        }

        protected Location location = null;
        protected Airframe plane = null;
        protected User user = null;
        protected Camera  camera = null;     
        protected List<MissionPoint> missionPoints = new List<MissionPoint>();

        public Mission()
        {
            PointCount = 0;
            TargetsDetected = 0;

            this.Description = "";
            this.MissionVideo = "";
            this.MissionLog = "";
        }

        public Mission(Location loc, Airframe plane, User usr, Camera camera, string description, string videoFile, string logFile)
        {
            this.DateFlown = DateTime.Now;

            this.location = loc;
            this.LocationID = loc.ID;

            this.plane = plane;
            this.AircraftID = plane.ID;

            this.user = usr;
            this.PilotID = usr.ID;

            this.camera = camera;
            this.CameraID = camera.ID;

            this.Description = description;
            this.MissionVideo = videoFile;
            this.MissionLog = logFile;

            PointCount = 0;
            TargetsDetected = 0;
        }       
            
        public Location Location
        {
            set
            {
                location = value;
                LocationID = location.ID;
            }

            get
            {
                return location;
            }
        }

        public Airframe Airframe
        {
            set
            {
                plane = value;
                AircraftID = plane.ID;
            }

            get
            {
                return plane;
            }
        }

        public User Pilot
        {
            set
            {
                user = value;
                PilotID = user.ID;
            }

            get
            {
                return user;
            }
        }

        public Camera MissionCamera
        {
            set
            {
                camera = value;
                CameraID = camera.ID;
            }

            get
            {
                return camera;
            }
        }

      

        public void AddMissionPoint(MissionPoint mp)
        {
            missionPoints.Add(mp);              

            PointCount = missionPoints.Count;
            if (mp.TargetDetected > 0)
            {
                TargetsDetected++;
            }
            missionPoints.Sort(new MissionPointComparer());
        }

        public void RemoveMissionPointAt(int index)
        {            
            missionPoints.RemoveAt(index);
            PointCount = missionPoints.Count;            
        }

        public void ClearMissionPoints()
        {            
            missionPoints.Clear();
            PointCount = 0;            
        }

        public override string ToString()
        {
            return @"Mission flown on " + DateFlown.ToLongTimeString() + @" at  " + location.LocationName;
        }

        
 
   
    }
}

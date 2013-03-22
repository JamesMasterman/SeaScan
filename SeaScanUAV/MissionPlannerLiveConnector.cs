using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Xml;

namespace SeaScanUAV
{
    public class MissionPlannerLiveConnector: IMissionPlannerReader
    {

        protected TcpClient client = null;
        protected HttpWebRequest request;
        protected HttpWebResponse response;

        protected IMavLinkListener callback = null;

        protected string hostIP = "127.0.0.1";
        protected string port = "56781";

        protected DateTime missionStart;
        protected DateTime currentTime;

        public string FileName { get; set; }
        public TimeSpan Duration { get; set; }
        public LogInfo LastMessage { get; set; }
        public bool IsOpen { get; set; }
        public bool Paused { get; set; }

        public Thread readerThread = null;     

        public DateTime CurrentMissionTime 
        {
            get
            {
                return DateTime.Now;
            }
        }

        public long Position { get; set; }
        public long MaximumPosition
        {
            get
            {
                return 1;
            }
        }                   
        
        public MissionPlannerLiveConnector(string hostIP, string port, IMavLinkListener callback)
        {
            this.hostIP = hostIP;
            this.port = port;
            this.FileName = "";
            this.IsOpen = false;           
            this.Position = 0;
            this.callback = callback;
        }

        public bool Open(bool doSync, DateTime syncTime)
        {
            IsOpen = true;
            readerThread = new Thread(new ThreadStart(ReadFromMissionPlanner));
            readerThread.Start();

            missionStart = DateTime.Now;
            

            return true;
        }
      

        public void Close()
        {
            if (IsOpen)
            {
                if (readerThread != null)
                {
                    IsOpen = false;
                }              
            }
        }

        public void ReadFromMissionPlanner()
        {
            WebServiceConsumer consumer = WebServiceConsumer.GetInstance();                
            DateTime currentTime;
            while (IsOpen)
            {
                string result = consumer.GetWebServiceData("http://" + hostIP + ":" + port + "/network.kml\n");
                currentTime = DateTime.Now;
                // Debug.WriteLine(result);
                if (result != null && result.Length > 0)
                {

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(result);

                    XmlNodeList location = xmlDoc.GetElementsByTagName("Location");

                    Coordinate3D coord = new Coordinate3D();
                    foreach (XmlNode node in location[0].ChildNodes)
                    {
                        double val = 0.0;
                        if (node.Name == "longitude")
                        {
                            if (Double.TryParse(node.InnerText, out val))
                            {
                                coord.Lon = val;
                            }
                        }
                        else if (node.Name == "latitude")
                        {
                            if (Double.TryParse(node.InnerText, out val))
                            {
                                coord.Lat = val;
                            }
                        }
                        else if (node.Name == "altitude")
                        {
                            if (Double.TryParse(node.InnerText, out val))
                            {
                                coord.Alt = val;
                            }
                        }
                    }

                    LastMessage = coord;
                }

                Duration = currentTime - missionStart;

                if (callback != null)
                {
                    callback.MissionPointUpdate(LastMessage, Position, MaximumPosition, currentTime, Duration);
                }

                Thread.Sleep(500);
            }
          
        }
    }
}

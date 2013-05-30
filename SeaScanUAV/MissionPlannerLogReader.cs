using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ArdupilotMega;
using System.Threading;
using System.Diagnostics;



namespace SeaScanUAV
{
    public class MissionPlannerLogReader: IMissionPlannerReader
    {
        protected bool activeLog = false;
        protected BinaryReader reader = null;
        volatile protected object readlock = new object();
        protected IMavLinkListener callback = null;
        protected long logElapsedTime = -1;
        protected bool logreadmode { get; set; }

        public string FileName{get; set;}
        public TimeSpan Duration { get; set; }
        public LogInfo LastMessage { get; set; }
        public bool IsOpen { get; set; }       

        protected byte sysid { get; set; }
        protected byte compid { get; set; }          

        protected DateTime currentLogTime;
        public Thread readerThread = null;       


        public DateTime CurrentMissionTime
        {
            get
            {
                return currentLogTime;
            }
        }
           

        public MissionPlannerLogReader(string filename, bool activeLog, IMavLinkListener callback)
        {
            this.FileName = filename;
            this.logreadmode = true;
            this.IsOpen = false;
            this.activeLog = activeLog;
            this.callback = callback;
            
        }

        public bool Paused { get; set; }      

        public long MaximumPosition
        {
            get
            {
                long pos = 0;
                if (reader != null && IsOpen)
                {
                    pos = reader.BaseStream.Length;
                }

                return pos;
            }
        }


        public long Position
        {
            get
            {
                long pos = 0;
                if (reader != null && IsOpen)
                {
                    pos = reader.BaseStream.Position;
                }

                return pos;
            }

            set
            {               
                if (reader != null && IsOpen)
                {
                    if (value < reader.BaseStream.Length)
                    {
#if DEBUG
                        Debug.WriteLine("Pos change for mavlink");
#endif  
                       logElapsedTime = -1;
                       reader.BaseStream.Position = value;
                    }
                }               
            }
        }        

        public bool Open(bool doSync, DateTime syncTime)
        {
            IsOpen = false;
            try
            {                
                reader = new BinaryReader(new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read));
                IsOpen = true;

                if (doSync)
                {
                    if (ReadToTime(syncTime))
                    {
                        readerThread = new Thread(new ThreadStart(ReadLogFile));
                        readerThread.Priority = ThreadPriority.Highest;
                        readerThread.Start();
                    }
                    else
                    {
                        reader.Close();
                        reader.Dispose();
                        IsOpen = false;
                    }
                }
            }
            catch (IOException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + ". " + e.InnerException.Message, "Error opening tlog file", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error);
                IsOpen = false;
            }

            return IsOpen;
        }

        public void Close()
        {
            if (IsOpen && reader != null)
            {
                if (readerThread != null)
                {
                    IsOpen = false;                 
                }

                reader.Close();
                reader = null;                
            }
        }

        private void ResetStream()
        {
            Close();
            Open(true, DateTime.Now);
        }
       

        public void ReadLogFile()
        {
            Stopwatch sw = new Stopwatch();                      
            
            long elapsedTime = -1;
            long syncStartTime = 0;
            logElapsedTime = -1;
            DateTime missionStart = new DateTime(0);
            DateTime lastLogTime = new DateTime(0);

            sw.Start();
            syncStartTime = sw.ElapsedMilliseconds;
            
            while (IsOpen)
            {
                if (!Paused)
                {
                    if (ReadNextMAVPacket())
                    {
                        if (logElapsedTime < 0)//first point
                        {  
                            logElapsedTime = currentLogTime.Ticks / TimeSpan.TicksPerMillisecond;

                            if (missionStart.Ticks == 0)
                            {
                                missionStart = currentLogTime;
                            }
                            lastLogTime = currentLogTime;
                            syncStartTime = sw.ElapsedMilliseconds;
                            callback.MissionPointUpdate(LastMessage , Position, MaximumPosition, currentLogTime, currentLogTime - missionStart);
                        }
                        else
                        {
                            logElapsedTime = (currentLogTime - lastLogTime).Ticks / TimeSpan.TicksPerMillisecond;
                            elapsedTime = sw.ElapsedMilliseconds - syncStartTime;


                            while (logElapsedTime - elapsedTime > 50)
                            {
                                Thread.Sleep(50);
                                if (!IsOpen)
                                {
                                    break;
                                }
                                elapsedTime = sw.ElapsedMilliseconds-syncStartTime;
                            }
#if DEBUG
                            // Debug.WriteLine("read another position");
#endif                      
                            syncStartTime = sw.ElapsedMilliseconds;
                            lastLogTime = currentLogTime;
                            Duration = currentLogTime - missionStart;
                            callback.MissionPointUpdate(LastMessage, Position, MaximumPosition, currentLogTime, Duration);                            

                        }

                    }
                    else
                    {
                        LastMessage = null;
                        callback.MissionPointUpdate(LastMessage, MaximumPosition, MaximumPosition, currentLogTime, currentLogTime - missionStart);
                        IsOpen = false;
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

#if DEBUG
            Debug.WriteLine("exiting logfile thread");
#endif
            sw.Stop();
        }

        private bool ReadToTime(DateTime position)
        {
            bool readOK = true;
            while (currentLogTime < position && readOK)
            {
                readOK = ReadNextMAVPacket();
            }

            return readOK;

        }
   
        private bool ReadNextMAVPacket()
        {
            bool readok = true;
            bool hasLat = false;         
            bool hasWind = false;

            LastMessage = new Coordinate3D();

            lock (this)
            {

                while (IsOpen && readok && !(hasLat && hasWind))
                {
                    readok = false;
                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        byte[] packet = readPacket();
                        if (packet[5] == MAVLinkTypes.MAVLINK_MSG_ID_GLOBAL_POSITION_INT)
                        {
                            var gps = packet.ByteArrayToStructure<MAVLinkTypes.mavlink_global_position_int_t>(6);
                           
                            ((Coordinate3D)LastMessage).Lat = gps.lat * 1.0e-7f;
                            ((Coordinate3D)LastMessage).Lon = gps.lon * 1.0e-7f;
                            ((Coordinate3D)LastMessage).Alt = gps.alt * 1.0e-3f;
                            hasLat = true;
                        }                        

                        if (packet[5] == MAVLinkTypes.MAVLINK_MSG_ID_WIND)
                        {
                            var vfr = packet.ByteArrayToStructure<MAVLinkTypes.mavlink_wind_t>(6);                           

                            ((Coordinate3D)LastMessage).Wind.WindSpeed = vfr.speed;
                            ((Coordinate3D)LastMessage).Wind.WindBearing = vfr.direction;
                            hasWind = true;
                        }

                        readok = true;
                    }
                }
            }

            return readok;
        }

        /// <summary>
        /// Serial Reader to read mavlink packets. POLL method
        /// </summary>
        /// <returns></returns>
        private byte[] readPacket()
        {
            byte[] buffer = new byte[300];
            int count = 0;
            int length = 0;
            int readcount = 0;
           

            byte[] headbuffer = new byte[6];         

            DateTime start = DateTime.Now;

            //Console.WriteLine(DateTime.Now.Millisecond + " SR0 " + BaseStream.BytesToRead);
                      
            lock (readlock)
            {
                //Console.WriteLine(DateTime.Now.Millisecond + " SR1 " + BaseStream.BytesToRead);

                while (true)
                {
                    try
                    {
                        if (readcount > 300)
                        {
                            break;
                        }
                        readcount++;
                        if (logreadmode)
                        {
                            buffer = readlogPacketMavlink();
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("MAVLink readpacket read error: " + e.ToString()); 
                        break;
                    }

                    // check if looks like a mavlink packet and check for exclusions and write to console
                    if (buffer[0] != 254)
                    {                        
                        continue;
                    }
                    // reset count on valid packet
                    readcount = 0;
                                    
                    // check for a header
                    if (buffer[0] == 254)
                    {   
                        // packet length
                        length = buffer[1] + 6 + 2 - 2; // data + header + checksum - U - length
                        if (count >= 5 || logreadmode)
                        {
                            if (sysid != 0)
                            {
                                if (sysid != buffer[3] || compid != buffer[4])
                                {
                                    if (buffer[3] == '3' && buffer[4] == 'D')
                                    {
                                        // this is a 3dr radio rssi packet
                                    }
                                    else
                                    {
                                        Debug.WriteLine("Mavlink Bad Packet (not addressed to this MAV) got {0} {1} vs {2} {3}", buffer[3], buffer[4], sysid, compid);
                                        return new byte[0];
                                    }
                                }
                            }                                                                                                                
                            count = length + 2;                           
                            break;
                        }
                    }

                    count++;
                    if (count == 299)
                        break;
                }
                               
            }// end readlock

            Array.Resize<byte>(ref buffer, count);
          
            if (buffer.Length >= 5 && buffer[3] == 255 && logreadmode) // gcs packet
            {               
                return buffer;// new byte[0];
            }    
                        

            return buffer;
        }

        private byte[] readlogPacketMavlink()
        {
            byte[] temp = new byte[300];

            sysid = 0;

            //byte[] datearray = BitConverter.GetBytes((ulong)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);

            byte[] datearray = new byte[8];

            int tem = reader.BaseStream.Read(datearray, 0, datearray.Length);

            Array.Reverse(datearray);

            DateTime date1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            UInt64 dateint = BitConverter.ToUInt64(datearray, 0);

            try
            {
                date1 = date1.AddMilliseconds(dateint / 1000);
                currentLogTime = date1.ToLocalTime();

                //limit it to nearest second for video frame comparison
                currentLogTime = new DateTime(currentLogTime.Ticks - (currentLogTime.Ticks % TimeSpan.TicksPerSecond), currentLogTime.Kind);

            }
            catch { }
          

            int length = 5;
            int a = 0;
            while (a < length)
            {
                temp[a] = (byte)reader.ReadByte();
                if (temp[0] != 'U' && temp[0] != 254)
                {
                    Debug.WriteLine("lost sync byte {0} pos {1}", temp[0], reader.BaseStream.Position);
                    a = 0;
                    continue;
                }
                if (a == 1)
                {
                    length = temp[1] + 6 + 2; // 6 header + 2 checksum
                }
                a++;
            }
            
            return temp;
        }

       

    }
}

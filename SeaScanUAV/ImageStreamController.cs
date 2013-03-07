using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace SeaScanUAV
{
    public enum FRAME_TYPE{
        ORIGINAL_FRAME,
        GRAY_FRAME,
        CANNY_FRAME
    };

    public class ImageStreamController
    {
        protected Capture capture = null;
        protected bool isEnabled = false;

        protected Queue<Image<Bgr, Byte>> frameBuffer=null;
        protected Thread captureThread = null;

        protected volatile bool isReading = false;

        protected int cachedFrameCount = -1;
        protected volatile int currentFrame = 0;

        protected bool isLastFrame = false;

        protected ImageStreamWriter writer= null;
        protected volatile bool isWriting = false;

        //Debug variables
        protected int framesRead = 0;
        protected int frameWriterCount = 0;

        protected Dictionary<DateTime, int> frameSync = new Dictionary<DateTime, int>();
        protected DateTime earliestFrame = DateTime.Now;

        public uint FramesToBuffer{
            get;
            set;         
        }

        protected Object lockObject = new Object();
            
        //read a capture source
        public ImageStreamController(int streamId, bool isInterlaced)
        {
            this.IsInterlaced = isInterlaced;
            FramesToBuffer = Properties.Settings.Default.frameBufferSize;
            try
            {
                frameBuffer = new Queue<Image<Bgr, Byte>>((int)FramesToBuffer);
                capture = new Capture(streamId);
                isEnabled = true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + " and " + e.InnerException.Message, "Error connecting to camera");
                isEnabled = false;
            }
        }

        //read an image file
        public ImageStreamController(String streamFile, bool isInterlaced)
        {
            this.IsInterlaced = isInterlaced;
            FramesToBuffer = Properties.Settings.Default.frameBufferSize;
            try
            {
                frameBuffer = new Queue<Image<Bgr, Byte>>((int)FramesToBuffer);
                ReadSychronisationData(streamFile);
                capture = new Capture(streamFile);              
                isEnabled = true;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error loading video file");
                isEnabled = false;
            }            
        }

        public int Codec
        {
            get
            {
                if (capture != null)
                {
                    return (int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FOURCC);
                }
                else
                {
                    return 0;
                }
            }
        }

        public DateTime StartTime
        {
            get
            {
                return earliestFrame;
            }
        }

        public bool SyncTimeIsBeforeStart(DateTime syncTime)
        {
            bool isBeforeStart = false;

            if (syncTime < earliestFrame)
            {
                isBeforeStart = true;
            }

            return isBeforeStart;
        }


        public void SyncFrames(DateTime syncTime)
        {
            if(frameSync.Count > 0)
            {
                if(frameSync.ContainsKey(syncTime))
                {
                    int currFrame = CurrentFrame;
                    if((currFrame < (frameSync[syncTime]-FramesToBuffer)) || (currFrame > (frameSync[syncTime] + FramesToBuffer)))
                    {
#if DEBUG
                        Debug.WriteLine("Sync frames to " + frameSync[syncTime]);
#endif
                        CurrentFrame = frameSync[syncTime];
                    }
                }
            }
        }

        public bool OpenWriteStream(String fileName, int codec, int fps, int width, int height, bool isColour)
        {
            isWriting= false;
            try
            {
                writer = new ImageStreamWriter(fileName, codec, fps, width, height, isColour);
                isWriting = true;
                frameWriterCount = 0;
            }
            catch (Exception e)
            {
                isWriting = false;
            }

            return isWriting;
            
        }

       
        public void StopWriting()
        {
            lock(lockObject)
            {
                if (writer != null)
                {
                    isWriting = false;
                    writer.Close();
                    writer = null;
                }
            }
        }

        public void StartReading()
        {
            if (!isReading)
            {
                isReading = true;
                isLastFrame = false;
                
                CurrentFrame = 0;
                cachedFrameCount = -1;
               
                captureThread = new Thread(new ThreadStart(BufferFrames));
                captureThread.Start();                
            }
        }

        public void StopReading()
        {
            if (isReading && captureThread != null)
            {
                isReading = false;

                if (captureThread != null)
                {
                    captureThread.Interrupt();
                    if (!captureThread.Join(2000))
                    {
                        captureThread.Abort();
                    }
                }
                
                StopWriting();
            }
        }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
        }

        public int FrameCount
        {
            get
            {
                if (capture != null)
                {
                    if (cachedFrameCount < 0)
                    {
                        lock (lockObject)
                        {
                            cachedFrameCount = (int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                        }
                    }
                    return cachedFrameCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public double FrameRate
        {
            get
            {
                if (capture != null)
                {
                    lock (lockObject)
                    {
                        return capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                    }
                }
                else
                {
                    return 0.0;
                }
            }

            set
            {
                if(capture != null)
                {
                    lock (lockObject)
                    {
                        capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS, value);
                    }
                }
            }
        }

        public int CurrentFrame
        {
            set
            {
                if (capture != null)
                {
                    lock (lockObject)
                    {
                        capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES, (double)value);            
                    
                        Debug.WriteLine("Set frame to " + value);
                        currentFrame = value;
                        frameBuffer.Clear();
                    }
                }
            }

            get
            {
                if (capture != null)
                {    lock(lockObject)              
                    { 
                        //Debug.WriteLine("Current frame = " + currentFrame + " frame buffer = " + frameBuffer.Count);
                        return (currentFrame);
                       
                    }                   
                }
                else
                {
                    return 0;
                }
            }
        }

        public int FrameHeight
        {
            get
            {
                if (capture != null)
                {
                    return (int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int FrameWidth
        {
            get
            {
                if (capture != null)
                {
                    return (int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ReadBufferFullness
        {
            get
            {                
               return (frameBuffer.Count * 100) / (int)FramesToBuffer;                
            }
        }

        public Image<Bgr, Byte> GetNextFrame(out bool lastFrame, out int frameNumber)
        {
            Image<Bgr, Byte> img = null;

            lastFrame = false;
            frameNumber = 0;
            try
            {
                lock(lockObject)
                {
                    lastFrame = (isLastFrame && (frameBuffer.Count <= 1));
                    if (frameBuffer.Count > 0)
                    {
                        img = frameBuffer.Dequeue();
                        frameNumber = currentFrame;
                        currentFrame++;
#if DEBUG
                       // Debug.WriteLine("Frame buffer size at dequeue = " + frameBuffer.Count);
#endif
                    }
                }
            }
            catch (Exception e)
            {
                lastFrame = true;
                img = null;
            }
            
            return img;
        }
      

        public bool IsInterlaced { get; set; }

        protected Image<Gray, Byte> GetNextGrayFrame(out bool lastFrame)
        {
            Image<Gray, Byte> grayFrame = null;
           // Image<Gray, Byte> smallGrayFrame = null;
           // Image<Gray, Byte> smoothedGrayFrame = null;
            lastFrame = true;
            if (capture != null)
            {
                if (capture.Grab())
                {
                    grayFrame = capture.RetrieveGrayFrame();
                    // smallGrayFrame = grayFrame.PyrDown();
                    //  smoothedGrayFrame = smallGrayFrame.PyrUp();

                    lastFrame = false;
                   
                    // smoothedGrayFrame.Dispose();
                    // smallGrayFrame.Dispose();
                }
            }

            return grayFrame;
        }

        protected void BufferFrames()
        {
            try
            {
                while (isReading)
                {
                    if (frameBuffer.Count < FramesToBuffer) //maintain a minimum buffer size
                    {
                        Image<Bgr, Byte> frame = CaptureFrame();
                        if (!isLastFrame)
                        {
                            lock (lockObject)
                            {
                                frameBuffer.Enqueue(frame);
                                currentFrame = (int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES) - 1;
                                currentFrame -= (frameBuffer.Count - 1); //current frame is the frame on the front of the queue.

#if DEBUG
                                // Debug.WriteLine("curr frame = " + currentFrame);
#endif
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            }
            catch (ThreadInterruptedException tie)
            {
            }
            catch (ThreadAbortException tae)
            {
            }
           
            Debug.WriteLine("exited buffer");

        }

        protected Image<Bgr, Byte> CaptureFrame()
        {
            Image<Bgr, Byte> frame = null;
                
            try
            {
                if (capture != null)
                {
                    lock (lockObject)
                    {
                        frame = capture.QueryFrame();
                    }

                    if(frame != null)
                    {
                     
                        //  frame._EqualizeHist();
                        if (IsInterlaced)
                        {
                            Deinterlace(ref frame);
                        }
#if DEBUG
                        framesRead++;
                        //Debug.WriteLine("Frames read = " + framesRead);                     
#endif
                        lock(lockObject)
                        {
                            if (isWriting && writer != null)
                            {
                                writer.WriteFrame(frame.Copy());    
#if DEBUG                            
                                frameWriterCount++;
                                Debug.WriteLine("Frames written = " + frameWriterCount);
#endif
                            }
                        }
                    }                    
                    else
                    {
                        isLastFrame = true;
#if DEBUG 
                        Debug.WriteLine("last frame at " + currentFrame);
#endif
                    }                     
                }                
            }
            catch (Exception e)
            {
                isLastFrame = true;
            }

            return frame;
            
        }

        protected void Deinterlace(ref Image<Bgr, Byte> img)
        {  
            IntPtr linea;  
            IntPtr lineb;            

            MIplImage mImg = img.MIplImage;
            Image<Bgr, Byte> img2 = img.Clone();
           
            img2 = img2.Resize(720, 576 / 2, Emgu.CV.CvEnum.INTER.CV_INTER_NN, false);
            MIplImage mImg2 = img2.MIplImage;
                       
            int widthStep = mImg.widthStep;
            int ht = img.Height;
            int scancount = 0;

            for (int i = 0; i < ht; i += 2)
            {
                //linea = mImg.imageData + (i * widthStep);
                lineb = mImg.imageData + (i * widthStep);
                linea = mImg2.imageData + (scancount * widthStep);
                
                Emgu.Util.Toolbox.memcpy(linea, lineb, widthStep);
               
                scancount++;                       
            }

            img = img2.Resize(720, 576, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC, false);
                      
        }  

        protected void ReadSychronisationData(String fileName)
        {
            bool firstFrame = true;
            try
            {
                String syncFile = ImageStreamWriter.CreateMatchingSynchronisationFileName(fileName);
                if (System.IO.File.Exists(syncFile))
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(syncFile);
                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine();

                        //get date and frame number parts
                        String[] parts = line.Split(new char[] { '\t' });
                        if (parts.Length == 2)
                        {   //construct date part
                            String[] dateParts = parts[0].Split(new char[] { ':' });
                            if (dateParts.Length == 6)
                            {
                                int year = int.Parse(dateParts[0]);
                                int month = int.Parse(dateParts[1]);
                                int day = int.Parse(dateParts[2]);
                                int hour = int.Parse(dateParts[3]);
                                int min = int.Parse(dateParts[4]);
                                int sec = int.Parse(dateParts[5]);

                                DateTime timeStamp = new DateTime(year, month, day, hour, min, sec, 0);

                                if (firstFrame)
                                {
                                    earliestFrame = timeStamp;
                                    firstFrame = false;
                                }

                                frameSync.Add(timeStamp, int.Parse(parts[1]));
                            }
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + ". " + e.InnerException.Message, "Error loading sychronisation data", System.Windows.Forms.MessageBoxButtons.OK);
            }

        }


       
    }
}

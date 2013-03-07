using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Diagnostics;
using System.IO;

namespace SeaScanUAV
{
    public class ImageStreamWriter
    {
        public static int MPEG1_CODEC = CvInvoke.CV_FOURCC('P', 'I', 'M', '1');
        public static int MJPG_CODEC = CvInvoke.CV_FOURCC('M', 'J', 'P', 'G');
        public static int DIB_CODEC = CvInvoke.CV_FOURCC('D', 'I', 'B', ' ');
        public static int FRAME_INDEX_INTERVAL = 50;

        protected VideoWriter writer = null;
        protected String fileName = "";
        protected bool isColour = true;
        protected String frameFileName = "";
        protected int frameCount = 0;
        protected StreamWriter frameIndexStream = null;
               
       
        public ImageStreamWriter(String fileName, int codec, int fps, int width, int height, bool isColour)
        {
            try
            {
                if (fps > 0 && width > 0 && height > 0)
                {
                    this.fileName = fileName;
                    this.isColour = isColour;

                    writer = new VideoWriter(fileName, codec, fps, width, height, isColour);

                    frameFileName = ImageStreamWriter.CreateMatchingSynchronisationFileName(fileName);
                    frameIndexStream = new System.IO.StreamWriter(frameFileName);
                }
            }
            catch (Exception e)
            {
                writer = null;
                frameIndexStream = null;
            }
        }

        public static String CreateMatchingSynchronisationFileName(String fileName)
        {
            String syncFile = "";
            syncFile = Path.GetFileNameWithoutExtension(fileName);
            syncFile = Path.GetDirectoryName(fileName) + "\\" + syncFile + "_frameIndex.txt";

            return syncFile;
        }
             
       
        public void WriteFrame(Image<Bgr,Byte> frame)
        {
            if (writer != null)
            {
                if (!isColour)
                {
                    throw new Exception("Image writer initialised for grayscale images only");
                }
                else
                {
                    writer.WriteFrame(frame);
                    frameCount++;

                    if (frameCount % FRAME_INDEX_INTERVAL == 0)
                    {
                        DateTime timeStamp = DateTime.Now;
                        frameIndexStream.WriteLine(timeStamp.Year + ":" + timeStamp.Month + ":" + timeStamp.Day + ":" + timeStamp.Hour + ":" + timeStamp.Minute + ":" + timeStamp.Second + "\t" + frameCount.ToString());
                        frameIndexStream.Flush();
                    }
                }
            }
        }

        public void WriteFrame(Image<Gray, Byte> frame)
        {
            if (writer != null)
            {
                if (isColour)
                {
                    throw new Exception("Image writer initialised for colour frames only");
                }
                else
                {
                    lock (this)
                    {
                        writer.WriteFrame(frame);
                    }
                }
            }
        }

        public void Close()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }

            if (frameIndexStream != null)
            {
                frameIndexStream.Close();
                frameIndexStream.Dispose();
                frameIndexStream = null;
            }
        }


    }
}

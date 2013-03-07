using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Drawing;
using System.IO;

namespace SeaScanUAV
{
    [DataContract]
    [Serializable()]
    public class MissionPoint
    {
        public const int MAX_IMAGE_DIMENSION = 100;
        protected Bitmap _image;
        protected TargetType targetType = null;

        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "MissionID")]
        public int MissionID { get; set; }

        [DataMember(Name = "PointNum")]
        public int PointNum { get; set; }

        [DataMember(Name = "XCoord")]
        public double XCoord { get; set; }

        [DataMember(Name = "YCoord")]
        public double YCoord { get; set; }

        [DataMember(Name = "ZCoord")]
        public double ZCoord { get; set; }

        [DataMember(Name = "TargetDetected")]
        public byte TargetDetected { get; set; }

        [DataMember(Name = "TargetTypeID")]
        public int TargetTypeID{get; set;}

        [DataMember(Name = "Annotation")]
        public string Annotation{get; set;}

        [DataMember(Name = "DateRecorded")]
        public DateTime DateRecorded{get; set;}

        [DataMember(Name = "ImageURL")]
        public string ImageURL { get; set; }

        [DataMember(Name = "ImageIndex")]
        public int ImageIndex { get; set; }

        [DataMember(Name = "Image")]
        public string ImageBase64 { get; set; }

        [DataMember(Name = "WindSpeed")]
        public double WindSpeed { get; set; }

        [DataMember(Name = "WindBearing")]
        public double WindBearing { get; set; }

        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;

                if (_image != null)
                {
                    Bitmap newImage = new Bitmap(_image);
                    if (newImage.Width > MAX_IMAGE_DIMENSION || newImage.Height > MAX_IMAGE_DIMENSION)
                    {
                        newImage = ImageUtils.ResizeImage(_image, MAX_IMAGE_DIMENSION);
                    }

                    _image = newImage;

                    ImageBase64 = ImageUtils.Base64EncodeImage(_image);

                }
            }
        }      


        public TargetType TargetType 
        {
            get
            {
                return targetType;
                
            }

            set
            {
                targetType = value;
                if (targetType != null)
                {
                    TargetTypeID = targetType.ID;
                }
            }
        
        }        


        public MissionPoint()
        {
            this.TargetDetected = 0;
            this.PointNum = 0;
            this.Image = null;
        }



        public MissionPoint(int missionID, int index, double lat, double lon, double alt, TargetType type, DateTime dateRecorded, string annotation)
        {
            this.ID = index;
            this.PointNum = index;
            this.XCoord = lon;
            this.YCoord = lat;
            this.ZCoord = alt;
            this.TargetType = type;
            if (!type.IsNavigation)
            {
                this.TargetDetected = 1;
            }
            else
            {
                this.TargetDetected = 0;
            }

            this.Annotation = annotation;
            this.DateRecorded = dateRecorded;     
        }

        public override string ToString()
        {
            string msg;

            msg =  @"Mission point " + PointNum + ".";
            if (TargetDetected == 0)
            {
                msg += "No target detected";
            }
            else
            {
                msg += "Target detected";
            }

            return msg;
        }


     
    }

   

    public class MissionPointComparer : IComparer<MissionPoint> {
      public int Compare(MissionPoint x, MissionPoint y) {
        int retVal = 0;

        if(x.PointNum > y.PointNum)
        {
            retVal = 1;
        }
        else if(x.PointNum < y.PointNum)
        {
            retVal = -1;
        }

        return retVal;
      }
    }

}

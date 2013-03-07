using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SeaScanUAV
{
    [DataContract]
    public class Camera
    {
        [DataMember]
        public int ID{get; set;}

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public int HorizontalRes { get; set; }

        [DataMember]
        public int VerticalRes { get; set; }

        [DataMember]
        public float FocalLength { get; set; }

        public Camera()
        {

        }

        public Camera(int ID, string model, int HRes, int VRes, float focalLength)
        {
            this.ID = ID;
            Model = model;
            HorizontalRes = HRes;
            VerticalRes = VRes;
            FocalLength = focalLength;
        }

        public override string ToString()
        {
            return Model;
        }
        
    }
}

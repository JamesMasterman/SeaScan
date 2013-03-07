using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SeaScanUAV
{
    [DataContract]
    public class TargetType
    {
        public const int NAVIGATION_ID = 1;       
        public const int WHALE_ID = 3;
        public const int DOLPHIN_ID = 4;
        public const int SEAL_ID = 5;
        public const int SHARK_ID = 6;        

        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "TargetName")]
        public string TargetName { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        public TargetType()
        {
            ID = 0;
            TargetName = "";
            Description = "";
        }

        public TargetType(string name, string description)
        {
            TargetName = name;
            Description = description;
        }

        public override string ToString()
        {
            return TargetName + " - " + Description;
        }

        public bool IsNavigation
        {
            get
            {
                return (ID == NAVIGATION_ID);
            }
        }

    }
}

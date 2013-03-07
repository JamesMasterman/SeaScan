using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SeaScanUAV
{
    [DataContract]
    public class Airframe
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PlaneName {get; set;}

        [DataMember]
        public string Description {get; set;}

        public Airframe()
        {
        }

        public Airframe(int ID, string planeName, string description)
        {
            this.PlaneName =  planeName;
            this.Description =  description;
        }

        public override string ToString()
        {
            return PlaneName;
        }
    }
}

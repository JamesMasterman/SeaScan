using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GMap.NET;

namespace SeaScanUAV
{
    [DataContract]
    public class Location
    {
        [DataMember(Name="ID")]
        public int ID { get; set;}

        [DataMember(Name = "LocationName")]
        public string LocationName { get; set; }

        [DataMember(Name = "Town")]
        public string Town { get; set; }

        [DataMember(Name = "Country")]
        public string Country { get; set; }

        [DataMember(Name="MinX")]
        public double MinX { get; set; }

        [DataMember(Name="MinY")]
        public double MinY { get; set; }

        [DataMember(Name="MaxX")]
        public double MaxX { get; set; }

        [DataMember(Name="MaxY")]
        public double MaxY { get; set; }

        public Location()
        {
        }

        public Location(int ID, string locationName, string town, string country, double minX, double minY, double maxX, double maxY)
        {
          this.ID = ID;
          this.LocationName = locationName;
          this.Town = town;
          this.Country = country;
          this.MinX = minX;
          this.MinY = minY;
          this.MaxX = maxX;
          this.MaxY = maxY;
        }


        public override string ToString()
        {
            return LocationName;           
        }

        public List<PointLatLng> BoundingPolygon
        {
            get
            {
                List<PointLatLng> pts = new List<PointLatLng>(4);
                pts.Add(new PointLatLng(MaxY, MinX));
                pts.Add(new PointLatLng(MaxY, MaxX));
                pts.Add(new PointLatLng(MinY, MaxX));
                pts.Add(new PointLatLng(MinY, MinX));
                pts.Add(new PointLatLng(MaxY, MinX));

                return pts;
            }
        }


            
    }
}

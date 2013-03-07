using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMap.NET;


namespace SeaScanUAV
{
    public class Coordinate3D: LogInfo
    {
     
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }

        public WindInfo Wind { get; set; }

        public Coordinate3D()
        {
            Lat = -1;
            Lon = -1;
            Alt = -1;
            this.Wind = new WindInfo();
        }

        public Coordinate3D(double lat, double lon, double alt)
        {
            this.Lat = lat;
            this.Lon = lon;
            this.Alt = alt;
            this.Wind = new WindInfo();
        }

        public override LogInfo.InfoType GetType
        {
            get
            {
                return InfoType.COORD;
            }
        }
       

        public PointLatLng GMapPoint
        {
            get
            {
                return new PointLatLng(Lat, Lon);
            }
        }

        public override string ToString()
        {
            return "Lat = " + Lat + ". Long = " + Lon + ". Alt = " + Alt;
        }
    }
}

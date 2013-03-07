using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public class WindInfo
    {
        protected double bearing = 0.0;

        public double WindSpeed { get; set; }        
        public string WindDir { get; set; }

        public WindInfo()
        {
            WindBearing = 0.0;
            WindSpeed = 0.0;
        }
               
        public double WindBearing 
        {
            get
            {
                return bearing;
            }
            
            set 
            {
                bearing = value;
                if (bearing > 337.5 || bearing <= 22.5)
                {
                    WindDir = "N";
                }
                else if(bearing > 22.5 && bearing <= 67.5)
                {
                    WindDir = "NE";
                }
                else if (bearing > 67.5 && bearing <= 112.5)
                {
                    WindDir = "E";
                }
                else if (bearing > 112.5 && bearing <= 157.5)
                {
                    WindDir = "SE";
                }
                else if (bearing > 157.5 && bearing <= 202.5)
                {
                    WindDir = "S";
                }
                else if (bearing > 202.5 && bearing <= 247.5)
                {
                    WindDir = "SW";
                }
                else if (bearing > 247.5 && bearing <= 292.5)
                {
                    WindDir = "W";
                }
                else if (bearing > 292.5 && bearing <= 337.5)
                {
                    WindDir = "NW";
                }
            }
        }


    }
}

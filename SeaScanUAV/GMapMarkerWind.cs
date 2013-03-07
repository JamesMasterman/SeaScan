using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;

namespace SeaScanUAV
{
    public class GMapMarkerWind:GMapMarker
    {
        public float ORANGE_SPEED_LIMIT = 5.0f;
        public float RED_SPEED_LIMIT = 10.0f;
        static readonly System.Drawing.Size SizeSt = new System.Drawing.Size(global::SeaScanUAV.Properties.Resources.Green_N.Width, global::SeaScanUAV.Properties.Resources.Green_N.Height);
            
        public GMapControl MainMap;
        protected Bitmap icon = global::SeaScanUAV.Properties.Resources.Green_N;
        protected float bearing = 0.0f;
        protected float speed = 0.0f;

        public GMapMarkerWind(PointLatLng p, GMapControl map, float bearing, float speed)
            : base(p)
        {
              
            Size = SizeSt;
            MainMap = map;

            this.bearing = bearing;
            this.speed = speed;

            if (speed > ORANGE_SPEED_LIMIT)
            {
                icon = global::SeaScanUAV.Properties.Resources.Orange_N;
            }
            else if (speed > RED_SPEED_LIMIT)
            {
                icon = global::SeaScanUAV.Properties.Resources.Red_N;
            }
        }

        public override void OnRender(Graphics g)
        {
            System.Drawing.Drawing2D.Matrix temp = g.Transform;               
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);
            g.RotateTransform(-MainMap.Bearing);

            try
            {
                g.RotateTransform(bearing);
            }
            catch { }

            g.DrawImageUnscaled(icon, icon.Width / -2, icon.Height / -2);
              
            g.Transform = temp;
                               
        }
    }
}

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
    public class GMapMarkerTarget:GMapMarker
    {
        public const int IMAGE_WIDTH = 30;
        public const int IMAGE_HEIGHT = 30;
        static readonly System.Drawing.Size SizeSt = new System.Drawing.Size(IMAGE_WIDTH, IMAGE_HEIGHT);
            
        public GMapControl MainMap;
        protected Bitmap icon;

        public GMapMarkerTarget(PointLatLng p, GMapControl map, Bitmap icon)
            : base(p)
        {
              
            Size = SizeSt;
            MainMap = map;
            this.icon = icon;
        }

        public override void OnRender(Graphics g)
        {
            if (icon != null)
            {
                System.Drawing.Drawing2D.Matrix temp = g.Transform;
                g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

                g.DrawImageUnscaled(icon, icon.Width / -2, icon.Height / -2);

                g.Transform = temp;
            }
                               
        }
    }
}

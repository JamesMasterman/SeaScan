using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Diagnostics;

namespace SeaScanUAV
{
    
        public class GMapMarkerPlane : GMapMarker
        {
            static readonly System.Drawing.Size SizeSt = new System.Drawing.Size(global::SeaScanUAV.Properties.Resources.Glossyblueplane.Width, global::SeaScanUAV.Properties.Resources.Glossyblueplane.Height);
            
            public GMapControl MainMap;

            public GMapMarkerPlane(PointLatLng p, GMapControl map)
                : base(p)
            {
             
                Size = SizeSt;
                MainMap = map;
            }

            public override void OnRender(Graphics g)
            {
                System.Drawing.Drawing2D.Matrix temp = g.Transform;
               
                g.TranslateTransform(LocalPosition.X, LocalPosition.Y);                 

                g.DrawImageUnscaled(global::SeaScanUAV.Properties.Resources.Glossyblueplane, global::SeaScanUAV.Properties.Resources.Glossyblueplane.Width / -2, global::SeaScanUAV.Properties.Resources.Glossyblueplane.Height / -2);//global::UAVVision.Properties.Resources.Glossyblueplane.Width / -2, global::UAVVision.Properties.Resources.Glossyblueplane.Height / -2);

                g.Transform = temp;
                               
            }
        }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SeaScanUAV
{
    [DefaultPropertyAttribute("FrameBufferSize")]
    public class ApplicationPropertyManager
    {
         List<IPropertyChangeListener> callbacks = new List<IPropertyChangeListener>();

         public void AddCallback(IPropertyChangeListener callback)
         {
             callbacks.Add(callback);
         }

        [CategoryAttribute("Video Properties"), DescriptionAttribute("Size of frame buffer for video replay")]
        public uint FrameBufferSize
        {
            get
            {
                return Properties.Settings.Default.frameBufferSize;
            }

            set
            {
                Properties.Settings.Default.frameBufferSize = value;
                Properties.Settings.Default.Save();
            }
        }

        [CategoryAttribute("Web Service Properties"), DescriptionAttribute("Address of web service")]
        public String webservice
        {
            get
            {
                return Properties.Settings.Default.webservice;
            }

            set
            {
                Properties.Settings.Default.webservice = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Web Service Properties"), DescriptionAttribute("Log in name for webservice requests")]
        public String webserviceusername
        {
            get
            {
                return Properties.Settings.Default.username;
            }

            set
            {
                Properties.Settings.Default.username = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Web Service Properties"), DescriptionAttribute("Log in password for webservice requests")]
        public String webservicepassword
        {
            get
            {
                return Properties.Settings.Default.password;
            }

            set
            {
                Properties.Settings.Default.password = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Video Properties"), DescriptionAttribute("Target frames per second for video playback and writing")]
        public uint fps
        {
            get
            {
                return Properties.Settings.Default.fps;
            }

            set
            {
                Properties.Settings.Default.fps = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Mission Properties"), DescriptionAttribute("Interval for polling UAV location when creating mission")]
        public uint missionPointPollingInterval
        {
            get
            {
                return Properties.Settings.Default.missionPointPollingInterval;
            }

            set
            {
                Properties.Settings.Default.missionPointPollingInterval = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Mission Properties"), DescriptionAttribute("Zoom level for map")]
        public uint mapZoom
        {
            get
            {
                return Properties.Settings.Default.initZoom;
            }

            set
            {
                Properties.Settings.Default.initZoom = value;
                Properties.Settings.Default.Save();

            }
        }

        [CategoryAttribute("Mission Properties"), DescriptionAttribute("units for speed")]
        public string speedUnits
        {
            get
            {
             //   return Properties.Settings.Default.units;
                return "";
            }

            set
            {
               // Properties.Settings.Default.units = value;
                Properties.Settings.Default.Save();

            }
        }

    }
}

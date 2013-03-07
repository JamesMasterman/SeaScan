using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public abstract class LogInfo
    {
       
        public enum InfoType
        {
            COORD
        };

        public LogInfo()
        {
        }

        public abstract InfoType GetType
        {
            get;
        }
             
    }
}

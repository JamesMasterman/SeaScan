using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public interface IMissionPlannerReader
    {
        string FileName { get; set; }   
        TimeSpan Duration { get; set; }
        LogInfo LastMessage { get; set; }
        bool IsOpen { get; set; }
        bool Paused { get; set; }     

        DateTime CurrentMissionTime { get;}
        long MaximumPosition { get; }
        long Position { get; set; }
        bool Open(bool doSync, DateTime syncTime);
        void Close();

    }
}

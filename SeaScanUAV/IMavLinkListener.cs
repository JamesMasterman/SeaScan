using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public interface IMavLinkListener
    {
        void MissionPointUpdate(LogInfo info, long currPos, long maxPos, DateTime loggedAt, TimeSpan span);
    }
}

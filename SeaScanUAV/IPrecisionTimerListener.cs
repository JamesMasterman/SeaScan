using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public interface IPrecisionTimerListener
    {
        void PrecisionTick(int interval);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SeaScanUAV
{
    public class PrecisionTimer
    {

        protected Thread timerThread = null;
        protected volatile bool isRunning = false;
        protected volatile bool isPaused = false;
        protected Stopwatch sw = new Stopwatch();
        protected long startTime;
        protected IPrecisionTimerListener callback;

        public long Interval { get; set; }

        public PrecisionTimer(int interval, IPrecisionTimerListener callback)
        {
            this.Interval = interval;
            this.callback = callback;
        }

        public void Start()
        {
            isRunning = true;
            isPaused = false;
            timerThread = new Thread(new ThreadStart(DoTimer));

            timerThread.Priority = ThreadPriority.Highest;  
            timerThread.Start();
           
        }

        public void Stop()
        {
            isRunning = false;
            if (timerThread != null)
            {
                timerThread.Interrupt();
                if (!timerThread.Join(2000))
                {
                    timerThread.Abort();
                }
            }
        }

        public bool Paused
        {
            get
            {
                return isPaused;
            }

            set
            {
                isPaused = value;
            }
        }
              

        public void DoTimer()
        {
            try
            {
                sw.Start();
                startTime = sw.ElapsedMilliseconds;
                long elapsed;
                // Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2); // Uses the second Core 
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;  	// Prevents "Normal" processes 


                while (isRunning)
                {
                    if (!isPaused)
                    {
                        elapsed = (sw.ElapsedMilliseconds - startTime);
                        if (elapsed >= Interval && isRunning)
                        {
#if DEBUG
                            //   Debug.WriteLine("Interval = " + Interval + ". Elapsed = " + elapsed.ToString());
#endif
                            startTime = sw.ElapsedMilliseconds;
                            if (startTime < 0)
                            {
                                sw.Reset();
                                sw.Start();
                                startTime = sw.ElapsedMilliseconds;
                            }
                            callback.PrecisionTick((int)elapsed);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }

                    // System.Windows.Forms.Application.DoEvents();
                    //Thread.Sleep(1);
                }
            }
            catch (ThreadInterruptedException tie)
            {
                sw.Stop();
            }
            catch (ThreadAbortException tae)
            {
                sw.Stop();
            }

#if DEBUG
            Debug.WriteLine("Gracefully exiting precision timer");
#endif
        }
    }
}

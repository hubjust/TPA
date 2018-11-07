using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        static TraceSwitch traceSwitch = new TraceSwitch("General", "The whole app");

        public Tracer(TraceListener listener, TraceLevel level = TraceLevel.Error)
        {
            traceSwitch.Level = level;
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
        }

        public void TracerLog(TraceLevel level, Object obj)
        {
            Trace.WriteLine(traceSwitch.Level, "  " + level + "  " +DateTime.Now + "  ");
        }
    }
}

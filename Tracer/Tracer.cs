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
            System.Diagnostics.Trace.Listeners.Add(listener);
            System.Diagnostics.Trace.AutoFlush = true;
        }

        public void TracerLog(TraceLevel level, Object obj)
        {
            Trace.WriteLineIf(level <= traceSwitch.Level, "" + level + ":\t" + DateTime.Now + "\t" + obj);

        }
    }
}

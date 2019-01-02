using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Logger
{
    public class Tracer : ITracer
    {
        static TraceSwitch traceSwitch = new TraceSwitch("General", "The whole app");

        [ImportingConstructor]
        public Tracer(TraceListener listener, TraceLevel level = TraceLevel.Error)
        {
            traceSwitch.Level = level;
            System.Diagnostics.Trace.Listeners.Add(listener);
            System.Diagnostics.Trace.AutoFlush = true;
        }

        public void TracerLog(TraceLevel level, Object obj)
        {
            Trace.WriteLineIf(level <= traceSwitch.Level, "[" + level + "] :\t" + DateTime.Now + "\t" + obj);

        }
    }
}

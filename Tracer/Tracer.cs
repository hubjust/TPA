﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        static TraceSwitch traceSwitch = new TraceSwitch("General", "The whole app");

        [ImportingConstructor]
        public Tracer(TraceListener listener, TraceLevel level = TraceLevel.Error)
        {
            traceSwitch.Level = level;
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
        }

        public void TracerLog(TraceLevel level, Object obj)
        {
            Trace.WriteLineIf(level <= traceSwitch.Level, "[" + level + "] :\t" + DateTime.Now + "\t" + obj);

        }
    }
}

using System;
using System.Diagnostics;

namespace Logger
{
    public interface ITracer
    {
        void TracerLog(TraceLevel level, Object obj);
    }
}

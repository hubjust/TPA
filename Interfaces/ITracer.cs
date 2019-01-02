using System;
using System.Diagnostics;

namespace Tracer
{
    public interface ITracer
    {
        void TracerLog(TraceLevel level, Object obj);
    }
}

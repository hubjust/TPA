using System.Diagnostics;

namespace Tracer
{
    public interface ITracer
    {
        void TracerLog(TraceLevel level, object obj);
    }
}

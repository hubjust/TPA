using System.Diagnostics;

namespace Interfaces
{
    public interface ITracer
    {
        void TracerLog(TraceLevel level, object obj);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Logger
{
    public interface ITracer
    {
        void TracerLog(TraceLevel level, Object obj);
    }
}

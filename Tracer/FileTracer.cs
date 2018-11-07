﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class FileTracer : Tracer
    {
        public FileTracer(string fileName, TraceLevel level = TraceLevel.Error)
            : base(new TextWriterTraceListener(DateTime.Now.ToString("d-m-yyyy_HH-mm-ss") + "_" + fileName), level) { }
    }
}

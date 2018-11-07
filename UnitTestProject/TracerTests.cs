using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Tracer.Tests
{
    [TestClass()]
    public class TracerTests
    {
        [TestMethod()]
        public void TraceTest()
        {
            ITracer  tracer = new Tracer(new ConsoleTraceListener(), TraceLevel.Warning);

            StringWriter str = new StringWriter();
            Console.SetOut(str);
             
            tracer.TracerLog(TraceLevel.Info, "warning");
            Assert.AreEqual(str.ToString(), "");
        }
    }
}
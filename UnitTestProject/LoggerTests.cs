using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace Logger.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void TraceTest()
        {
            ITracer tracer = new Tracer(new ConsoleTraceListener(), TraceLevel.Warning);

            StringWriter str = new StringWriter();
            Console.SetOut(str);
             
            tracer.TracerLog(TraceLevel.Info, "warning");
            Assert.AreEqual(str.ToString(), "");
        }

        [TestMethod()]
        public void FileTracerTest()
        {
            ITracer tracer = new FileTracer("FileTracerTest.log");
            tracer.TracerLog(TraceLevel.Info, "FileTracerTest");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Logger;

namespace Serializers
{
    public class XmlSerializer : ISerializer
    {
        private string fileName;
        private ITracer tracer;

        public XmlSerializer(string fileName, ITracer tracer)
        {
            this.fileName = fileName;
            this.tracer = tracer;
        }

        public void Serialize(DataContext dataContext)
        {
            DataContractSerializer serializer = new DataContractSerializer(dataContext.GetType());

            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Indent = true
            };

            tracer.TracerLog(TraceLevel.Info, "Started xml serializing: " + fileName);
            
            using (XmlWriter writer = XmlWriter.Create(fileName, xmlSettings))
            {
                try
                {
                    serializer.WriteObject(writer, dataContext);
                }
                catch (SerializationException e)
                {
                    tracer.TracerLog(TraceLevel.Error, "Error during xml serializing: " + fileName);
                }
            }
            tracer.TracerLog(TraceLevel.Info, "Finished xml serializing: " + fileName);
        }
    }
}

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
    public class XmlDeserializer : IDeserializer
    {
        private string fileName;
        private ITracer tracer;

        public XmlDeserializer(string fileName, ITracer tracer)
        {
            this.fileName = fileName;
            this.tracer = tracer;
        }

        public DataContext Deserialize()
        {
            tracer.TracerLog(TraceLevel.Info, "Started xml deserializing: " + fileName);
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                DataContractSerializer deserializer = new DataContractSerializer(typeof(DataContext));

                tracer.TracerLog(TraceLevel.Info, "Finished xml deserializing: " + fileName);

                return (DataContext)deserializer.ReadObject(reader);
            }
        }
    }
}

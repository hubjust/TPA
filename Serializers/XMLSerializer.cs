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
        private DataContractSerializer serializer;

        public void Serialize(string filePath, object target)
        {
            serializer = new DataContractSerializer(target.GetType());
            using (XmlWriter writer = XmlWriter.Create(filePath))
            {
                serializer.WriteObject(writer, target);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            serializer = new DataContractSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                return (T)serializer.ReadObject(reader);
            }
        }
    }
}

using System.Runtime.Serialization;
using System.Xml;
using System.ComponentModel.Composition;
using System.IO;
using Serializers.Model;
using DBCore;
using DBCore.Model;

namespace Serializers
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class XmlSerializer : ISerializer<AssemblyBase>
    {
        private DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyModel));

        public void Serialize(string filePath, AssemblyBase ab)
        {
            AssemblyModel assemblyModel = new AssemblyModel(ab);
            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(writer, assemblyModel);
            }
        }

        public AssemblyBase Deserialize(string filePath)
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return DataTransferGraph.AssemblyBase((AssemblyModel)serializer.ReadObject(reader));

            }
        }
    }
}

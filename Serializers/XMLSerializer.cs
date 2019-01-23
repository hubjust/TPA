using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using System.IO;

using Interfaces;
using Serializers.Model;
using DBCore.Model;

namespace Serializers
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class XmlSerializer : ISerializer<AssemblyBase>
    {
        private DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyModel));

        public void Serialize(IFileSelector selector, AssemblyBase ab)
        {
            AssemblyModel assemblyModel = new AssemblyModel(ab);
            string path = selector.FileToSave("XML file (.xml) | *.xml");
            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(writer, assemblyModel);
            }
        }

        public AssemblyBase Deserialize(IFileSelector selector)
        {
            using (FileStream reader = new FileStream(selector.FileToOpen(), FileMode.Open))
            {
                return DataTransferGraph.AssemblyBase((AssemblyModel)serializer.ReadObject(reader));

            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using DBCore.Model;
using Interfaces;
using Serializers;

namespace Serializers.Tests
{
    [TestClass()]
    public class XmlSerializerTests
    {
        [TestMethod]
        public void SerializationTest()
        {
            IFileSelector selector = new Selector();
            List<NamespaceBase> namespaceList = new List<NamespaceBase>()
                {
                    new NamespaceBase()
                    {
                        Name = "name1"
                    },
                    new NamespaceBase()
                    {
                        Name = "name2"
                    }
                };   

            AssemblyBase assemblyObject = new AssemblyBase()
            {
                Name = "test",
                Namespaces = namespaceList
            };

            XMLSerializer serializer = new XMLSerializer();
            string path = "SerializationXMLTest.xml";
            serializer.Serialize(selector, assemblyObject);
            AssemblyBase deserializedObject = serializer.Deserialize(selector);
            Assert.AreEqual(assemblyObject.Name, deserializedObject.Name);
            Assert.AreEqual(assemblyObject.Namespaces.ToList()[0].Name, deserializedObject.Namespaces.ToList()[0].Name);
            Assert.AreEqual(assemblyObject.Namespaces.ToList()[1].Name, deserializedObject.Namespaces.ToList()[1].Name);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    class Selector : IFileSelector
    {
        public string FileToOpen(string filter = null)
        {
            return "SerializationTestFile.xml";
        }

        public string FileToSave(string filter = null)
        {
            return "SerializationTestFile.xml";
        }
    }
}
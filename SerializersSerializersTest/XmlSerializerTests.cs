using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using Model;
using DBCore.Model;
using System.Xml.Serialization;
using System.IO;

namespace Serializers.Tests
{
    [TestClass()]
    public class XmlSerializerTests
    {
        [TestMethod]
        public void SerializationTest()
        {
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

            XmlSerializer serializer = new XmlSerializer();
            string path = "SerializationXMLTest.xml";
            serializer.Serialize(path, assemblyObject);
            AssemblyBase deserializedObject = serializer.Deserialize(path);
            Assert.AreEqual(assemblyObject.Name, deserializedObject.Name);
            Assert.AreEqual(assemblyObject.Namespaces.ToList()[0].Name, deserializedObject.Namespaces.ToList()[0].Name);
            Assert.AreEqual(assemblyObject.Namespaces.ToList()[1].Name, deserializedObject.Namespaces.ToList()[1].Name);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
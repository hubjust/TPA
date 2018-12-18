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

namespace Serializers.Tests
{
    [TestClass()]
    public class XmlSerializerTests
    {
        private Assembly assembly;

        [TestInitialize]
        public void Init()
        {
            string str = @"Model.dll";
            assembly = Assembly.LoadFrom(str);
        }

        [TestMethod]
        public void XmlSerialization()
        {
            ISerializer serializer = new XmlSerializer();
            string path = @"serializer.xml";
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata(assembly);
            serializer.Serialize(path, assemblyMetadata);

            AssemblyMetadata assemblyTest = serializer.Deserialize<AssemblyMetadata>(path);

            Assert.AreEqual(assemblyTest.Namespaces.Count(), 1);
        }
    }
}
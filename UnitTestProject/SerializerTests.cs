using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializers;
using Model;
using ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTestProject
{
    [TestClass]
    public class SerializerTests
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

            Assert.AreEqual(assemblyTest.Namespaces.Count(), 2);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTestProject
{
    [TestClass]
    public class ReflectionTest
    {
        private Assembly assembly;

        [TestInitialize]
        public void Init()
        {
            string str = @"..\..\File\Model.dll";
            assembly = Assembly.LoadFrom(str);
        }

        [TestMethod]
        public void LoadingAssemblyTest()
        {
            Assert.IsTrue(assembly != null);
        }

        [TestMethod]
        public void AssemblyMetadataShouldNotBeNullTest()
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata();
            Assert.IsTrue(assemblyMetadata != null);
        }

        [TestMethod]
        public void NamespacesInAssemblyMetadataTest()
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata(assembly);

            List<string> namespaceNames = new List<string>(from Namespaces in assemblyMetadata.Namespaces
                                                           select Namespaces.Name);

            Assert.IsTrue(namespaceNames.Contains("Model"));
        }

        [TestMethod]
        public void EmitPropertiesTest()
        {
            Type type = typeof(ExampleClass);
            List<PropertyMetadata> properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList<PropertyMetadata>();

            Assert.AreEqual(1, properties.Count);
        }

        [TestMethod]
        public void TypesInMetadata() // Nie działa na razie
        {
            AssemblyMetadata assemblyMeta = new AssemblyMetadata(assembly);

            //NamespaceMetadata namespaceMetadata = assemblyMeta.Namespaces.ToList<NamespaceMetadata>()[1];
        }
    }
}

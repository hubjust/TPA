using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ViewModelTest
{
    [TestClass]
    public class ReflectionTest
    {
       private Assembly assembly;

        [TestInitialize]
        public void Init()
        {
            string str = @"..\..\ExampleDll\TPAApplicationArchitecture.dll";
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

            Assert.IsTrue(namespaceNames.Contains("TPA.ApplicationArchitecture.Data.CircularReference"));
            Assert.IsTrue(namespaceNames.Contains("TPA.ApplicationArchitecture.BusinessLogic"));
            Assert.IsTrue(namespaceNames.Contains("TPA.ApplicationArchitecture.Data"));
            Assert.IsTrue(namespaceNames.Contains("TPA.ApplicationArchitecture.Presentation"));
        }

        [TestMethod]
        public void CountNamespacesInMetadata()
        {
            AssemblyMetadata assemblyMeta = new AssemblyMetadata(assembly);

            List<NamespaceMetadata> namespaceMetadata = assemblyMeta.Namespaces.ToList<NamespaceMetadata>();

            Assert.AreEqual(4, namespaceMetadata.Count);
        }

        [TestMethod]
        public void TypesInMetadata()
        {
            AssemblyMetadata assemblyMeta = new AssemblyMetadata(assembly);

            List<NamespaceMetadata> namespaceMetadata = assemblyMeta.Namespaces.ToList<NamespaceMetadata>();

            NamespaceMetadata name = namespaceMetadata[0];

            List<string> list = new List<string>(from Type in name.Types
                                                 select Type.Name);

            Assert.IsTrue(list.Contains("Model"));
            Assert.IsTrue(list.Contains("ServiceA"));
            Assert.IsTrue(list.Contains("ServiceB"));
            Assert.IsTrue(list.Contains("ServiceC"));
            Assert.IsTrue(list.Contains("ViewModel"));
        }

        #region TestClass
        class TestClass
        {
            public string property1 { get; set; }
            public double property2 { get; set; }

            virtual public void function(string property1, double property2) { }
        }

        class genericTestClass<T> { }
        #endregion

        [TestMethod]
        public void EmitReferencesTest()
        {
            TypeMetadata typeMetadata = TypeMetadata.EmitReference(typeof(TestClass));

            Assert.IsNull(typeMetadata.GenericArguments);
            Assert.AreEqual("ViewModelTest", typeMetadata.NamespaceName);
        }

        [TestMethod]
        public void EmitPropertiesTest()
        {
            Type type = typeof(TestClass);
            List<PropertyMetadata> properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList<PropertyMetadata>();

            Assert.AreEqual(2, properties.Count);
            Assert.AreEqual("property1", properties[0].Name);
            Assert.AreEqual("property2", properties[1].Name);
            Assert.AreEqual("String", properties[0].Type.Name);
            Assert.AreEqual("Double", properties[1].Type.Name);
        }

        [TestMethod]
        public void EmitMethodsTest()
        {
            Type type = typeof(TestClass);
            List<MethodMetadata> methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList<MethodMetadata>();

            MethodMetadata method = methods.Find(m => m.Name.Equals("function"));

            Assert.AreEqual(AccessLevel.IsPublic, method.AccessLevel);
            Assert.AreEqual(AbstractEnum.NotAbstract, method.AbstractEnum);
            Assert.AreEqual(StaticEnum.NotStatic, method.StaticEnum);
            Assert.AreEqual(VirtualEnum.Virtual, method.VirtualEnum);
            Assert.AreEqual("Void", method.ReturnType.Name);
        }

        [TestMethod]
        public void EmitGenericArgumentsTest()
        {
            List<TypeMetadata> typeMetadatas = TypeMetadata.EmitGenericArguments(typeof(genericTestClass<Double>).GetGenericArguments())
                                                   .ToList<TypeMetadata>();

            Assert.AreEqual(1, typeMetadatas.Count);
            Assert.AreEqual("Double", typeMetadatas[0].Name);
        }
    }
}

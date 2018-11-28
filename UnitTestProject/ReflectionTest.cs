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
            string str = @"Model.dll";
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
            Assert.AreEqual("UnitTestProject", typeMetadata.NamespaceName);
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

            Assert.AreEqual(AccessLevel.IsPublic, method.Modifiers.Item1);
            Assert.AreEqual(AbstractEnum.NotAbstract, method.Modifiers.Item2);
            Assert.AreEqual(StaticEnum.NotStatic, method.Modifiers.Item3);
            Assert.AreEqual(VirtualEnum.Virtual, method.Modifiers.Item4);
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

        [TestMethod]
        public void TypesInMetadata() 
        {
            AssemblyMetadata assemblyMeta = new AssemblyMetadata(assembly);

            List<NamespaceMetadata> namespaceMetadata = assemblyMeta.Namespaces.ToList<NamespaceMetadata>();

            Assert.AreEqual(2, namespaceMetadata.Count);

            NamespaceMetadata name = namespaceMetadata[0];

            List<string> list = new List<string>(from Type in name.Types
                                                    select Type.Name);

            Assert.IsTrue(list.Contains("AbstractEnum"));
            Assert.IsTrue(list.Contains("AccessLevel"));
            Assert.IsTrue(list.Contains("SealedEnum"));
            Assert.IsTrue(list.Contains("StaticEnum"));
            Assert.IsTrue(list.Contains("VirtualEnum"));
        }
    }
}

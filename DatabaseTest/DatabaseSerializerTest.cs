using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using Database;
using Database.Model;
using System.Collections.Generic;
using Core.Enum;
using System.Linq;

namespace DatabaseTest
{
    [TestClass]
    public class DatabaseSerializerTest
    {
        Mock<DatabaseContext> mockContext;
        DBSerializer serializer;

        List<DatabaseNamespace> namespaces;
        List<DatabaseType> types;

        [TestInitialize]
        public void Init()
        {
            types = new List<DatabaseType>
            {
                new DatabaseType{ Name = "AbstractClass", Type = TypeKind.Class},
                new DatabaseType{ Name = "ClassWithAttribute", Type = TypeKind.Class},
                new DatabaseType{ Name = "AbstractClass", Type = TypeKind.Class},
                new DatabaseType{ Name = "OuterClass", Type = TypeKind.Class}
            };

            namespaces = new List<DatabaseNamespace>
            {
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.BusinessLogic"},
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.Data", Types = types},
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.Presentation"},
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.Data.CircularReference"}
            };

            var data = new List<DatabaseAssembly>
            {
                new DatabaseAssembly { Name = @"..\..\ExampleDll\TPAApplicationArchitecture.dll", Namespaces = namespaces }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DatabaseAssembly>>();
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(x => x.AssemblyModel).Returns(mockSet.Object);

            serializer = new DBSerializer();
        }

        [TestMethod]
        public void AssemblyNameTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);
            Assert.AreEqual(@"..\..\ExampleDll\TPAApplicationArchitecture.dll", baseAssembly.Name);
        }

        [TestMethod]
        public void NamespacesTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);
            CollectionAssert.AreEquivalent(baseAssembly.Namespaces.ToList(), namespaces);
        }

        [TestMethod]
        public void TypesTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);
            Assert.AreEqual(4, baseAssembly.Namespaces.ToList()[1].Types.Count());
            CollectionAssert.AreEquivalent(baseAssembly.Namespaces.ToList()[1].Types.ToList(), types);
        }
    }
}


using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.ViewModelMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace UnitTestProject
{
    [TestClass()]
    public class ViewModelMetadataTests
    {
        [TestMethod()]
        public void GetAbstractStringTest()
        {
            Assert.AreEqual("abstract", ViewModelMetadata.GetAbstractString(AbstractEnum.Abstract));
            Assert.AreEqual("", ViewModelMetadata.GetAbstractString(AbstractEnum.NotAbstract));
        }

        [TestMethod()]
        public void GetAccessLevelStringTest()
        {
            Assert.AreEqual("public", ViewModelMetadata.GetAccessLevelString(AccessLevel.IsPublic));
            Assert.AreEqual("private", ViewModelMetadata.GetAccessLevelString(AccessLevel.IsPrivate));
            Assert.AreEqual("protected", ViewModelMetadata.GetAccessLevelString(AccessLevel.IsProtected));
            Assert.AreEqual("internal", ViewModelMetadata.GetAccessLevelString(AccessLevel.IsProtectedInternal));
        }

        [TestMethod()]
        public void GetSealedStringTest()
        {
            Assert.AreEqual("sealed", ViewModelMetadata.GetSealedString(SealedEnum.Sealed)); 
            Assert.AreEqual("", ViewModelMetadata.GetSealedString(SealedEnum.NotSealed)); 
        }

        [TestMethod()] 
        public void GetStaticStringTest()
        {
            Assert.AreEqual("static", ViewModelMetadata.GetStaticString(StaticEnum.Static));
            Assert.AreEqual("", ViewModelMetadata.GetStaticString(StaticEnum.NotStatic));
        }

        [TestMethod()]
        public void GetVirtualString()
        {
            Assert.AreEqual("virtual", ViewModelMetadata.GetVirtualString(VirtualEnum.Virtual));
            Assert.AreEqual("", ViewModelMetadata.GetVirtualString(VirtualEnum.NotVirtual));
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Малахов.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, Authorization.FindUser("manager"));
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(false, Authorization.FindUser("manager1"));
        }
    }
}

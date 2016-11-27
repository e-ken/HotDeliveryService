using NUnit.Framework;
using System;

namespace HotDeliveryService.Tests {
    [TestFixture]
    public class Test {
        [Test]
        public void Test_GetStorage_ReturnSqlLiteStorage () {
            IStorage storage = null;
            storage = Storage.Get ("SQLite");
            Assert.IsTrue (storage is SQLiteStorage);
        }

        [Test]
        public void Test_GetStorage_ReturnXmlLiteStorage () {
            IStorage storage = null;
            storage = Storage.Get ("Xml");
            Assert.IsTrue (storage is XmlStorage);
        }
    }
}

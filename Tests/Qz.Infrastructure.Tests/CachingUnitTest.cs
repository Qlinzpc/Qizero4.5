
namespace Qz.Infrastructure.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    using Qz.Caching;

    [TestClass]
    public class CachingUnitTest
    {
        [TestMethod]
        public void TestQCache()
        {
            var key = "qcache";
            var value = QCache.Get(key);
            Assert.IsNull(value);

            QCache.Set(key, "qcache1", TimeSpan.FromSeconds(10));
            value = QCache.Get(key);
            Assert.AreEqual(value, "qcache1");

            QCache.Set("mcache", "MCache2", 10);
            value = QCache.Get("mcache");
            Assert.AreNotEqual(value, "MCache1");

            QCache.Set("mcache1", "MCache3", 10);
            value = QCache.Get("mcache1");
            Assert.AreEqual(value, "MCache3");

            QCache.Remove("mcache");
            value = QCache.Get("mcache");
            Assert.IsNull(value);

            QCache.Set("mcache", "MCache1", 1000);
        }

        [TestMethod]
        public void TestMCache()
        {
            var obj = MCache.Get("mcache");
            Assert.IsNull(obj);

            MCache.Add("mcache", "MCache1", 10);
            obj = MCache.Get("mcache");
            Assert.AreEqual(obj, "MCache1");

            MCache.Set("mcache", "MCache2", 10);
            obj = MCache.Get("mcache");
            Assert.AreNotEqual(obj, "MCache1");

            MCache.Add("mcache1", "MCache3", 10);
            obj = MCache.Get("mcache1");
            Assert.AreEqual(obj, "MCache3");

            MCache.Remove("mcache");
            obj = MCache.Get("mcache");
            Assert.IsNull(obj);

            MCache.Add("mcache", "MCache1", 1000);

        }

    }
}

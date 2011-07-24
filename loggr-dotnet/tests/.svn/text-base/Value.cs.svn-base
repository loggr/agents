using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Value
    {
        [TestMethod]
        public void Value_Base()
        {
            var e = Events.Create().Value(10).Event;
            Assert.IsTrue(e.Value.HasValue);
            Assert.AreEqual(10, e.Value);
        }

        [TestMethod]
        public void Value_Clear()
        {
            var e = Events.Create().ValueClear().Event;
            Assert.IsFalse(e.Value.HasValue);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Timestamp
    {
        [TestMethod]
        public void Timestamp_Base()
        {
            var e = Events.Create().Timestamp(DateTime.Today).Event;
            Assert.IsTrue(e.Timestamp.HasValue);
            Assert.AreEqual(DateTime.Today, e.Timestamp);
        }

        [TestMethod]
        public void Timestamp_Clear()
        {
            var e = Events.Create().TimestampClear().Event;
            Assert.IsFalse(e.Timestamp.HasValue);
        }

        [TestMethod]
        public void Timestamp_Server()
        {
            for (int i = 1; i < 105; i++)
                LogDateEvent(DateTime.Now.AddDays(-3));
        }

        public void LogDateEvent(DateTime dt)
        {
            Events.Create().Text(dt.ToString("MMM dd"))
                .Timestamp(dt)
                .UseLog("testlog", "8aa2f78930aa4930b6f0657b7fe070e9")
                .Post(false);
        }
    }
}

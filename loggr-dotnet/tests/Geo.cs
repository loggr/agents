using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Geo
    {
        [TestMethod]
        public void Geo_Base()
        {
            var e = Events.Create().Geo(-123.456, 567.789).Event;
            Assert.AreEqual("-123.456,567.789", e.Geo);
        }

        [TestMethod]
        public void Geo_String()
        {
            var e = Events.Create().Geo("-123.456", "567.789").Event;
            Assert.AreEqual("-123.456,567.789", e.Geo);
        }

        [TestMethod]
        public void GeoIP_Base()
        {
            var e = Events.Create().GeoIP("47.58.67.123").Event;
            Assert.AreEqual("ip:47.58.67.123", e.Geo);
        }

        [TestMethod]
        public void Geo_Clear()
        {
            var e = Events.Create().Geo(-123.456, 567.789).GeoClear().Event;
            Assert.IsNull(e.Geo);
        }
    }
}

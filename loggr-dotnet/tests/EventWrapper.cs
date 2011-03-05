using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class EventWrapper
    {
        [TestMethod]
        public void EventWrapper_Clear()
        {
            var e = Events.Create().Text("foo").Tags("bar").Geo(1,2).Clear();
            Assert.AreEqual("", e.Event.Text);
            Assert.IsTrue(e.Event.Tags.Count == 0);
            Assert.IsNull(e.Event.Geo);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Link
    {
        [TestMethod]
        public void Link_Base()
        {
            var e = Events.Create().Link("foo").Event;
            Assert.AreEqual("foo", e.Link);
        }

        [TestMethod]
        public void Link_Formatted()
        {
            var e = Events.Create().Link("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("1-foo", e.Link);
        }

        [TestMethod]
        public void Link_Macro()
        {
            var e = Events.Create().Link("foo").Link("123-$$-456").Event;
            Assert.AreEqual("123-foo-456", e.Link);
        }

        [TestMethod]
        public void Link_FormattedMacro()
        {
            var e = Events.Create().Link("foo").Link("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("123-foo-456", e.Link);
        }
    }
}

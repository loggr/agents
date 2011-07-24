using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Source
    {
        [TestMethod]
        public void Source_Base()
        {
            var e = Events.Create().Source("foo").Event;
            Assert.AreEqual("foo", e.Source);
        }

        [TestMethod]
        public void Source_Formatted()
        {
            var e = Events.Create().Source("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("1-foo", e.Source);
        }

        [TestMethod]
        public void Source_Macro()
        {
            var e = Events.Create().Source("foo").Source("123-$$-456").Event;
            Assert.AreEqual("123-foo-456", e.Source);
        }

        [TestMethod]
        public void Source_FormattedMacro()
        {
            var e = Events.Create().Source("foo").Source("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("123-foo-456", e.Source);
        }
    }
}

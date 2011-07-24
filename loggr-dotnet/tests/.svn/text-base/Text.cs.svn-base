using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Text
    {
        [TestMethod]
        public void Text_Base()
        {
            var e = Events.Create().Text("foo").Event;
            Assert.AreEqual("foo", e.Text);
        }

        [TestMethod]
        public void Text_Formatted()
        {
            var e = Events.Create().Text("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("1-foo", e.Text);
        }

        [TestMethod]
        public void Text_Macro()
        {
            var e = Events.Create().Text("foo").Text("123-$$-456").Event;
            Assert.AreEqual("123-foo-456", e.Text);
        }

        [TestMethod]
        public void Text_FormattedMacro()
        {
            var e = Events.Create().Text("foo").Text("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("123-foo-456", e.Text);
        }

        [TestMethod]
        public void Text_Add()
        {
            var e = Events.Create().Text("foo").AddText("bar").Event;
            Assert.AreEqual("foobar", e.Text);
        }

        [TestMethod]
        public void Text_AddFormatted()
        {
            var e = Events.Create().Text("foo").AddText("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("foo1-foo", e.Text);
        }

        [TestMethod]
        public void Text_AddMacro()
        {
            var e = Events.Create().Text("foo").AddText("123-$$-456").Event;
            Assert.AreEqual("foo123-foo-456", e.Text);
        }

        [TestMethod]
        public void Text_AddFormattedMacro()
        {
            var e = Events.Create().Text("foo").AddText("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("foo123-foo-456", e.Text);
        }
    }
}

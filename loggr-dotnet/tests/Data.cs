using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Data
    {
        [TestMethod]
        public void Data_Base()
        {
            var e = Events.Create().Data("foo").Event;
            Assert.AreEqual("foo", e.Data);
        }

        [TestMethod]
        public void Data_Formatted()
        {
            var e = Events.Create().Data("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("1-foo", e.Data);
        }

        [TestMethod]
        public void Data_Macro()
        {
            var e = Events.Create().Data("foo").Data("123-$$-456").Event;
            Assert.AreEqual("123-foo-456", e.Data);
        }

        [TestMethod]
        public void Data_FormattedMacro()
        {
            var e = Events.Create().Data("foo").Data("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("123-foo-456", e.Data);
        }

        [TestMethod]
        public void Data_Add()
        {
            var e = Events.Create().Data("foo").AddData("bar").Event;
            Assert.AreEqual("foobar", e.Data);
        }

        [TestMethod]
        public void Data_AddFormatted()
        {
            var e = Events.Create().Data("foo").AddData("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("foo1-foo", e.Data);
        }

        [TestMethod]
        public void Data_AddMacro()
        {
            var e = Events.Create().Data("foo").AddData("123-$$-456").Event;
            Assert.AreEqual("foo123-foo-456", e.Data);
        }

        [TestMethod]
        public void Data_AddFormattedMacro()
        {
            var e = Events.Create().Data("foo").AddData("{0}-$$-{1}", 123, "456").Event;
            Assert.AreEqual("foo123-foo-456", e.Data);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    class TestFluentEvent : FluentEventBase<TestFluentEvent>
    {
        public override TestFluentEvent Text(string Text)
        {
            return base.Text("bar");
        }
    }

    [TestClass]
    public class Inheritance
    {
        [TestMethod]
        public void Inheritance_Base()
        {
            var e = Events.Create<TestFluentEvent>().Link("foo").Event;
            Assert.AreEqual("foo", e.Link);
        }

        [TestMethod]
        public void Inheritance_Override()
        {
            var e = Events.Create<TestFluentEvent>().Text("foo").Event;
            Assert.AreEqual("bar", e.Text);
        }
    }
}

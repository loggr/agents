using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class Tags
    {
        [TestMethod]
        public void Tags_Base()
        {
            var e = Events.Create().Tags("foo").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
        }

        [TestMethod]
        public void Tags_Multiple()
        {
            var e = Events.Create().Tags("foo bar").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
        }

        [TestMethod]
        public void Tags_BadCharacters()
        {
            var e = Events.Create().Tags("f$o%o b!a>r").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
        }

        [TestMethod]
        public void Tags_Array()
        {
            var e = Events.Create().Tags(new string[] {"foo", "bar"}).Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
        }

        [TestMethod]
        public void Tags_Replace()
        {
            var e = Events.Create().Tags("carrot onion").Tags("foo bar").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
            Assert.IsFalse(e.Tags.Contains("carrot"));
            Assert.IsFalse(e.Tags.Contains("onion"));
        }

        [TestMethod]
        public void Tags_Add()
        {
            var e = Events.Create().Tags("foo").AddTags("bar").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
        }

        [TestMethod]
        public void Tags_AddMultiple()
        {
            var e = Events.Create().Tags("foo").AddTags("bar onion").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
            Assert.IsTrue(e.Tags.Contains("onion"));
        }

        [TestMethod]
        public void Tags_AddBadCharacters()
        {
            var e = Events.Create().Tags("on&~}ion").AddTags("f$o%o b!a>r").Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
            Assert.IsTrue(e.Tags.Contains("onion"));
        }

        [TestMethod]
        public void Tags_AddArray()
        {
            var e = Events.Create().AddTags(new string[] { "foo", "bar" }).Event;
            Assert.IsTrue(e.Tags.Contains("foo"));
            Assert.IsTrue(e.Tags.Contains("bar"));
        } 
    }
}

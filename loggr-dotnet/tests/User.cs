using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    [TestClass]
    public class User
    {
        [TestMethod]
        public void User_Base()
        {
            var e = Events.Create().User("foo").Event;
            Assert.AreEqual("foo", e.User);
        }

        [TestMethod]
        public void User_Formatted()
        {
            var e = Events.Create().User("{0}-{1}", 1, "foo").Event;
            Assert.AreEqual("1-foo", e.User);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loggr;

namespace tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class Fluent
    {
        [TestMethod]
        public void Fluent_CreateEvent()
        {
            var e = Events.Create();
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_CreatEventFromException()
        {
            var ex = new ApplicationException();
            var e = Events.CreateFromException(ex);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_CreatEventFromVariable()
        {
            string foo = "foo";
            var e = Events.CreateFromVariable(foo);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Event()
        {
            var e = Events.Create().Event;
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Post()
        {
            var e = Events.Create().Post();
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_PostAsync()
        {
            var e = Events.Create().Post(true);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Clear()
        {
            var e = Events.Create().Clear();
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_UseLog()
        {
            var e = Events.Create().UseLog("test", "test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Text()
        {
            var e = Events.Create().Text("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_TextFormatted()
        {
            var e = Events.Create().Text("test", 1, 2, 3);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddText()
        {
            var e = Events.Create().AddText("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddTextFormatted()
        {
            var e = Events.Create().AddText("test", 1, 2, 3);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Link()
        {
            var e = Events.Create().Link("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Source()
        {
            var e = Events.Create().Source("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Tags()
        {
            var e = Events.Create().Tags("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_TagsArray()
        {
            var e = Events.Create().Tags(new string[] {"test", "foo"});
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddTags()
        {
            var e = Events.Create().AddTags("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddTagsArray()
        {
            var e = Events.Create().AddTags(new string[] { "test", "foo" });
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Value()
        {
            var e = Events.Create().Value(123);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_ValueClear()
        {
            var e = Events.Create().ValueClear();
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Data()
        {
            var e = Events.Create().Data("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_DataFormatted()
        {
            var e = Events.Create().Data("test", 1, 2, 3);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddData()
        {
            var e = Events.Create().AddData("test");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_AddDataFormatted()
        {
            var e = Events.Create().AddData("test", 1, 2, 3);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Geo()
        {
            var e = Events.Create().Geo(-123.456, -123.456);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_GeoText()
        {
            var e = Events.Create().Geo("-123.456", "-123.456");
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_GeoClear()
        {
            var e = Events.Create().GeoClear();
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_DataType()
        {
            var e = Events.Create().DataType(DataType.html);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Fluent_Several()
        {
            var e = Events.Create()
                    .UseLog("testlog", "o9cywnr8qyco87qycro87qyc8o7")
                    .Text("test")
                    .AddText("test")
                    .Link("test")
                    .Source("test")
                    .Tags("test")
                    .AddTags("test test")
                    .Value(123)
                    .Data("test")
                    .AddData("test")
                    .Geo(-123.456, -123.456)
                    .Geo("-123.456", "-123.456")
                    .Post()
                    .Event;
            Assert.IsNotNull(e);
        }
    }
}

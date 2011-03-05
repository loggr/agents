using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace driver
{
    class Program
    {
        static void Main(string[] args)
        {
            string foo = "foo";
            var ex = new ApplicationException("This was an app exception");
            Loggr.Events.CreateFromException(ex, foo)
                .Text("test event")
                .Source("driver")
                .Post(false);
        }
    }
}

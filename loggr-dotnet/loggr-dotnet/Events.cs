using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loggr
{
    public class Events
    {
        private Events()
        {
        }

        public static FluentEvent Create()
        {
            return new FluentEvent(new Event());
        }

        public static FluentEvent CreateFromException(Exception ex)
        {
            return CreateFromException(ex, null);
        }

        public static FluentEvent CreateFromException(Exception ex, object traceObject)
        {
            return Create()
                .Text(ex.Message.ToString())
                .Tags("error " + Utility.ExceptionFormatter.FormatType(ex))
                .Source(ex.TargetSite == null?"":ex.TargetSite.DeclaringType.ToString())
                .Data(Utility.ExceptionFormatter.Format(ex, traceObject));
        }

        public static FluentEvent CreateFromVariable(object traceObject)
        {
            return CreateFromVariable(traceObject, 1);
        }

        public static FluentEvent CreateFromVariable(object traceObject, int depth)
        {
            return Create().Data(Utility.ObjectDumper.DumpObject(traceObject, depth));
        }
    }
}


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
            return Create<FluentEvent>();
        }

        public static FluentEvent CreateFromException(Exception ex)
        {
            return CreateFromException<FluentEvent>(ex, null);
        }

        public static FluentEvent CreateFromException(Exception ex, object traceObject)
        {
            return CreateFromException<FluentEvent>(ex, traceObject);
        }

        public static FluentEvent CreateFromVariable(object traceObject)
        {
            return CreateFromVariable<FluentEvent>(traceObject, 1);
        }

        public static FluentEvent CreateFromVariable(object traceObject, int depth)
        {
            return CreateFromVariable<FluentEvent>(traceObject, depth);
        }

        #region Generic Methods

        public static T Create<T>() where T : class, IFluentEvent<T>, new()
        {
            return FluentEventBase<T>.Create();
        }

        public static T CreateFromException<T>(Exception ex) where T : class, IFluentEvent<T>, new()
        {
            return CreateFromException<T>(ex, null);
        }

        public static T CreateFromException<T>(Exception ex, object traceObject) where T : class, IFluentEvent<T>, new()
        {
            return Create<T>()
                .Text(ex.Message.ToString())
                .Tags("error " + Utility.ExceptionFormatter.FormatType(ex))
                .Source(ex.TargetSite == null ? "" : ex.TargetSite.DeclaringType.ToString())
                .Data(Utility.ExceptionFormatter.Format(ex, traceObject))
                .DataType(DataType.html);
        }

        public static T CreateFromVariable<T>(object traceObject) where T : class, IFluentEvent<T>, new()
        {
            return CreateFromVariable<T>(traceObject, 1);
        }

        public static T CreateFromVariable<T>(object traceObject, int depth) where T : class, IFluentEvent<T>, new()
        {
            return Create<T>()
                .Data(Utility.ObjectDumper.DumpObject(traceObject, depth))
                .DataType(DataType.html);
        }

        #endregion
    }
}


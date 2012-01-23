using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Loggr
{
    public static class Events
    {
        #region Create Events

        /// <summary>
        /// Creates a new Loggr.Event, wrapped with a fluent event API
        /// </summary>
        /// <returns></returns>
        public static FluentEvent Create()
        {
            return Create<FluentEvent>();
        }

        /// <summary>
        /// Creates a new Loggr.Event pre-formatted by the specified Exception, wrapped with a fluent event API
        /// </summary>
        /// <param name="ex">A System.Exception to introspect</param>
        /// <returns></returns>
        public static FluentEvent CreateFromException(Exception ex)
        {
            return CreateFromException<FluentEvent>(ex, null);
        }

        /// <summary>
        /// Creates a new Loggr.Event pre-formatted by the specified Exception, wrapped with a fluent event API
        /// </summary>
        /// <param name="ex">A System.Exception to introspect</param>
        /// <param name="traceObject">A variable that will be traced in the Loggr.Event's Data property</param>
        /// <returns></returns>
        public static FluentEvent CreateFromException(Exception ex, object traceObject)
        {
            return CreateFromException<FluentEvent>(ex, traceObject);
        }

        /// <summary>
        /// Creates a new Loggr.Event that dumps the specified trace variable, wrapped with a fluent event API
        /// </summary>
        /// <param name="traceObject">A variable that will be traced in the Loggr.Event's Data property</param>
        /// <returns></returns>
        public static FluentEvent CreateFromVariable(object traceObject)
        {
            return CreateFromVariable<FluentEvent>(traceObject, 1);
        }

        /// <summary>
        /// Creates a new Loggr.Event that dumps the specified trace variable, wrapped with a fluent event API
        /// </summary>
        /// <param name="traceObject">A variable that will be traced in the Loggr.Event's Data property</param>
        /// <param name="depth">The depth of recursion to trace the specified variable</param>
        /// <returns></returns>
        public static FluentEvent CreateFromVariable(object traceObject, int depth)
        {
            return CreateFromVariable<FluentEvent>(traceObject, depth);
        }

        #endregion

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


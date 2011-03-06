using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Web;

namespace Loggr.Utility
{
    public class ExceptionFormatter
    {
        private ExceptionFormatter()
        {
        }

        public static string FormatType(string type)
        {
            if (type == null || type.Length == 0)
            {
                return string.Empty;
            }

            int lastDotIndex = CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(type, '.');

            if (lastDotIndex > 0)
            {
                type = type.Substring(lastDotIndex + 1);
            }

            const string conventionalSuffix = "Exception";

            if (type.Length > conventionalSuffix.Length)
            {
                int suffixIndex = type.Length - conventionalSuffix.Length;

                if (string.Compare(type, suffixIndex, conventionalSuffix, 0, conventionalSuffix.Length, true, CultureInfo.InvariantCulture) == 0)
                {
                    type = type.Substring(0, suffixIndex);
                }
            }

            return type;
        }

        public static string FormatType(Exception ex)
        {
            if (ex == null)
            {
                throw new System.ArgumentNullException("error");
            }

            return FormatType(ex.GetType().ToString());
        }

        public static string Format(Exception ex)
        {
            return Format(ex, null);
        }

        public static string Format(Exception ex, object traceObject)
        {
            string res = "";

            // send basic info
            res += string.Format("<b>Exception</b>: {0}<br />", ex.Message);
            res += string.Format("<b>Type</b>: {0}<br />", ex.GetType().ToString());
            res += string.Format("<b>Machine</b>: {0}<br />", System.Environment.MachineName);

            res += "<br />";

            // see if we have web details
            if (HttpContext.Current != null)
            {
                res += string.Format("<b>Request URL</b>: {0}<br />", HttpContext.Current.Request.Url.ToString());
                res += string.Format("<b>Is Authenticated</b>: {0}<br />", (HttpContext.Current.User.Identity.IsAuthenticated ? "True" : "False"));
                res += string.Format("<b>User</b>: {0}<br />", (HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "anonymous"));
                res += string.Format("<b>User host address</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                res += string.Format("<b>Request Method</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"]);
                res += string.Format("<b>Request Address</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                res += string.Format("<b>User Agent</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);
                res += string.Format("<b>Referer</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
                res += string.Format("<b>Script Name</b>: {0}<br />", HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]);
            }

            if (traceObject != null)
            {
                res += "<br />";
                res += "<b>Traced Object(s)</b><br />";
                res += "<br />";
                if (traceObject != null)
                {
                    res += ObjectDumper.DumpObject(traceObject, 1);
                }
                else
                {
                    res += "Not specified<br />";
                }
            }

            res += "<br />";
            res += "<b>Stack Trace</b><br />";
            res += "<br />";

            FormatStack(ex, ref res);

            return res;
        }

        protected static void FormatStack(Exception Ex, ref string Buffer)
        {
            if (Ex.InnerException != null)
            {
                FormatStack(Ex.InnerException, ref Buffer);
            }
            Buffer += string.Format("[{0}: {1}]<br />", Ex.GetType().ToString(), Ex.Message);
            if (Ex.StackTrace != null)
            {
                Buffer += Ex.StackTrace.Replace(Environment.NewLine, "<br />");
            }
            else
            {
                Buffer += "No stack trace";
            }
            Buffer += "<br/>";
            Buffer += "<br/>";
        }
    }
}
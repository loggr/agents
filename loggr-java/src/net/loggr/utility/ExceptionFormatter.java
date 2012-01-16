package net.loggr.utility;

import java.io.PrintWriter;
import java.io.StringWriter;

public class ExceptionFormatter {

	private ExceptionFormatter(){}
	
	public static String FormatType(String type)
    {
        if (type == null || type.length() == 0)
        {
            return "";
        }

        int lastDotIndex = Common.GetLastIndexOf(type, '.');

        if (lastDotIndex > 0)
        {
            type = type.substring(lastDotIndex + 1);
        }

        final String conventionalSuffix = "Exception";

        if (type.length() > conventionalSuffix.length())
        {
            int suffixIndex = type.length() - conventionalSuffix.length();

            if (type.substring(suffixIndex).equalsIgnoreCase(conventionalSuffix))
            {
                type = type.substring(0, suffixIndex);
            }
        }

        return type;
    }

    public static String FormatType(Exception ex)
    {
        if (ex == null)
        {
            throw new java.lang.IllegalArgumentException("error");
        }

        return FormatType(ex.getClass().getCanonicalName());
    }

    public static String Format(Exception ex)
    {
        return Format(ex, null);
    }

    public static String Format(Exception ex, Object traceObject)
    {
        String res = "";

        // send basic info
        res += String.format("<b>Exception</b>: %s<br />", ex.getMessage());
        res += String.format("<b>Type</b>: %s<br />", ex.getClass().getCanonicalName());
        res += String.format("<b>Machine</b>: %s<br />", System.getProperty("user.name"));

        res += "<br />";

        // see if we have web details
        ///TODO: We need to get last error from Apache or something, asking Dave in the AM
//        if (HttpContext.Current != null)
//        {
//            res += string.Format("<b>Request URL</b>: %s<br />", HttpContext.Current.Request.Url.ToString());
//            res += string.Format("<b>Is Authenticated</b>: %s<br />", (HttpContext.Current.User.Identity.IsAuthenticated ? "True" : "False"));
//            res += string.Format("<b>User</b>: %s<br />", (HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "anonymous"));
//            res += string.Format("<b>User host address</b>: %s<br />", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
//            res += string.Format("<b>Request Method</b>: %s<br />", HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"]);
//            res += string.Format("<b>User Agent</b>: %s<br />", HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);
//            res += string.Format("<b>Referer</b>: %s<br />", HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
//            res += string.Format("<b>Script Name</b>: %s<br />", HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]);
//        }

        if (traceObject != null)
        {
            res += "<br />";
            res += "<b>Traced Object(s)</b><br />";
            res += "<br />";
            if (traceObject != null)
            {
                res += ObjectDumper.dumpObject(traceObject, 1);
            }
        }

        res += "<br />";
        res += "<b>Stack Trace</b><br />";
        res += "<br />";

        res += FormatStack(ex, res);

        return res;
    }

    protected static String FormatStack(Exception Ex, String Buffer)
    {
    	String retVal = Buffer;
    	
        if (Ex.getCause() != null)
        {
            FormatStack((Exception)Ex.getCause(), retVal);
        }
        retVal += String.format("[%s: %s]<br />", Ex.getClass().getCanonicalName(), Ex.getMessage());
        if (Ex.getStackTrace() != null)
        {
        	retVal += getStackTrace(Ex);
        }
        else
        {
        	retVal += "No stack trace";
        }
        retVal += "<br/>";
        retVal += "<br/>";
        return (retVal);
    }
    
    public static String getStackTrace(Throwable t)
    {
        StringWriter sw = new StringWriter();
        PrintWriter pw = new PrintWriter(sw, true);
        t.printStackTrace(pw);
        pw.flush();
        sw.flush();
        return sw.toString();
    }
}

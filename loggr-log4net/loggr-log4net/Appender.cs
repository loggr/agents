using System;
using System.Web;

namespace Loggr.Log4Net
{
    public class Appender : log4net.Appender.AppenderSkeleton
    {
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            Loggr.FluentEvent ev = null;
            if (loggingEvent.ExceptionObject != null)
                ev = Loggr.Events.CreateFromException(loggingEvent.ExceptionObject);
            else ev = Loggr.Events.Create();
            ev.Text(loggingEvent.RenderedMessage)
                .Tags(loggingEvent.Level.ToString())
                .Source(loggingEvent.UserName);
            SetGeoIP(ev);
            ev.Post();
        }

        protected static void SetGeoIP(Loggr.FluentEvent ev)
        {
            string ip = "";
            if (HttpContext.Current != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (!String.IsNullOrEmpty(ip))
                    ev.GeoIP(ip);
            }
        }
    }
}

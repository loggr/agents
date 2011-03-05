using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Diagnostics;

namespace Loggr
{
    public class LogClient
    {
        #region Constructors

        public LogClient()
        {
            _logKey = Utility.Configuration.LogKey;
            _apiKey = Utility.Configuration.ApiKey;
            _server = Utility.Configuration.Server;
            _version = Utility.Configuration.Version;
        }

        public LogClient(string LogKey, string ApiKey) : base()
        {
            _logKey = LogKey;
            _apiKey = ApiKey;
        }

        #endregion

        #region Properties

        protected string _apiKey = "";
        protected string _logKey = "";
        protected string _version = "";
        protected string _server = "";

        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }

        public string LogKey
        {
            get
            {
                return _logKey;
            }
            set
            {
                _logKey = value;
            }
        }

        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        #endregion

        #region Post

        internal class LogWebClient : WebClient
        {
            protected override System.Net.WebRequest GetWebRequest(System.Uri address)
            {
                WebRequest req = base.GetWebRequest(address);
                if (req is HttpWebRequest)
                {
                    ((HttpWebRequest)req).KeepAlive = false;
                }
                return req;
            }
        }

        private delegate void PostDelegate(Event EventObj);

        public void Post(Event Event)
        {
            this.Post(Event, true);
        }

        public void Post(Event Event, bool Async)
        {
            if (Async)
            {
                PostDelegate del = new PostDelegate(PostBase);
                del.BeginInvoke(Event, null, null);
            }
            else PostBase(Event);
        }

        [DebuggerNonUserCode()]
        protected void PostBase(Event EventObj)
        {
            if (!string.IsNullOrEmpty(this.ApiKey) && !string.IsNullOrEmpty(this.LogKey))
            {
                string url = string.Format("http://{0}/{1}/logs/{2}/events", this.Server, this.Version, this.LogKey);
                string postStr = string.Format("{0}&apikey={1}", CreateQuerystring(EventObj), this.ApiKey);
                LogWebClient cli = new LogWebClient();
                cli.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cli.UploadData(new Uri(url), "POST", System.Text.Encoding.ASCII.GetBytes(postStr));
            }
        }

        #endregion

        #region Protected Methods

        protected string CreateQuerystring(Event Event)
        {
            string qs = "";
            AppendQuerystringNameValue("text", Event.Text, ref qs);
            AppendQuerystringNameValue("link", Event.Link, ref qs);
            AppendQuerystringNameValueList("tags", Event.Tags, ref qs);
            AppendQuerystringNameValue("source", Event.Source, ref qs);
            AppendQuerystringNameValue("data", Event.Data, ref qs);
            if (Event.Value.HasValue)
            {
                AppendQuerystringNameValueObject("value", Event.Value.Value, ref qs);
            }
            if (Event.Geo != null)
            {
                AppendQuerystringNameValueObject("lat", Event.Geo.Latitude, ref qs);
                AppendQuerystringNameValueObject("lon", Event.Geo.Longitude, ref qs);
            }
            return qs;
        }

        protected object AppendQuerystringNameValue(string Name, string Value, ref string Querystring)
        {
            if (string.IsNullOrEmpty(Value))
                return Querystring;
            if (Querystring.Length > 0)
                Querystring += "&";
            Querystring += string.Format("{0}={1}", Name, HttpUtility.UrlEncode(Value));
            return Querystring;
        }

        protected object AppendQuerystringNameValueObject(string Name, object Value, ref string Querystring)
        {
            if (Querystring.Length > 0)
                Querystring += "&";
            Querystring += string.Format("{0}={1}", Name, HttpUtility.UrlEncode(Value.ToString()));
            return Querystring;
        }

        protected object AppendQuerystringNameValueList(string Name, List<string> Value, ref string Querystring)
        {
            if (Value.Count == 0)
                return Querystring;
            if (Querystring.Length > 0)
                Querystring += "&";
            Querystring += string.Format("{0}={1}", Name, HttpUtility.UrlEncode(string.Join(" ", Value)));
            return Querystring;
        }

        #endregion

    }
}

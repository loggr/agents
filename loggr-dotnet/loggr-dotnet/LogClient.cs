using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Diagnostics;

namespace Loggr
{
    /// <summary>
    /// Allows applications to post events and track users with Loggr
    /// </summary>
    public class LogClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Loggr.LogClient class by using configuration file settings
        /// </summary>
        public LogClient()
        {
            _logKey = Utility.Configuration.LogKey;
            _apiKey = Utility.Configuration.ApiKey;
            _server = Utility.Configuration.Server;
            _version = Utility.Configuration.Version;
        }

        /// <summary>
        /// Initializes a new instance of Loggr.LogClient class by using the specified LogKey and ApiKey
        /// </summary>
        /// <param name="LogKey">Key used to identify a log on Loggr</param>
        /// <param name="ApiKey">Key used to provide access to API on Loggr</param>
        public LogClient(string LogKey, string ApiKey) : this()
        {
            _logKey = LogKey;
            _apiKey = ApiKey;
        }

        /// <summary>
        /// Initializes a new instance of Loggr.LogClient class by using the specified LogKey, ApiKey, Server and Version
        /// </summary>
        /// <param name="LogKey">Key used to identify a log on Loggr</param>
        /// <param name="ApiKey">Key used to provide access to API on Loggr</param>
        /// <param name="Server">Hostname of server for posting to Loggr (typically post.loggr.net)</param>
        /// <param name="Version">Version of API for posting to Loggr (typically 1)</param>
        public LogClient(string LogKey, string ApiKey, string Server, string Version) : this()
        {
            _logKey = LogKey;
            _apiKey = ApiKey;
            _server = Server;
            _version = Version;
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

        private delegate void PostEventDelegate(Event eventObj);

        /// <summary>
        /// Posts the specified event to Loggr (posts asynchronously)
        /// </summary>
        /// <param name="eventObj">A Loggr.Event that contains the event to send</param>
        public void Post(Event eventObj)
        {
            this.Post(eventObj, true);
        }

        /// <summary>
        /// Posts the specified event to Loggr
        /// </summary>
        /// <param name="eventObj">A Loggr.Event that contains the event to send</param>
        /// <param name="async">A bool that specifies how the event should be posted. Typically an application will post asynchronously for best performance, but sometimes an event needs to be posted synchronously if the application needs to block until the event has completed posting</param>
        public void Post(Event eventObj, bool async)
        {
            // modify event based on configuration
            MergeConfigurationWithEvent(eventObj);

            // post async or sync
            if (async)
            {
                PostEventDelegate del = new PostEventDelegate(PostEventBase);
                del.BeginInvoke(eventObj, null, null);
            }
            else PostEventBase(eventObj);
        }

        [DebuggerNonUserCode()]
        protected void PostEventBase(Event eventObj)
        {
            if (!string.IsNullOrEmpty(this.ApiKey) && !string.IsNullOrEmpty(this.LogKey))
            {
                string url = string.Format("http://{0}/{1}/logs/{2}/events", this.Server, this.Version, this.LogKey);
                string postStr = string.Format("{0}&apikey={1}", CreateEventQuerystring(eventObj), this.ApiKey);
                LogWebClient cli = new LogWebClient();
                cli.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                try
                {
                    cli.UploadData(new Uri(url), "POST", System.Text.Encoding.ASCII.GetBytes(postStr));
                }
                catch (Exception)
                {
                    // ignore ex from post
                }
            }
        }

        #endregion

        #region Track

        private delegate void TrackUserDelegate(string username, string email, string page);

        /// <summary>
        /// Tracks a user on Loggr
        /// </summary>
        /// <param name="username">Username of user to track</param>
        public void TrackUser(string username)
        {
            this.TrackUser(username, "", "", true);
        }

        /// <summary>
        /// Tracks a user on Loggr
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        public void TrackUser(string username, string email)
        {
            this.TrackUser(username, email, "", true);
        }

        /// <summary>
        /// Tracks a user on Loggr
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        /// <param name="page">Page being viewed by user</param>
        public void TrackUser(string username, string email, string page)
        {
            this.TrackUser(username, email, page, true);
        }

        /// <summary>
        /// Tracks a user on Loggr
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        /// <param name="page">Page being viewed by user</param>
        /// <param name="async">A bool that specifies how user tracking should be sent to Loggr. Typically an application will post asynchronously for best performance, but sometimes it needs to be posted synchronously if the application needs to block until the post has completed</param>
        public void TrackUser(string username, string email, string page, bool async)
        {
            // post async or sync
            if (async)
            {
                TrackUserDelegate del = new TrackUserDelegate(TrackUserBase);
                del.BeginInvoke(username, email, page, null, null);
            }
            else TrackUserBase(username, email, page);
        }

        [DebuggerNonUserCode()]
        protected void TrackUserBase(string username, string email, string page)
        {
            if (!string.IsNullOrEmpty(this.ApiKey) && !string.IsNullOrEmpty(this.LogKey))
            {
                string url = string.Format("http://{0}/{1}/logs/{2}/users", this.Server, this.Version, this.LogKey);
                string postStr = string.Format("{0}&apikey={1}", CreateUserQuerystring(username, email, page), this.ApiKey);
                LogWebClient cli = new LogWebClient();
                cli.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                try
                {
                    cli.UploadData(new Uri(url), "POST", System.Text.Encoding.ASCII.GetBytes(postStr));
                }
                catch (Exception)
                {
                    // ignore ex from post
                }
            }
        }

        #endregion

        #region Protected Methods

        protected void MergeConfigurationWithEvent(Event eventObj)
        {
            // merge in default tags from config file
            if (!string.IsNullOrEmpty(Utility.Configuration.Tags))
            {
                eventObj.Tags.AddRange(Utility.Tags.TokenizeAndFormat(Utility.Configuration.Tags));
            }

            // overwrite default source from config file
            if (!string.IsNullOrEmpty(Utility.Configuration.Source))
            {
                eventObj.Source = Utility.Configuration.Source;
            }

            // overwrite default user from config file
            if (!string.IsNullOrEmpty(Utility.Configuration.User))
            {
                eventObj.User = Utility.Configuration.User;
            }
        }

        protected string CreateEventQuerystring(Event eventObj)
        {
            string qs = "";
            AppendQuerystringNameValue("text", eventObj.Text, ref qs, 500);
            AppendQuerystringNameValue("link", eventObj.Link, ref qs, 200);
            AppendQuerystringNameValueList("tags", eventObj.Tags, ref qs, 200);
            AppendQuerystringNameValue("source", eventObj.Source, ref qs, 200);
            AppendQuerystringNameValue("user", eventObj.User, ref qs, 200);
            if (eventObj.DataType == DataType.html)
            {
                AppendQuerystringNameValue("data", "@html" + Environment.NewLine + eventObj.Data, ref qs, 5120);
            }
            else if (eventObj.DataType == DataType.json)
            {
                AppendQuerystringNameValue("data", "@json" + Environment.NewLine + eventObj.Data, ref qs, 5120);
            }
            else
            {
                AppendQuerystringNameValue("data", eventObj.Data, ref qs, 5120);
            }
            if (eventObj.Timestamp.HasValue)
            {
                AppendQuerystringNameValueDate("timestamp", eventObj.Timestamp, ref qs, 30);
            }
            if (eventObj.Value.HasValue)
            {
                AppendQuerystringNameValueObject("value", eventObj.Value.Value, ref qs, 30);
            }
            if (eventObj.Geo != null)
            {
                AppendQuerystringNameValueObject("geo", eventObj.Geo, ref qs, 30);
            }
            return qs;
        }

        protected string CreateUserQuerystring(string username, string email, string page)
        {
            string qs = "";
            AppendQuerystringNameValue("user", username, ref qs, 100);
            AppendQuerystringNameValue("email", email, ref qs, 100);
            AppendQuerystringNameValue("page", page, ref qs, 100);

            return qs;
        }

        protected object AppendQuerystringNameValue(string name, string value, ref string querystring, int length)
        {
            if (string.IsNullOrEmpty(value))
                return querystring;
            if (querystring.Length > 0)
                querystring += "&";
            querystring += string.Format("{0}={1}", name, HttpUtility.UrlEncode(Cap(value, length)));
            return querystring;
        }

        protected object AppendQuerystringNameValueObject(string name, object value, ref string querystring, int length)
        {
            if (querystring.Length > 0)
                querystring += "&";
            querystring += string.Format("{0}={1}", name, HttpUtility.UrlEncode(Cap(value.ToString(), length)));
            return querystring;
        }

        protected object AppendQuerystringNameValueList(string name, List<string> value, ref string querystring, int length)
        {
            if (value.Count == 0)
                return querystring;
            if (querystring.Length > 0)
                querystring += "&";
            querystring += string.Format("{0}={1}", name, HttpUtility.UrlEncode(Cap(string.Join(" ", value.ToArray()), length)));
            return querystring;
        }

        protected object AppendQuerystringNameValueDate(string name, DateTime? value, ref string querystring, int length)
        {
            if (!value.HasValue)
                return querystring;
            if (querystring.Length > 0)
                querystring += "&";
            querystring += string.Format("{0}={1}", name, HttpUtility.UrlEncode(Cap(DateToMilliseconds(value.Value).ToString(), length)));
            return querystring;
        }

        protected double DateToMilliseconds(DateTime input)
        {
            return (input - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        protected string Cap(string input, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("Length", length, "Length must be > 0");
            else if (length == 0 || input.Length == 0)
                return "";
            else if (input.Length <= length)
                return input;
            else
                return input.Substring(0, length);
        }

        #endregion

    }
}

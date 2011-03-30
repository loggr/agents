using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Loggr
{
    public class FluentEvent : FluentEventBase<FluentEvent>
    {
        // inherits for creating a simple API
    }

    public class FluentEventBase<T> : IFluentEvent<T> where T : class, IFluentEvent<T>, new()
    {
        protected Event _event = new Event();
        protected LogClient _client;

        protected FluentEventBase()
        {
        }

        internal static T Create()
        {
            return new T();
        }

        #region Fluent Methods

        public Event Event
        {
            get
            {
                return _event;
            }
        }

        public virtual T Post()
        {
            this.Post(true);
            return this as T;
        }

        public virtual T Post(bool Async)
        {
            if (_client == null)
            {
                _client = new LogClient();
            }
            _client.Post(this.Event, Async);
            return this as T;
        }

        public virtual T Clear()
        {
            _event = new Event();
            return this as T;
        }

        public virtual T Text(string Text)
        {
            this.Event.Text = AssignWithMacro(Text, this.Event.Text);
            return this as T;
        }

        public virtual T Text(string FormatString, params object[] FormatArguments)
        {
            return Text(string.Format(FormatString, FormatArguments));
        }

        public virtual T AddText(string Text)
        {
            this.Event.Text += AssignWithMacro(Text, this.Event.Text);
            return this as T;
        }

        public virtual T AddText(string FormatString, params object[] FormatArguments)
        {
            return AddText(string.Format(FormatString, FormatArguments));
        }

        public virtual T Link(string Link)
        {
            this.Event.Link = AssignWithMacro(Link, this.Event.Link);
            return this as T;
        }

        public virtual T Link(string FormatString, params object[] FormatArguments)
        {
            return Link(string.Format(FormatString, FormatArguments));
        }

        public virtual T Source(string Source)
        {
            this.Event.Source = AssignWithMacro(Source, this.Event.Source);
            return this as T;
        }

        public virtual T Source(string FormatString, params object[] FormatArguments)
        {
            return Source(string.Format(FormatString, FormatArguments));
        }

        public virtual T Tags(string Tags)
        {
            this.Event.Tags = new List<string>();
            return this.AddTags(Tags);
        }

        public virtual T Tags(string[] Tags)
        {
            this.Event.Tags = new List<string>();
            return this.AddTags(Tags);
        }

        public virtual T AddTags(string Tags)
        {
            this.Event.Tags.AddRange(TokenizeAndFormatTags(Tags));
            return this as T;
        }

        public virtual T AddTags(string[] Tags)
        {
            this.Event.Tags.AddRange(TokenizeAndFormatTags(Tags));
            return this as T;
        }

        public virtual T Value(double Value)
        {
            this.Event.Value = Value;
            return this as T;
        }

        public virtual T ValueClear()
        {
            this.Event.Value = null;
            return this as T;
        }

        public virtual T Data(string Data)
        {
            this.Event.Data = AssignWithMacro(Data, this.Event.Data);
            return this as T;
        }

        public virtual T Data(string FormatString, params object[] FormatArguments)
        {
            return Data(string.Format(FormatString, FormatArguments));
        }

        public virtual T AddData(string Data)
        {
            this.Event.Data += AssignWithMacro(Data, this.Event.Data);
            return this as T;
        }

        public virtual T AddData(string FormatString, params object[] FormatArguments)
        {
            return AddData(string.Format(FormatString, FormatArguments));
        }

        public virtual T DataType(DataType type)
        {
            this.Event.DataType = type;
            return this as T;
        }

        public virtual T Geo(double Lat, double Lon)
        {
            this.Event.Geo = String.Format("{0},{1}", Lat.ToString(), Lon.ToString());
            return this as T;
        }

        public virtual T Geo(string Lat, string Lon)
        {
            double lat = 0, lon = 0;
            double.TryParse(Lat, out lat);
            double.TryParse(Lon, out lon);
            return this.Geo(lat, lon);
        }

        public virtual T GeoIP(string IPAddress)
        {
            this.Event.Geo = String.Format("ip:{0}", IPAddress);
            return this as T;
        }

        public virtual T GeoClear()
        {
            this.Event.Geo = null;
            return this as T;
        }

        public T UseLog(string LogKey, string ApiKey)
        {
            _client = new LogClient(LogKey, ApiKey);
            return this as T;
        }

        #endregion

        #region Protected Methods

        protected string AssignWithMacro(string Input, string Base)
        {
            return Input.Replace("$$", Base);
        }

        protected string[] TokenizeAndFormatTags(string[] Tags, bool StripSpecialChars = true)
        {
            return TokenizeAndFormatTags(string.Join(" ", Tags), StripSpecialChars);
        }

        protected string[] TokenizeAndFormatTags(string Tagstring, bool StripSpecialChars = true)
        {
            List<string> res = new List<string>();
            string[] tokens = Tagstring.ToLower().Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= (tokens.Length - 1); i++)
            {
                string token = null;
                if (StripSpecialChars)
                {
                    token = Regex.Replace(tokens[i].Trim(), "[^a-zA-Z0-9\\-]", "");
                }
                else
                {
                    token = tokens[i].Trim();
                }
                if (token.Length > 0)
                {
                    res.Add(token);
                }
            }
            return res.ToArray();
        }

        #endregion
    }
}
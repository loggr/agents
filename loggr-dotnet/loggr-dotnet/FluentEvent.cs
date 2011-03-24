using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Loggr
{
    public class FluentEvent
    {
        protected Event _event;
        protected LogClient _client;

        internal FluentEvent(Event Event)
        {
            _event = Event;
        }

        #region Fluent Methods

        public Event Event
        {
            get
            {
                return _event;
            }
        }

        public FluentEvent Post()
        {
            this.Post(true);
            return this;
        }

        public FluentEvent Post(bool Async)
        {
            if (_client == null)
            {
                _client = new LogClient();
            }
            _client.Post(this.Event, Async);
            return this;
        }

        public FluentEvent Clear()
        {
            _event = new Event();
            return this;
        }

        public FluentEvent Text(string Text)
        {
            this.Event.Text = AssignWithMacro(Text, this.Event.Text);
            return this;
        }

        public FluentEvent Text(string FormatString, params object[] FormatArguments)
        {
            return Text(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent AddText(string Text)
        {
            this.Event.Text += AssignWithMacro(Text, this.Event.Text);
            return this;
        }

        public FluentEvent AddText(string FormatString, params object[] FormatArguments)
        {
            return AddText(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent Link(string Link)
        {
            this.Event.Link = AssignWithMacro(Link, this.Event.Link);
            return this;
        }

        public FluentEvent Link(string FormatString, params object[] FormatArguments)
        {
            return Link(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent Source(string Source)
        {
            this.Event.Source = AssignWithMacro(Source, this.Event.Source);
            return this;
        }

        public FluentEvent Source(string FormatString, params object[] FormatArguments)
        {
            return Source(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent Tags(string Tags)
        {
            this.Event.Tags = new List<string>();
            return this.AddTags(Tags);
        }

        public FluentEvent Tags(string[] Tags)
        {
            this.Event.Tags = new List<string>();
            return this.AddTags(Tags);
        }

        public FluentEvent AddTags(string Tags)
        {
            this.Event.Tags.AddRange(TokenizeAndFormatTags(Tags));
            return this;
        }

        public FluentEvent AddTags(string[] Tags)
        {
            this.Event.Tags.AddRange(TokenizeAndFormatTags(Tags));
            return this;
        }

        public FluentEvent Value(double Value)
        {
            this.Event.Value = Value;
            return this;
        }

        public FluentEvent ValueClear()
        {
            this.Event.Value = null;
            return this;
        }

        public FluentEvent Data(string Data)
        {
            this.Event.Data = AssignWithMacro(Data, this.Event.Data);
            return this;
        }

        public FluentEvent Data(string FormatString, params object[] FormatArguments)
        {
            return Data(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent AddData(string Data)
        {
            this.Event.Data += AssignWithMacro(Data, this.Event.Data);
            return this;
        }

        public FluentEvent AddData(string FormatString, params object[] FormatArguments)
        {
            return AddData(string.Format(FormatString, FormatArguments));
        }

        public FluentEvent DataType(DataType type)
        {
            this.Event.DataType = type;
            return this;
        }

        public FluentEvent Geo(double Lat, double Lon)
        {
            this.Event.Geo = String.Format("{0},{1}", Lat.ToString(), Lon.ToString());
            return this;
        }

        public FluentEvent Geo(string Lat, string Lon)
        {
            double lat = 0, lon = 0;
            double.TryParse(Lat, out lat);
            double.TryParse(Lon, out lon);
            return this.Geo(lat, lon);
        }

        public FluentEvent GeoIP(string IPAddress)
        {
            this.Event.Geo = String.Format("ip:{0}", IPAddress);
            return this;
        }

        public FluentEvent GeoClear()
        {
            this.Event.Geo = null;
            return this;
        }

        public FluentEvent UseLog(string LogKey, string ApiKey)
        {
            _client = new LogClient(LogKey, ApiKey);
            return this;
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
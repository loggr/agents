using System;
using System.Collections.Generic;
using System.Text;

namespace Loggr
{
    public enum DataType
    {
        html = 0,
        plaintext = 1,
        json = 2
    }

    /// <summary>
    /// Represents an event that can be posted to Loggr using the Loggr.LogClient class
    /// </summary>
    public class Event
    {
        #region Properties

        protected string _text = "";
        protected string _link = "";
        protected string _source = "";
        protected List<string> _tags = new List<string>();
        protected string _data = "";
        protected double? _value;
        protected string _geo;
        protected DataType _dataType = DataType.plaintext;
        protected string _user;
        protected DateTime? _timestamp;

        /// <summary>
        /// The text value for an event
        /// </summary>
        public string Text
        {
            get
            {
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();

                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// A URL to be associated with the event
        /// </summary>
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }

        /// <summary>
        /// A source to be associated with an event
        /// </summary>
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        /// <summary>
        /// A list of tags to be associated with an event
        /// </summary>
        public List<string> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
            }
        }

        /// <summary>
        /// String data to be associated with an event
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// A numeric value to be associated with an event
        /// </summary>
        public double? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Geographic coordinates to be associated with an event
        /// </summary>
        public string Geo
        {
            get
            {
                return _geo;
            }
            set
            {
                _geo = value;
            }
        }

        /// <summary>
        /// A format for the Data property
        /// </summary>
        public DataType DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                _dataType = value;
            }
        }

        /// <summary>
        /// A username to be associated with an event
        /// </summary>
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// An optional timestamp to indicate when an event was created. This is typically not needed unless your application
        /// is queuing events for later posting to Loggr
        /// </summary>
        public DateTime? Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
            }
        }

        #endregion
    }
}
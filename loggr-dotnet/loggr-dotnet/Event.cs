using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loggr
{
    public enum DataType
    {
        html = 0,
        plaintext = 1
    }

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

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

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

        #endregion
    }
}
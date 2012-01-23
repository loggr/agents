using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Loggr.Utility
{
    public class Configuration
    {
        private Configuration()
        {
        }

        protected static string _apiKey = "";
        protected static string _logKey = "";
        protected static string _server = "";
        protected static string _version = "";
        protected static string _tags = "";
        protected static string _source = "";
        protected static string _user = "";

        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                {
                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "apiKey")
                            {
                                _apiKey = config[i].ToString();
                            }
                        }
                    }
                }
                return _apiKey;
            }
        }

        public static string LogKey
        {
            get
            {
                if (string.IsNullOrEmpty(_logKey))
                {
                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "logKey")
                            {
                                _logKey = config[i].ToString();
                            }
                        }
                    }
                }
                return _logKey;
            }
        }

        public static string Server
        {
            get
            {
                if (string.IsNullOrEmpty(_server))
                {
                    _server = "post.loggr.net";

                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "server")
                            {
                                _server = config[i].ToString();
                            }
                        }
                    }
                }
                return _server;
            }
        }

        public static string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    _version = "1";

                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "version")
                            {
                                _version = config[i].ToString();
                            }
                        }
                    }
                }
                return _version;
            }
        }

        public static string Tags
        {
            get
            {
                if (string.IsNullOrEmpty(_tags))
                {
                    _tags = "";

                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "tags")
                            {
                                _tags = config[i].ToString();
                            }
                        }
                    }
                }
                return _tags;
            }
        }

        public static string Source
        {
            get
            {
                if (string.IsNullOrEmpty(_source))
                {
                    _source = "";

                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "source")
                            {
                                _source = config[i].ToString();
                            }
                        }
                    }
                }
                return _source;
            }
        }

        public static string User
        {
            get
            {
                if (string.IsNullOrEmpty(_user))
                {
                    _user = "";

                    NameValueCollection config = new NameValueCollection();
                    config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("loggr/log");
                    if ((config != null))
                    {
                        int i = 0;
                        for (i = 0; i <= config.Keys.Count - 1; i++)
                        {
                            if (config.Keys[i] == "user")
                            {
                                _user = config[i].ToString();
                            }
                        }
                    }
                }
                return _user;
            }
        }

    }
}

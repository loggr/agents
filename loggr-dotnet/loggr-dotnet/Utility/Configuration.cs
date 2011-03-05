using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

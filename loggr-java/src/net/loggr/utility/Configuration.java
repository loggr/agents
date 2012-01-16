package net.loggr.utility;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

public class Configuration {
	private Configuration() {
	}

	protected static String _apiKey = "";
	protected static String _logKey = "";
	protected static String _server = "";
	protected static String _version = "";
	protected static String _tags = "";
	protected static String _source = "";

	public static String getApiKey() {
		if (Common.IsNullOrEmpty(_apiKey)) {
			try {
				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_apiKey = props.getProperty("apiKey");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return _apiKey;
	}

	public static String getLogKey() {
		if (Common.IsNullOrEmpty(_logKey)) {
			try {
				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_logKey = props.getProperty("logKey");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return _logKey;
	}

	public static String getServer() {
		if (Common.IsNullOrEmpty(_server)) {
			try {
				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_server = props.getProperty("server");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return _server;
	}

	public static String getVersion() {
		if (Common.IsNullOrEmpty(_version)) {
			try {
				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_version = props.getProperty("version");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return _version;
	}

	public static String getTags() {
		if (Common.IsNullOrEmpty(_tags)) {
			try {
				_tags = "";

				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_tags = props.getProperty("tags");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return (_tags);
	}

	public static String getSource() {
		if (Common.IsNullOrEmpty(_source)) {
			try {
				_source = "";

				Properties props = new Properties();
				FileInputStream in = new FileInputStream(System.getProperty("user.dir") + "/src/app.properties");
				props.load(in);
				in.close();

				if (props != null) {
					_source = props.getProperty("source");
				}
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
		return (_source);
	}

}

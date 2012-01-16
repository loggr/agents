package net.loggr;

import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;

import net.loggr.utility.Common;
import net.loggr.utility.Configuration;
import net.loggr.utility.Tags;


public class LogClient {
	 protected String _apiKey = "";
     protected String _logKey = "";
     protected String _version = "";
     protected String _server = "";
	
	public LogClient()
    {
        _logKey = Configuration.getLogKey();
        _apiKey = Configuration.getApiKey();
        _server = Configuration.getServer();
        _version = Configuration.getVersion();
    }

    public LogClient(String LogKey, String ApiKey)
    {
        _logKey = LogKey;
        _apiKey = ApiKey;
        _server = Configuration.getServer();
        _version = Configuration.getVersion();
    }
    
    public String getApiKey(){
    	return (_apiKey);
    }
    public void setApiKey(String value){
    	_apiKey = value;
    }
    public String getLogKey(){
    	return (_logKey);
    }
    public void setLogKey(String value){
    	_logKey = value;
    }
    public String getVersion(){
    	return (_version);
    }
    public void setVersion(String value){
    	_version = value;
    }
    public String getServer(){
    	return (_server);
    }
    public void setServer(String value){
    	_server = value;
    }
    
    public void post(Event Event, boolean Async)
    {
    	// modify event based on configuration
        mergeConfigurationWithEvent(Event);

        // post async or sync
        postBase(Event);
    }
    
    protected void postBase(Event EventObj)
    {
        if (this._apiKey != null && this._apiKey != "" && this._logKey != null && this._logKey != "")
        {
            String url = String.format("http://%s/%s/logs/%s/events", this._server, this._version, this._logKey);
            String postStr = String.format("%s&apikey=%s", createQuerystring(EventObj), this._apiKey);
            LogWebClient cli = new LogWebClient();
            cli.addHeader("Content-Type", "application/x-www-form-urlencoded");
            try {
				cli.uploadData(new URL(url), "POST", postStr);
			} catch (MalformedURLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
    }
    
    
    protected void mergeConfigurationWithEvent(Event Event)
    {
        // merge in default tags from config file
        if (!Common.IsNullOrEmpty(Configuration.getTags()))
        {
            Event.getTags().addAll(Arrays.asList(Tags.TokenizeAndFormat(Configuration.getTags())));
        }

        // overwrite default source from config file
        if (!Common.IsNullOrEmpty(Configuration.getSource()))
        {
            Event.setSource(Configuration.getSource());
        }
    }

    protected String createQuerystring(Event Event)
    {
        String qs = "";
        qs = (String)appendQuerystringNameValue("text", Event.getText(), qs);
        qs = (String)appendQuerystringNameValue("link", Event.getLink(), qs);
        qs = (String)appendQuerystringNameValueList("tags", Event.getTags(), qs);
        qs = (String)appendQuerystringNameValue("source", Event.getSource(), qs);
        qs = (String)appendQuerystringNameValue("user", Event.getUser(), qs);
        if (Event.getDataType() == DataType.html)
        {
        	qs = (String)appendQuerystringNameValue("data", "@html" + System.getProperty("line.separator") + Event.getData(), qs);
        }
        else
        {
        	qs = (String)appendQuerystringNameValue("data", Event.getData(), qs);
        }
        if (Event.getValue() != null)
        {
        	qs = (String)appendQuerystringNameValueObject("value", Event.getValue(), qs);
        }
        if (Event.getGeo() != null)
        {
        	qs = (String)appendQuerystringNameValueObject("geo", Event.getGeo(), qs);
        }
        return qs;
    }

    protected Object appendQuerystringNameValue(String Name, String Value, String Querystring)
    {
        if (Common.IsNullOrEmpty(Value))
            return Querystring;
        if (Querystring.length() > 0)
            Querystring += "&";
        try {
			Querystring += String.format("%s=%s", Name, java.net.URLEncoder.encode(Value, "UTF-8"));
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return Querystring;
    }

    protected Object appendQuerystringNameValueObject(String Name, Object Value, String Querystring)
    {
        if (Querystring.length() > 0)
            Querystring += "&";
        try {
			Querystring += String.format("%s=%s", Name,java.net.URLEncoder.encode(Value.toString(), "UTF-8"));
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return Querystring;
    }

    protected Object appendQuerystringNameValueList(String Name, ArrayList<String> Value, String Querystring)
    {
        if (Value.size() == 0)
            return Querystring;
        if (Querystring.length() > 0)
            Querystring += "&";
        String [] values = new String[Value.size()];
        values = Value.toArray(values);
        try {
			Querystring += String.format("%s=%s", Name, java.net.URLEncoder.encode(Common.Join(" ", values), "UTF-8"));
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return Querystring;
    }
}

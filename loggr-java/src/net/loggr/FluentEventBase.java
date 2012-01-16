package net.loggr;

import java.util.ArrayList;
import java.util.Arrays;

public abstract class FluentEventBase<T> implements IFluentEvent<T> {
	protected Event _event = new Event();
    protected LogClient _client;

    protected FluentEventBase()
    {
    }

    protected FluentEventBase<T> create(Class<T> _class) throws InstantiationException, IllegalAccessException
    {
        return this;
    }
    

    public Event getEvent()
    {
        return _event;
    }

    public T post()
    {
        this.post(true);
        return getReturnClass();
    }

    public T post(boolean Async)
    {
        if (_client == null)
        {
            _client = new LogClient();
        }
        _client.post(this._event, Async);
        return getReturnClass();
    }

    public T clear()
    {
        _event = new Event();
        return getReturnClass();
    }

	public T text(String Text)
    {
        this._event.setText(assignWithMacro(Text, this._event.getText()));
        return getReturnClass();
    }

    public T text(String FormatString, Object... FormatArguments)
    {
        return text(String.format(FormatString, FormatArguments));
    }

    public T addText(String Text)
    {
        this._event.setText(this._event.getText() + assignWithMacro(Text, this._event.getText()));
        return getReturnClass();
    }

    public T addText(String FormatString, Object... FormatArguments)
    {
        return addText(String.format(FormatString, FormatArguments));
    }

    public T link(String Link)
    {
        this._event.setLink(assignWithMacro(Link, this._event.getLink()));
        return getReturnClass();
    }

    public T link(String FormatString, Object... FormatArguments)
    {
        return link(String.format(FormatString, FormatArguments));
    }

    public T source(String Source)
    {
        this._event.setSource(assignWithMacro(Source, this._event.getSource()));
        return getReturnClass();
    }

    public T source(String FormatString, Object... FormatArguments)
    {
        return source(String.format(FormatString, FormatArguments));
    }

    public T user(String User)
    {
        this._event.setUser(assignWithMacro(User, this._event.getUser()));
        return getReturnClass();
    }

    public T user(String FormatString, Object... FormatArguments)
    {
        return user(String.format(FormatString, FormatArguments));
    }

    public T tags(String Tags)
    {
        this._event.setTags(new ArrayList<String>());
        return this.addTags(Tags);
    }

    public T tags(String[] Tags)
    {
        this._event.setTags(new ArrayList<String>());
        return this.addTags(Tags);
    }

    public T addTags(String tags)
    {
        this._event.getTags().addAll(Arrays.asList(net.loggr.utility.Tags.TokenizeAndFormat(tags)));
        return getReturnClass();
    }

    public T addTags(String[] tags)
    {
        this._event.getTags().addAll(Arrays.asList(net.loggr.utility.Tags.TokenizeAndFormat(tags)));
        return getReturnClass();
    }

    public T value(double Value)
    {
        this._event.setValue(Value);
        return getReturnClass();
    }

    public T valueClear()
    {
        this._event.setValue(new Double(0));
        return getReturnClass();
    }

    public T data(String Data)
    {
        this._event.setData(assignWithMacro(Data, this._event.getData()));
        return getReturnClass();
    }

    public T data(String FormatString, Object... FormatArguments)
    {
        return data(String.format(FormatString, FormatArguments));
    }

    public T addData(String Data)
    {
        this._event.setData(this._event.getData() + assignWithMacro(Data, this._event.getData()));
        return getReturnClass();
    }

    public T addData(String FormatString, Object... FormatArguments)
    {
        return addData(String.format(FormatString, FormatArguments));
    }

    public T dataType(DataType type)
    {
        this._event.setDataType(type);
        return getReturnClass();
    }

    public T geo(double Lat, double Lon)
    {
        this._event.setGeo(String.format("%s,%s", Lat, Lon));
        return getReturnClass();
    }

    public T geo(String Lat, String Lon)
    {
        double lat = 0, lon = 0;
        try{
        	lat = Double.parseDouble(Lat);
        }catch (NumberFormatException ex){
        	// no worries
        }

        try{
        	lon = Double.parseDouble(Lon);
        }catch (NumberFormatException ex){
        	// no worries
        }
        
        return this.geo(lat, lon);
    }

    public T geoIP(String IPAddress)
    {
        this._event.setGeo(String.format("ip:%s", IPAddress));
        return getReturnClass();
    }

    public T geoClear()
    {
        this._event.setGeo(null);
        return getReturnClass();
    }

    public T useLog(String LogKey, String ApiKey)
    {
        _client = new LogClient(LogKey, ApiKey);
        return getReturnClass();
    }

    @SuppressWarnings("unchecked")
	protected T getReturnClass()
    {
    	return (T)(this);
    }
    
    protected String assignWithMacro(String Input, String Base)
    {
        return Input.replace("$$", Base);
    }

	
}

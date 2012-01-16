package net.loggr;

import java.util.ArrayList;

public class Event {

    public String getText() {
		return _text;
	}
	public void setText(String _text) {
		this._text = _text;
	}
	public String getLink() {
		return _link;
	}
	public void setLink(String _link) {
		this._link = _link;
	}
	public String getSource() {
		return _source;
	}
	public void setSource(String _source) {
		this._source = _source;
	}
	public ArrayList<String> getTags() {
		return _tags;
	}
	public void setTags(ArrayList<String> _tags) {
		this._tags = _tags;
	}
	public String getData() {
		return _data;
	}
	public void setData(String _data) {
		this._data = _data;
	}
	public Double getValue() {
		return _value;
	}
	public void setValue(Double _value) {
		this._value = _value;
	}
	public String getGeo() {
		return _geo;
	}
	public void setGeo(String _geo) {
		this._geo = _geo;
	}
	public DataType getDataType() {
		return _dataType;
	}
	public void setDataType(DataType _dataType) {
		this._dataType = _dataType;
	}
	public String getUser() {
		return _user;
	}
	public void setUser(String _user) {
		this._user = _user;
	}
	
	
	
	protected String _text = "";
    protected String _link = "";
    protected String _source = "";
    protected ArrayList<String> _tags = new ArrayList<String>();
    protected String _data = "";
    protected Double _value;
    protected String _geo;
    protected DataType _dataType = DataType.plaintext;
    protected String _user;
    
    
    
}

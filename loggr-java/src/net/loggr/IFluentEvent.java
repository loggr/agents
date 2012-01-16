/**
 * 
 */
package net.loggr;


/**
 * @author Justin Kunder
 *
 */
public interface IFluentEvent<T> {
	T text(String text);
	T link(String link);
	T source(String source);
	T user(String user);
	T tags(String tags);
	T value(double value);
	T data(String data);
	T dataType(DataType dataType);
	
	public Event event();
}

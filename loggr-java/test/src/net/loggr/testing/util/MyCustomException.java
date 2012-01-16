package net.loggr.testing.util;

public class MyCustomException extends Exception {

	private String myMessage;
	
	@Override
	public String getMessage() {
		// TODO Auto-generated method stub
		return (myMessage);
	}

	/**
	 * 
	 */
	private static final long serialVersionUID = 2362052703909198693L;

	
	public MyCustomException(String message)
	{
		myMessage = message;
	}
	
	
}

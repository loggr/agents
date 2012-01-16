package net.loggr;

import java.io.InputStream;

public class AsyncWebResponse {
	private InputStream body;
	
	public AsyncWebResponse(InputStream body){
		this.body = body;
	}
	public InputStream getBody(){
		return body;
	}
}

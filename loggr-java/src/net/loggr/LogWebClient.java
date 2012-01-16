package net.loggr;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.*;
import java.util.Hashtable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

public class LogWebClient {
	private Hashtable<String, String> _headers;
	
	public LogWebClient(){
		_headers = new Hashtable<String, String>();
	}
	
	public void addHeader(String key, String value){
		if (_headers.containsKey(key))
		{
			String oldVal = _headers.get(key);
			_headers.put(key, oldVal + value);
		}
		else
		{
			_headers.put(key, value);
		}
	}
	
	public InputStream makeAsyncRequest(URL url) throws InterruptedException, ExecutionException
	{
		ExecutorService executor = Executors.newFixedThreadPool(1);
		
		Future<AsyncWebResponse> response = executor.submit(new AsyncWebRequest(url));
		
		InputStream body = response.get().getBody();
		
		executor.shutdown();
		
		return (body);
	}

	public String uploadData(URL url, String method, String postStr) {
		// TODO Auto-generated method stub
		String failure = "";
		try {
			URL _url = new URL(url.toExternalForm());
			
			URLConnection urlConn = _url.openConnection();
			
			urlConn.setDoInput(true);
			
			urlConn.setDoOutput(true);
			
			urlConn.setUseCaches(false);
			
			urlConn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
			DataOutputStream output = new DataOutputStream(urlConn.getOutputStream());
			
			output.writeBytes(postStr);
			output.flush();
			output.close();
			System.out.println("URL: " + _url.toExternalForm());
			System.out.println("PostStr: " + postStr);
			BufferedReader input = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));

			String response = "";
			
			String str;
			
			while ((str = input.readLine()) != null)
			{
				response += str;
			}
			
			return (response);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			failure = e.getMessage();
		}
		
		return (failure);
	}
}

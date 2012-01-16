package net.loggr.testing.util;

import junit.framework.Assert;

import net.loggr.Events;
import net.loggr.FluentEvent;

public class FullTestCaseThread implements Runnable {

	private int myId;
	
	public FullTestCaseThread(int threadId)
	{
		myId = threadId;
	}
	
	@Override
	public void run() {
		// TODO Auto-generated method stub
		
		Events.create().addData("THREAD[" + myId + "]: Testing a basic event").text("Test").post();
		
		int zero = 0;
		
		int one = 1;
		
		try{
			@SuppressWarnings("unused")
			int divideByZero = one / zero;
		}
		catch (Exception ex){
			FluentEvent evt = Events.createFromException(ex).addTags("error exception").addText("THREAD[" + myId + "]");
			Assert.assertNotNull("The FluentEvent was null after a divide by zero exception was caught", evt);
			
			evt = evt.post();
			Assert.assertNotNull("The FluentEvent was null after trying to post divide by zero exception that was caught", evt);
		}
		
		
	}

}

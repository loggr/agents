package net.loggr.testing;

import junit.framework.Assert;

import org.junit.Test;

import net.loggr.Events;
import net.loggr.FluentEvent;

public class TestException {

	@Test
	public void test() {
		
		int zero = 0;
		
		int one = 1;
		
		try{
			@SuppressWarnings("unused")
			int divideByZero = one / zero;
		}
		catch (Exception ex){
			FluentEvent evt = Events.createFromException(ex).addTags("error exception");
			Assert.assertNotNull("The FluentEvent was null after a divide by zero exception was caught", evt);
			
			evt = evt.post();
			Assert.assertNotNull("The FluentEvent was null after trying to post divide by zero exception that was caught", evt);
		}
		
		

		try{
			@SuppressWarnings("unused")
			int overFlow = Integer.MAX_VALUE + Integer.MAX_VALUE;
		}
		catch (ArithmeticException ex){
			FluentEvent evt = Events.createFromException(ex).addTags("error exception");
			Assert.assertNotNull("The FluentEvent was null after an integer overflow", evt);
			
			evt = evt.post();
			Assert.assertNotNull("The FluentEvent was null after trying to post integer overflow exception that was caught", evt);
		}
	}

}

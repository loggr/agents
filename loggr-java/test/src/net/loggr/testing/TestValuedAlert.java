package net.loggr.testing;

import java.text.DecimalFormat;
import java.util.Random;

import junit.framework.Assert;

import org.junit.Test;

import net.loggr.Events;
import net.loggr.FluentEvent;

public class TestValuedAlert {

	@Test
	public void test() {
		
		int max = 5;
		
		double myPrice = 0.0;
		
		Random rand = new Random();
		
		for(int i = 0; i < max; i ++)
		{
			int multiplier = rand.nextInt(99) + 1;
			myPrice = rand.nextDouble() * multiplier;
			
			DecimalFormat money = new DecimalFormat("$0.00");
			DecimalFormat value = new DecimalFormat("#.##");
			
			FluentEvent evt = Events.create().value(Double.valueOf(value.format(myPrice))).addTags("order completed").addData("Order completed for amount: " + money.format(myPrice) + ".").addText("Order" + i + " Completed. Value: " + money.format(myPrice));
			Assert.assertNotNull("Event was not created for valued alert.", evt);
			
			Assert.assertNotNull("Event was posted for valued alert.", evt.post());
			
			try {
				Thread.sleep(5000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}

}

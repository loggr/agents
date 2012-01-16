package net.loggr.testing;

import org.junit.Test;

import net.loggr.*;

public class TestBasicEvent {

	@Test
	public void test() {
		
		Events.create().text("Test").post();
		
		
	}

}

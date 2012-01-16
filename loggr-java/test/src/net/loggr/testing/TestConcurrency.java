package net.loggr.testing;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import net.loggr.testing.util.FullTestCaseThread;

import org.junit.Test;

public class TestConcurrency {

	@Test
	public void test() {
		
		ExecutorService threadPool = Executors.newFixedThreadPool(100);
		
		for(int i = 0; i < 10; i++)
		{
			FullTestCaseThread t = new FullTestCaseThread(i);
			threadPool.execute(t);
		}
	}

}

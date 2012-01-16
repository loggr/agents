package net.loggr.testing;

import java.util.ArrayList;

import net.loggr.testing.util.ComplexObject;

import org.junit.Test;

import net.loggr.Events;

public class TestComplexObject {

	@Test
	public void test() {
		ComplexObject obj = new ComplexObject();
		
		obj.setAddress1("123 Abc Lane");
		obj.setAddress2("Apt B");
		ArrayList<String> categories = new ArrayList<String>();
		categories.add("Food");
		categories.add("Pets");
		categories.add("Fun");
		categories.add("Music");
		obj.setCategories(categories);
		
		obj.setCity("Lancaster");
		obj.setState("PA");
		obj.setZip("17603");
		
		
		try{
			obj.causeAnException();			
		}
		catch (Exception ex)
		{
			Events.createFromVariable(obj).tags("object dump").text("Object Dump").post();
			Events.createFromException(ex).tags("error").post();
		}
		
	}

}

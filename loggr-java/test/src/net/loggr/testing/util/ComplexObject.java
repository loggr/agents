package net.loggr.testing.util;

import java.util.ArrayList;
import java.util.Random;

public class ComplexObject {

	private String address1;
	private String address2;
	private String city;
	private String state;
	private String zip;
	
	private String name;
	private ArrayList<String> categories;
	
	
	public ComplexObject(){}
	
	public String getAddress1() {
		return address1;
	}
	public void setAddress1(String address1) {
		this.address1 = address1;
	}
	public String getAddress2() {
		return address2;
	}
	public void setAddress2(String address2) {
		this.address2 = address2;
	}
	public String getCity() {
		return city;
	}
	public void setCity(String city) {
		this.city = city;
	}
	public String getState() {
		return state;
	}
	public void setState(String state) {
		this.state = state;
	}
	public String getZip() {
		return zip;
	}
	public void setZip(String zip) {
		this.zip = zip;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public ArrayList<String> getCategories() {
		return categories;
	}
	public void setCategories(ArrayList<String> categories) {
		this.categories = categories;
	}
	
	public void causeAnException() throws Exception
	{
		Random rand = new Random();
		int mystery = rand.nextInt(10);
		
		if (mystery > 8)
			throwArithmeticException();
		else if (mystery > 5)
			throwIllegalStateException();
		else if (mystery > 3)
			throwCustomException();
		else
			throw new IllegalArgumentException();
	}
	
	
	public void throwIllegalStateException() {
		// TODO Auto-generated method stub
		throw new IllegalStateException();
	}

	public void throwCustomException() throws MyCustomException {
		// TODO Auto-generated method stub
		throw new MyCustomException("Custom error exception.");
	}

	public void throwArithmeticException()
	{
		throw new ArithmeticException();
	}
}

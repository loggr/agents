package net.loggr.utility;

import java.util.ArrayList;

public class Common {
	private Common(){}
	
	public static boolean IsNullOrEmpty(String str){
		return !(str != null && str.trim() != "");
	}

	public static int GetLastIndexOf(String type, char c) {
		// TODO Auto-generated method stub
		
		int lastIndex = -1;
		
		int curIdx = 0;
		
		
		for(int i = 0; i < type.length(); i++)
		{

			String tmp = type.substring(i);
			
			curIdx = tmp.indexOf(c);
			
			if (curIdx == -1)
				break;
			
			lastIndex = curIdx;
		}
		
		
		
		return lastIndex;
	}

	public static String Join(String seperator, String[] values) {
		// TODO Auto-generated method stub
		String retVal = "";
		for(int i = 0; i < values.length; i++)
		{
			if (i == 0)
			{
				retVal += values[i];
			}
			else
			{
				retVal += seperator + values[i];
			}
		}
		
		return retVal;
	}

	public static String[] RemoveEmptyEntries(String[] tokens) {
		// TODO Auto-generated method stub
		ArrayList<String> vals = new ArrayList<String>();
		for (int i = 0; i < tokens.length; i++)
		{
			if (tokens[i].trim() != ""){
				vals.add(tokens[i]);
			}
		}
		
		String [] retVal = new String[vals.size()];
		retVal = vals.toArray(retVal);
		
		return (retVal);
	}
}

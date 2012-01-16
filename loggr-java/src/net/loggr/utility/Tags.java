package net.loggr.utility;

import java.util.ArrayList;

public class Tags {
	
	public static String[] TokenizeAndFormat(String[] Tags)
	{
		return (TokenizeAndFormat(Tags, true));
	}
	
	public static String[] TokenizeAndFormat(String[] Tags, boolean StripSpecialChars)
    {
        return TokenizeAndFormat(Common.Join(" ", Tags), StripSpecialChars);
    }

    public static String[] TokenizeAndFormat(String Tagstring)
    {
    	return (TokenizeAndFormat(Tagstring, true));
    }

    public static String[] TokenizeAndFormat(String Tagstring, boolean StripSpecialChars)
    {
        ArrayList<String> res = new ArrayList<String>();
        String[] tokens = Tagstring.toLowerCase().split(" ");
        tokens = Common.RemoveEmptyEntries(tokens);
        for (int i = 0; i <= (tokens.length - 1); i++)
        {
            String token = null;
            if (StripSpecialChars)
            {
                token = tokens[i].trim().replaceAll("[^a-zA-Z0-9\\-]", "");
            }
            else
            {
                token = tokens[i].trim();
            }
            if (token.length() > 0)
            {
                res.add(token);
            }
        }
        
        String [] retVal = new String[res.size()];
        retVal = res.toArray(retVal);
        
        return (retVal);
    }
}

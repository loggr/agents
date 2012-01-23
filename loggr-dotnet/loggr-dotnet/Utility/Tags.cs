using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Loggr.Utility
{
    public static class Tags
    {
        public static string[] TokenizeAndFormat(string[] Tags, bool StripSpecialChars = true)
        {
            return TokenizeAndFormat(string.Join(" ", Tags), StripSpecialChars);
        }

        public static string[] TokenizeAndFormat(string Tagstring, bool StripSpecialChars = true)
        {
            List<string> res = new List<string>();
            string[] tokens = Tagstring.ToLower().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= (tokens.Length - 1); i++)
            {
                string token = null;
                if (StripSpecialChars)
                {
                    token = Regex.Replace(tokens[i].Trim(), "[^a-zA-Z0-9\\-]", "");
                }
                else
                {
                    token = tokens[i].Trim();
                }
                if (token.Length > 0)
                {
                    res.Add(token);
                }
            }
            return res.ToArray();
        }


    }
}

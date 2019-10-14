using System.Collections.Generic;

namespace Money
{
	public static class StringExtensions
	{
        public static Dictionary<string, string> charactersToReplace = new Dictionary<string, string>
        {
            {"é", "e"},
            {"ê", "e"},
            {"è", "e"},
            {"à", "a"},
            {"ù", "u"},
            {"ô", "o"}
        };

        public static string Sanitize(this string stringToEdit)
        {
            stringToEdit = stringToEdit.ToLower();
            string newString = "";
            for (int index = 0; index < stringToEdit.Length; index++)
            {
                string c = stringToEdit.Substring(index, 1);
                foreach (string characterKey in charactersToReplace.Keys)
                {
                    if (c == characterKey)
                    {
                        c = charactersToReplace[characterKey];
                        break;
                    }
                }
                newString += c;
            }
            return newString;
        }
    }
}
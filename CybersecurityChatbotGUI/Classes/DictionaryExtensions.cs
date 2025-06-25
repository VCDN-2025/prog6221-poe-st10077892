using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbotGUI.Classes
{
    static class DictionaryExtensions
    {
        public static string GetKeywordResponse(this Dictionary<string, string> dictionary, string input)
        {
            foreach (var keyword in dictionary.Keys)
            {
                if (input.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return dictionary[keyword];
                }
            }
            return string.Empty;
        }
    }
}

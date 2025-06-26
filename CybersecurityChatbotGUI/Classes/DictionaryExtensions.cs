using System;
using System.Collections.Generic;
using System.Linq;

//Name: WPF Coffee Shop App
//Author: fb-shaik
//Link: https://github.com/fb-shaik/PROG6221-Group2-2025/tree/main/WPF_Coffee_Shop_App
//Date accessed: 24 June 2025

//Name: WPF Demo App
//Author: fb-shaik
//Link: https://github.com/fb-shaik/PROG6221-Group2-2025/tree/main/WPF_Demo_App
//Date accessed: 24 June 2025

//Name: Create a bot with the Bot Framework SDK
//Author: Microsoft
//Link: https://learn.microsoft.com/en-us/azure/bot-service/bot-service-quickstart-create-bot?view=azure-bot-service-4.0&tabs=csharp%2Cvs
//Date accessed: 24 June 2025

//Name: C# Inheritance
//Author: W3Schools
//Link:https://www.w3schools.com/cs/cs_inheritance.php
//Date accessed: 24 June 2025

//Name: Types of Cyber Attacks
//Author: Check Point
//Link: https://www.checkpoint.com/cyber-hub/cyber-security/what-is-cyber-attack/types-of-cyber-attacks/
//Date accessed: 24 June 2025

//Name: SoundPlayer Class
//Author: Microsoft
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer?view=windowsdesktop-9.0
//Date accessed: 24 June 2025

//Name: Validating Input
//Author: Makers Institute
//Link: https://makersinstitute.gitbooks.io/c-basics/content/validating-input.html
//Date accessed: 24 June 2025

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

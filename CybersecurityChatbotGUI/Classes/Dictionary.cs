using System;
using System.Collections.Generic;


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
    public class ChatbotKnowledgeBase
    {
        // Dictionary questions
        protected Dictionary<string, string> Responses = new(StringComparer.OrdinalIgnoreCase)
        {
            {"hello", "Hello! I'm your friendly cybersecurity chatbot. How can I assist you today?" },
            { "hi", "Hi there! Ready to chat about cybersecurity?" },
            { "help", "I'm here to help! What do you need assistance with?" },
            { "bye", "Goodbye! Stay safe online!" },
            { "thanks", "You're welcome! If you have more questions, just ask." },
            { "what is your name?", "I’m CyberBot, your cybersecurity assistant!" },
            { "how are you?", "I’m thriving in the matrix! Thanks for asking, human." },
            { "what is your purpose?", "To help South Africans stay protected from cyber threats like phishing and scams." },
            {"im worried about online scams", "Its completely understandable to feel that way. Scammers can be very convincing." },
            {"im frustrated with all these risks", "I hear you. Cybersecurity can be very overwhelming sometimes" },
            {"im curious about ransomware", "Curiosity is great! It's the first step to becoming more cyber-aware." },
            { "what can i ask you about?", "You can ask about password attacks, ransomware, phishing, safe browsing, social engineering." },
            { "password attack", "A password attack is any attempt by a malicious actor to gain unauthorized access by cracking passwords. Passwords should be like your toothbrush: long, unique, and never shared. 🪥" },
            { "phishing", "Phishing is when someone tricks you into revealing sensitive info using fake emails or messages. 🐟" },
            { "safe browsing", "Safe browsing means staying away from shady websites and always checking for HTTPS 🔒." },
            { "ransomware", "Ransomware locks your files and asks for money. It's like cyber-kidnapping for data! 💸" },
            { "social engineering", "This is when hackers use charm and manipulation instead of code to trick people. Think of it as hacking humans. 🧠" }
        };

        protected Dictionary<string, string> KeywordResponses = new(StringComparer.OrdinalIgnoreCase)
    {
        { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords." },
        { "phishing", "Be cautious of unsolicited messages or offers that seem too good to be true. Always verify sources." },
        { "privacy", "Protect your privacy by limiting the personal information you share online and reviewing app permissions regularly." }
    };

        protected Dictionary<string, List<string>> RandomResponses = new(StringComparer.OrdinalIgnoreCase)
    {
        { "phishing tip", new List<string>
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "Always check the sender's email address before clicking links. If in doubt, don’t click!",
                "Hover over links to preview URLs before clicking. Stay alert!",
                "Never download attachments from unknown or suspicious emails.",
                "Use two-factor authentication to add an extra layer of protection."
            }
        }
    };

        public Dictionary<string, string> SentimentResponses = new Dictionary<string, string>()
{
    { "worried", "It's completely understandable to feel that way. Cyber threats can be scary. Let me share some tips to help you stay safe." },
    { "frustrated", "I hear you. Cybersecurity can be overwhelming at times. You're not alone — I'm here to help simplify things." },
    { "curious", "Curiosity is great! It's the first step to becoming more cyber-aware. Let me tell you more." }
};


        Random rng = new();

        private List<string> followUpTriggers = new List<string>
{
    "tell me more", "what else", "why", "how", "explain"
};

        private string currentTopic = null; // store the latest topic





        public bool TryGetResponse(string input, out string response)
        {
            return Responses.TryGetValue(input, out response);
        }

        public bool TryGetKeywordResponse(string input, out string response)
        {
            foreach (var keyword in KeywordResponses.Keys)
            {
                if (input.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    currentTopic = keyword; 
                    response = KeywordResponses[keyword];
                    return true;
                }
            }

            response = null!;
            return false;
        }


        public bool TryGetRandomResponse(string input, out string response)
        {
            foreach (var kvp in RandomResponses)
            {
                if (input.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    var responses = kvp.Value;
                    response = responses[rng.Next(responses.Count)];
                    return true;
                }
            }
            response = null!;
            return false;
        }

        public bool TryGetFollowUpResponse(string input, out string response)
        {
            foreach (var trigger in followUpTriggers)
            {
                if (input.Contains(trigger, StringComparison.OrdinalIgnoreCase) && currentTopic != null)
                {
                    response = GetFollowUpForTopic(currentTopic);
                    return true;
                }
            }

            response = null!;
            return false;
        }

        private string GetFollowUpForTopic(string topic)
        {
            return topic.ToLower() switch
            {
                "password" => "Consider using a password manager for even stronger security.",
                "scam" => "If something seems too good to be true, it probably is. Stay alert!",
                "privacy" => "Review app permissions regularly and disable location tracking where unnecessary.",
                "phishing" => "Always double-check the sender's email address — phishing scams often fake it.",
                _ => "I'm not sure what to add, but it's always good to stay informed!"
            };
        }

        




    }
}
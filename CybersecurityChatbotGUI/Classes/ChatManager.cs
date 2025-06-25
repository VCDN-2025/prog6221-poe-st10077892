// ChatManager.cs
using System;
using CybersecurityChatbotGUI.Classes;

namespace CybersecurityChatbotGUI.Classes
{
    public class ChatManager
    {
        private ChatbotKnowledgeBase knowledgeBase = new ChatbotKnowledgeBase();
        private string userName = "User";
        private string userInterest = string.Empty;

        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a message.";

            if (input.Contains("name is"))
            {
                var parts = input.Split("name is");
                if (parts.Length > 1)
                {
                    userName = parts[1].Trim();
                    return $"Nice to meet you, {userName}!";
                }
            }

            if (input.Contains("interested in"))
            {
                var parts = input.Split("interested in");
                if (parts.Length > 1)
                {
                    userInterest = parts[1].Trim();
                    return $"Great! I’ll remember that you're interested in {userInterest}.";
                }
            }

            if (knowledgeBase.TryGetResponse(input, out var directResponse))
                return directResponse;

            if (knowledgeBase.TryGetKeywordResponse(input, out var keywordResponse))
                return keywordResponse;

            if (knowledgeBase.TryGetRandomResponse(input, out var randomResponse))
                return randomResponse;

            if (knowledgeBase.TryGetFollowUpResponse(input, out var followUpResponse))
                return followUpResponse;

            return $"I'm still learning, {userName}. Can you ask that in another way?";
        }
    }
}

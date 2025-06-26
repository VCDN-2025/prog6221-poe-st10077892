using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CybersecurityChatbotGUI.Classes
{
    public class ChatManager
    {
        private ChatbotKnowledgeBase knowledgeBase = new ChatbotKnowledgeBase();
        private string userName = "User";
        private string userInterest = string.Empty;

        // Task state tracking
        private string pendingTaskTitle = string.Empty;
        private string pendingTaskDescription = string.Empty;
        private bool waitingForReminder = false;

        // Basic task model
        public List<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

        public string GetResponse(string input)
        {
            input = input.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a message.";

            string lowerInput = input.ToLower();

            // 1. Name
            if (lowerInput.Contains("name is"))
            {
                var parts = input.Split("name is", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    userName = parts[1].Trim();
                    return $"Nice to meet you, {userName}!";
                }
            }

            // 2. Interest
            if (lowerInput.Contains("interested in"))
            {
                var parts = input.Split("interested in", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    userInterest = parts[1].Trim();
                    return $"Great! I’ll remember that you're interested in {userInterest}.";
                }
            }

            // 3. Task addition
            if (lowerInput.StartsWith("add task"))
            {
                var taskText = input.Substring("add task".Length).Trim();
                if (!string.IsNullOrEmpty(taskText))
                {
                    pendingTaskTitle = taskText;
                    pendingTaskDescription = $"Cybersecurity task: {taskText}";
                    waitingForReminder = true;
                    return $"Task added with the description \"{pendingTaskDescription}\". Would you like a reminder?";
                }
                return "Please specify a task after saying 'Add task'.";
            }

            // 4. Reminder parsing
            if (waitingForReminder && (lowerInput.Contains("remind me") || lowerInput.StartsWith("yes")))
            {
                DateTime reminderDate = DateTime.Now;
                var match = Regex.Match(lowerInput, @"in (\d+) (day|days|week|weeks)");
                if (match.Success)
                {
                    int amount = int.Parse(match.Groups[1].Value);
                    string unit = match.Groups[2].Value;

                    reminderDate = unit.StartsWith("week")
                        ? DateTime.Now.AddDays(amount * 7)
                        : DateTime.Now.AddDays(amount);

                    Tasks.Add(new TaskItem
                    {
                        Title = pendingTaskTitle,
                        Description = pendingTaskDescription,
                        Reminder = reminderDate
                    });

                    // Reset state
                    waitingForReminder = false;
                    pendingTaskTitle = string.Empty;
                    pendingTaskDescription = string.Empty;

                    return $"Got it! I'll remind you in {amount} {unit}.";
                }

                return "Okay, please specify when you want to be reminded, like 'in 3 days'.";
            }

            // 5. View tasks
            if (lowerInput == "view tasks")
            {
                if (Tasks.Count == 0)
                    return "You don't have any tasks yet.";

                string taskList = "Here are your tasks:\n";
                for (int i = 0; i < Tasks.Count; i++)
                {
                    var task = Tasks[i];
                    taskList += $"- {task.Title}: {task.Description} (Remind on: {task.Reminder.ToShortDateString()})\n";
                }
                return taskList;
            }

            // Knowledge base fallback
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

    // Simple task class
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
    }
}

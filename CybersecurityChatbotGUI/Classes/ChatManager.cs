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

            // 3. NLP task/reminder detection
            if (lowerInput.Contains("remind me to") || lowerInput.Contains("set a reminder") || lowerInput.Contains("add a task"))
            {
                string title = ExtractTaskTitle(input);
                DateTime? date = ParseNaturalDate(input);

                var task = new TaskItem
                {
                    Title = title,
                    Description = $"Cybersecurity task: {title}",
                    Reminder = date ?? DateTime.Now.AddDays(1)
                };

                Tasks.Add(task);

                if (date.HasValue)
                    return $"🔔 Reminder set for \"{title}\" on {date.Value:dd MMM yyyy}.";
                else
                    return $"📝 Task added: \"{title}\". Reminder set for tomorrow by default.";
            }

            // 4. Manual task addition
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

            // 5. Follow-up reminder parsing
            if (waitingForReminder && (lowerInput.Contains("remind me") || lowerInput.StartsWith("yes")))
            {
                DateTime reminderDate = ParseNaturalDate(input) ?? DateTime.Now.AddDays(1);

                Tasks.Add(new TaskItem
                {
                    Title = pendingTaskTitle,
                    Description = pendingTaskDescription,
                    Reminder = reminderDate
                });

                waitingForReminder = false;
                pendingTaskTitle = string.Empty;
                pendingTaskDescription = string.Empty;

                return $"Got it! I'll remind you on {reminderDate:dd MMM yyyy}.";
            }

            // 6. View tasks
            if (lowerInput == "view tasks")
            {
                if (Tasks.Count == 0)
                    return "You don't have any tasks yet.";

                string taskList = "Here are your tasks:\n";
                for (int i = 0; i < Tasks.Count; i++)
                {
                    var task = Tasks[i];
                    taskList += $"- {task.Title}: {task.Description} (Remind on: {task.Reminder:dd MMM yyyy})\n";
                }
                return taskList;
            }

            // 7. "What have you done for me?"
            if (lowerInput.Contains("what have you done for me"))
            {
                if (Tasks.Count == 0)
                    return "I haven’t done anything for you yet.";

                string summary = "Here’s a summary of recent actions:\n";
                int count = 1;
                foreach (var task in Tasks)
                {
                    summary += $"{count++}. Task: \"{task.Title}\"";
                    if (task.Reminder != DateTime.MinValue)
                        summary += $" – Reminder: {task.Reminder:dd MMM yyyy}";
                    summary += "\n";
                }
                return summary;
            }

            // Fallback responses
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

        private string ExtractTaskTitle(string input)
        {
            var lower = input.ToLower();
            if (lower.Contains("to"))
            {
                int index = lower.IndexOf("to");
                return input.Substring(index + 3).Trim();
            }
            return input;
        }

        private DateTime? ParseNaturalDate(string input)
        {
            string lower = input.ToLower();

            if (lower.Contains("tomorrow"))
                return DateTime.Now.AddDays(1);

            if (lower.Contains("next week"))
                return DateTime.Now.AddDays(7);

            if (lower.Contains("in 3 days"))
                return DateTime.Now.AddDays(3);

            var match = Regex.Match(lower, @"in (\d+) (day|days|week|weeks)");
            if (match.Success)
            {
                int amount = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value;

                return unit.StartsWith("week") ? DateTime.Now.AddDays(amount * 7) : DateTime.Now.AddDays(amount);
            }

            return null;
        }
    }

    public class TaskItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Reminder { get; set; }
    }
}

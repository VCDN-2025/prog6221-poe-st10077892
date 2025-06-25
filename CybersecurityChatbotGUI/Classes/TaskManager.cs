using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Classes
{
    class TaskManager
    {
        public class Task
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? ReminderDate { get; set; }
            public bool Completed { get; set; }

            public override string ToString() => $"{Title}: {Description} (Reminder: {ReminderDate?.ToShortDateString() ?? "None"})";
        }

        private List<Task> tasks = new List<Task>();

        public string AddTask(string title, string description, DateTime? reminderDate)
        {
            if (string.IsNullOrWhiteSpace(title)) return "Task title cannot be empty.";
            tasks.Add(new Task { Title = title, Description = description, ReminderDate = reminderDate, Completed = false });
            return $"Task '{title}' added successfully.";
        }

        public List<Task> GetTasks() => tasks;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

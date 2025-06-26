using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Classes
{
    class UserTask
    {
        public string Title { get; set; } = string.Empty; // Initialize with a default value
        public string Description { get; set; } = string.Empty; // Initialize with a default value
        public DateTime? Reminder { get; set; }
    }
}

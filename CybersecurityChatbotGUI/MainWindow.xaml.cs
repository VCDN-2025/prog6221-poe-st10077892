using System;
using System.Collections.Generic;
using System.Globalization;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CybersecurityChatbotGUI.Classes;

namespace CybersecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        private ChatManager chatManager = new ChatManager();
        private TaskManager taskManager = new TaskManager();
        private QuizManager quizManager = new QuizManager();
        private List<string> activityLog = new List<string>();

        private DispatcherTimer reminderTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            reminderTimer.Interval = TimeSpan.FromSeconds(10);
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Resources/greeting.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing greeting: " + ex.Message);
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text;
            if (!string.IsNullOrWhiteSpace(input))
            {
                string lowerInput = input.ToLower();

                // NLP Simulation: Check for reminder phrases
                if (lowerInput.Contains("remind me to") || lowerInput.Contains("set a reminder to"))
                {
                    string taskTitle = ExtractReminderTitle(lowerInput);
                    DateTime? reminderDate = DetectDatePhrase(lowerInput);

                    taskManager.AddTask(taskTitle, taskTitle, reminderDate);
                    chatManager.Tasks.Add(new TaskItem
                    {
                        Title = taskTitle,
                        Description = taskTitle,
                        Reminder = reminderDate ?? DateTime.MinValue
                    });

                    string response = $"Reminder set for '{taskTitle}'";
                    if (reminderDate.HasValue)
                        response += $" on {reminderDate.Value:dddd, dd MMMM}";

                    ChatOutput.Text += $"\nYou: {input}\nBot: {response}";
                    activityLog.Add($"Reminder added: {taskTitle} -> {reminderDate?.ToShortDateString() ?? "No date"}");
                    UserInput.Clear();
                    return;
                }

                // NLP Simulation: Summary of recent actions
                if (lowerInput.Contains("what have you done") || lowerInput.Contains("summary") || lowerInput.Contains("activity"))
                {
                    var recent = activityLog.Count > 0 ? string.Join("\n", activityLog) : "No recent activity.";
                    ChatOutput.Text += $"\nYou: {input}\nBot: Here's a summary of recent actions:\n{recent}";
                    UserInput.Clear();
                    return;
                }

                string responseText = chatManager.GetResponse(input);
                ChatOutput.Text += $"\nYou: {input}\nBot: {responseText}";
                activityLog.Add($"User: {input} -> Bot: {responseText}");

                if (input.ToLower().StartsWith("delete task"))
                {
                    string title = input.Substring("delete task".Length).Trim();
                    bool removed = chatManager.Tasks.RemoveAll(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase)) > 0;

                    if (removed)
                    {
                        ChatOutput.Text += $"\nBot: Task \"{title}\" has been deleted.";
                        activityLog.Add($"Deleted task: {title}");
                    }
                    else
                    {
                        ChatOutput.Text += $"\nBot: Task \"{title}\" not found.";
                    }
                }

                UserInput.Clear();
            }
        }

        private string ExtractReminderTitle(string input)
        {
            Match match = Regex.Match(input, @"remind me to (.+)");
            if (!match.Success)
                match = Regex.Match(input, @"set a reminder to (.+)");
            return match.Success ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(match.Groups[1].Value.Trim()) : "Unnamed Task";
        }

        private DateTime? DetectDatePhrase(string input)
        {
            input = input.ToLower();
            if (input.Contains("tomorrow"))
                return DateTime.Today.AddDays(1);
            if (input.Contains("today"))
                return DateTime.Today;
            if (input.Contains("next week"))
                return DateTime.Today.AddDays(7);

            return null;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitle.Text;
            string description = TaskDescription.Text;
            DateTime? reminder = TaskReminder.SelectedDate;

            string result = taskManager.AddTask(title, description, reminder);
            TaskOutput.Text += $"\n{result}";
            activityLog.Add($"Task added: {title}, Reminder: {reminder?.ToShortDateString() ?? "None"}");

            TaskTitle.Clear();
            TaskDescription.Clear();
            TaskReminder.SelectedDate = null;
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            string question = quizManager.GetNextQuestion();
            QuizQuestion.Text = question;
            QuizAnswer.Clear();
            QuizFeedback.Text = string.Empty;
            activityLog.Add("Quiz question loaded.");
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            string answer = QuizAnswer.Text;
            if (!string.IsNullOrWhiteSpace(answer))
            {
                string feedback = quizManager.CheckAnswer(answer);
                QuizFeedback.Text = feedback;
                activityLog.Add($"Answered: {answer} -> Feedback: {feedback}");
                QuizAnswer.Clear();

                if (quizManager.IsQuizComplete())
                {
                    int score = quizManager.GetScore();
                    int total = quizManager.GetTotalQuestions();
                    string message = $"Quiz complete! Score: {score}/{total}\n";

                    if (score == total)
                        message += "🌟 Perfect! You're a cybersecurity master!";
                    else if (score >= total * 0.7)
                        message += "✅ Great job! You're a cybersecurity pro!";
                    else
                        message += "📚 Keep learning to stay safe online.";

                    MessageBox.Show(message, "Quiz Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            var recent = activityLog.Count > 10 ? activityLog.GetRange(activityLog.Count - 10, 10) : activityLog;
            ActivityOutput.Text = string.Join("\n", recent);
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            foreach (var task in chatManager.Tasks.ToArray())
            {
                if (task.Reminder <= now)
                {
                    MessageBox.Show($"🔔 Reminder: {task.Title}\n{task.Description}", "Cybersecurity Reminder");
                    chatManager.Tasks.Remove(task);
                    activityLog.Add($"Reminder shown for task: {task.Title}");
                }
            }
        }
    }
}

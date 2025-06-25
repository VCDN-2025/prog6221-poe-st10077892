using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CybersecurityChatbotGUI.Classes;

namespace CybersecurityChatbotGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ChatManager chatManager = new ChatManager();
    private TaskManager taskManager = new TaskManager();
    private QuizManager quizManager = new QuizManager();
    private List<string> activityLog = new List<string>();

    public MainWindow()
    {
        InitializeComponent();
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
            string response = chatManager.GetResponse(input);
            ChatOutput.Text += $"\nYou: {input}\nBot: {response}";
            activityLog.Add($"User: {input} -> Bot: {response}");
            UserInput.Clear();
        }
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
        }
    }

    private void ShowLog_Click(object sender, RoutedEventArgs e)
    {
        var recent = activityLog.Count > 10 ? activityLog.GetRange(activityLog.Count - 10, 10) : activityLog;
        ActivityOutput.Text = string.Join("\n", recent);
    }
}
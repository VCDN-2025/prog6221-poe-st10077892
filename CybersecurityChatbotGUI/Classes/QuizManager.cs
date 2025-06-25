using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Classes
{
    public class QuizManager
    {
        private int currentQuestionIndex = 0;
        private int score = 0;

        private List<(string Question, string Answer)> questions = new List<(string, string)>
        {
            ("What should you do if you receive an email asking for your password?\nA) Reply\nB) Ignore\nC) Report it as phishing", "c"),
            ("Which password is the strongest?\nA) password123\nB) John2020\nC) 7u@!Kf9#", "c"),
            ("True or False: You should use the same password for every account.", "false"),
            ("What is phishing?\nA) A way to catch fish\nB) A type of cyber scam\nC) A programming language", "b"),
            ("What should you look for in a suspicious email?\nA) Grammar errors\nB) Unfamiliar sender\nC) All of the above", "c")
        };

        public string GetNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex].Question;
            }
            return "Quiz complete! Your final score is: " + score + "/" + questions.Count;
        }

        public string CheckAnswer(string userAnswer)
        {
            if (currentQuestionIndex >= questions.Count)
                return "The quiz is already complete.";

            string correct = questions[currentQuestionIndex].Answer.ToLower();
            string feedback;

            if (userAnswer.Trim().ToLower() == correct)
            {
                score++;
                feedback = "Correct! Good job.";
            }
            else
            {
                feedback = $"Incorrect. The correct answer was: {correct.ToUpper()}.";
            }

            currentQuestionIndex++;
            return feedback;
        }
    }
}

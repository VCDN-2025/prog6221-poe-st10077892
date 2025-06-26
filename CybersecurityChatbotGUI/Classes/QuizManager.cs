using System;
using System.Collections.Generic;

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
            ("What should you look for in a suspicious email?\nA) Grammar errors\nB) Unfamiliar sender\nC) All of the above", "c"),
            ("True or False: Public Wi-Fi is always safe to use without a VPN.", "false"),
            ("Which one is a secure action online?\nA) Clicking random popups\nB) Using 2FA\nC) Sharing passwords", "b"),
            ("Which of these is a social engineering attack?\nA) Brute-force\nB) Phishing email\nC) Firewall", "b"),
            ("How often should you update your passwords?\nA) Never\nB) Every few years\nC) Regularly", "c"),
            ("What is malware?\nA) Safety software\nB) Malicious software\nC) A chat app", "b")
        };

        public string? GetNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex].Question;
            }
            return null; // No more questions
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

        public bool IsQuizComplete()
        {
            return currentQuestionIndex >= questions.Count;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetTotalQuestions()
        {
            return questions.Count;
        }
    }
}

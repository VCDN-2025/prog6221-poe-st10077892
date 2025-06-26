using System;
using System.Collections.Generic;

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

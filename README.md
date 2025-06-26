🛡️ Cybersecurity Awareness Chatbot (WPF GUI)
Module: PROG6221 Programming 2A
Student: ST10077892
Institution: The Independent Institute of Education (IIE)
Year: 2025

📌 Overview
The Cybersecurity Awareness Chatbot is an educational desktop application designed to teach South African users about safe online practices. Built with C# and WPF, it features an interactive chatbot, cybersecurity quiz game, task assistant, activity log, and basic simulated Natural Language Processing (NLP).

The project was developed in three phases:

Console-based prototype (Part 1)

Enhanced response logic with ASCII UI and Sound

Graphical User Interface (WPF) with multiple functional tabs

🧠 Key Features
💬 Chatbot (NLP Simulation)
Accepts user inputs and returns friendly, educational responses about cybersecurity topics.

Uses basic keyword detection and string matching to simulate NLP.

Recognizes variations in phrasing such as:

“Add a task to enable 2FA”

“Can you remind me to change my password tomorrow?”

Supports commands like:

"What have you done for me?"

"View tasks"

"Show activity log"

🧠 Cybersecurity Quiz Game
Includes 10+ questions on phishing, safe browsing, social engineering, and passwords.

Supports Multiple Choice and True/False formats

Tracks and displays user score with feedback:

🌟 Perfect! You’re a cybersecurity master!

✅ Great job! You’re a cybersecurity pro!

📚 Keep learning to stay safe online.

🗂️ Task Assistant with Reminders
Allows users to:

Add tasks with titles, descriptions, and reminders

View all tasks

Delete tasks via chatbot commands

Detects phrases like “Remind me in 3 days” or “Remind me tomorrow”

Built-in DispatcherTimer checks for upcoming reminders every 10 seconds

Triggers pop-up alert and removes task from queue

📜 Activity Log (User Audit Trail)
Every major action is logged with:

Timestamps or action descriptions

Chat commands, quiz attempts, task additions, reminders, etc.

View recent actions through:

"Show activity log" or "What have you done for me?"

WPF tab shows up to 10 most recent activities

🛠 Technologies Used
.NET 6.0 / .NET Core WPF

C# (OOP principles)

XAML for layout and UI styling

System.Media for sound playback

Regex & String Matching for NLP simulation

Git / GitHub for version control

🎯 Learning Outcomes Achieved
Used Agile and OOP principles

Developed multi-featured WPF app using event-driven programming

Simulated NLP via regex and string handling

Designed interactive UI using XAML + C#

Handled input validation, timers, and reminders

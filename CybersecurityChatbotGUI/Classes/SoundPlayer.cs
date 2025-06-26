using System;
using System.Media;
using System.Windows;

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
class SoundPlayerWrapper
{
    public static void Play()
    {
        try
        {
            var player = new SoundPlayer("Resources/Greeting.wav");
            player.Play(); // Use Play() for non-blocking, PlaySync() can freeze UI
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not play greeting: {ex.Message}", "Audio Error");
        }
    }
}


using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui;

public sealed class SettingsScreen : Screen
{
    #region Properties And Ctor
    /// <summary>
    /// Data service.
    /// </summary>

    private ISettingsService _settingsService;
    private IDataService _dataService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public SettingsScreen(
        ISettingsService settingsService,
         IDataService dataService)
    {
        _settingsService = settingsService;
        _dataService = dataService;
    }

    #endregion Properties And Ctor

    #region Public Methods
    public override void Show()
    {
        /// <summary>
        /// Initialization of necessary elements
        /// </summary>
        ScreenDefinitionJson = "SettingsScreenLines.json"; 
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens
        history.Add("Settings");

        while (true)
        {
            _settingsService.className = this.GetType().Name; // Get name for current class
            _settingsService.ApplyColors("settings.json", _settingsService.className);


            /// <summary>
            /// This part of code used for manipulatin with arrowkeys
            /// </summary>
            string? choiceAsString = "";
            for (int i = 0; i <= 4;)
            {
                if (i > 3)
                {
                    i = 0;
                }
                if (i == -1)
                {
                    i = 3;
                }
                ShowHistory(history);
                DisplayLine(1);
                for (int line = 0; line <= 3; line++)
                {
                    if (line == -1)
                    {
                        line = 3;
                    }
                    if (line > 3)
                    {
                        line = 0;
                    }

                    if (line == i)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        DisplayLineWOutColor(line + 2);
                    }
                    else
                    {
                        DisplayLine(line + 2);
                    }
                }
                DisplayLine(6);
                Console.Write(i);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Console.Clear();
                    i--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Console.Clear();
                    i++;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    choiceAsString = i.ToString();
                    SettingsScreenChoices choice = (SettingsScreenChoices)Int32.Parse(choiceAsString);
                    break;
                }
            }


            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }
                SettingsScreenChoices choice = (SettingsScreenChoices)Int32.Parse(choiceAsString);
                switch(choice)
                {

                    case SettingsScreenChoices.ChangeColors:
                        _settingsService.ChangeColor();
                        _settingsService.ApplyColors("settings.json", _settingsService.className);
                        Console.Clear();
                        
                    break;

                    case SettingsScreenChoices.ListOfColors:
                        Console.ResetColor();
                        Console.WriteLine();
                        _settingsService.ListColors();
                        _settingsService.ApplyColors("settings.json", _settingsService.className);
                        break;

                    case SettingsScreenChoices.ListOfScreens:
                        Console.ResetColor();
                        Console.WriteLine();
                        _settingsService.ListScreens();
                        
                        break;

                    case SettingsScreenChoices.Exit:
                        DisplayLine(8);
                        history.RemoveAt(history.Count - 1);
                        Console.ResetColor();
                        Console.Clear();

                    return;           

                }

            }
            catch
            {
                DisplayLine(7);
            }
        }
    }
    #endregion Public Methods
}


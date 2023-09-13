using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    
    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;

    private SettingsScreen _settingsScreen;

    private ISettingsService _settingsService;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public MainScreen(
        IDataService dataService,
        AnimalsScreen animalsScreen,
        SettingsScreen settingsScreen,
        ISettingsService settingsService)
    {
        _dataService = dataService;
        _animalsScreen = animalsScreen;
        _settingsService = settingsService;
        _settingsScreen = settingsScreen;
    }
    

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        /// <summary>
        /// Initialization of necessary elements
        /// </summary>
        ScreenDefinitionJson = "MainScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
        history.Add("Main Screen");


        while (true)
        {
            Console.ResetColor();
            _settingsService.className = this.GetType().Name; // Get name for current class
            _settingsService.ApplyColors("settings.json", _settingsService.className);

            /// <summary>
            /// This part of code used for manipulatin with arrowkeys
            /// </summary>
            string? choiceAsString = "";
            for (int i = 0; i <= 3;)
            {
                if (i > 2)
                {
                    i = 0;
                }
                if (i == -1)
                {
                    i = 2;
                }
                ShowHistory(history);
                DisplayLine(1);
                for (int line = 0; line <= 2; line++)
                {
                    if (line == -1)
                    {
                        line = 3;
                    }
                    if (line > 2)
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
                DisplayLine(5);
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
                    MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
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

                MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.Animals:
                        Console.ResetColor();
                        Console.Clear();
                        _animalsScreen.Show();
                        break;

                    case MainScreenChoices.Settings:
                        Console.ResetColor();
                        Console.Clear();
                        _settingsScreen.Show();
                        break;

                    case MainScreenChoices.Exit:
                        DisplayLine(6);
                        history.RemoveAt(history.Count - 1);
                        Console.ResetColor();
                        return;

                }
            }
            catch
            {
                DisplayLine(7);
            }
        }
    }

    #endregion // Public Methods
}
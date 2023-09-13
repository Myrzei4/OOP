using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;

    private ISettingsService _settingsService;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        ISettingsService settingsService)
    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settingsService = settingsService;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        /// <summary>
        /// Initialization of necessary elements
        /// </summary>
        ScreenDefinitionJson = "AnimalsScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
        history.Add("Animals");


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
                    AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
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

                AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case AnimalsScreenChoices.Mammals:
                        Console.ResetColor();
                        Console.Clear();
                        _mammalsScreen.Show();
                        break;

                    case AnimalsScreenChoices.Read:
                        Console.Clear();
                        ReadFromFile();
                        break;

                    case AnimalsScreenChoices.Save:
                        Console.Clear();
                        SaveToFile();
                        break;

                    case AnimalsScreenChoices.Exit:
                        DisplayLine(7);
                        history.RemoveAt(history.Count - 1);
                        Console.ResetColor();
                        Console.Clear();
                        return;
                }
            }
            catch
            {
                DisplayLine(8);
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            DisplayLine(9);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
        }
        catch
        {
            DisplayLine(10);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            DisplayLine(11); ;
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Read(fileName); // why there was Write()???
            Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
        }
        catch
        {
            DisplayLine(12); ;
        }
    }

    #endregion // Private Methods
}

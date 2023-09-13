using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    private DogsScreen _dogsScreen;
    private AfricanElephantsScreen _aficanElephantsScreen;
    private PolarBearsScreen _polarBearsScreen;
    private GrizzlyBearsScreen _grizzlyBearsScreen;
    private ISettingsService _settingsService;
    
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(
        DogsScreen dogsScreen,
        ISettingsService settingsService,
        AfricanElephantsScreen aficanElephantsScreen,
        PolarBearsScreen polarBearsScreen,
        GrizzlyBearsScreen grizzlyBearsScreen )
    {
        _dogsScreen = dogsScreen;
        _settingsService = settingsService;
        _aficanElephantsScreen = aficanElephantsScreen;
        _polarBearsScreen = polarBearsScreen;
        _grizzlyBearsScreen = grizzlyBearsScreen;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {

        /// <summary>
        /// Initialization of necessary elements
        /// </summary>
        ScreenDefinitionJson = "MammalsScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
        history.Add("Mammals");

        while (true)
        {
            _settingsService.className = this.GetType().Name; // Get name for current class
            _settingsService.ApplyColors("settings.json", _settingsService.className);

            /// <summary>
            /// This part of code used for manipulatin with arrowkeys
            /// </summary>
            string? choiceAsString = "";
            for (int i = 0; i <= 5;)
            {
                if (i > 4)
                {
                    i = 0;
                }
                if (i == -1)
                {
                    i = 4;
                }
                ShowHistory(history);
                DisplayLine(1);
                for (int line = 0; line <= 4; line++)
                {
                    if (line == -1)
                    {
                        line = 5;
                    }
                    if (line > 4)
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
                DisplayLine(7);
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
                    MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
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

                MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MammalsScreenChoices.AfricanElephants:
                        Console.ResetColor();
                        Console.Clear();
                        _aficanElephantsScreen.Show();
                        break;

                    case MammalsScreenChoices.PolarBears:
                        Console.ResetColor();
                        Console.Clear();
                        _polarBearsScreen.Show();
                        break;
                    case MammalsScreenChoices.GrizzlyBears:
                        Console.ResetColor();
                        Console.Clear();
                        _grizzlyBearsScreen.Show();
                        break;
                    case MammalsScreenChoices.Dogs:
                        Console.ResetColor();
                        Console.Clear();
                        _dogsScreen.Show();
                        break;

                    case MammalsScreenChoices.Exit:
                        DisplayLine(8);
                        history.RemoveAt(history.Count - 1);
                        Console.ResetColor();
                        Console.Clear();
                        return;

                    
                }
            }
            catch
            {
                DisplayLine(9);
            }
        }
    }

    #endregion // Public Methods
}

using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Runtime.CompilerServices;

namespace SampleHierarchies.Gui;

public sealed class PolarBearsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
     /// <summary>
     /// Settings sercvice.
     /// </summary>
    private ISettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="settingsService">Settings service reference</param>
    public PolarBearsScreen(
        IDataService dataService,
        ISettingsService settingsService)
    {
        _dataService = dataService;
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
        ScreenDefinitionJson = "PolarBearsScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
        history.Add("Polar Bears");


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
                    if( line == -1)
                    {
                        line = 5;
                    }
                    if( line > 4)
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
                    PolarBearsScreenChoices choice = (PolarBearsScreenChoices)Int32.Parse(choiceAsString);
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

                PolarBearsScreenChoices choice = (PolarBearsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice) 
                {
                    case PolarBearsScreenChoices.List:
                        Console.ResetColor();
                        Console.Clear();
                        ListPolarBears();
                        break;

                    case PolarBearsScreenChoices.Create:
                        Console.ResetColor();
                        Console.Clear(); 
                        AddPolarBear();
                        break;

                    case PolarBearsScreenChoices.Delete:
                        Console.ResetColor();
                        Console.Clear();
                        DeletePolarBear();
                        break;

                    case PolarBearsScreenChoices.Modify:
                        Console.ResetColor();
                        Console.Clear();
                        EditPolarBearMain();
                        break;

                    case PolarBearsScreenChoices.Exit:
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

    #endregion Public Methods


    #region Private Methods

    /// <summary>
    /// List all polar bears.
    /// </summary>
    private void ListPolarBears()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.PolarBears is not null &&
            _dataService.Animals.Mammals.PolarBears.Count > 0)
        {
            DisplayLine(10);
            int i = 1;
            foreach (PolarBear polarBear in _dataService.Animals.Mammals.PolarBears)
            {
                Console.Write($"Polar bears number {i}, ");
                polarBear.Display();
                
                i++;
            }
        }
        else
        {
            DisplayLine(11);
        }
    }


    
    /// <summary>
    /// Add a polar bear.
    /// </summary>
    private void AddPolarBear()
    {
        try
        {
            PolarBear polarBear = AddEditPolarBear();
            _dataService?.Animals?.Mammals?.PolarBears?.Add(polarBear);
            Console.WriteLine("Polar bear with name: {0} has been added to a list of polar bears", polarBear.Name);
        }
        catch
        {
            DisplayLine(17);
        }
    }

    /// <summary>
    /// Deletes a polar bear.
    /// </summary>
    private void DeletePolarBear()
    {
        try
        {
            DisplayLine(12);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            PolarBear? polarBear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
            if (polarBear is not null)
            {
                _dataService?.Animals?.Mammals?.PolarBears?.Remove(polarBear); 
                Console.WriteLine("Polar bear with name: {0} has been deleted from a list of polar bears", polarBear.Name);
            }
            else
            {
                DisplayLine(13);
            }
        }
        catch
        {
            DisplayLine(17);
        }
    }

    /// <summary>
    /// Edits an existing polar bear after choice made.
    /// </summary>
    private void EditPolarBearMain()
    {
        try
        {
            DisplayLine(14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            PolarBear? polarBear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
            if (polarBear is not null)
            {
                PolarBear polarBearEdited = AddEditPolarBear();
                polarBear.Copy(polarBearEdited);
                DisplayLine(15);
                polarBear.Display();
            }
            else
            {
                DisplayLine(16);
            }
        }
        catch
        {
            DisplayLine(17);
        }
    }

    /// <summary>
    /// Adds/edit specific polar bear.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private PolarBear AddEditPolarBear()
    {
        DisplayLine(18);
        string? name = Console.ReadLine();
        DisplayLine(19);
        string? ageAsString = Console.ReadLine();
        DisplayLine(20);
        string? FurCoat = Console.ReadLine();
        DisplayLine(21);
        string? sizePawsAsString = Console.ReadLine();
        DisplayLine(22);
        string? CarnivorousDiet = Console.ReadLine();
        DisplayLine(23);
        string? SemiAquatic = Console.ReadLine();
        DisplayLine(24);
        string? SenseOfSmell = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (FurCoat is null) 
        {
            throw new ArgumentNullException(nameof(FurCoat));
        }
        if (sizePawsAsString is null) 
        {
            throw new ArgumentNullException(nameof(sizePawsAsString));
        }
        if (CarnivorousDiet is null) 
        {
            throw new ArgumentNullException(nameof(CarnivorousDiet));
        }
        if (SemiAquatic is null) 
        {
            throw new ArgumentNullException(nameof(SemiAquatic));
        }
        if (SenseOfSmell is null) 
        {
            throw new ArgumentNullException(nameof(SenseOfSmell));
        }
        int age = Int32.Parse(ageAsString);
        int SizePaws = Int32.Parse(sizePawsAsString);
        PolarBear polarBear = new PolarBear(name, age, FurCoat,  SizePaws, CarnivorousDiet, SemiAquatic, SenseOfSmell);

        return polarBear;
    }

    #endregion // Private Methods
}
using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data.Mammals;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui;

public sealed class AfricanElephantsScreen : Screen
{
    #region Properties And Ctor
    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public AfricanElephantsScreen(
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
        ScreenDefinitionJson = "AfricanElephantsScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
        history.Add("African Elephants");


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
                        line = 4;
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
                    AfricanElephantsScreenChoices choice = (AfricanElephantsScreenChoices)Int32.Parse(choiceAsString);
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

                AfricanElephantsScreenChoices choice = (AfricanElephantsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case AfricanElephantsScreenChoices.List:
                        Console.ResetColor();
                        Console.Clear();
                        ListAfricanElephants();
                        break;

                    case AfricanElephantsScreenChoices.Create:
                        Console.ResetColor();
                        Console.Clear();
                        AddAfricanElephant();
                        break;

                    case AfricanElephantsScreenChoices.Delete:
                        Console.ResetColor();
                        Console.Clear();
                        DeleteAfricanElephant();
                        break;

                    case AfricanElephantsScreenChoices.Modify:
                        Console.ResetColor();
                        Console.Clear();
                        EditAfricanElephantMain();
                        break;

                    case AfricanElephantsScreenChoices.Exit:
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
    private void ListAfricanElephants()
    {
        if (_dataService?.Animals?.Mammals?.AfricanElephants is not null &&
            _dataService.Animals.Mammals.AfricanElephants.Count > 0)
        {
            DisplayLine(10); 
            int i = 1;
            foreach (AfricanElephant africanElephant in _dataService.Animals.Mammals.AfricanElephants)
            {
                Console.Write($"African elephant number {i}, ");
                africanElephant.Display();
                i++;
            }
        }
        else
        {
            DisplayLine(11);
        }
    }

    /// <summary>
    /// Add a african elephant.
    /// </summary>
    private void AddAfricanElephant()
    {
        try
        {
            AfricanElephant africanElephant = AddEditAfricanElephant();
            _dataService?.Animals?.Mammals?.AfricanElephants?.Add(africanElephant);
            Console.WriteLine("African elephants with name: {0} has been added to a list of african elephants", africanElephant.Name);
        }
        catch
        {
            DisplayLine(18);
        }
    }

    /// <summary>
    /// Deletes a polar bear.
    /// </summary>
    private void DeleteAfricanElephant()
    {
        try
        {
            DisplayLine(12);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? africanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(e => e is not null && string.Equals(e.Name, name)));
            if (africanElephant is not null)
            {
                _dataService?.Animals?.Mammals?.AfricanElephants?.Remove(africanElephant);
                Console.WriteLine("African elephant with name: {0} has been deleted from a list of afrincan elephants", africanElephant.Name);
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
    private void EditAfricanElephantMain()
    {
        try
        {
            DisplayLine(14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? africanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(e => e is not null && string.Equals(e.Name, name)));
            if (africanElephant is not null)
            {
                AfricanElephant africanElephantEdited = AddEditAfricanElephant();
                africanElephant.Copy(africanElephantEdited);
                DisplayLine(15);
                africanElephant.Display();
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
    /// Adds/edit specific african elephant.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private AfricanElephant AddEditAfricanElephant()
    {
        DisplayLine(18);
        string? name = Console.ReadLine();
        DisplayLine(19);
        string? ageAsString = Console.ReadLine();
        DisplayLine(20);
        string? heightAsString = Console.ReadLine();
        DisplayLine(21);
        string? weightAsString = Console.ReadLine();
        DisplayLine(22);
        string? tuskLenghtAsString = Console.ReadLine();
        DisplayLine(23);
        string? lifespanAsString = Console.ReadLine();
        DisplayLine(24);
        string? SocialBehavior = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (heightAsString is null)
        {
            throw new ArgumentNullException(nameof(heightAsString));
        }
        if (weightAsString is null)
        {
            throw new ArgumentNullException(nameof(weightAsString));
        }
        if (tuskLenghtAsString is null)
        {
            throw new ArgumentNullException(nameof(tuskLenghtAsString));
        }
        if (lifespanAsString is null)
        {
            throw new ArgumentNullException(nameof(lifespanAsString));
        }
        if (SocialBehavior is null)
        {
            throw new ArgumentNullException(nameof(SocialBehavior));
        }
        int age = Int32.Parse(ageAsString);
        float Height = float.Parse(heightAsString);
        float Weight = float.Parse(weightAsString);
        float TuskLenght = float.Parse(tuskLenghtAsString);
        int Lifespan = Int32.Parse(lifespanAsString);
        AfricanElephant africanElephant = new AfricanElephant(name, age, Height, Weight, TuskLenght, Lifespan, SocialBehavior);

        return africanElephant;
    }

    #endregion // Private Methods
}


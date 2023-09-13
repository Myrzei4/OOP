using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class DogsScreen : Screen
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
    public DogsScreen(
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
        ScreenDefinitionJson = "DogsScreenLines.json";
        ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);    // information about screens output
        history.Add("Dogs");

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
                    DogsScreenChoices choice = (DogsScreenChoices)Int32.Parse(choiceAsString);
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

                DogsScreenChoices choice = (DogsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case DogsScreenChoices.List:
                        Console.ResetColor();
                        Console.Clear();
                        ListDogs();
                        break;

                    case DogsScreenChoices.Create:
                        Console.ResetColor();
                        Console.Clear();
                        AddDog();
                        break;

                    case DogsScreenChoices.Delete:
                        Console.ResetColor();
                        Console.Clear();
                        DeleteDog();
                        break;

                    case DogsScreenChoices.Modify:
                        Console.ResetColor();
                        Console.Clear();
                        EditDogMain();
                        break;

                    case DogsScreenChoices.Exit:
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

    #region Private Methods

    /// <summary>
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            DisplayLine(10);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                Console.Write($"Dog number {i}, ");
                dog.Display();
                i++;
            }
        }
        else
        {
            DisplayLine(11);
        }
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            Console.WriteLine("Dog with name: {0} has been added to a list of dogs", dog.Name);
        }
        catch
        {
            DisplayLine(17);
        }
    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            DisplayLine(12);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                Console.WriteLine("Dog with name: {0} has been deleted from a list of dogs", dog.Name);
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
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            DisplayLine(14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                DisplayLine(15);
                dog.Display();
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
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {
        DisplayLine(18);
        string? name = Console.ReadLine();
        DisplayLine(19);
        string? ageAsString = Console.ReadLine();
        DisplayLine(20);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new Dog(name, age, breed);

        return dog;
    }

    #endregion // Private Methods
}

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

namespace SampleHierarchies.Gui
{
    public sealed class GrizzlyBearsScreen : Screen
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
        public GrizzlyBearsScreen(
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
            ScreenDefinitionJson = "GrizzlyBearsScreenLines.json";
            ScreenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson); // information about screens output
            history.Add("GrizzlyBears");

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
                            line = 4;
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
                        GrizzlyBearsScreenChoices choice = (GrizzlyBearsScreenChoices)Int32.Parse(choiceAsString);
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

                    GrizzlyBearsScreenChoices choice = (GrizzlyBearsScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case GrizzlyBearsScreenChoices.List:
                            Console.ResetColor();
                            Console.Clear();
                            ListGrizzlyBears();
                            break;

                        case GrizzlyBearsScreenChoices.Create:
                            Console.ResetColor();
                            Console.Clear();
                            AddGrizzlyBear();
                            break;

                        case GrizzlyBearsScreenChoices.Delete:
                            Console.ResetColor();
                            Console.Clear();
                            DeleteGrizzlyBear();
                            break;

                        case GrizzlyBearsScreenChoices.Modify:
                            Console.ResetColor();
                            Console.Clear();
                            EditGrizzlyBearMain();
                            break;

                        case GrizzlyBearsScreenChoices.Exit:
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
        /// List all grizzly bears.
        /// </summary>
        private void ListGrizzlyBears()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.GrizzlyBears is not null &&
                _dataService.Animals.Mammals.GrizzlyBears.Count > 0)
            {
                DisplayLine(10);
                int i = 1;
                foreach (GrizzlyBear grizzlyBear in _dataService.Animals.Mammals.GrizzlyBears)
                {
                    Console.Write($"Grizzly bears number {i}, ");
                    grizzlyBear.Display();

                    i++;
                }
            }
            else
            {
                DisplayLine(11);
            }
        }



        /// <summary>
        /// Add a grizzly bear.
        /// </summary>
        private void AddGrizzlyBear()
        {
            try
            {
                GrizzlyBear grizzlyBear = AddEditGrizzlyBear();
                _dataService?.Animals?.Mammals?.GrizzlyBears?.Add(grizzlyBear);
                Console.WriteLine("Grizzly bear with name: {0} has been added to a list of grizzly bears", grizzlyBear.Name);
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Deletes a grizzly bear.
        /// </summary>
        private void DeleteGrizzlyBear()
        {
            try
            {
                DisplayLine(12);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                GrizzlyBear? grizzlyBear = (GrizzlyBear?)(_dataService?.Animals?.Mammals?.GrizzlyBears
                    ?.FirstOrDefault(gb => gb is not null && string.Equals(gb.Name, name)));
                if (grizzlyBear is not null)
                {
                    _dataService?.Animals?.Mammals?.GrizzlyBears?.Remove(grizzlyBear);
                    Console.WriteLine("Grizzly bear with name: {0} has been added to a list of grizzly bears", grizzlyBear.Name);
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
        /// Edits an existing grizzly bear after choice made.
        /// </summary>
        private void EditGrizzlyBearMain()
        {
            try
            {
                DisplayLine(14);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                GrizzlyBear? grizzlyBear = (GrizzlyBear?)(_dataService?.Animals?.Mammals?.GrizzlyBears
                    ?.FirstOrDefault(gb => gb is not null && string.Equals(gb.Name, name)));
                if (grizzlyBear is not null)
                {
                    GrizzlyBear grizzlyBearEdited = AddEditGrizzlyBear();
                    grizzlyBear.Copy(grizzlyBearEdited);
                    DisplayLine(15);
                    grizzlyBear.Display();
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
        /// Adds/edit specific grizzly bear.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private GrizzlyBear AddEditGrizzlyBear()
        {
            DisplayLine(18);
            string? name = Console.ReadLine();
            DisplayLine(19);
            string? ageAsString = Console.ReadLine();
            DisplayLine(20);
            string? hibernationAsString = Console.ReadLine();
            DisplayLine(21);
            string? sizeAsString = Console.ReadLine();
            DisplayLine(22);
            string? Diet = Console.ReadLine();
            DisplayLine(23);
            string? ClawsUsage = Console.ReadLine();
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
            if (hibernationAsString is null)
            {
                throw new ArgumentNullException(nameof(hibernationAsString));
            }
            if (sizeAsString is null)
            {
                throw new ArgumentNullException(nameof(sizeAsString));
            }
            if (Diet is null)
            {
                throw new ArgumentNullException(nameof(Diet));
            }
            if (ClawsUsage is null)
            {
                throw new ArgumentNullException(nameof(ClawsUsage));
            }
            if (SenseOfSmell is null)
            {
                throw new ArgumentNullException(nameof(SenseOfSmell));
            }
            int age = Int32.Parse(ageAsString);
            float Size = float.Parse(sizeAsString);
            int Hibernation = Int32.Parse(hibernationAsString);
            GrizzlyBear grizzlyBear = new GrizzlyBear(name, age, Hibernation, Diet, Size, ClawsUsage, SenseOfSmell);

            return grizzlyBear;
        }

        #endregion // Private Methods
    }
}

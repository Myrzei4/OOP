using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using Newtonsoft.Json;
using SampleHierarchies.Data;
using System.Reflection;
using System.Security.Authentication;
using Newtonsoft.Json.Linq;

namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
public class SettingsService : ISettingsService
{
    // Dictionary of screen settings
    private Dictionary<string, ISettings> colorSettings = new Dictionary<string, ISettings>();
    public string? className { get; set; }
    List<string> screensNames = new List<string>
        {
            "Default",
            "AfricanElephantsScreen",
            "MainScreen",
            "AnimalsScreen",
            "DogsScreen",
            "SettingsScreen",
            "GrizzlyBearsScreen",
            "MammalsScreen",
            "PolarBearsScreen",
        };
    #region ISettings Implementation
    /// <summary>
    /// Method for adding new screen settings
    /// </summary>
    /// <param name="newScreenName">Name of the new screen</param>
    /// <param name="backgroundColor">Color of Background.</param>
    /// <param name="foregroundColor">Color of Text.</param>
    public void AddSetting(string fileName)
    {
        // Default colors
        string defaultBackgroundColor = "Black";
        string defaultForegroundColor = "White";


        if (File.Exists(fileName))
        {
            colorSettings = Read(fileName);
            foreach (string screenName in screensNames)
            {
                if (colorSettings.ContainsKey(screenName))
                {
                }
                else
                {
                    Console.WriteLine($"Dictionary dont have {screenName}");
                    // Add new color settings for the screen
                    colorSettings.Add(screenName, new Settings
                    {
                        BackgroundColor = defaultBackgroundColor,
                        ForegroundColor = defaultForegroundColor
                    });
                }
            }
        }
        else
        {
            foreach (string screenName in screensNames)
            {
                if (colorSettings.ContainsKey(screenName))
                {
                }
                else
                {
                    Console.WriteLine($"Dictionary dont have {screenName}");
                    // Add new color settings for the screen
                    colorSettings.Add(screenName, new Settings
                    {
                        BackgroundColor = defaultBackgroundColor,
                        ForegroundColor = defaultForegroundColor
                    });
                }
            }
        }
    }



    public void Write(string fileName)
    {
        AddSetting(fileName); // Ensure settings are populated

        // Convert colorSettings dictionary to a format suitable for JSON serialization
        var settingsToSerialize = colorSettings.ToDictionary(
            kvp => kvp.Key,
            kvp => new
            {
                BackgroundColor = kvp.Value.BackgroundColor,
                ForegroundColor = kvp.Value.ForegroundColor
            }
        );

        // Serialize the settings to JSON with indentation
        string json = JsonConvert.SerializeObject(settingsToSerialize, Formatting.Indented);

        // Write the JSON to the specified file
        File.WriteAllText(fileName, json);

        //Console.WriteLine($"Settings saved in file: {fileName}");
    }


    /// <inheritdoc/>
    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="fileName">Json path</param>

    public Dictionary<string, ISettings> Read(string fileName)
    {
        // Check if the file exists
        if (File.Exists(fileName))
        {
            // Read the JSON content from the file
            string json = File.ReadAllText(fileName);


            // Deserialize the JSON into a dictionary of ISettings
            var settingsDictionary = JsonConvert.DeserializeObject<Dictionary<string, Settings>>(json);
            if (settingsDictionary != null)
            {
                Dictionary<string, ISettings>? result = settingsDictionary.ToDictionary(kvp => kvp.Key, kvp => (ISettings)kvp.Value);

                return result;
            }
            return colorSettings;
        }
        else
        {
            // Set default value for screen
            colorSettings.Add("Default", new Settings { BackgroundColor = "Green", ForegroundColor = "Blue" });
            return colorSettings;

        }
    }

    public void ApplyColors(string fileName, string className)
    {
        Write(fileName);

        if (File.Exists(fileName))
        {
            colorSettings = Read(fileName); // Read the settings from the JSON file
            if (colorSettings.ContainsKey(className))
            {
                ISettings colors = colorSettings[className];

                if (IsColorExist(colors.BackgroundColor) && IsColorExist(colors.ForegroundColor))
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colors.BackgroundColor);
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colors.ForegroundColor);

                }
                else
                {

                    Console.WriteLine($"Error: color is not available. Applying default settings");

                }
            }
            else
            {
                ISettings colorss = colorSettings["Default"];
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorss.BackgroundColor);
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorss.ForegroundColor);
              
            }
        }
        else
        {
            ISettings colorss = colorSettings["Default"];
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorss.BackgroundColor);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorss.ForegroundColor);
      
        }
    }
    public bool IsColorExist(string colour)
    {
        foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
        {
            if (colour == color.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeColor()
    {
        try
        {
            Console.Write("Enter the name of screen you want to change: ");
            string? screenName = Console.ReadLine();
            string jsonContent = File.ReadAllText("settings.json");
            JObject screens = JObject.Parse(jsonContent);

            var selectedScreen = screens.Properties()
                .FirstOrDefault(prop => prop.Name == screenName);

            if (selectedScreen != null)
            {
                Console.Write("Enter the new background color: ");
                string? newBGcolor = Console.ReadLine();
                selectedScreen.Value["BackgroundColor"] = newBGcolor;
                
                Console.Write("Enter the new foreground color: ");
                selectedScreen.Value["ForegroundColor"] = Console.ReadLine();

                File.WriteAllText("settings.json", screens.ToString());
                Console.WriteLine("Screen colors updated successfully.");
            }
            else
            {
                Console.WriteLine("Screen not found or colors not updated.");
            }
        }
        catch (Exception)
        {
            // Handle exceptions (file not found, invalid JSON, etc.)
            Console.WriteLine("An error occurred.");
        }
    }
    /// <summary>
    /// Listing colors in two columns
    /// </summary>
    public void ListColors() 
    {
        ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        int colorsPerColumn = colors.Length / 2;

        for (int i = 0; i < colorsPerColumn; i++)
        {
            Console.ForegroundColor = colors[i];
            Console.Write($"{colors[i],-12}");

            Console.ForegroundColor = colors[i + colorsPerColumn];
            Console.WriteLine(colors[i + colorsPerColumn]);
        }

        
        if (colors.Length % 2 != 0)
        {
            Console.ForegroundColor = colors[colors.Length - 1];
            Console.WriteLine(colors[colors.Length - 1]);
        }
        Console.ResetColor();

    }
    /// <summary>
    /// Listing Screens
    /// </summary>
    public void ListScreens() 
    {
        for (int i = 0; i < screensNames.Count; i++)
        {
            Console.WriteLine(screensNames[i]);
        }
    }
        #endregion // ISettingsService Implementation
}
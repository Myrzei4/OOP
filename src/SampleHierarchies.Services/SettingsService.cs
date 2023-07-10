using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SampleHierarchies.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;


namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
public class SettingsService : ISettingsService
{
    // Dictionary of screen settings
    private Dictionary<string, ISettings> colorSettings = new Dictionary<string, ISettings>
    {
        { "Default", new Settings {BackgroundColor = "black", ForegroundColor ="white" } }
    };
  
    public void ApplyColorSettings()
    {
        string className = GetType().Name;
        
    }
    
    #region ISettings Implementation

    /// <summary>
    /// Method for adding new screen settings
    /// </summary>
    /// <param name="newScreenName">Name of the new screen</param>
    /// <param name="backgroundColor">Color of Background.</param>
    /// <param name="foregroundColor">Color of Text.</param>
    public void AddSetting(string newScreenName, string backgroundColor = "black", string foregroundColor = "white") 
    {
        // get the assembly 
        Assembly assembly = Assembly.GetExecutingAssembly(); 

        // Get all types that satisfy the specified conditions
        Type[] types = assembly.GetTypes()
            .Where(t => t.Namespace == "SampleHierarchies.Gui" && t.Name.EndsWith("Screen") && t.Name.Length>6) 
            .ToArray();

        foreach (Type type in types)
        {
            // Extract the screen name from the type name
            string screenName = type.Name;

            // Check if the screen is not already added
            if (!colorSettings.ContainsKey(screenName))
            { 
                // Create a new instance of the Settings class with the specified background and foreground colors
                colorSettings.Add(screenName, new Settings
                {
                    BackgroundColor = backgroundColor,
                    ForegroundColor = foregroundColor
                });

            }
        }

        
    }

    /// <inheritdoc/>
    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    public ISettings? Read(string fileName)
    {    
        // Check if the file exists
        if (File.Exists(fileName))
        {
            // Read the JSON content from the file
            string json = File.ReadAllText(fileName);

            // Deserialize the JSON into a dictionary of ISettings
            colorSettings = JsonConvert.DeserializeObject<Dictionary<string, ISettings>>(json);

            Console.WriteLine($"Settings loaded from {fileName}");
        }
        else {
            Console.WriteLine($"File {fileName} not found. Settings not loaded");
        }
        return colorSettings.Values.FirstOrDefault();
    } 

    /// <inheritdoc/>
    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    public void Write(string fileName)
    {
        string json = JsonConvert.SerializeObject(colorSettings, Formatting.Indented);
        File.WriteAllText(fileName, json);
        Console.WriteLine($"Setings saved in file: {fileName}");
    }


    
       
    #endregion // ISettings Implementation
}
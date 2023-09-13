using SampleHierarchies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleHierarchies.Services;

public static class ScreenDefinitionService
{
    /// <summary>
    /// Loads a screen definition from a JSON file.
    /// </summary>
    /// <param name="jsonFileName"></param>
    /// <returns></returns>
    public static ScreenDefinition? Load(string jsonFileName)
    {
        try
        {
            if (File.Exists(jsonFileName))
            {
                string jsonContent = File.ReadAllText(jsonFileName);                            
                return JsonConvert.DeserializeObject<ScreenDefinition>(jsonContent);
                
            }
            else
            {
                // File does not exist
                return null;
                throw new FileNotFoundException("JSON file {0} does not exist.", jsonFileName);
            }
        }
        catch
        {
            // Handle loading errors
            throw new Exception();
        }
    }

    /// <summary>
    /// Saves a screen definition to a JSON file.
    /// </summary>
    /// <param name="screenDefinition"></param>
    /// <param name="jsonFileName"></param>
    /// <returns></returns>
    public static bool Save(ScreenDefinition screenDefinition, string jsonFileName)
    {
        try
        {
            if (screenDefinition == null)
            {
                return false;
            }
            else
            {

                string jsonContent = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
                File.WriteAllText(jsonFileName, jsonContent);
                return true;
            }
        }
        catch
        {
            // Saving error
            return false;
        }
    }
}

using SampleHierarchies.Data;
using Newtonsoft.Json;
using SampleHierarchies.Services;
using System.Diagnostics;

namespace SampleHierarchies.Gui;

/// <summary>
/// Abstract base class for a screen.
/// </summary>
public abstract class Screen
{
    #region Public Methods
    public ScreenDefinition? ScreenDefinition { get; set; }
    public string? ScreenDefinitionJson;

    public static List<string> history = new List<string>();
    /// <summary>
    /// Show the screen.
    /// </summary>
    public virtual void Show()
    {

        Console.WriteLine("Show Screen");
    }

    /// <summary>
    /// Method for displaying text line by line with color
    /// </summary>
    /// <param name="lineNumber"></param>
    public void DisplayLine(int lineNumber)
    {
        try
        {
            if (ScreenDefinition != null)
            {
                var lineEntry = ScreenDefinition.LineEntries[lineNumber];
                Console.BackgroundColor = lineEntry.BackgroundColor;
                Console.ForegroundColor = lineEntry.ForegroundColor;
                Console.WriteLine();
                Console.Write(lineEntry.Text);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Method for displaying text line by line without color
    /// </summary>
    /// <param name="lineNumber"></param>
    public void DisplayLineWOutColor(int lineNumber)
    {
        try
        {
            if (ScreenDefinition != null)
            {
                var lineEntry = ScreenDefinition.LineEntries[lineNumber];
                Console.WriteLine();
                Console.Write(lineEntry.Text);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    public void ShowHistory(List<string> history)
    {
        Console.ResetColor();
        foreach (var screen in history)
        {
            Console.Write($"=> {screen} ");
        }
        Console.WriteLine() ;
    }


    #endregion // Public Methods
}

using System.Drawing;

namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Settings interface.
/// </summary>
public interface ISettings
{
    #region Interface Members
    /// <summary>
    /// Color settings.
    /// </summary>
 
    string BackgroundColor { get; set; } // Color of background
    string ForegroundColor { get; set; } // Color of text

    #endregion // Interface Members
}


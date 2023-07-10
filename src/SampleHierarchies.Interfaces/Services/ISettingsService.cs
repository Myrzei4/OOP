using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

public interface ISettingsService
{
    #region Interface Members

    void AddSetting(string newScreenName, string backgroundColor = "black", string foregroundColor = "white"); // Method of adding Settings
    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    void Write(string fileName);
   
    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    ISettings? Read(string fileName);

    #endregion // Interface Members
}

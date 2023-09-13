using SampleHierarchies.Interfaces.Data;



namespace SampleHierarchies.Interfaces.Services;

public interface ISettingsService
{
    #region Interface Members

    void AddSetting(string fileName); // Method of adding Settings
    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    void Write(string fileName);

    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="fileName">Json path</param>
    public Dictionary<string, ISettings> Read(string fileName);

    public string? className { get; set; }
    public void ChangeColor();
    public void ListColors();
    public void ListScreens();
    public void ApplyColors(string fileName, string className);
    #endregion // Interface Members
}

using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Services;
namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Load_ValidJsonFile_ReturnsScreenDefinition()
    {
        // Arrange
        string jsonFileName = "valid.json"; // ������� ���� � ������������� JSON-�����
        ScreenDefinition screenDefinition = new ScreenDefinition();
        screenDefinition.LineEntries.Add(new ScreenLineEntry(ConsoleColor.White, ConsoleColor.Blue, "aboba"));
        string jsonContent = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
        File.WriteAllText(jsonFileName, jsonContent); // �������� ��������� JSON-���� � �������
        // Act
        ScreenDefinition result = ScreenDefinitionService.Load(jsonFileName);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.LineEntries.Count); // ������ ��������, ��� ��������� ������ ScreenDefinition � ����� �������
    }

    [TestMethod]
    public void Load_NonExistentJsonFile_ReturnsNull()
    {
        // Arrange
        string jsonFileName = "nonexistent.json"; // ������� �������������� JSON-����

        // Act
        ScreenDefinition result = ScreenDefinitionService.Load(jsonFileName);

        // Assert
        Assert.IsNull(result); // �������, ��� ������������ �������� ����� null
    }

    [TestMethod]
    public void Save_ValidScreenDefinition_SavesToFile()
    {
        // Arrange
        string jsonFileName = "valid.json"; // ������� ���� � ������������� JSON-�����
        ScreenDefinition screenDefinition = new ScreenDefinition();
        screenDefinition.LineEntries.Add(new ScreenLineEntry
        (
            ConsoleColor.Green,
            ConsoleColor.DarkGray,
            "Sample Text"
        ));

        // Act
        bool result = ScreenDefinitionService.Save(screenDefinition, jsonFileName);

        // Assert
        Assert.IsTrue(result); // �������, ��� ����� ���������� true ����� �������� ������
        Assert.IsTrue(File.Exists(jsonFileName)); // �������, ��� ���� ��� ������
    }

    [TestMethod]
    public void Save_InvalidScreenDefinition_FailsToSave()
    {
        // Arrange
        string jsonFileName = "invalid.json"; // ������� ���� � ������������� JSON-�����
        ScreenDefinition? screenDefinition = null; // ������������ ScreenDefinition
        
        // Act
        bool result = ScreenDefinitionService.Save(screenDefinition, jsonFileName);

        // Assert
        Assert.IsFalse(result); // �������, ��� ����� ���������� false ��-�� ������������ ������
    }
}
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
        string jsonFileName = "valid.json"; // Укажите путь к существующему JSON-файлу
        ScreenDefinition screenDefinition = new ScreenDefinition();
        screenDefinition.LineEntries.Add(new ScreenLineEntry(ConsoleColor.White, ConsoleColor.Blue, "aboba"));
        string jsonContent = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
        File.WriteAllText(jsonFileName, jsonContent); // Создайте временный JSON-файл с данными
        // Act
        ScreenDefinition result = ScreenDefinitionService.Load(jsonFileName);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.LineEntries.Count); // Пример проверки, что возвращен объект ScreenDefinition с одной строкой
    }

    [TestMethod]
    public void Load_NonExistentJsonFile_ReturnsNull()
    {
        // Arrange
        string jsonFileName = "nonexistent.json"; // Укажите несуществующий JSON-файл

        // Act
        ScreenDefinition result = ScreenDefinitionService.Load(jsonFileName);

        // Assert
        Assert.IsNull(result); // Ожидаем, что возвращенное значение будет null
    }

    [TestMethod]
    public void Save_ValidScreenDefinition_SavesToFile()
    {
        // Arrange
        string jsonFileName = "valid.json"; // Укажите путь к существующему JSON-файлу
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
        Assert.IsTrue(result); // Ожидаем, что метод возвращает true после успешной записи
        Assert.IsTrue(File.Exists(jsonFileName)); // Ожидаем, что файл был создан
    }

    [TestMethod]
    public void Save_InvalidScreenDefinition_FailsToSave()
    {
        // Arrange
        string jsonFileName = "invalid.json"; // Укажите путь к существующему JSON-файлу
        ScreenDefinition? screenDefinition = null; // Недопустимый ScreenDefinition
        
        // Act
        bool result = ScreenDefinitionService.Save(screenDefinition, jsonFileName);

        // Assert
        Assert.IsFalse(result); // Ожидаем, что метод возвращает false из-за недопустимых данных
    }
}
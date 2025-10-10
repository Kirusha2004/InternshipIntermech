using System.IO;
using System.Windows.Media;

namespace Task9.Tests;

[TestClass]
public class FileSettingsServiceTests
{
    private string _testSettingsPath;

    [TestInitialize]
    public void TestInitialize()
    {
        _testSettingsPath = Path.Combine(Path.GetTempPath(), "test_settings.json");
        if (File.Exists(_testSettingsPath))
        {
            File.Delete(_testSettingsPath);
        }
    }

    [TestMethod]
    public void SaveAndLoadSettingsRoundTripReturnsSameValues()
    {
        FileSettingsService service = new FileSettingsService(_testSettingsPath);
        AppSettings originalSettings = new AppSettings
        {
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            FontSize = 18,
            FontStyle = "Italic",
            UseFileConfig = true
        };

        service.SaveSettings(originalSettings);
        AppSettings loadedSettings = service.LoadSettings();

        Assert.AreEqual(originalSettings.BackgroundColor.ToString(), loadedSettings.BackgroundColor.ToString());
        Assert.AreEqual(originalSettings.TextColor.ToString(), loadedSettings.TextColor.ToString());
        Assert.AreEqual(originalSettings.FontSize, loadedSettings.FontSize);
        Assert.AreEqual(originalSettings.FontStyle, loadedSettings.FontStyle);
        Assert.AreEqual(originalSettings.UseFileConfig, loadedSettings.UseFileConfig);
    }

    [TestMethod]
    public void LoadSettingsFileNotExistsReturnsDefaultSettings()
    {
        FileSettingsService service = new FileSettingsService(_testSettingsPath);
        AppSettings settings = service.LoadSettings();

        Assert.IsNotNull(settings);
        Assert.AreEqual(Colors.White.ToString(), settings.BackgroundColor.ToString());
        Assert.AreEqual(Colors.Black.ToString(), settings.TextColor.ToString());
        Assert.AreEqual(16, settings.FontSize);
        Assert.AreEqual("Normal", settings.FontStyle);
    }
}

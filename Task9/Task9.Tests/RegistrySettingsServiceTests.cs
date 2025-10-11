using System.Windows.Media;
using Microsoft.Win32;

namespace Task9.Tests;

[TestClass]
public class RegistrySettingsServiceTests
{
    private readonly string _testRegistryPath = @"Software\SettingsAppTests";

    [TestInitialize]
    public void TestInitialize()
    {
        CleanTestRegistry();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        CleanTestRegistry();
    }

    private void CleanTestRegistry()
    {
        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(_testRegistryPath))
        {
            if (key != null)
            {
                Registry.CurrentUser.DeleteSubKeyTree(_testRegistryPath);
            }
        }
    }

    [TestMethod]
    public void SaveAndLoadSettingsRoundTripReturnsSameValues()
    {
        RegistrySettingsService service = new RegistrySettingsService(_testRegistryPath);
        AppSettings originalSettings = new AppSettings
        {
            BackgroundColor = Colors.Purple,
            TextColor = Colors.Orange,
            FontSize = 24,
            FontStyle = "Normal",
            UseFileConfig = false
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
    public void LoadSettingsRegistryKeyNotExistsReturnsDefaultSettings()
    {
        RegistrySettingsService service = new RegistrySettingsService(_testRegistryPath);
        AppSettings settings = service.LoadSettings();

        Assert.IsNotNull(settings);
        Assert.AreEqual(Colors.White.ToString(), settings.BackgroundColor.ToString());
        Assert.AreEqual(Colors.Black.ToString(), settings.TextColor.ToString());
        Assert.AreEqual(16, settings.FontSize);
        Assert.AreEqual("Normal", settings.FontStyle);
    }
}

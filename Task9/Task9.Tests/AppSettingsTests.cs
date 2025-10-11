using System.Windows.Media;

namespace Task9.Tests;

[TestClass]
public class AppSettingsTests
{
    [TestMethod]
    public void AppSettingsDefaultValuesAreCorrect()
    {
        AppSettings settings = new AppSettings();

        Assert.AreEqual(Colors.White.ToString(), settings.BackgroundColor.ToString());
        Assert.AreEqual(Colors.Black.ToString(), settings.TextColor.ToString());
        Assert.AreEqual(16, settings.FontSize);
        Assert.AreEqual("Normal", settings.FontStyle);
        Assert.IsTrue(settings.UseFileConfig);
    }

    [TestMethod]
    public void AppSettingsPropertySettersWorkCorrectly()
    {
        AppSettings settings = new AppSettings
        {
            BackgroundColor = Colors.Blue,
            TextColor = Colors.Red,
            FontSize = 20,
            FontStyle = "Italic",
            UseFileConfig = false
        };

        Assert.AreEqual(Colors.Blue.ToString(), settings.BackgroundColor.ToString());
        Assert.AreEqual(Colors.Red.ToString(), settings.TextColor.ToString());
        Assert.AreEqual(20, settings.FontSize);
        Assert.AreEqual("Italic", settings.FontStyle);
        Assert.IsFalse(settings.UseFileConfig);
    }
}

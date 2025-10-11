using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace Task9;

public class RegistrySettingsService
{
    private readonly string _registryPath;

    public RegistrySettingsService()
    {
        _registryPath = @"Software\SettingsApp";
    }

    public RegistrySettingsService(string testRegistryPath)
    {
        _registryPath = testRegistryPath;
    }

    public void SaveSettings(AppSettings settings)
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(_registryPath);
            key.SetValue("BackgroundColor", settings.BackgroundColor.ToString());
            key.SetValue("TextColor", settings.TextColor.ToString());
            key.SetValue("FontSize", settings.FontSize);
            key.SetValue("FontStyle", settings.FontStyle);
            key.SetValue("UseFileConfig", settings.UseFileConfig ? 1 : 0);
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Ошибка сохранения в реестр: {ex.Message}");
        }
    }

    public AppSettings LoadSettings()
    {
        try
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(_registryPath);
            return key == null
                ? GetDefaultSettings()
                : new AppSettings
                {
                    BackgroundColor = (Color)ColorConverter.ConvertFromString(key.GetValue("BackgroundColor", "#FFFFFFFF")?.ToString() ?? "#FFFFFFFF"),
                    TextColor = (Color)ColorConverter.ConvertFromString(key.GetValue("TextColor", "#FF000000")?.ToString() ?? "#FF000000"),
                    FontSize = Convert.ToDouble(key.GetValue("FontSize", 16)),
                    FontStyle = (string)key.GetValue("FontStyle", "Normal"),
                    UseFileConfig = (int)key.GetValue("UseFileConfig", 1) == 1
                };
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Ошибка загрузки из реестра: {ex.Message}");
            return GetDefaultSettings();
        }
    }

    private static AppSettings GetDefaultSettings()
    {
        return new AppSettings
        {
            UseFileConfig = false
        };
    }
}

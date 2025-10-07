using System.IO;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
namespace Task9;

public class FileSettingsService
{
    private readonly string _settingsPath;

    public FileSettingsService()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string appFolder = Path.Combine(appDataPath, "SettingsApp");
        _ = Directory.CreateDirectory(appFolder);
        _settingsPath = Path.Combine(appFolder, "settings.json");
    }

    public FileSettingsService(string testSettingsPath)
    {
        _settingsPath = testSettingsPath;
        string? directory = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrEmpty(directory))
        {
            _ = Directory.CreateDirectory(directory);
        }
    }

    public void SaveSettings(AppSettings settings)
    {
        try
        {
            var settingsData = new
            {
                BackgroundColor = settings.BackgroundColor.ToString(),
                TextColor = settings.TextColor.ToString(),
                settings.FontSize,
                settings.FontStyle,
                settings.UseFileConfig
            };

            string json = JsonConvert.SerializeObject(settingsData, Formatting.Indented);
            File.WriteAllText(_settingsPath, json);
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Ошибка сохранения в файл: {ex.Message}");
        }
    }

    public AppSettings LoadSettings()
    {
        try
        {
            if (!File.Exists(_settingsPath))
            {
                return new AppSettings();
            }

            string json = File.ReadAllText(_settingsPath);
            dynamic? settingsData = JsonConvert.DeserializeObject<dynamic>(json);

            return new AppSettings
            {
                BackgroundColor = (Color)ColorConverter.ConvertFromString((string)settingsData.BackgroundColor),
                TextColor = (Color)ColorConverter.ConvertFromString((string)settingsData.TextColor),
                FontSize = (double)settingsData.FontSize,
                FontStyle = (string)settingsData.FontStyle,
                UseFileConfig = (bool)settingsData.UseFileConfig
            };
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Ошибка загрузки из файла: {ex.Message}");
            return new AppSettings();
        }
    }
}

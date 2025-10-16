using System.Windows;
using System.Windows.Media;

namespace Task9;

public partial class MainWindow : Window
{
    private readonly FileSettingsService _fileService;
    private readonly RegistrySettingsService _registryService;

    public MainWindow()
    {
        InitializeComponent();

        _fileService = new FileSettingsService();
        _registryService = new RegistrySettingsService();

        LoadSettings();
    }

    private void LoadSettings()
    {
        AppSettings settings;

        settings = _fileService.LoadSettings();

        ApplySettingsToUI(settings);

        FileConfigRadio.IsChecked = settings.UseFileConfig;
        RegistryConfigRadio.IsChecked = !settings.UseFileConfig;
    }

    private void ApplySettingsToUI(AppSettings settings)
    {
        MainTextBlock.Background = new SolidColorBrush(settings.BackgroundColor);
        MainTextBlock.Foreground = new SolidColorBrush(settings.TextColor);
        MainTextBlock.FontSize = settings.FontSize;
        MainTextBlock.FontStyle = GetFontStyleFromString(settings.FontStyle);

        BgColorPicker.SelectedColor = settings.BackgroundColor;
        TextColorPicker.SelectedColor = settings.TextColor;
        FontSizeComboBox.Text = settings.FontSize.ToString();
        FontStyleComboBox.Text = settings.FontStyle;
    }

    private static FontStyle GetFontStyleFromString(string fontStyle)
    {
        return fontStyle.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
        {
            "italic" => FontStyles.Italic,
            "oblique" => FontStyles.Oblique,
            _ => FontStyles.Normal,
        };
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        AppSettings settings = new AppSettings
        {
            BackgroundColor = BgColorPicker.SelectedColor ?? Colors.White,
            TextColor = TextColorPicker.SelectedColor ?? Colors.Black,
            FontSize = double.TryParse(FontSizeComboBox.Text, out double size) ? size : 16,
            FontStyle = FontStyleComboBox.Text,
            UseFileConfig = FileConfigRadio.IsChecked == true
        };

        if (settings.UseFileConfig)
        {
            _fileService.SaveSettings(settings);
        }
        else
        {
            _registryService.SaveSettings(settings);
        }

        ApplySettingsToUI(settings);

        _ = MessageBox.Show("Настройки сохранены успешно!", "Успех",
                      MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

using System.Windows.Media;

namespace Task9;

public class AppSettings
{
    public AppSettings()
    {
        BackgroundColor = Colors.White;
        TextColor = Colors.Black;
        FontSize = 16;
        FontStyle = "Normal";
        UseFileConfig = true;
    }

    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }
    public double FontSize { get; set; }
    public string FontStyle { get; set; }
    public bool UseFileConfig { get; set; }
}

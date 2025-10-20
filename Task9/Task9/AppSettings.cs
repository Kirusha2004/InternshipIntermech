using System.Windows.Media;
namespace Task9;

public class AppSettings
{
    private double _fontSize;
    private string _fontStyle = "Normal";

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

    public double FontSize
    {
        get => _fontSize;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("FontSize must be positive.");
            }
            _fontSize = value;
        }
    }

    public string FontStyle
    {
        get => _fontStyle;
        set
        {
            if (!_sourceArray.Contains(value))
            {
                throw new ArgumentException("Invalid FontStyle.");
            }
            _fontStyle = value;
        }
    }

    public bool UseFileConfig { get; set; }

    private readonly string[] _sourceArray = ["Normal", "Italic", "Oblique"];

    public bool IsValid()
    {
        return FontSize > 0 && !string.IsNullOrEmpty(FontStyle);
    }
}

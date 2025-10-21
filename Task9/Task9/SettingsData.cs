namespace Task9;
public class SettingsData
{
    public string BackgroundColor { get; }
    public string TextColor { get; }
    public double FontSize { get; }
    public string FontStyle { get; }
    public bool UseFileConfig { get; }

    private static readonly string[] SourceArray = ["Normal", "Italic", "Oblique"];

    public SettingsData(
        string backgroundColor = "#FFFFFFFF",
        string textColor = "#FF000000",
        double fontSize = 16,
        string fontStyle = "Normal",
        bool useFileConfig = true)
    {
        if (fontSize <= 0)
        {
            throw new ArgumentException("FontSize must be positive.");
        }

        if (!SourceArray.Contains(fontStyle))
        {
            throw new ArgumentException("Invalid FontStyle.");
        }

        BackgroundColor = backgroundColor;
        TextColor = textColor;
        FontSize = fontSize;
        FontStyle = fontStyle;
        UseFileConfig = useFileConfig;
    }
}

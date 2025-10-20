namespace Task18;

public class FileReader
{
    public string ReadFile(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения {filePath}: {ex.Message}");
            return string.Empty;
        }
    }
}

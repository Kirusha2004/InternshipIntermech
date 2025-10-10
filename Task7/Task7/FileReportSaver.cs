using System.Text;

namespace Task7;

public class FileReportSaver : IReportSaver
{
    public void SaveReport(string content, string fileName)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
            writer.Write(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }
    }
}

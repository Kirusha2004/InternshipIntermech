using System.IO.Compression;
using System.Text;

namespace Task6;

public class FileManager
{
    public static string? FindFile(string fileName, string searchPath = "C:\\")
    {
        try
        {
            if (!Directory.Exists(searchPath))
            {
                Console.WriteLine($"Путь не существует: {searchPath}");
                return null;
            }

            string[] files = Directory.GetFiles(searchPath, fileName, SearchOption.AllDirectories);
            return files.Length > 0 ? files[0] : null;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Нет доступа к некоторым папкам в пути: {searchPath}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка поиска: {ex.Message}");
            return null;
        }
    }

    public static string ViewFile(string path)
    {
        if (!File.Exists(path))
        {
            return "Файл не найден";
        }

        try
        {
            using var reader = new StreamReader(path, Encoding.UTF8);
            string textFromFile = reader.ReadToEnd();
            Console.WriteLine($"Текст из файла: {textFromFile}");
            return textFromFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            return $"Ошибка чтения файла: {ex.Message}";
        }
    }

    public static bool CompressFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        string compressedFile = filePath + ".gz";

        try
        {
            using (FileStream source = new FileStream(filePath, FileMode.Open))
            using (FileStream target = new FileStream(compressedFile, FileMode.Create))
            using (GZipStream gzip = new GZipStream(target, CompressionMode.Compress))
            {
                source.CopyTo(gzip);
            }

            Console.WriteLine($"Файл сжат: {compressedFile}");
            return File.Exists(compressedFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сжатия: {ex.Message}");
            return false;
        }
    }
}

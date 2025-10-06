using System.IO.Compression;
using System.Text;

namespace Task6;

public class FileManager : IFileManager
{
    public string FindFile(string fileName, string searchPath = null)
    {
        searchPath ??= Path.GetPathRoot(Environment.CurrentDirectory) ?? "/";

        if (!Directory.Exists(searchPath))
        {
            throw new DirectoryNotFoundException($"Директория не существует: {searchPath}");
        }

        try
        {
            return SafeFileSearch(fileName, searchPath);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"Нет доступа к некоторым папкам в пути: {searchPath}", ex);
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"Файл '{fileName}' не найден в директории: {searchPath}");
        }
    }

    private string SafeFileSearch(string fileName, string searchPath)
    {
        try
        {
            var files = Directory.EnumerateFiles(searchPath, fileName, SearchOption.TopDirectoryOnly);
            var foundFile = files.FirstOrDefault();
            if (foundFile != null)
                return foundFile;

            foreach (var dir in Directory.EnumerateDirectories(searchPath))
            {
                try
                {
                    var result = SafeFileSearch(fileName, dir);
                    return result; 
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
            }

            throw new FileNotFoundException($"Файл '{fileName}' не найден в директории: {searchPath}");
        }
        catch (UnauthorizedAccessException)
        {
            throw new FileNotFoundException($"Файл '{fileName}' не найден (отсутствует доступ к некоторым директориям)");
        }
    }

    public string ViewFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Файл не найден: {path}");
        }

        try
        {
            using var reader = new StreamReader(path, Encoding.UTF8);
            string content = reader.ReadToEnd();
            Console.WriteLine($"Текст из файла: {content}");
            return content;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new IOException($"Ошибка чтения файла: {ex.Message}", ex);
        }
    }

    public void CompressFile(string sourcePath, string targetPath = null)
    {
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException($"Исходный файл не найден: {sourcePath}");
        }

        targetPath ??= sourcePath + ".gz";

        try
        {
            using var source = File.OpenRead(sourcePath);
            using var target = File.Create(targetPath);
            using var gzip = new GZipStream(target, CompressionMode.Compress);

            source.CopyTo(gzip);
            Console.WriteLine($"Файл сжат: {targetPath}");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new IOException($"Ошибка сжатия файла: {ex.Message}", ex);
        }
    }
}

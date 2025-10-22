using System.IO;
using SharedInterfaces;

namespace Task22;

public class FileLoggerWpf : IFileLoggerWpf
{
    private readonly string _filePath;
    private readonly object _lockObject = new object();

    public FileLoggerWpf(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<string> ReadAllTextAsync()
    {
        try
        {
            return await Task.Run(() =>
            {
                if (!File.Exists(_filePath))
                {
                    return string.Empty;
                }

                lock (_lockObject)
                {
                    return File.ReadAllText(_filePath);
                }
            });
        }
        catch (Exception ex)
        {
            return $"Error reading log file: {ex.Message}";
        }
    }
}

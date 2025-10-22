using SharedInterfaces;

namespace FileMonitorService;

public class FileLogger : IFileLogger
{
    private readonly string _filePath;
    private readonly object _lockObject = new object();

    public FileLogger(string filePath)
    {
        _filePath = filePath;
        EnsureDirectoryExists();
    }

    private void EnsureDirectoryExists()
    {
        try
        {
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                _ = Directory.CreateDirectory(directory);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Directory creation error: {ex.Message}");
        }
    }

    public async Task LogAsync(string message)
    {
        try
        {
            await Task.Run(() =>
            {
                lock (_lockObject)
                {
                    File.AppendAllText(_filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Logging error: {ex.Message}");
        }
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
            System.Diagnostics.Debug.WriteLine($"Read error: {ex.Message}");
            return string.Empty;
        }
    }
}

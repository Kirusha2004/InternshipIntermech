namespace Task18;

public class FileWriter : IDisposable
{
    private static readonly string MutexName = $"MutexSample:{Guid.NewGuid():N}";
    private readonly Mutex _mutexObj = new Mutex(false, MutexName);
    private readonly string _outputFile;
    private bool _disposed;

    public FileWriter(string outputFile)
    {
        _outputFile = outputFile;
        File.WriteAllText(_outputFile, string.Empty);
    }

    public void WriteContent(string sourceFile, string content)
    {
        _ = _mutexObj.WaitOne();
        try
        {
            File.AppendAllText(_outputFile, $"\n--- {sourceFile} ---\n");
            File.AppendAllText(_outputFile, content + "\n");
        }
        finally
        {
            _mutexObj.ReleaseMutex();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _mutexObj?.Dispose();
            }
            _disposed = true;
        }
    }
}

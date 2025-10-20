namespace Task17;
public class FileWriter : IDisposable
{

    private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(true);
    private readonly string _outputFile;
    private bool _disposed;

    public FileWriter(string outputFile)
    {
        _outputFile = outputFile;
        File.WriteAllText(_outputFile, string.Empty);
    }

    public void WriteContent(string sourceFile, string content)
    {
        _ = _autoResetEvent.WaitOne();
        try
        {
            File.AppendAllText(_outputFile, $"\n--- {sourceFile} ---\n");
            File.AppendAllText(_outputFile, content + "\n");
        }
        finally
        {
            _ = _autoResetEvent.Set();
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
                _autoResetEvent?.Dispose();
            }
            _disposed = true;
        }
    }
}

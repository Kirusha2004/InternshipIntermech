namespace Task17;
public class FileProcessor : IDisposable
{
    private readonly FileReader _fileReader;
    private readonly FileWriter _fileWriter;
    private readonly IFileCreator _fileCreator;
    private bool _disposed;


    public FileProcessor(string outputFileName, IFileCreator? fileCreator = null)
    {
        _fileReader = new FileReader();
        _fileWriter = new FileWriter(outputFileName);
        _fileCreator = fileCreator ?? new FileCreator();

        _fileCreator.CreateFile();
    }

    public void ProcessFile(string inputFile)
    {
        string content = _fileReader.ReadFile(inputFile);

        if (!string.IsNullOrEmpty(content))
        {
            _fileWriter.WriteContent(inputFile, content);
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
                _fileWriter?.Dispose();
            }
            _disposed = true;
        }
    }
}

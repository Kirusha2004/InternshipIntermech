namespace Task16;
public class FileWriter
{

    private readonly object _lock = new object();
    private readonly string _outputFile;

    public FileWriter(string outputFile)
    {
        _outputFile = outputFile;
        File.WriteAllText(_outputFile, string.Empty);
    }

    public void WriteContent(string sourceFile, string content)
    {
        lock (_lock)
        {
            File.AppendAllText(_outputFile, $"\n--- {sourceFile} ---\n");
            File.AppendAllText(_outputFile, content + "\n");
        }
    }
}

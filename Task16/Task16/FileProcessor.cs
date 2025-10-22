namespace Task16;

public class FileProcessor
{
    private readonly FileReader _fileReader;
    private readonly FileWriter _fileWriter;
    private readonly IFileCreator _fileCreator;

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
}

namespace Task6;

public interface IFileManager
{
    public string FindFile(string fileName, string? searchPath = null);
    public string ViewFile(string path);
    public void CompressFile(string sourcePath, string? targetPath = null);
}

namespace Task6;

public interface IFileManager
{
    string FindFile(string fileName, string searchPath = null);
    string ViewFile(string path);
    void CompressFile(string sourcePath, string targetPath = null);
}

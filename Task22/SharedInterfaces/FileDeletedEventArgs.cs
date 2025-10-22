namespace SharedInterfaces;
public class FileDeletedEventArgs : EventArgs
{
    public string FullPath { get; }
    public string FileName { get; }

    public FileDeletedEventArgs(string fullPath, string fileName)
    {
        FullPath = fullPath;
        FileName = fileName;
    }
}

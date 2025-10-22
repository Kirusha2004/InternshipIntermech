namespace SharedInterfaces;

public interface IFileLogger
{
    Task LogAsync(string message);
    Task<string> ReadAllTextAsync();
}

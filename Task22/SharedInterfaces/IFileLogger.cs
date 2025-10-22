namespace SharedInterfaces;

public interface IFileLogger
{
    public Task LogAsync(string message);
    public Task<string> ReadAllTextAsync();
}

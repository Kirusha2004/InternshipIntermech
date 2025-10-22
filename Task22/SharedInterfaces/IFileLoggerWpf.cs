namespace SharedInterfaces;

public interface IFileLoggerWpf
{
    public Task<string> ReadAllTextAsync();
}

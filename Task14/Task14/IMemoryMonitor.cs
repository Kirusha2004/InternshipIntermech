namespace Task14;

public interface IMemoryMonitor : IDisposable
{
    public bool IsMemoryLimitExceeded();
    public long GetCurrentMemoryUsage();
    public long MemoryLimit { get; }
    public event EventHandler<MemoryLimitExceededEventArgs> MemoryLimitExceeded;
}

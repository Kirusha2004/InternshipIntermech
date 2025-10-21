namespace Task14;

public class MonitorMemorySpace : IMemoryMonitor
{
    private readonly Timer _timer;
    private bool _disposed;
    private readonly object _lockObject = new object();

    public long MemoryLimit { get; }
    public event EventHandler<MemoryLimitExceededEventArgs>? MemoryLimitExceeded;

    public MonitorMemorySpace(long memoryLimit, int checkInterval = 200)
    {
        if (memoryLimit <= 0)
        {
            throw new ArgumentException("Memory limit must be greater than zero", nameof(memoryLimit));
        }

        MemoryLimit = memoryLimit;
        _timer = new Timer(CheckMemory, null, 0, checkInterval);
    }

    public bool IsMemoryLimitExceeded()
    {
        return GetCurrentMemoryUsage() > MemoryLimit;
    }

    public long GetCurrentMemoryUsage()
    {
        return GC.GetTotalMemory(false);
    }

    private void CheckMemory(object? state)
    {
        lock (_lockObject)
        {
            if (_disposed)
            {
                return;
            }

            if (IsMemoryLimitExceeded())
            {
                OnMemoryLimitExceeded();
            }
        }
    }

    protected virtual void OnMemoryLimitExceeded()
    {
        long currentMemory = GetCurrentMemoryUsage();
        MemoryLimitExceededEventArgs args = new MemoryLimitExceededEventArgs(currentMemory, MemoryLimit);
        MemoryLimitExceeded?.Invoke(this, args);
    }

    public void StopMonitoring()
    {
        Dispose();
    }

    protected virtual void Dispose(bool disposing)
    {
        lock (_lockObject)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }
                _disposed = true;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

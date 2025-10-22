namespace Task14;

public class MemoryLimitExceededEventArgs : EventArgs
{
    public long CurrentMemory { get; }
    public long MemoryLimit { get; }
    public DateTime Timestamp { get; }

    public MemoryLimitExceededEventArgs(long currentMemory, long memoryLimit)
    {
        CurrentMemory = currentMemory;
        MemoryLimit = memoryLimit;
        Timestamp = DateTime.Now;
    }
}

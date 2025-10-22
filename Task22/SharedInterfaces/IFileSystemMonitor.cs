namespace SharedInterfaces;

public interface IFileSystemMonitor : IDisposable
{
    public void StartMonitoring();
    public void StopMonitoring();
    public event EventHandler<FileDeletedEventArgs>? FileDeleted;
    public event EventHandler<ErrorEventArgs>? MonitorError;
}

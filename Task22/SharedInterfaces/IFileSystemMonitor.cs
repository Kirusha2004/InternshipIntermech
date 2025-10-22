namespace SharedInterfaces;

public interface IFileSystemMonitor : IDisposable
{
    void StartMonitoring();
    void StopMonitoring();
    event EventHandler<FileDeletedEventArgs>? FileDeleted;
    event EventHandler<ErrorEventArgs>? MonitorError;
}

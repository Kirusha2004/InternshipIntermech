using SharedInterfaces;

namespace FileMonitorService;

public class FileSystemMonitor : IFileSystemMonitor
{
    private FileSystemWatcher[]? _watchers;
    private readonly IFileLogger _logger;

    public event EventHandler<FileDeletedEventArgs>? FileDeleted;
    public event EventHandler<ErrorEventArgs>? MonitorError;

    public FileSystemMonitor(IFileLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void StartMonitoring()
    {
        try
        {
            DriveInfo[] drives = [.. DriveInfo.GetDrives()
                .Where(drive => drive.DriveType == DriveType.Fixed && drive.IsReady)];

            _watchers = new FileSystemWatcher[drives.Length];

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo drive = drives[i];
                FileSystemWatcher watcher = new FileSystemWatcher
                {
                    Path = drive.RootDirectory.FullName,
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    EnableRaisingEvents = true
                };

                watcher.Deleted += OnFileDeleted;
                watcher.Error += OnWatcherError;

                _watchers[i] = watcher;
            }

            _ = _logger.LogAsync("File system monitoring started");
        }
        catch (Exception ex)
        {
            OnMonitorError(new ErrorEventArgs(ex));
        }
    }

    public void StopMonitoring()
    {
        if (_watchers == null)
        {
            return;
        }

        foreach (FileSystemWatcher? watcher in from FileSystemWatcher watcher in _watchers
                                               where watcher != null
                                               select watcher)
        {
            watcher.Deleted -= OnFileDeleted;
            watcher.Error -= OnWatcherError;
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }

        _watchers = null;
        _ = _logger.LogAsync("File system monitoring stopped");
    }

    private async void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        try
        {
            await _logger.LogAsync($"DELETED: {e.FullPath}");
            FileDeleted?.Invoke(this, new FileDeletedEventArgs(e.FullPath, e.Name ?? string.Empty));
        }
        catch (Exception ex)
        {
            OnMonitorError(new ErrorEventArgs(ex));
        }
    }

    private void OnWatcherError(object sender, ErrorEventArgs e)
    {
        OnMonitorError(e);
    }

    private void OnMonitorError(ErrorEventArgs e)
    {
        MonitorError?.Invoke(this, e);
        _ = _logger.LogAsync($"MONITOR ERROR: {e.GetException().Message}");
    }

    public void Dispose()
    {
        StopMonitoring();
        GC.SuppressFinalize(this);
    }
}

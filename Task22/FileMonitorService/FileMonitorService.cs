using System.ComponentModel;
using System.ServiceProcess;
using SharedInterfaces;

namespace FileMonitorService;

public partial class FileMonitorService : ServiceBase
{
    private IContainer? _components;
    private readonly IFileSystemMonitor _fileMonitor;
    private readonly IFileLogger _fileLogger;
    private readonly string _logFilePath;

    public FileMonitorService()
    {
        InitializeComponent();
        ServiceName = "FileMonitorService";

        string currentDirectory = AppContext.BaseDirectory;
        _logFilePath = Path.Combine(currentDirectory, "deleted_files.log");

        _fileLogger = new FileLogger(_logFilePath);
        _fileMonitor = new FileSystemMonitor(_fileLogger);

        _fileMonitor.MonitorError += OnMonitorError;
    }

    private void InitializeComponent()
    {
        _components = new Container();
        ServiceName = "FileMonitorService";
    }

    protected override void OnStart(string[] args)
    {
        _ = _fileLogger.LogAsync("Service starting...");
        _fileMonitor.StartMonitoring();
        _ = _fileLogger.LogAsync("Service started successfully");
    }

    protected override void OnStop()
    {
        _ = _fileLogger.LogAsync("Service stopping...");
        _fileMonitor.StopMonitoring();
        _ = _fileLogger.LogAsync("Service stopped successfully");
    }

    protected override void OnShutdown()
    {
        _ = _fileLogger.LogAsync("Service shutting down...");
        _fileMonitor.StopMonitoring();
        base.OnShutdown();
    }

    private void OnMonitorError(object? sender, ErrorEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Monitor error: {e.GetException().Message}");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _components?.Dispose();
            _fileMonitor?.Dispose();
        }
        base.Dispose(disposing);
    }
}

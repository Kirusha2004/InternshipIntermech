using System.IO;
using System.Windows;
using System.Windows.Threading;
using SharedInterfaces;

namespace Task22;

public partial class MainWindow : Window
{
    private readonly IServiceManager? _serviceManager;
    private readonly IFileLoggerWpf? _fileLogger;
    private readonly DispatcherTimer? _refreshTimer;
    private readonly string? _logFilePath;
    private readonly string? _serviceExecutablePath;

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            string currentDirectory = AppContext.BaseDirectory;
            string serviceDir = Path.Combine(currentDirectory, "FileMonitor");
            _ = Directory.CreateDirectory(serviceDir);

            _logFilePath = Path.Combine(serviceDir, "deleted_files.log");
            _serviceExecutablePath = Path.Combine(serviceDir, "FileMonitorService.exe");

            _serviceManager = new ServiceManager(_serviceExecutablePath);
            _fileLogger = new FileLoggerWpf(_logFilePath);

            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _refreshTimer.Tick += RefreshTimer_Tick;

            Loaded += MainWindow_Loaded;
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (_serviceManager == null || _fileLogger == null)
        {
            return;
        }

        await RefreshServiceStatusAsync();
        await RefreshLogContentAsync();

        if (AutoRefreshCheckBox?.IsChecked == true)
        {
            _refreshTimer?.Start();
        }
    }

    private async void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        await RefreshLogContentAsync();
    }

    private async Task RefreshServiceStatusAsync()
    {
        try
        {
            if (_serviceManager == null)
            {
                return;
            }

            ServiceStatus status = _serviceManager.GetServiceStatus();
            UpdateStatusDisplay(status);
            UpdateButtonStates(status);
        }
        catch (Exception ex)
        {
            ShowError($"Failed to refresh service status: {ex.Message}");
        }
    }

    private async Task RefreshLogContentAsync()
    {
        try
        {
            if (_fileLogger == null)
            {
                return;
            }

            string content = await _fileLogger.ReadAllTextAsync();
            if (LogTextBox != null)
            {
                LogTextBox.Text = content;
                if (!string.IsNullOrEmpty(content))
                {
                    LogTextBox.ScrollToEnd();
                }
            }
            if (LastUpdateText != null)
            {
                LastUpdateText.Text = $"Last update: {DateTime.Now:HH:mm:ss}";
            }
        }
        catch (Exception ex)
        {
            if (LogTextBox != null)
            {
                LogTextBox.Text = $"Error reading log file: {ex.Message}";
            }
        }
    }

    private void UpdateStatusDisplay(ServiceStatus status)
    {
        if (StatusText != null)
        {
            StatusText.Text = status.ToString();
            StatusText.Foreground = status == ServiceStatus.Running ?
                System.Windows.Media.Brushes.Green :
                System.Windows.Media.Brushes.Red;
        }
    }

    private void UpdateButtonStates(ServiceStatus status)
    {
        bool isInstalled = status != ServiceStatus.NotInstalled;
        bool isRunning = status == ServiceStatus.Running;
        bool isStopped = status == ServiceStatus.Stopped;

        if (InstallBtn != null)
        {
            InstallBtn.IsEnabled = !isInstalled;
        }

        if (UninstallBtn != null)
        {
            UninstallBtn.IsEnabled = isInstalled;
        }

        if (StartBtn != null)
        {
            StartBtn.IsEnabled = isInstalled && isStopped;
        }

        if (StopBtn != null)
        {
            StopBtn.IsEnabled = isInstalled && isRunning;
        }
    }

    private async void InstallBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_serviceManager == null)
            {
                return;
            }

            if (InstallBtn != null)
            {
                InstallBtn.IsEnabled = false;
            }

            _ = await _serviceManager.InstallServiceAsync();
            await RefreshServiceStatusAsync();
            ShowInfo("Service installed successfully");
        }
        catch (Exception ex)
        {
            ShowError($"Installation failed: {ex.Message}");
            await RefreshServiceStatusAsync();
        }
    }

    private async void UninstallBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_serviceManager == null)
            {
                return;
            }

            if (UninstallBtn != null)
            {
                UninstallBtn.IsEnabled = false;
            }

            _ = await _serviceManager.UninstallServiceAsync();
            await RefreshServiceStatusAsync();
            ShowInfo("Service uninstalled successfully");
        }
        catch (Exception ex)
        {
            ShowError($"Uninstallation failed: {ex.Message}");
            await RefreshServiceStatusAsync();
        }
    }

    private async void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_serviceManager == null)
            {
                return;
            }

            if (StartBtn != null)
            {
                StartBtn.IsEnabled = false;
            }

            _ = await _serviceManager.StartServiceAsync();
            await RefreshServiceStatusAsync();
            ShowInfo("Service started successfully");
        }
        catch (Exception ex)
        {
            ShowError($"Failed to start service: {ex.Message}");
            await RefreshServiceStatusAsync();
        }
    }

    private async void StopBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_serviceManager == null)
            {
                return;
            }

            if (StopBtn != null)
            {
                StopBtn.IsEnabled = false;
            }

            _ = await _serviceManager.StopServiceAsync();
            await RefreshServiceStatusAsync();
            ShowInfo("Service stopped successfully");
        }
        catch (Exception ex)
        {
            ShowError($"Failed to stop service: {ex.Message}");
            await RefreshServiceStatusAsync();
        }
    }

    private async void RefreshStatusBtn_Click(object sender, RoutedEventArgs e)
    {
        await RefreshServiceStatusAsync();
        await RefreshLogContentAsync();
    }

    private async void ClearLogBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_logFilePath != null && File.Exists(_logFilePath))
            {
                File.Delete(_logFilePath);
                await RefreshLogContentAsync();
                ShowInfo("Log file cleared");
            }
        }
        catch (Exception ex)
        {
            ShowError($"Failed to clear log: {ex.Message}");
        }
    }

    private void AutoRefreshCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (_refreshTimer != null && AutoRefreshCheckBox != null)
        {
            _refreshTimer.IsEnabled = AutoRefreshCheckBox.IsChecked == true;
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _refreshTimer?.Stop();
    }

    private void ShowError(string message)
    {
        _ = MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void ShowInfo(string message)
    {
        _ = MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

using System.Windows;
using System.Windows.Threading;

namespace Task21;

public partial class MainWindow : Window
{
    private readonly DatabaseService _databaseService;
    private readonly TimerService _timerService;

    public MainWindow()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _timerService = new TimerService(TimeSpan.FromSeconds(3));

        SubscribeToEvents();
        UpdateUIState(ConnectionState.Disconnected);
    }

    private void SubscribeToEvents()
    {
        _timerService.DataReceived += OnDataReceived;
        _databaseService.ConnectionStateChanged += OnConnectionStateChanged;
    }

    private void OnDataReceived(object? sender, string data)
    {
        Dispatcher.Invoke(() =>
        {
            TextBox.Text += data;
            TextBox.ScrollToEnd();
        });
    }

    private void OnConnectionStateChanged(object? sender, ConnectionState state)
    {
        Dispatcher.Invoke(() => UpdateUIState(state));
    }

    private async void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        await _databaseService.ConnectAsync();
        _timerService.Start();
    }

    private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {
        _timerService.Stop();
        await _databaseService.DisconnectAsync();
    }

    private void UpdateUIState(ConnectionState state)
    {
        switch (state)
        {
            case ConnectionState.Connected:
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = true;
                break;
            case ConnectionState.Disconnected:
                ConnectButton.IsEnabled = true;
                DisconnectButton.IsEnabled = false;
                break;
            case ConnectionState.Connecting:
            case ConnectionState.Disconnecting:
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = false;
                break;
            default:
                break;
        }
    }
}

namespace Task21;

public class DatabaseService
{
    public event EventHandler<ConnectionState> ConnectionStateChanged;
    public event EventHandler<string> MessageReceived;

    private ConnectionState _currentState = ConnectionState.Disconnected;

    public ConnectionState CurrentState
    {
        get => _currentState;
        private set
        {
            if (_currentState != value)
            {
                _currentState = value;
                ConnectionStateChanged?.Invoke(this, _currentState);
            }
        }
    }

    public async Task ConnectAsync()
    {
        CurrentState = ConnectionState.Connecting;
        MessageReceived?.Invoke(this, "Подключение к базе данных..." + Environment.NewLine);

        await Task.Run(() => Thread.Sleep(3000));

        MessageReceived?.Invoke(this, "Подключен к базе данных" + Environment.NewLine);
        CurrentState = ConnectionState.Connected;
    }

    public async Task DisconnectAsync()
    {
        CurrentState = ConnectionState.Disconnecting;
        MessageReceived?.Invoke(this, "Отключение от базы данных..." + Environment.NewLine);

        await Task.Run(() => Thread.Sleep(3000));

        MessageReceived?.Invoke(this, "Отключен от базы данных" + Environment.NewLine);
        CurrentState = ConnectionState.Disconnected;
    }
}

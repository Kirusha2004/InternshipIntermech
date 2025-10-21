using System.Windows.Threading;

namespace Task21;

public class TimerService
{
    private readonly DispatcherTimer _timer;

    public event EventHandler<string> DataReceived;

    public TimerService(TimeSpan interval)
    {
        _timer = new DispatcherTimer
        {
            Interval = interval
        };
        _timer.Tick += OnTimerTick;
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    public bool IsRunning => _timer.IsEnabled;

    private void OnTimerTick(object sender, EventArgs e)
    {
        string data = "Данные получены" + Environment.NewLine;
        DataReceived?.Invoke(this, data);
    }
}

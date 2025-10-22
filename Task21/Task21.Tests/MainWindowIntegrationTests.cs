namespace Task21.Tests;

[TestClass]
public class MainWindowIntegrationTests
{
    [TestMethod]
    public async Task DatabaseServiceWithTimerServiceShouldWorkIndependently()
    {
        DatabaseService databaseService = new DatabaseService();
        TimerService timerService = new TimerService(TimeSpan.FromSeconds(1));

        IList<ConnectionState> dbStates = [];
        IList<string> dbMessages = [];
        bool timerStarted = false;
        bool timerStopped = false;

        databaseService.ConnectionStateChanged += (s, state) => dbStates.Add(state);
        databaseService.MessageReceived += (s, msg) => dbMessages.Add(msg);

        timerService.Start();
        timerStarted = timerService.IsRunning;
        timerService.Stop();
        timerStopped = !timerService.IsRunning;

        await databaseService.ConnectAsync();
        await databaseService.DisconnectAsync();

        Assert.IsTrue(timerStarted, "Timer should start successfully");
        Assert.IsTrue(timerStopped, "Timer should stop successfully");
        Assert.AreEqual(ConnectionState.Disconnected, databaseService.CurrentState);
        Assert.IsTrue(dbMessages.Count >= 4, $"Expected at least 4 database messages, but got {dbMessages.Count}");
    }

    [TestMethod]
    public async Task UIStateManagementShouldHandleConnectionStatesCorrectly()
    {
        DatabaseService databaseService = new DatabaseService();
        IList<ConnectionState> states = [];

        databaseService.ConnectionStateChanged += (s, state) => states.Add(state);

        await databaseService.ConnectAsync();
        bool wasConnected = databaseService.CurrentState == ConnectionState.Connected;

        await databaseService.DisconnectAsync();
        bool wasDisconnected = databaseService.CurrentState == ConnectionState.Disconnected;

        Assert.IsTrue(wasConnected, "Database should be in connected state after ConnectAsync");
        Assert.IsTrue(wasDisconnected, "Database should be in disconnected state after DisconnectAsync");
        Assert.IsTrue(states.Count >= 2, $"Expected at least 2 state changes, but got {states.Count}");
    }
}

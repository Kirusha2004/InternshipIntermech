namespace Task21.Tests;

[TestClass]
public class DatabaseServiceTests
{
    [TestMethod]
    public async Task ConnectAsyncShouldChangeStateFromDisconnectedToConnected()
    {
        DatabaseService databaseService = new DatabaseService();
        IList<ConnectionState> states = [];
        databaseService.ConnectionStateChanged += (s, state) => states.Add(state);

        await databaseService.ConnectAsync();

        Assert.AreEqual(ConnectionState.Connected, databaseService.CurrentState);
        Assert.AreEqual(2, states.Count);
        Assert.AreEqual(ConnectionState.Connected, states[^1]);
    }

    [TestMethod]
    public async Task DisconnectAsyncShouldChangeStateFromConnectedToDisconnected()
    {
        DatabaseService databaseService = new DatabaseService();
        await databaseService.ConnectAsync();
        IList<ConnectionState> states = [];
        databaseService.ConnectionStateChanged += (s, state) => states.Add(state);

        await databaseService.DisconnectAsync();

        Assert.AreEqual(ConnectionState.Disconnected, databaseService.CurrentState);
    }

    [TestMethod]
    public async Task ConnectAsyncShouldEmitCorrectMessages()
    {
        DatabaseService databaseService = new DatabaseService();
        IList<string> messages = [];
        databaseService.MessageReceived += (s, msg) => messages.Add(msg);

        await databaseService.ConnectAsync();

        Assert.AreEqual(2, messages.Count);
        StringAssert.Contains(messages[0], "Подключение к базе данных");
        StringAssert.Contains(messages[1], "Подключен к базе данных");
    }

    [TestMethod]
    public async Task DisconnectAsyncShouldEmitCorrectMessages()
    {
        DatabaseService databaseService = new DatabaseService();
        await databaseService.ConnectAsync();
        IList<string> messages = [];
        databaseService.MessageReceived += (s, msg) => messages.Add(msg);

        await databaseService.DisconnectAsync();

        Assert.AreEqual(2, messages.Count);
        StringAssert.Contains(messages[0], "Отключение от базы данных");
        StringAssert.Contains(messages[1], "Отключен от базы данных");
    }

    [TestMethod]
    public async Task ConnectAsyncShouldHaveDelay()
    {
        DatabaseService databaseService = new DatabaseService();
        DateTime startTime = DateTime.Now;

        await databaseService.ConnectAsync();
        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - startTime;

        Assert.IsTrue(duration >= TimeSpan.FromSeconds(2.9),
            $"Expected at least 3 seconds delay, but got {duration.TotalSeconds} seconds");
    }

    [TestMethod]
    public void CurrentStateInitiallyShouldBeDisconnected()
    {
        DatabaseService databaseService = new DatabaseService();

        Assert.AreEqual(ConnectionState.Disconnected, databaseService.CurrentState);
    }
}

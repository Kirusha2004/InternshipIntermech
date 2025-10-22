namespace Task21.Tests;

[TestClass]
public class TimerServiceTests
{
    [TestMethod]
    public void StartShouldStartTimer()
    {
        TimerService timerService = new TimerService(TimeSpan.FromSeconds(1));

        timerService.Start();

        Assert.IsTrue(timerService.IsRunning);
    }

    [TestMethod]
    public void StopShouldStopTimer()
    {
        TimerService timerService = new TimerService(TimeSpan.FromSeconds(1));
        timerService.Start();

        timerService.Stop();

        Assert.IsFalse(timerService.IsRunning);
    }

    [TestMethod]
    public void TimerInitialStateShouldBeStopped()
    {
        TimerService timerService = new TimerService(TimeSpan.FromSeconds(1));ф

        Assert.IsFalse(timerService.IsRunning);
    }
}

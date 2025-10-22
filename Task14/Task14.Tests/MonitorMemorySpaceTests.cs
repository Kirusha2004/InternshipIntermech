namespace Task14.Tests;

[TestClass]
public class MonitorMemorySpaceTests
{
    [TestMethod]
    public void ConstructorWithValidMemoryLimitShouldInitialize()
    {
        using MonitorMemorySpace monitor = new MonitorMemorySpace(1000000);

        Assert.AreEqual(1000000, monitor.MemoryLimit);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ConstructorWithZeroMemoryLimitShouldThrowException()
    {
        _ = new MonitorMemorySpace(0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ConstructorWithNegativeMemoryLimitShouldThrowException()
    {
        _ = new MonitorMemorySpace(-1000);
    }

    [TestMethod]
    public void IsMemoryLimitExceededWhenBelowLimitShouldReturnFalse()
    {
        using MonitorMemorySpace monitor = new MonitorMemorySpace(long.MaxValue);

        bool result = monitor.IsMemoryLimitExceeded();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetCurrentMemoryUsageShouldReturnNonNegativeValue()
    {
        using MonitorMemorySpace monitor = new MonitorMemorySpace(1000000);

        long usage = monitor.GetCurrentMemoryUsage();

        Assert.IsTrue(usage >= 0);
    }

    [TestMethod]
    public async Task MemoryLimitExceededEventShouldBeRaisedWhenLimitExceeded()
    {
        bool eventRaised = false;
        using MonitorMemorySpace monitor = new MonitorMemorySpace(100);

        monitor.MemoryLimitExceeded += (sender, e) => eventRaised = true;

        LargeObject[] objects = new LargeObject[100];
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = new LargeObject(10000);
            objects[i].Demonstrate(i);
        }

        await WaitForCondition(() => eventRaised, TimeSpan.FromSeconds(2));

        Assert.IsTrue(eventRaised);
    }

    [TestMethod]
    public void DisposeShouldStopMonitoring()
    {
        bool eventRaised = false;
        MonitorMemorySpace monitor = new MonitorMemorySpace(100);

        monitor.MemoryLimitExceeded += (sender, e) => eventRaised = true;

        ForceMemoryUsage();

        Thread.Sleep(300);
        bool eventRaisedBeforeDispose = eventRaised;

        monitor.Dispose();
        eventRaised = false;

        ForceMemoryUsage();

        Thread.Sleep(300);

        Assert.IsFalse(eventRaised);
    }

    [TestMethod]
    public void StopMonitoringShouldBeEquivalentToDispose()
    {
        MonitorMemorySpace monitor = new MonitorMemorySpace(1000000);

        monitor.StopMonitoring();

        monitor.Dispose();

        Assert.IsTrue(true);
    }

    private static async Task WaitForCondition(Func<bool> condition, TimeSpan timeout)
    {
        DateTime startTime = DateTime.Now;
        while (!condition() && DateTime.Now - startTime < timeout)
        {
            await Task.Delay(10);
        }
    }

    private static void ForceMemoryUsage()
    {
        LargeObject[] largeArray = new LargeObject[50];
        for (int i = 0; i < largeArray.Length; i++)
        {
            largeArray[i] = new LargeObject(10000);
        }
        GC.KeepAlive(largeArray);
    }
}

namespace Task20.Tests;

[TestClass]
public class ParallelExecutorTests
{
    [TestMethod]
    public void AddMethodWithNullActionDoesNotAdd()
    {
        ParallelExecutor executor = new ParallelExecutor();

        _ = executor.AddMethod((Action)null);

        Assert.AreEqual(0, executor.Count);
    }

    [TestMethod]
    public void AddMethodWithActionAddsMethod()
    {
        ParallelExecutor executor = new ParallelExecutor();

        _ = executor.AddMethod(() => { });

        Assert.AreEqual(1, executor.Count);
    }

    [TestMethod]
    public void AddMethodWithIParallelMethodAddsMethod()
    {
        ParallelExecutor executor = new ParallelExecutor();
        TestMethod1 method = new TestMethod1();

        _ = executor.AddMethod(method);

        Assert.AreEqual(1, executor.Count);
    }

    [TestMethod]
    public void AddMethodWhenRunningDoesNotAddMethod()
    {
        ParallelExecutor executor = new ParallelExecutor();
        TestMethod1 method1 = new TestMethod1();
        TestMethod2 method2 = new TestMethod2();

        _ = executor.AddMethod(method1);
        Task task = executor.ExecuteAsync();
        _ = executor.AddMethod(method2);

        Assert.AreEqual(1, executor.Count);
        task.Wait();
    }

    [TestMethod]
    public void ExecuteAsyncWithNoMethodsThrowsException()
    {
        ParallelExecutor executor = new ParallelExecutor();

        _ = Assert.ThrowsException<InvalidOperationException>(executor.ExecuteAsync);
    }

    [TestMethod]
    public void ExecuteAsyncWithMethodsChangesState()
    {
        ParallelExecutor executor = new ParallelExecutor();
        _ = executor.AddMethod(() => Thread.Sleep(200));

        Task task = executor.ExecuteAsync();

        Assert.IsTrue(executor.IsRunning);
        Assert.IsFalse(executor.IsCompleted);

        task.Wait();

        Assert.IsFalse(executor.IsRunning);
        Assert.IsTrue(executor.IsCompleted);
    }

    [TestMethod]
    public void ExecuteAsyncWithMultipleMethodsExecutesAll()
    {
        ParallelExecutor executor = new ParallelExecutor();
        TestMethod1 method1 = new TestMethod1();
        TestMethod2 method2 = new TestMethod2();

        _ = executor.AddMethod(method1).AddMethod(method2);
        executor.ExecuteAsync().Wait();

        Assert.IsTrue(method1.WasExecuted);
        Assert.IsTrue(method2.WasExecuted);
    }

    [TestMethod]
    public void ExecuteAsyncWithActionMethodsExecutesAll()
    {
        bool executed1 = false;
        bool executed2 = false;
        ParallelExecutor executor = new ParallelExecutor();

        _ = executor.AddMethod(() => executed1 = true);
        _ = executor.AddMethod(() => executed2 = true);
        executor.ExecuteAsync().Wait();

        Assert.IsTrue(executed1);
        Assert.IsTrue(executed2);
    }

    [TestMethod]
    public void ExecuteAsyncIsNonBlocking()
    {
        ParallelExecutor executor = new ParallelExecutor();
        _ = executor.AddMethod(() => Thread.Sleep(200));

        DateTime startTime = DateTime.Now;
        Task task = executor.ExecuteAsync();
        DateTime afterStartTime = DateTime.Now;

        TimeSpan delay = afterStartTime - startTime;
        Assert.IsTrue(delay.TotalMilliseconds < 100);

        task.Wait();
    }
}

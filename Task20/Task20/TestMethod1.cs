namespace Task20;

public class TestMethod1 : IParallelMethod
{
    public string Name => "Test1";

    public bool WasExecuted { get; private set; }

    public void Execute()
    {
        WasExecuted = true;
        Thread.Sleep(100);
    }
}

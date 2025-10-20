namespace Task20;

public class TestMethod2 : IParallelMethod
{
    public string Name => "Test2";

    public bool WasExecuted { get; private set; }

    public void Execute()
    {
        WasExecuted = true;
        Thread.Sleep(100);
    }
}

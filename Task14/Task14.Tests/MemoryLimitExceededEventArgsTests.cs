namespace Task14.Tests;

[TestClass]
public class MemoryLimitExceededEventArgsTests
{
    [TestMethod]
    public void ConstructorShouldSetPropertiesCorrectly()
    {
        long currentMemory = 1000000L;
        long memoryLimit = 500000L;

        MemoryLimitExceededEventArgs args = new MemoryLimitExceededEventArgs(currentMemory, memoryLimit);

        Assert.AreEqual(currentMemory, args.CurrentMemory);
        Assert.AreEqual(memoryLimit, args.MemoryLimit);
        Assert.IsTrue(args.Timestamp <= DateTime.Now);
    }

}

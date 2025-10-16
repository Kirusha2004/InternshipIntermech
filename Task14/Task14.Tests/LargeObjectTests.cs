namespace Task14.Tests;

[TestClass]
public class LargeObjectTests
{
    [TestMethod]
    public void ConstructorWithDefaultSizeShouldCreateObject()
    {
        LargeObject obj = new LargeObject();

        Assert.IsNotNull(obj);
        Assert.IsTrue(obj.ArraySize > 0);
    }

    [TestMethod]
    public void ConstructorWithCustomSizeShouldCreateObjectWithSpecifiedSize()
    {
        LargeObject obj = new LargeObject(5000);

        Assert.AreEqual(5000, obj.ArraySize);
    }

    [TestMethod]
    public void DemonstrateShouldExecuteWithoutExceptions()
    {
        LargeObject obj = new LargeObject(100);
        obj.Demonstrate(5);
    }

    [TestMethod]
    public void DemonstrateWithDifferentIndexesShouldWorkCorrectly()
    {
        LargeObject obj1 = new LargeObject(100);
        LargeObject obj2 = new LargeObject(100);

        obj1.Demonstrate(0);
        obj2.Demonstrate(100);
    }
}

namespace Task15.Tests;

[TestClass]
public class DataProcessorTests
{
    [TestMethod]
    public void DataProcessorIsAbstract()
    {

        bool isAbstract = typeof(DataProcessor).IsAbstract;

        Assert.IsTrue(isAbstract, "DataProcessor должен быть абстрактным классом");
    }

    [TestMethod]
    public void ProcessCoreIsProtectedAbstract()
    {
        System.Reflection.MethodInfo method = typeof(DataProcessor).GetMethod("ProcessCore",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.IsNotNull(method, "Метод ProcessCore должен существовать");
        Assert.IsTrue(method.IsAbstract, "ProcessCore должен быть абстрактным");
        Assert.IsTrue(method.IsFamily, "ProcessCore должен быть protected");
    }
}

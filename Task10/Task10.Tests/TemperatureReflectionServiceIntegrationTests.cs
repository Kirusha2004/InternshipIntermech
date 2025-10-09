using ReflectionTaskExtentionProject;

namespace Task10.Tests;
[TestClass]
public class TemperatureReflectionServiceIntegrationTests
{
    [TestMethod]
    public void MultipleConversionsWorkCorrectly()
    {
        AssemblyLoader assemblyLoader = new AssemblyLoader();
        System.Reflection.Assembly assembly = assemblyLoader.LoadAssembly("Task10.dll");
        TemperatureReflectionService service = new TemperatureReflectionService(assembly);

        Assert.AreEqual(32, service.ConvertCelsiusToFahrenheit(0));
        Assert.AreEqual(212, service.ConvertCelsiusToFahrenheit(100));
        Assert.AreEqual(77, service.ConvertCelsiusToFahrenheit(25));
    }
}

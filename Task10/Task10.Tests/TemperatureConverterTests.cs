using System.Reflection;
using ReflectionTaskExtentionProject;

namespace Task10.Tests;

[TestClass]
public class TemperatureConverterTests
{
    private AssemblyLoader? _assemblyLoader;
    private TemperatureReflectionService? _reflectionService;

    [TestInitialize]
    public void TestInitialize()
    {
        _assemblyLoader = new AssemblyLoader();
        Assembly? assembly = _assemblyLoader.LoadAssembly("Task10.dll");
        _reflectionService = new TemperatureReflectionService(assembly);
    }

    [TestMethod]
    public void LoadAssemblyValidPathReturnsAssembly()
    {
        Assembly? assembly = _assemblyLoader.LoadAssembly("Task10.dll");

        Assert.IsNotNull(assembly);
        Assert.AreEqual("Task10", assembly.GetName().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void LoadAssemblyInvalidPathThrowsException()
    {
        _assemblyLoader.LoadAssembly("NonExistent.dll");
    }

    [TestMethod]
    public void ConvertCelsiusToFahrenheitZeroCelsiusReturns32Fahrenheit()
    {
        double result = _reflectionService.ConvertCelsiusToFahrenheit(0);

        Assert.AreEqual(32, result);
    }

    [TestMethod]
    public void ConvertCelsiusToFahrenheit100CelsiusReturns212Fahrenheit()
    {
        double result = _reflectionService.ConvertCelsiusToFahrenheit(100);

        Assert.AreEqual(212, result);
    }

    [TestMethod]
    public void ConvertCelsiusToFahrenheitNegative40CelsiusReturnsNegative40Fahrenheit()
    {
        double result = _reflectionService.ConvertCelsiusToFahrenheit(-40);

        Assert.AreEqual(-40, result);
    }

    [TestMethod]
    public void ConvertCelsiusToFahrenheit25CelsiusReturns77Fahrenheit()
    {
        double result = _reflectionService.ConvertCelsiusToFahrenheit(25);

        Assert.AreEqual(77, result);
    }

    [TestMethod]
    public void GetConverterTypeReturnsCorrectType()
    {
        Type? type = _reflectionService.GetConverterType();

        Assert.AreEqual("Task10.TemperatureConverter", type.FullName);
    }

    [TestMethod]
    public void GetConversionMethodInfoReturnsCorrectMethod()
    {
        MethodInfo? methodInfo = _reflectionService.GetConversionMethodInfo();

        Assert.IsNotNull(methodInfo);
        Assert.AreEqual("CelsiusToFahrenheit", methodInfo.Name);
        Assert.AreEqual(1, methodInfo.GetParameters().Length);
        Assert.AreEqual(typeof(double), methodInfo.ReturnType);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TemperatureReflectionServiceInvalidAssemblyThrowsException()
    {
        Assembly? invalidAssembly = Assembly.GetExecutingAssembly();

        _ = new TemperatureReflectionService(invalidAssembly);
    }
}

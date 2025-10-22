using System.IO;

namespace Task22.Tests;

[TestClass]
public class ServiceManagerTests
{
    private string? _testServicePath;
    private ServiceManager? _serviceManager;

    [TestInitialize]
    public void TestInitialize()
    {
        _testServicePath = Path.Combine(Path.GetTempPath(), $"test_service_{Guid.NewGuid()}.exe");
        File.WriteAllText(_testServicePath, "dummy content");
        _serviceManager = new ServiceManager(_testServicePath);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (_testServicePath != null && File.Exists(_testServicePath))
        {
            File.Delete(_testServicePath);
        }
    }

    [TestMethod]
    public void ConstructorShouldInitializeWithValidPath()
    {
        Assert.IsNotNull(_serviceManager);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorShouldThrowWithNullPath()
    {
        _ = new ServiceManager(null!);
    }

    [TestMethod]
    public void IsServiceInstalledShouldReturnFalseForNonExistentService()
    {
        bool result = _serviceManager!.IsServiceInstalled();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetServiceStatusShouldReturnNotInstalledForNonExistentService()
    {
        ServiceStatus status = _serviceManager!.GetServiceStatus();
        Assert.AreEqual(ServiceStatus.NotInstalled, status);
    }
}

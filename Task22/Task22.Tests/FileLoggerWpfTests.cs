using System.IO;

namespace Task22.Tests;

[TestClass]
public class FileLoggerWpfTests
{
    private string? _testLogPath;
    private FileLoggerWpf? _fileLogger;

    [TestInitialize]
    public void TestInitialize()
    {
        _testLogPath = Path.Combine(Path.GetTempPath(), $"test_wpf_log_{Guid.NewGuid()}.txt");
        _fileLogger = new FileLoggerWpf(_testLogPath);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (_testLogPath != null && File.Exists(_testLogPath))
        {
            File.Delete(_testLogPath);
        }
    }

    [TestMethod]
    public async Task ReadAllTextAsyncShouldReturnEmptyWhenFileNotExists()
    {
        string content = await _fileLogger!.ReadAllTextAsync();
        Assert.AreEqual(string.Empty, content);
    }

    [TestMethod]
    public async Task ReadAllTextAsyncShouldReturnContentWhenFileExists()
    {
        string expectedContent = "Test content line 1\nTest content line 2";
        await File.WriteAllTextAsync(_testLogPath!, expectedContent);

        string actualContent = await _fileLogger!.ReadAllTextAsync();
        Assert.AreEqual(expectedContent, actualContent);
    }
}

namespace Task7.Tests;

[TestClass]
public class FileReportSaverTests
{
    private readonly string _testFileName = "test_report.txt";

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFileName))
        {
            File.Delete(_testFileName);
        }
    }

    [TestMethod]
    public void TestSaveReportCreatesFile()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test report content";

        saver.SaveReport(testContent, _testFileName);

        Assert.IsTrue(File.Exists(_testFileName));
    }

    [TestMethod]
    public void TestSaveReportWritesCorrectContent()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test report content with special chars: áéíóú";

        saver.SaveReport(testContent, _testFileName);

        string fileContent = File.ReadAllText(_testFileName);
        Assert.AreEqual(testContent, fileContent);
    }

    [TestMethod]
    public void TestSaveReportOverwritesExistingFile()
    {
        IReportSaver saver = new FileReportSaver();
        string initialContent = "Initial content";
        string newContent = "New content";

        File.WriteAllText(_testFileName, initialContent);

        saver.SaveReport(newContent, _testFileName);

        string fileContent = File.ReadAllText(_testFileName);
        Assert.AreEqual(newContent, fileContent);
        Assert.AreNotEqual(initialContent, fileContent);
    }

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnInvalidPath()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        _ = Assert.ThrowsException<IOException>(() =>
            saver.SaveReport(testContent, "invalid|path/file.txt"));
    }

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnNonExistentDirectory()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        _ = Assert.ThrowsException<DirectoryNotFoundException>(() =>
            saver.SaveReport(testContent, @"C:\NonExistentDirectory\test.txt"));
    }

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnReadOnlyFile()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        File.WriteAllText(_testFileName, "initial");
        File.SetAttributes(_testFileName, FileAttributes.ReadOnly);

        try
        {
            _ = Assert.ThrowsException<UnauthorizedAccessException>(() =>
                saver.SaveReport(testContent, _testFileName));
        }
        finally
        {
            File.SetAttributes(_testFileName, FileAttributes.Normal);
        }
    }
}

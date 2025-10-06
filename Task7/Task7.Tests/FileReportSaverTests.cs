namespace Task7.Tests;

[TestClass]
public class FileReportSaverTests
{
    private string _testFileName = "test_report.txt";

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
        var saver = new FileReportSaver();
        string testContent = "Test report content";

        saver.SaveReport(testContent, _testFileName);

        Assert.IsTrue(File.Exists(_testFileName));
    }

    [TestMethod]
    public void TestSaveReportWritesCorrectContent()
    {
        var saver = new FileReportSaver();
        string testContent = "Test report content with special chars: áéíóú";

        saver.SaveReport(testContent, _testFileName);

        string fileContent = File.ReadAllText(_testFileName);
        Assert.AreEqual(testContent, fileContent);
    }

    [TestMethod]
    public void TestSaveReportOverwritesExistingFile()
    {
        var saver = new FileReportSaver();
        string initialContent = "Initial content";
        string newContent = "New content";

        File.WriteAllText(_testFileName, initialContent);

        saver.SaveReport(newContent, _testFileName);

        string fileContent = File.ReadAllText(_testFileName);
        Assert.AreEqual(newContent, fileContent);
        Assert.AreNotEqual(initialContent, fileContent);
    }
}

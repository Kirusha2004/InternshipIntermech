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
    public void TestSaveReportThrowsExceptionOnNullPath()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        _ = Assert.ThrowsException<ArgumentNullException>(() =>
            saver.SaveReport(testContent, null!));
    }

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnEmptyPath()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        _ = Assert.ThrowsException<ArgumentException>(() =>
            saver.SaveReport(testContent, ""));
    }

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnNonExistentDirectory()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        string nonExistentPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "test.txt");
        _ = Assert.ThrowsException<DirectoryNotFoundException>(() =>
            saver.SaveReport(testContent, nonExistentPath));
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

    [TestMethod]
    public void TestSaveReportThrowsExceptionOnPathTooLong()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content";

        string longDirectory = new string('a', 100);
        string longFileName = new string('b', 100) + ".txt";
        string veryLongPath = Path.Combine(longDirectory, longFileName, longFileName, longFileName);
        saver.SaveReport(testContent, veryLongPath);
        Assert.Fail("Expected exception was not thrown");

    }

    [TestMethod]
    public void TestSaveReportWithValidUnicodeCharacters()
    {
        IReportSaver saver = new FileReportSaver();
        string testContent = "Test content with Unicode: 🚀✨";
        string unicodeFileName = "test_🚀_report.txt";

        try
        {
            saver.SaveReport(testContent, unicodeFileName);
            Assert.IsTrue(File.Exists(unicodeFileName));

            string fileContent = File.ReadAllText(unicodeFileName);
            Assert.AreEqual(testContent, fileContent);
        }
        finally
        {
            if (File.Exists(unicodeFileName))
            {
                File.Delete(unicodeFileName);
            }
        }
    }
}

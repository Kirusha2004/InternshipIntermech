using System.Text;

namespace Task6.Tests;

[TestClass]
public class FileManagerTests
{
    private string? _testFilePath;
    private readonly string _testContent = "Hello, Test World!";

    [TestInitialize]
    public void TestInitialize()
    {
        _testFilePath = Path.GetTempFileName();

        using (var writer = new StreamWriter(_testFilePath, false, new UTF8Encoding(false)))
        {
            writer.Write(_testContent);
        }
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);

        if (File.Exists(_testFilePath + ".gz"))
            File.Delete(_testFilePath + ".gz");
    }

    [TestMethod]
    public void ViewFileWhenFileExistsReturnsFileContent()
    {
        string result = FileManager.ViewFile(_testFilePath);
        Assert.AreEqual(_testContent, result);
    }

    [TestMethod]
    public void ViewFileWhenFileNotExistsReturnsErrorMessage()
    {
        string nonExistentFile = Path.Combine(Path.GetTempPath(), "non_existent_file.txt");
        string result = FileManager.ViewFile(nonExistentFile);
        Assert.AreEqual("Файл не найден", result);
    }

    [TestMethod]
    public void CompressFileWhenFileExistsCreatesCompressedFile()
    {
        bool result = FileManager.CompressFile(_testFilePath);
        string compressedFilePath = _testFilePath + ".gz";

        Assert.IsTrue(result);
        Assert.IsTrue(File.Exists(compressedFilePath));

        FileInfo compressedFileInfo = new FileInfo(compressedFilePath);
        Assert.IsTrue(compressedFileInfo.Length > 0);
    }

    [TestMethod]
    public void CompressFileWhenFileNotExistsReturnsFalse()
    {
        string nonExistentFile = Path.Combine(Path.GetTempPath(), "non_existent_file.txt");
        bool result = FileManager.CompressFile(nonExistentFile);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void FindFileWhenFileExistsReturnsFilePath()
    {
        string testDir = Path.GetTempPath();
        string testFileName = $"test_find_file_{Guid.NewGuid()}.txt";
        string testFileFullPath = Path.Combine(testDir, testFileName);

        using (var writer = new StreamWriter(testFileFullPath, false, new UTF8Encoding(false)))
        {
            writer.Write("test content");
        }

        try
        {
            string foundPath = FileManager.FindFile(testFileName, testDir);
            Assert.IsNotNull(foundPath);
            Assert.AreEqual(testFileFullPath, foundPath);
        }
        finally
        {
            if (File.Exists(testFileFullPath))
                File.Delete(testFileFullPath);
        }
    }

    [TestMethod]
    public void FindFileWhenFileNotExistsReturnsNull()
    {
        string result = FileManager.FindFile($"non_existent_file_{Guid.NewGuid()}.txt");
        Assert.IsNull(result);
    }
}

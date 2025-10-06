using System.Text;

namespace Task6.Tests;

[TestClass]
public class FileManagerTests
{
    private string _testFilePath;
    private readonly string _testContent = "Hello, Test World!";
    private IFileManager _fileManager;
    private string _testDirectory;

    [TestInitialize]
    public void TestInitialize()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), $"Test_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDirectory);

        _testFilePath = Path.Combine(_testDirectory, "test_file.txt");
        _fileManager = new FileManager();

        File.WriteAllText(_testFilePath, _testContent, Encoding.UTF8);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }

    [TestMethod]
    public void ViewFile_WhenFileExists_ReturnsFileContent()
    {
        string result = _fileManager.ViewFile(_testFilePath);
        Assert.AreEqual(_testContent, result);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void ViewFileWhenFileNotExistsThrowsFileNotFoundException()
    {
        string nonExistentFile = Path.Combine(_testDirectory, $"nonexistent_{Guid.NewGuid()}.txt");
        _fileManager.ViewFile(nonExistentFile);
    }

    [TestMethod]
    public void FindFileWhenFileExistsReturnsFilePath()
    {
        string testFileName = $"test_find_{Guid.NewGuid()}.txt";
        string testFileFullPath = Path.Combine(_testDirectory, testFileName);

        File.WriteAllText(testFileFullPath, "test content", Encoding.UTF8);
        string foundPath = _fileManager.FindFile(testFileName, _testDirectory);

        Assert.IsNotNull(foundPath);
        Assert.AreEqual(testFileFullPath, foundPath);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FindFileWhenFileNotExistsThrowsFileNotFoundException()
    {
        string nonExistentFile = $"nonexistent_{Guid.NewGuid()}.txt";
        _fileManager.FindFile(nonExistentFile, _testDirectory);
    }

    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException))]
    public void FindFileWhenDirectoryNotExistsThrowsDirectoryNotFoundException()
    {
        string nonExistentDir = Path.Combine(_testDirectory, $"nonexistent_dir_{Guid.NewGuid()}");
        _fileManager.FindFile("anyfile.txt", nonExistentDir);
    }

    [TestMethod]
    public void CompressFileWhenFileExistsCreatesCompressedFile()
    {
        _fileManager.CompressFile(_testFilePath);
        string compressedFilePath = _testFilePath + ".gz";

        Assert.IsTrue(File.Exists(compressedFilePath));
        FileInfo compressedFileInfo = new FileInfo(compressedFilePath);
        Assert.IsTrue(compressedFileInfo.Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void CompressFileWhenFileNotExistsHrowsFileNotFoundException()
    {
        string nonExistentFile = Path.Combine(_testDirectory, $"nonexistent_{Guid.NewGuid()}.txt");
        _fileManager.CompressFile(nonExistentFile);
    }

    [TestMethod]
    public void CompressFileWithCustomTargetPathCreatesFileAtSpecifiedPath()
    {
        string customTargetPath = Path.Combine(_testDirectory, $"compressed_{Guid.NewGuid()}.gz");
        _fileManager.CompressFile(_testFilePath, customTargetPath);
        Assert.IsTrue(File.Exists(customTargetPath));
    }

    [TestMethod]
    public void FindFileWithDefaultSearchPathUsesCrossPlatformDefault()
    {
        string testFileName = $"test_default_{Guid.NewGuid()}.txt";
        string testFileFullPath = Path.Combine(Environment.CurrentDirectory, testFileName);

        try
        {
            File.WriteAllText(testFileFullPath, "test content", Encoding.UTF8);
            string foundPath = _fileManager.FindFile(testFileName);

            Assert.IsNotNull(foundPath);
            StringAssert.EndsWith(foundPath, testFileName);
        }
        finally
        {
            if (File.Exists(testFileFullPath))
                File.Delete(testFileFullPath);
        }
    }
}

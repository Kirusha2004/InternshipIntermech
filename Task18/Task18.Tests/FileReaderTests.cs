namespace Task18.Tests;

[TestClass]
public class FileReaderTests
{
    [TestMethod]
    public void ReadFileExistingFileReturnsContent()
    {
        string testFile = "test_read.txt";
        string expectedContent = "Test content";
        File.WriteAllText(testFile, expectedContent);
        FileReader reader = new FileReader();

        string result = reader.ReadFile(testFile);

        Assert.AreEqual(expectedContent, result);

        File.Delete(testFile);
    }

    [TestMethod]
    public void ReadFileNonExistingFileReturnsEmptyString()
    {
        FileReader reader = new FileReader();

        string result = reader.ReadFile("nonexistent.txt");

        Assert.AreEqual(string.Empty, result);
    }
}

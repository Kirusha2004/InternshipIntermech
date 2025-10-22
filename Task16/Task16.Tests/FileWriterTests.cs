namespace Task16.Tests;

[TestClass]
public class FileWriterTests
{
    [TestMethod]
    public void WriteContentMultipleThreadsWritesWithoutCorruption()
    {
        string outputFile = "test_output.txt";
        FileWriter writer = new FileWriter(outputFile);

        Thread thread1 = new Thread(() => writer.WriteContent("file1.txt", "Content1"));
        Thread thread2 = new Thread(() => writer.WriteContent("file2.txt", "Content2"));

        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();

        string result = File.ReadAllText(outputFile);
        Assert.IsTrue(result.Contains("Content1"));
        Assert.IsTrue(result.Contains("Content2"));

        File.Delete(outputFile);
    }

    [TestMethod]
    public void WriteContentSingleWriteContainsSourceAndContent()
    {
        string outputFile = "test_single.txt";
        FileWriter writer = new FileWriter(outputFile);

        writer.WriteContent("source.txt", "test content");

        string result = File.ReadAllText(outputFile);
        Assert.IsTrue(result.Contains("source.txt"));
        Assert.IsTrue(result.Contains("test content"));

        File.Delete(outputFile);
    }
}

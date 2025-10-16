namespace Task17.Tests;

[TestClass]
public class FileProcessorTests
{
    [TestMethod]
    public void ProcessFileValidFileProcessesSuccessfully()
    {
        string inputFile = "test_input.txt";
        string outputFile = "test_processed.txt";
        string testContent = "test data";
        File.WriteAllText(inputFile, testContent);
        FileProcessor processor = new FileProcessor(outputFile, new FileCreator());

        processor.ProcessFile(inputFile);

        string result = File.ReadAllText(outputFile);
        Assert.IsTrue(result.Contains(inputFile));
        Assert.IsTrue(result.Contains(testContent));

        File.Delete(inputFile);
        File.Delete(outputFile);
    }

    [TestMethod]
    public void ProcessFileNonExistingFileNoExceptionThrown()
    {
        string outputFile = "test_nonexistent.txt";
        FileProcessor processor = new FileProcessor(outputFile, new FileCreator());

        try
        {
            processor.ProcessFile("nonexistent.txt");
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail("Exception was thrown for non-existing file");
        }

        File.Delete(outputFile);
    }

    [TestMethod]
    public void ProcessFileMultipleThreadsProcessWithoutConflict()
    {
        string file1 = "thread1.txt";
        string file2 = "thread2.txt";
        string outputFile = "thread_output.txt";
        File.WriteAllText(file1, "content1");
        File.WriteAllText(file2, "content2");
        FileProcessor processor = new FileProcessor(outputFile, new FileCreator());

        Thread thread1 = new Thread(() => processor.ProcessFile(file1));
        Thread thread2 = new Thread(() => processor.ProcessFile(file2));
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();

        string result = File.ReadAllText(outputFile);
        Assert.IsTrue(result.Contains("content1"));
        Assert.IsTrue(result.Contains("content2"));

        File.Delete(file1);
        File.Delete(file2);
        File.Delete(outputFile);
    }
}

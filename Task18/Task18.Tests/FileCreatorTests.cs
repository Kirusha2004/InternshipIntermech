namespace Task18.Tests;
[TestClass]
public class FileCreatorTests
{
    [TestMethod]
    public void CreateFileFilesDontExistCreatesFiles()
    {
        string file1 = "text1.txt";
        string file2 = "text2.txt";
        File.Delete(file1);
        File.Delete(file2);
        FileCreator creator = new FileCreator();

        creator.CreateFile();

        Assert.IsTrue(File.Exists(file1));
        Assert.IsTrue(File.Exists(file2));

        File.Delete(file1);
        File.Delete(file2);
    }

    [TestMethod]
    public void CreateFileFilesExistDoesNotOverwrite()
    {
        string file1 = "text1.txt";
        string file2 = "text2.txt";
        string originalContent = "original content";
        File.WriteAllText(file1, originalContent);
        File.WriteAllText(file2, originalContent);
        FileCreator creator = new FileCreator();

        creator.CreateFile();

        string content1 = File.ReadAllText(file1);
        string content2 = File.ReadAllText(file2);
        Assert.AreEqual(originalContent, content1);
        Assert.AreEqual(originalContent, content2);

        File.Delete(file1);
        File.Delete(file2);
    }
}

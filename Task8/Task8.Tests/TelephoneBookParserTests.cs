namespace Task8.Tests;

[TestClass]
public class TelephoneBookParserTests
{
    [TestMethod]
    public void TelephoneBookValidFileReturnsExpectedNumbers()
    {
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "TelephoneBook.xml"
        );
        TelephoneBookParser telephoneBook = new(filePath);

        List<string> result = [.. telephoneBook];
        List<string> expected = ["+375259527099", "+375259527088", "+375339521077"];

        CollectionAssert.AreEqual(expected, result);
    }
}

namespace Task8.Tests;

[TestClass]
public class TelephoneBookParserTests
{
    [TestMethod]
    public void ParseTelephoneNumbersValidFileReturnsExpectedNumbers()
    {
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "TelephoneBook.xml"
        );
        TelephoneBookParser telephoneBookParser = new TelephoneBookParser();

        IList<string> result = telephoneBookParser.ParseTelephoneNumbers(filePath);

        IList<string> expected = ["+375259527099", "+375259527088", "+375339521077"];
        Assert.IsTrue(expected.SequenceEqual(result));
    }
}

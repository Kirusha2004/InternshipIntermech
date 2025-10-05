namespace Task8.Tests;

[TestClass]
public class TelephoneBookParserTests
{
    [TestMethod]
    public void ParseTelephoneNumbersValidFileReturnsExpectedNumbers()
    {
        string filePath = @"..\..\..\..\TelephoneBook.xml";

        if (!File.Exists(filePath))
        {
            Assert.Inconclusive("Файл не найден по указанному пути");
            return;
        }

        var result = TelephoneBookParser.ParseTelephoneNumbers(filePath);

        List<string> expected = new List<string>
        {
            "+375259527099",
            "+375259527088",
            "+375339521077"
        };
        CollectionAssert.AreEqual(expected, result);
    }
}

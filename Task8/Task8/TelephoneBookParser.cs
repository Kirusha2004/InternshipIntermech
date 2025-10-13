using System.Xml.Linq;

namespace Task8;

public class TelephoneBookParser
{
    public IEnumerable<string> ParseTelephoneNumbers(string filePath)
    {
        XDocument xDoc = XDocument.Load(filePath);

        return [.. xDoc.Descendants("TelephoneNumber")
            .Select(node => node.Value.Trim())
            ];
    }
}

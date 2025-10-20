using System.Collections;
using System.Xml.Linq;

namespace Task8;

public class TelephoneBookParser : IEnumerable<string>
{
    private readonly XDocument _document;

    public TelephoneBookParser(string filePath)
    {
        _document = XDocument.Load(filePath);
    }

    public IEnumerator<string> GetEnumerator()
    {
        foreach (XElement node in _document.Descendants("TelephoneNumber"))
        {
            yield return node.Value.Trim();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

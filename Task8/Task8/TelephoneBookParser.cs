using System.Xml;

namespace Task8;

public class TelephoneBookParser
{
    public static List<string> ParseTelephoneNumbers(string filePath)
    {
        List<string> numbers = new List<string>();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(filePath);
        XmlElement xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            foreach (XmlElement xnode in xRoot)
            {
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "TelephoneNumber")
                    {
                        numbers.Add(childnode.InnerText);
                    }
                }
            }
        }
        return numbers;
    }
}

using System.Xml.Serialization;

namespace Task13;

[Serializable]
[XmlRoot("Person")]
public class PersonAsAttributes
{
    public PersonAsAttributes() { }

    public PersonAsAttributes(string name, int age)
    {
        Name = name;
        Age = age;
    }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Age")]
    public int Age { get; set; }
}

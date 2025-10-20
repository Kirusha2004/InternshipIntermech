using System.Xml.Serialization;

namespace Task13;

[Serializable]
[XmlRoot("Person")]
public class Person
{
    public Person() { }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    [XmlElement("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlElement("Age")]
    public int Age { get; set; }
}

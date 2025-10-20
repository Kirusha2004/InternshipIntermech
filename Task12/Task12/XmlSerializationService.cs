using System.Xml.Serialization;

namespace Task12;

public class XmlSerializationService : IXmlSerializationService
{
    public void SerializeToFile<T>(T obj, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        serializer.Serialize(fileStream, obj);
    }
}

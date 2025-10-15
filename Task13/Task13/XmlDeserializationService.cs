using System.Xml;
using System.Xml.Serialization;

namespace Task13;

public class XmlDeserializationService : IXmlDeserializationService
{
    public T? DeserializeFromFile<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        XmlReaderSettings settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };

        using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using XmlReader reader = XmlReader.Create(fileStream, settings);

        return (T?)serializer.Deserialize(reader);
    }
}

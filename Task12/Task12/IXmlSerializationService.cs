namespace Task12;

public interface IXmlSerializationService
{
    public void SerializeToFile<T>(T obj, string filePath);
}

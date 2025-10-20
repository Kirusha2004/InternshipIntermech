namespace Task13;

public interface IXmlDeserializationService
{
    public T? DeserializeFromFile<T>(string filePath);
}

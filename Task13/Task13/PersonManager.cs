namespace Task13;

public class PersonManager : IPersonManager
{
    private readonly IXmlDeserializationService _deserializationService;

    public PersonManager(IXmlDeserializationService deserializationService)
    {
        _deserializationService = deserializationService;
    }

    public Person DeserializePerson(string filePath)
    {
        return !File.Exists(filePath)
            ? throw new FileNotFoundException($"Файл не найден: {filePath}")
            : _deserializationService.DeserializeFromFile<Person>(filePath)
               ?? throw new InvalidOperationException("Десериализация вернула null");
    }

    public PersonAsAttributes DeserializePersonAsAttributes(string filePath)
    {
        return !File.Exists(filePath)
            ? throw new FileNotFoundException($"Файл не найден: {filePath}")
            : _deserializationService.DeserializeFromFile<PersonAsAttributes>(filePath)
               ?? throw new InvalidOperationException("Десериализация вернула null");
    }
}

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
        return _deserializationService.DeserializeFromFile<Person>(filePath)
               ?? throw new InvalidOperationException("Deserialization returned null");
    }

    public PersonAsAttributes DeserializePersonAsAttributes(string filePath)
    {
        return _deserializationService.DeserializeFromFile<PersonAsAttributes>(filePath)
               ?? throw new InvalidOperationException("Deserialization returned null");
    }
}

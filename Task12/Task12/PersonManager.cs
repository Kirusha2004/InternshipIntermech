namespace Task12;

public class PersonManager : IPersonManager
{
    private readonly IXmlSerializationService _serializationService;

    public PersonManager(IXmlSerializationService serializationService)
    {
        _serializationService = serializationService;
    }

    public void SerializePerson(Person person, string filePath)
    {
        _serializationService.SerializeToFile(person, filePath);
    }

    public void SerializePersonAsAttributes(PersonAsAttributes person, string filePath)
    {
        _serializationService.SerializeToFile(person, filePath);
    }
}

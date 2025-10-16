namespace Task12;

public interface IPersonManager
{
    public void SerializePerson(Person person, string filePath);
    public void SerializePersonAsAttributes(PersonAsAttributes person, string filePath);
}

namespace Task13;

public interface IPersonManager
{
    public Person DeserializePerson(string filePath);
    public PersonAsAttributes DeserializePersonAsAttributes(string filePath);
}

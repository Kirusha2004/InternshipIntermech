namespace Task12.Tests;

[TestClass]
public class PersonSerializationTests
{
    private IXmlSerializationService _serializationService;
    private IPersonManager _personManager;
    private string _testFilePath;

    [TestInitialize]
    public void TestInitialize()
    {
        _serializationService = new XmlSerializationService();
        _personManager = new PersonManager(_serializationService);
        _testFilePath = Path.GetTempFileName();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [TestMethod]
    public void SerializePersonShouldCreateFile()
    {
        Person person = new Person("Kirill", 20);

        _personManager.SerializePerson(person, _testFilePath);

        Assert.IsTrue(File.Exists(_testFilePath));
        Assert.IsTrue(new FileInfo(_testFilePath).Length > 0);
    }

    [TestMethod]
    public void SerializePersonAsAttributesShouldCreateFile()
    {
        PersonAsAttributes person = new PersonAsAttributes("Kirill", 20);

        _personManager.SerializePersonAsAttributes(person, _testFilePath);

        Assert.IsTrue(File.Exists(_testFilePath));
        Assert.IsTrue(new FileInfo(_testFilePath).Length > 0);
    }

    [TestMethod]
    public void SerializeToFileShouldCreateFile()
    {
        Person person = new Person("Test", 25);

        _serializationService.SerializeToFile(person, _testFilePath);

        Assert.IsTrue(File.Exists(_testFilePath));
        Assert.IsTrue(new FileInfo(_testFilePath).Length > 0);
    }

    [TestMethod]
    public void PersonConstructorShouldSetProperties()
    {
        Person person = new Person("John", 35);

        Assert.AreEqual("John", person.Name);
        Assert.AreEqual(35, person.Age);
    }

    [TestMethod]
    public void PersonAsAttributesConstructorShouldSetProperties()
    {
        PersonAsAttributes person = new PersonAsAttributes("Jane", 28);

        Assert.AreEqual("Jane", person.Name);
        Assert.AreEqual(28, person.Age);
    }
}

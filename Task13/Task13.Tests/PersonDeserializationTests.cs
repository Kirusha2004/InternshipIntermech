namespace Task13.Tests;

[TestClass]
public class PersonDeserializationTests
{
    private IXmlDeserializationService _deserializationService;
    private IPersonManager _personManager;
    private string _testFilePath;

    [TestInitialize]
    public void TestInitialize()
    {
        _deserializationService = new XmlDeserializationService();
        _personManager = new PersonManager(_deserializationService);
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
    public void DeserializePersonShouldReturnCorrectObject()
    {
        string xmlContent = @"<?xml version='1.0' encoding='utf-8'?>
<Person>
    <Name>Anna</Name>
    <Age>30</Age>
</Person>";
        File.WriteAllText(_testFilePath, xmlContent);

        Person deserializedPerson = _personManager.DeserializePerson(_testFilePath);

        Assert.IsNotNull(deserializedPerson);
        Assert.AreEqual("Anna", deserializedPerson.Name);
        Assert.AreEqual(30, deserializedPerson.Age);
    }

    [TestMethod]
    public void DeserializePersonAsAttributesShouldReturnCorrectObject()
    {
        string xmlContent = @"<?xml version='1.0' encoding='utf-8'?>
<Person Name='Kirill' Age='20' />";
        File.WriteAllText(_testFilePath, xmlContent);

        PersonAsAttributes deserializedPerson = _personManager.DeserializePersonAsAttributes(_testFilePath);

        Assert.IsNotNull(deserializedPerson);
        Assert.AreEqual("Kirill", deserializedPerson.Name);
        Assert.AreEqual(20, deserializedPerson.Age);
    }

    [TestMethod]
    public void DeserializeFromFileShouldReturnCorrectObject()
    {
        string xmlContent = @"<?xml version='1.0' encoding='utf-8'?>
<Person>
    <Name>John</Name>
    <Age>25</Age>
</Person>";
        File.WriteAllText(_testFilePath, xmlContent);

        Person? deserializedPerson = _deserializationService.DeserializeFromFile<Person>(_testFilePath);

        Assert.IsNotNull(deserializedPerson);
        Assert.AreEqual("John", deserializedPerson.Name);
        Assert.AreEqual(25, deserializedPerson.Age);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DeserializePersonShouldThrowWhenFileNotFound()
    {
        _ = _personManager.DeserializePerson("nonexistent.xml");
    }
}

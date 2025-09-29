namespace Task2.Tests;

[TestClass]
public class CitizenCollectionTests
{
    [TestMethod]
    public void AddStudentReturnsCorrectPosition()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");

        int position = collection.Add(student);

        Assert.AreEqual(1, position);
        Assert.AreEqual(1, collection.Count);
    }

    [TestMethod]
    public void AddPensionerInsertsAtBeginning()
    {
        CitizenCollection collection = [];
        Pensioner pensioner = new("CD654321", "Петр Петров", 15000m);

        int position = collection.Add(pensioner);

        Assert.AreEqual(1, position);
    }

    [TestMethod]
    public void PensionersAreGroupedTogether()
    {
        CitizenCollection collection = [];
        Pensioner pensioner1 = new("CD654321", "Петр Петров", 15000m);
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        Pensioner pensioner2 = new("EF789012", "Сидор Сидоров", 12000m);

        _ = collection.Add(pensioner1);
        _ = collection.Add(student);
        int position = collection.Add(pensioner2);

        Assert.AreEqual(2, position);
        Assert.IsTrue(collection.Take(2).All(c => c is Pensioner));
    }

    [TestMethod]
    public void AddDuplicateCitizenReturnsMinusOne()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");

        int firstAdd = collection.Add(student);
        int secondAdd = collection.Add(student);

        Assert.AreEqual(1, firstAdd);
        Assert.AreEqual(-1, secondAdd);
        Assert.AreEqual(1, collection.Count);
    }

    [TestMethod]
    public void RemoveFromBeginningReturnsFirstCitizen()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        Worker worker = new("CD654321", "Петр Петров", "Инженер");

        _ = collection.Add(student);
        _ = collection.Add(worker);

        Citizen removed = collection.Remove();

        Assert.AreEqual(student, removed);
        Assert.AreEqual(1, collection.Count);
    }

    [TestMethod]
    public void RemoveSpecificCitizenReturnsTrue()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        _ = collection.Add(student);

        bool removed = collection.Remove(student);

        Assert.IsTrue(removed);
        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void ContainsReturnsCorrectResult()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        _ = collection.Add(student);

        (bool exists, int position) = collection.Contains(student);

        Assert.IsTrue(exists);
        Assert.AreEqual(1, position);
    }

    [TestMethod]
    public void ReturnLastReturnsLastCitizenAndPosition()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        Worker worker = new("CD654321", "Петр Петров", "Инженер");

        _ = collection.Add(student);
        _ = collection.Add(worker);

        (Citizen? lastCitizen, int position) = collection.ReturnLast();

        Assert.AreEqual(worker, lastCitizen);
        Assert.AreEqual(2, position);
    }

    [TestMethod]
    public void ClearRemovesAllCitizens()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        _ = collection.Add(student);

        collection.Clear();

        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void CollectionSupportsForEach()
    {
        CitizenCollection collection = [];
        Student student = new("AB123456", "Иван Иванов", "МГУ");
        Worker worker = new("CD654321", "Петр Петров", "Инженер");

        _ = collection.Add(student);
        _ = collection.Add(worker);

        int count = 0;
        foreach (Citizen citizen in collection)
        {
            count++;
            Assert.IsNotNull(citizen);
        }

        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public void CitizenEqualsComparesByPassportNumber()
    {
        Student student1 = new("AB123456", "Иван Иванов", "МГУ");
        Student student2 = new("AB123456", "Петр Петров", "СПбГУ");

        Assert.AreEqual(student1, student2);
    }

    [TestMethod]
    public void DifferentPassportNumbersAreNotEqual()
    {
        Student student1 = new("AB123456", "Иван Иванов", "МГУ");
        Student student2 = new("CD654321", "Иван Иванов", "МГУ");

        Assert.AreNotEqual(student1, student2);
    }

    [TestMethod]
    public void AddNullCitizenThrowsException()
    {
        CitizenCollection collection = [];
        _ = Assert.ThrowsException<ArgumentNullException>(() => collection.Add(null!));
    }

    [TestMethod]
    public void EmptyCollectionReturnLastReturnsNull()
    {
        CitizenCollection collection = [];

        (Citizen? lastCitizen, int position) = collection.ReturnLast();

        Assert.IsNull(lastCitizen);
        Assert.AreEqual(-1, position);
    }

    [TestMethod]
    public void ComplexScenarioWithAllCitizenTypes()
    {
        CitizenCollection collection = [];

        Pensioner pensioner1 = new("P001", "Пенсионер 1", 10000m);
        Student student1 = new("S001", "Студент 1", "Универ 1");
        Worker worker1 = new("W001", "Рабочий 1", "Должность 1");
        Pensioner pensioner2 = new("P002", "Пенсионер 2", 12000m);
        Student student2 = new("S002", "Студент 2", "Универ 2");

        _ = collection.Add(pensioner1);
        _ = collection.Add(student1);
        _ = collection.Add(worker1);
        _ = collection.Add(pensioner2);
        _ = collection.Add(student2);

        Assert.AreEqual(5, collection.Count);

        List<Citizen> citizens = [.. collection];
        Assert.IsTrue(citizens[0] is Pensioner);
        Assert.IsTrue(citizens[1] is Pensioner);
        Assert.IsTrue(citizens[2] is Student);
        Assert.IsTrue(citizens[3] is Worker);
        Assert.IsTrue(citizens[4] is Student);
    }
}

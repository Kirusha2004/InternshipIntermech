using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CitizenCollectionTests
{
    [TestMethod]
    public void Add_Student_ReturnsCorrectPosition()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "12345" };

        var position = collection.Add(student);

        Assert.AreEqual(1, position);
    }

    [TestMethod]
    public void Add_Pensioner_AddedBeforeOtherTypes()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var pensioner = new Pensioner { NumberOfPassport = "22222" };

        collection.Add(student);
        var pensionerPosition = collection.Add(pensioner);

        Assert.AreEqual(1, pensionerPosition);
    }

    [TestMethod]
    public void Add_DuplicatePassport_ReturnsMinusOne()
    {
        var collection = new CitizenCollection();
        var student1 = new Student { NumberOfPassport = "12345" };
        var student2 = new Student { NumberOfPassport = "12345" };

        collection.Add(student1);
        var result = collection.Add(student2);

        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void Contains_ExistingCitizen_ReturnsTrueAndPosition()
    {
        var collection = new CitizenCollection();
        var worker = new Worker { NumberOfPassport = "33333" };
        collection.Add(worker);

        var result = collection.Contains(worker);

        Assert.IsTrue(result.exists);
        Assert.AreEqual(1, result.index);
    }

    [TestMethod]
    public void ReturnLast_WithElements_ReturnsLastElement()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var worker = new Worker { NumberOfPassport = "22222" };
        collection.Add(student);
        collection.Add(worker);

        var result = collection.ReturnLast();

        Assert.AreEqual(worker, result.citizen);
        Assert.AreEqual(2, result.index);
    }

    [TestMethod]
    public void Remove_FromBeginning_RemovesFirstElement()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var worker = new Worker { NumberOfPassport = "22222" };
        collection.Add(student);
        collection.Add(worker);

        collection.Remove();

        var containsStudent = collection.Contains(student);
        Assert.IsFalse(containsStudent.exists);
    }

    [TestMethod]
    public void Clear_RemovesAllElements()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var worker = new Worker { NumberOfPassport = "22222" };
        collection.Add(student);
        collection.Add(worker);

        collection.Clear();

        var result = collection.ReturnLast();
        Assert.IsNull(result.citizen);
        Assert.AreEqual(0, result.index);
    }

    [TestMethod]
    public void IEnumerable_ForeachWorksCorrectly()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var worker = new Worker { NumberOfPassport = "22222" };
        collection.Add(student);
        collection.Add(worker);

        int count = 0;
        foreach (var citizen in collection)
        {
            count++;
            Assert.IsNotNull(citizen);
        }

        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public void Pensioners_AreGroupedTogether()
    {
        var collection = new CitizenCollection();
        var student = new Student { NumberOfPassport = "11111" };
        var pensioner1 = new Pensioner { NumberOfPassport = "22222" };
        var worker = new Worker { NumberOfPassport = "33333" };
        var pensioner2 = new Pensioner { NumberOfPassport = "44444" };

        collection.Add(student);
        var pos1 = collection.Add(pensioner1);
        collection.Add(worker);
        var pos2 = collection.Add(pensioner2);

        Assert.AreEqual(1, pos1);
        Assert.AreEqual(2, pos2);
    }
}
namespace Task5.Tests;

[TestClass]
public class OrderedDictionaryTests
{
    [TestMethod]
    public void TestAddAndGet()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);

        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual(1, dict["one"]);
        Assert.AreEqual(2, dict["two"]);
    }

    [TestMethod]
    public void TestIndexerSetGet()
    {
        var dict = new OrderedDictionary<string, string>();
        dict["first"] = "value1";
        dict["second"] = "value2";

        Assert.AreEqual("value1", dict["first"]);
        Assert.AreEqual("value2", dict["second"]);
        Assert.AreEqual(2, dict.Count);
    }

    [TestMethod]
    public void TestIndexerUpdate()
    {
        var dict = new OrderedDictionary<string, int>();
        dict["key"] = 1;
        dict["key"] = 2;

        Assert.AreEqual(1, dict.Count);
        Assert.AreEqual(2, dict["key"]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);
        dict.Add("three", 3);

        bool removed = dict.Remove("two");

        Assert.IsTrue(removed);
        Assert.AreEqual(2, dict.Count);
        Assert.IsFalse(dict.ContainsKey("two"));
        Assert.AreEqual(0, dict.IndexOf("one"));
        Assert.AreEqual(1, dict.IndexOf("three"));
    }

    [TestMethod]
    public void TestRemoveNonExistent()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);

        bool removed = dict.Remove("two");

        Assert.IsFalse(removed);
        Assert.AreEqual(1, dict.Count);
    }

    [TestMethod]
    public void TestContainsKey()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("test", 123);

        Assert.IsTrue(dict.ContainsKey("test"));
        Assert.IsFalse(dict.ContainsKey("nonexistent"));
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("exists", 42);

        bool found = dict.TryGetValue("exists", out int value);

        Assert.IsTrue(found);
        Assert.AreEqual(42, value);

        found = dict.TryGetValue("nonexistent", out value);
        Assert.IsFalse(found);
        Assert.AreEqual(0, value);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("first", 1);
        dict.Add("second", 2);
        dict.Add("third", 3);

        Assert.AreEqual(0, dict.IndexOf("first"));
        Assert.AreEqual(1, dict.IndexOf("second"));
        Assert.AreEqual(2, dict.IndexOf("third"));
        Assert.AreEqual(-1, dict.IndexOf("nonexistent"));
    }

    [TestMethod]
    public void TestClear()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);

        dict.Clear();

        Assert.AreEqual(0, dict.Count);
        Assert.IsFalse(dict.ContainsKey("one"));
        Assert.IsFalse(dict.ContainsKey("two"));
    }

    [TestMethod]
    public void TestEnumeration()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);
        dict.Add("three", 3);

        var items = dict.ToList();

        Assert.AreEqual(3, items.Count);
        Assert.AreEqual("one", items[0].Key);
        Assert.AreEqual(1, items[0].Value);
        Assert.AreEqual("two", items[1].Key);
        Assert.AreEqual(2, items[1].Value);
        Assert.AreEqual("three", items[2].Key);
        Assert.AreEqual(3, items[2].Value);
    }

    [TestMethod]
    public void TestCustomComparer()
    {
        var dict = new OrderedDictionary<string, int>(new MyEqualityComparer<string>());
        dict.Add("ONE", 1);

        Assert.AreEqual(1, dict["one"]);
        Assert.AreEqual(1, dict["One"]);
        Assert.AreEqual(1, dict["ONE"]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAddDuplicateKey()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("key", 1);
        dict.Add("key", 2);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void TestGetNonExistentKey()
    {
        var dict = new OrderedDictionary<string, int>();
        var value = dict["nonexistent"];
    }

    [TestMethod]
    public void TestIndexAccess()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);
        dict.Add("three", 3);

        Assert.AreEqual("one", dict[0].Key);
        Assert.AreEqual(1, dict[0].Value);
        Assert.AreEqual("two", dict[1].Key);
        Assert.AreEqual(2, dict[1].Value);
        Assert.AreEqual("three", dict[2].Key);
        Assert.AreEqual(3, dict[2].Value);
    }

    [TestMethod]
    public void TestOrderPreservation()
    {
        var dict = new OrderedDictionary<int, string>();
        dict.Add(3, "three");
        dict.Add(1, "one");
        dict.Add(2, "two");

        var keys = dict.Select(kvp => kvp.Key).ToList();
        var values = dict.Select(kvp => kvp.Value).ToList();

        CollectionAssert.AreEqual(new[] { 3, 1, 2 }, keys);
        CollectionAssert.AreEqual(new[] { "three", "one", "two" }, values);
    }

    [TestMethod]
    public void TestComplexObjectKey()
    {
        var dict = new OrderedDictionary<Person, string>();
        var person1 = new Person { Id = 1, Name = "John" };
        var person2 = new Person { Id = 1, Name = "John" };

        dict.Add(person1, "Developer");

        Assert.ThrowsException<KeyNotFoundException>(() => dict[person2]);
    }

    [TestMethod]
    public void TestIDictionaryInterface()
    {
        IDictionary<string, int> dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add(new KeyValuePair<string, int>("two", 2));

        Assert.AreEqual(2, dict.Count);
        Assert.IsTrue(dict.Contains(new KeyValuePair<string, int>("one", 1)));
        Assert.IsFalse(dict.Contains(new KeyValuePair<string, int>("one", 999)));

        var keys = dict.Keys.ToList();
        var values = dict.Values.ToList();

        CollectionAssert.AreEqual(new[] { "one", "two" }, keys);
        CollectionAssert.AreEqual(new[] { 1, 2 }, values);
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);
        dict.Add("three", 3);

        var array = new KeyValuePair<string, int>[5];
        dict.CopyTo(array, 1);

        Assert.AreEqual(default(KeyValuePair<string, int>), array[0]);
        Assert.AreEqual("one", array[1].Key);
        Assert.AreEqual(1, array[1].Value);
        Assert.AreEqual("two", array[2].Key);
        Assert.AreEqual(2, array[2].Value);
        Assert.AreEqual("three", array[3].Key);
        Assert.AreEqual(3, array[3].Value);
        Assert.AreEqual(default(KeyValuePair<string, int>), array[4]);
    }

    [TestMethod]
    public void TestRemoveByKeyValuePair()
    {
        var dict = new OrderedDictionary<string, int>();
        dict.Add("one", 1);
        dict.Add("two", 2);

        var removed = dict.Remove(new KeyValuePair<string, int>("one", 1));
        Assert.IsTrue(removed);
        Assert.AreEqual(1, dict.Count);
        Assert.IsFalse(dict.ContainsKey("one"));

        removed = dict.Remove(new KeyValuePair<string, int>("two", 999));
        Assert.IsFalse(removed);
        Assert.AreEqual(1, dict.Count);
    }
}

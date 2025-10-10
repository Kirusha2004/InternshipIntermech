namespace Task5.Tests;

[TestClass]
public class OrderedDictionaryTests
{
    [TestMethod]
    public void TestAddAndGet()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
        };

        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual(1, dict["one"]);
        Assert.AreEqual(2, dict["two"]);
    }

    [TestMethod]
    public void TestIndexerSetGet()
    {
        OrderedDictionary<string, string> dict = new OrderedDictionary<string, string>
        {
            ["first"] = "value1",
            ["second"] = "value2",
        };

        Assert.AreEqual("value1", dict["first"]);
        Assert.AreEqual("value2", dict["second"]);
        Assert.AreEqual(2, dict.Count);
    }

    [TestMethod]
    public void TestIndexerUpdate()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            ["key"] = 2,
        };

        Assert.AreEqual(1, dict.Count);
        Assert.AreEqual(2, dict["key"]);
    }

    [TestMethod]
    public void TestRemove()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };

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
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
        };

        bool removed = dict.Remove("two");

        Assert.IsFalse(removed);
        Assert.AreEqual(1, dict.Count);
    }

    [TestMethod]
    public void TestContainsKey()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "test", 123 },
        };

        Assert.IsTrue(dict.ContainsKey("test"));
        Assert.IsFalse(dict.ContainsKey("nonexistent"));
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "exists", 42 },
        };

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
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "first", 1 },
            { "second", 2 },
            { "third", 3 },
        };

        Assert.AreEqual(0, dict.IndexOf("first"));
        Assert.AreEqual(1, dict.IndexOf("second"));
        Assert.AreEqual(2, dict.IndexOf("third"));
        Assert.AreEqual(-1, dict.IndexOf("nonexistent"));
    }

    [TestMethod]
    public void TestClear()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
        };

        dict.Clear();

        Assert.AreEqual(0, dict.Count);
        Assert.IsFalse(dict.ContainsKey("one"));
        Assert.IsFalse(dict.ContainsKey("two"));
    }

    [TestMethod]
    public void TestEnumeration()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };

        List<KeyValuePair<string, int>> items = [.. dict];

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
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>(
            new MyEqualityComparer<string>()
        )
        {
            { "ONE", 1 },
        };

        Assert.AreEqual(1, dict["one"]);
        Assert.AreEqual(1, dict["One"]);
        Assert.AreEqual(1, dict["ONE"]);
    }

    [TestMethod]
    public void TestAddDuplicateKey()
    {
        _ = Assert.ThrowsException<ArgumentException>(() =>
            _ = new OrderedDictionary<string, int> { { "key", 1 }, { "key", 2 } }
        );
    }

    [TestMethod]
    public void TestGetNonExistentKey()
    {
        OrderedDictionary<string, int> dict = [];

        _ = Assert.ThrowsException<KeyNotFoundException>(() => _ = dict["nonexistent"]);
    }

    [TestMethod]
    public void TestIndexAccess()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };

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
        OrderedDictionary<int, string> dict = new OrderedDictionary<int, string>
        {
            { 3, "three" },
            { 1, "one" },
            { 2, "two" },
        };

        List<int> keys = dict.Select(kvp => kvp.Key).ToList();
        List<string> values = dict.Select(kvp => kvp.Value).ToList();

        int[] expectedKeys = [3, 1, 2];
        string[] expectedValues = ["three", "one", "two"];

        CollectionAssert.AreEqual(expectedKeys, keys);
        CollectionAssert.AreEqual(expectedValues, values);
    }

    [TestMethod]
    public void TestComplexObjectKey()
    {
        OrderedDictionary<Person, string> dict = [];
        Person person1 = new Person { Id = 1, Name = "John" };
        Person person2 = new Person { Id = 1, Name = "John" };

        dict.Add(person1, "Developer");

        _ = Assert.ThrowsException<KeyNotFoundException>(() => dict[person2]);
    }

    [TestMethod]
    public void TestIDictionaryInterface()
    {
        IDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            new KeyValuePair<string, int>("two", 2),
        };

        Assert.AreEqual(2, dict.Count);
        Assert.IsTrue(dict.Contains(new KeyValuePair<string, int>("one", 1)));
        Assert.IsFalse(dict.Contains(new KeyValuePair<string, int>("one", 999)));

        List<string> keys = [.. dict.Keys];
        List<int> values = [.. dict.Values];

        string[] expectedKeysArray = ["one", "two"];
        int[] expectedValuesArray = [1, 2];

        CollectionAssert.AreEqual(expectedKeysArray, keys);
        CollectionAssert.AreEqual(expectedValuesArray, values);
    }

    [TestMethod]
    public void TestCopyTo()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };

        KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[5];
        dict.CopyTo(array, 1);

        Assert.AreEqual(default, array[0]);
        Assert.AreEqual("one", array[1].Key);
        Assert.AreEqual(1, array[1].Value);
        Assert.AreEqual("two", array[2].Key);
        Assert.AreEqual(2, array[2].Value);
        Assert.AreEqual("three", array[3].Key);
        Assert.AreEqual(3, array[3].Value);
        Assert.AreEqual(default, array[4]);
    }

    [TestMethod]
    public void TestRemoveByKeyValuePair()
    {
        OrderedDictionary<string, int> dict = new OrderedDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
        };

        bool removed = dict.Remove(new KeyValuePair<string, int>("one", 1));
        Assert.IsTrue(removed);
        Assert.AreEqual(1, dict.Count);
        Assert.IsFalse(dict.ContainsKey("one"));

        removed = dict.Remove(new KeyValuePair<string, int>("two", 999));
        Assert.IsFalse(removed);
        Assert.AreEqual(1, dict.Count);
    }
}

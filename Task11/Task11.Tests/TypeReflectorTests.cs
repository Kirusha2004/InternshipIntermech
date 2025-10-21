namespace Task11.Tests;

[TestClass]
public class TypeReflectorTests
{
    [TestMethod]
    [Obsolete("Тест устарел, будет заменен новым в следующей версии")]
    public void TestMethodsAndPropertiesWithAttributes()
    {
        TypeReflector reflector = new TypeReflector(typeof(SampleClass), MemberTypesToShow.Methods | MemberTypesToShow.Properties);

        string info = reflector.GetReflectionInfo();

        StringAssert.Contains(info, "Тип: SampleClass");
        StringAssert.Contains(info, "Атрибуты типа:");
        StringAssert.Contains(info, "- ObsoleteAttribute");
        StringAssert.Contains(info, "Методы:");
        StringAssert.Contains(info, "- MyMethod");
        StringAssert.Contains(info, "Атрибут: ObsoleteAttribute");
        StringAssert.Contains(info, "- IsPropertyInRange");
        StringAssert.Contains(info, "Свойства:");
        StringAssert.Contains(info, "- MyProperty (Int32)");
        StringAssert.Contains(info, "Атрибут: ObsoleteAttribute");
    }

    [TestMethod]
    [Obsolete("Тест устарел, будет заменен новым в следующей версии")]
    public void TestOnlyMethods()
    {
        TypeReflector reflector = new TypeReflector(typeof(SampleClass), MemberTypesToShow.Methods);

        string info = reflector.GetReflectionInfo();

        StringAssert.Contains(info, "Методы:");
        StringAssert.Contains(info, "- MyMethod");
        StringAssert.Contains(info, "- IsPropertyInRange");
        Assert.IsFalse(info.Contains("Свойства:"), "Свойства не должны отображаться");
    }

    [TestMethod]
    [Obsolete("Тест устарел, будет заменен новым в следующей версии")]
    public void TestSampleClassBehavior()
    {
        SampleClass sample = new SampleClass
        {
            MyProperty = 5
        };
        sample.MyMethod();
        bool inRange = sample.IsPropertyInRange(10, 20);

        Assert.AreEqual(15, sample.MyProperty);
        Assert.IsTrue(inRange);
    }

    [TestMethod]
    [Obsolete("Тест устарел, будет заменен новым в следующей версии")]
    [ExpectedException(typeof(ArgumentException))]
    public void TestPropertyValidation()
    {
        _ = new SampleClass
        {
            MyProperty = -1
        };
    }
}

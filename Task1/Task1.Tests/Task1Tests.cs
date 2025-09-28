using UserCollections1;

namespace UserCollections.Tests1;

[TestClass]
public class Task1Tests
{
    private static List<Month> CreateTestMonths()
    {
        return
        [
            new Month(1, "Январь", 31),
            new Month(2, "Февраль", 28),
            new Month(3, "Март", 31),
            new Month(4, "Апрель", 30),
            new Month(5, "Май", 31),
            new Month(6, "Июнь", 30),
            new Month(7, "Июль", 31),
            new Month(8, "Август", 31),
            new Month(9, "Сентябрь", 30),
            new Month(10, "Октябрь", 31),
            new Month(11, "Ноябрь", 30),
            new Month(12, "Декабрь", 31),
        ];
    }

    [TestMethod]
    public void MonthByNumberReturnsCorrectMonth()
    {
        List<Month> list = CreateTestMonths();
        List<Month> result = [.. list.Where(m => m.Number == 5)];

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Май", result[0].Name);
        Assert.AreEqual(31, result[0].Days);
    }

    [TestMethod]
    public void MonthsWith31DaysReturns7Months()
    {
        List<Month> list = CreateTestMonths();
        List<Month> result = [.. list.Where(m => m.Days == 31)];

        Assert.AreEqual(7, result.Count);
        Assert.IsTrue(result.All(m => m.Days == 31));
    }

    [TestMethod]
    public void MonthsAfterJuneWith30DaysReturns2Months()
    {
        List<Month> list = CreateTestMonths();
        List<Month> result = [.. list.Where(m => m.Number > 6 && m.Days == 30)];

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(m => m.Number > 6 && m.Days == 30));
    }

    [TestMethod]
    public void MonthClassCreatesCorrectly()
    {
        Month month = new Month(5, "Май", 31);

        Assert.AreEqual(5, month.Number);
        Assert.AreEqual("Май", month.Name);
        Assert.AreEqual(31, month.Days);
    }
}

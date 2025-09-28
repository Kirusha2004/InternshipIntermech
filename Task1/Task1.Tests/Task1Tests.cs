using UserCollections1;

namespace UserCollections.Tests1;

[TestClass]
public class Task1Tests
{
    private static MonthsInYear CreateTestMonths()
    {
        return new MonthsInYear(2023);
    }

    [TestMethod]
    public void MonthByNumberReturnsCorrectMonth()
    {
        MonthsInYear months = CreateTestMonths();
        Month result = months.Single(m => m.Number == 5);

        Assert.AreEqual(5, result.Number);
        Assert.AreEqual("Май", result.Name);
        Assert.AreEqual(31, result.Days);
    }

    [TestMethod]
    public void MonthsWith31DaysReturns7Months()
    {
        MonthsInYear months = CreateTestMonths();
        List<Month> result = [.. months.Where(m => m.Days == 31)];

        Assert.AreEqual(7, result.Count);
        Assert.IsTrue(result.All(m => m.Days == 31));
    }

    [TestMethod]
    public void MonthsAfterJuneWith30DaysReturns2Months()
    {
        MonthsInYear months = CreateTestMonths();
        List<Month> result = [.. months.Where(m => m.Number > 6 && m.Days == 30)];

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

    [TestMethod]
    public void MonthsInYearContains12Months()
    {
        MonthsInYear months = new MonthsInYear(2023);
        int count = months.Count();

        Assert.AreEqual(12, count);
    }

    [TestMethod]
    public void MonthsInYearCurrentYearConstructorWorks()
    {
        MonthsInYear months = new MonthsInYear();

        Assert.AreEqual(12, months.Count());
        Assert.IsTrue(months.All(m => m.Number is >= 1 and <= 12));
    }

    [TestMethod]
    public void FebruaryHasCorrectDaysInLeapYear()
    {
        MonthsInYear leapYearMonths = new MonthsInYear(2020);
        Month february = leapYearMonths.Single(m => m.Number == 2);

        Assert.AreEqual(29, february.Days);
    }

    [TestMethod]
    public void FebruaryHasCorrectDaysInNonLeapYear()
    {
        MonthsInYear nonLeapYearMonths = new MonthsInYear(2023);
        Month february = nonLeapYearMonths.Single(m => m.Number == 2);

        Assert.AreEqual(28, february.Days);
    }

    [TestMethod]
    public void MonthsInYearImplementsIEnumerableCorrectly()
    {
        MonthsInYear months = new MonthsInYear(2023);
        IEnumerator<Month> enumerator = months.GetEnumerator();
        int count = 0;

        while (enumerator.MoveNext())
        {
            count++;
            Assert.IsNotNull(enumerator.Current);
        }

        Assert.AreEqual(12, count);
    }

    [TestMethod]
    public void AllMonthsHaveCorrectNames()
    {
        MonthsInYear months = new MonthsInYear(2023);
        string[] expectedNames =
        [
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        ];

        string[] actualNames = [.. months.Select(m => m.Name)];

        CollectionAssert.AreEqual(expectedNames, actualNames);
    }
}

namespace Lesson2Task1.Tests;

[TestClass]
public class AccountCollectionsTests
{
    private readonly Dictionary<int, double> _accountBalanceDict;
    private readonly SortedDictionary<int, double> _sortedAccountBalance;
    private readonly SortedList<int, double> _sortedAccountList;

    public AccountCollectionsTests()
    {
        _accountBalanceDict = new Dictionary<int, double>
    {
        { 1001, 15000.75 },
        { 1002, 23450.25 },
        { 1003, 5000.00 },
        { 1004, 18765.40 },
        { 1005, 32000.00 }
    };

        _sortedAccountBalance = new SortedDictionary<int, double>(_accountBalanceDict);
        _sortedAccountList = new SortedList<int, double>(_accountBalanceDict);
    }

    [TestMethod]
    public void HighBalanceAccountsWithCondition20000ReturnsEmpty()
    {
        var highBalanceAccounts = _accountBalanceDict.Where(n => n.Value > 20.000);

        Assert.AreEqual(5, highBalanceAccounts.Count());
    }

    [TestMethod]
    public void TopAccountIdsByBalanceReturnsCorrectOrder()
    {
        var topAccountIds = _accountBalanceDict.OrderByDescending(n => n.Value).Select(n => n.Key).ToList();

        Assert.AreEqual(1005, topAccountIds[0]);
        Assert.AreEqual(1002, topAccountIds[1]);
        Assert.AreEqual(1004, topAccountIds[2]);
        Assert.AreEqual(1001, topAccountIds[3]);
        Assert.AreEqual(1003, topAccountIds[4]);
    }

    [TestMethod]
    public void TotalBalanceReturnsCorrectSum()
    {
        var total = _accountBalanceDict.Sum(n => n.Value);

        Assert.AreEqual(94216.4, total, 0.01);
    }

    [TestMethod]
    public void MaximumBalanceReturnsCorrectValue()
    {
        var max = _accountBalanceDict.Max(n => n.Value);

        Assert.AreEqual(32000.00, max);
    }

    [TestMethod]
    public void FirstThreeAccountsWithOrderByOnSortedDictionaryReturnsFirstThree()
    {
        var firstThree = _sortedAccountBalance.OrderBy(n => n.Key).Take(3);

        Assert.AreEqual(3, firstThree.Count());
        var keys = firstThree.Select(x => x.Key).ToList();
        CollectionAssert.AreEqual(new[] { 1001, 1002, 1003 }, keys);
    }

    [TestMethod]
    public void HasAccount1006ReturnsFalse()
    {
        var exists = _sortedAccountBalance.ContainsKey(1006);

        Assert.IsFalse(exists);
    }

    [TestMethod]
    public void BalancesInRangeKeyBetween1002And1004ReturnsOnly1003()
    {
        var balances = _sortedAccountBalance.Where(n => n.Key > 1002 && n.Key < 1004).Select(n => n.Value);

        Assert.AreEqual(1, balances.Count());
        Assert.AreEqual(5000.00, balances.Single());
    }

    [TestMethod]
    public void AverageAccountBalanceReturnsCorrectValue()
    {
        var average = _sortedAccountList.Average(n => n.Value);

        Assert.AreEqual(18843.28, average, 0.01);
    }

    [TestMethod]
    public void AccountsWithExactThousandsReturns5000And32000()
    {
        var exactThousands = _sortedAccountList.Where(n => n.Value % 1000 == 0);

        Assert.AreEqual(2, exactThousands.Count());
        Assert.IsTrue(exactThousands.Any(a => a.Key == 1003 && a.Value == 5000.00));
        Assert.IsTrue(exactThousands.Any(a => a.Key == 1005 && a.Value == 32000.00));
    }

    [TestMethod]
    public void BalancesInEurosReturnsCorrectConversion()
    {
        var euros = _sortedAccountList.ToDictionary(n => n.Key, n => n.Value * 0.85);

        Assert.AreEqual(5, euros.Count);
        Assert.AreEqual(12750.6375, euros[1001], 0.001);
        Assert.AreEqual(19932.7125, euros[1002], 0.001);
        Assert.AreEqual(4250.00, euros[1003], 0.001);
        Assert.AreEqual(15950.59, euros[1004], 0.001);
        Assert.AreEqual(27200.00, euros[1005], 0.001);
    }
}

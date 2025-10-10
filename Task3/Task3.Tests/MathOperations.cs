namespace Task3.Tests;

[TestClass]
public class MathOperationsTests
{
    private static readonly IList<int> ExpectedSquaresForMixedNumbers =
    [
        1,
        9,
        25,
    ];
    private static readonly IList<int> ExpectedSquaresForAllOddNumbers =
    [
        1,
        9,
        25,
        49,
    ];
    private static readonly IList<int> ExpectedSquaresForNegativeNumbers =
    [
        9,
        1,
        1,
        9,
    ];

    [TestMethod]
    public void SquaresReturnsCorrectSquaresForMixedNumbers()
    {
        IList<int> numbers = [1, 2, 3, 4, 5, 6];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        CollectionAssert.AreEqual(
            ExpectedSquaresForMixedNumbers.ToList(),
            result.ToList()
        );
    }

    [TestMethod]
    public void SquaresWithOnlyEvenNumbersReturnsEmpty()
    {
        IList<int> numbers = [2, 4, 6, 8];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithOnlyOddNumbersReturnsAllSquares()
    {
        IList<int> numbers = [1, 3, 5, 7];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        CollectionAssert.AreEqual(
            ExpectedSquaresForAllOddNumbers.ToList(),
            result.ToList()
        );
    }

    [TestMethod]
    public void SquaresWithEmptyArrayReturnsEmpty()
    {
        IList<int> numbers = [];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithNegativeOddNumbersReturnsPositiveSquares()
    {
        IList<int> numbers = [-3, -2, -1, 0, 1, 2, 3];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        CollectionAssert.AreEqual(
            ExpectedSquaresForNegativeNumbers.ToList(),
            result.ToList()
        );
    }

    [TestMethod]
    public void SquaresWithZeroReturnsEmpty()
    {
        IList<int> numbers = [0];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithLargeNumbersWorksCorrectly()
    {
        IList<int> numbers = [999, 1000, 1001];
        Squares squares = new Squares(numbers);

        IList<int> result = [.. squares];

        CollectionAssert.AreEqual(
            new List<int> { 999 * 999, 1001 * 1001 },
            result.ToList()
        );
    }

    [TestMethod]
    public void SquaresUsesYieldReturn()
    {
        IList<int> numbers = [1, 2, 3];
        Squares squares = new Squares(numbers);

        int count = 0;
        foreach (int item in squares)
        {
            count++;
            Assert.IsTrue(item is 1 or 9);
        }

        Assert.AreEqual(2, count);
    }
}

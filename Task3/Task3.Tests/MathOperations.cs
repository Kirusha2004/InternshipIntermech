namespace Task3.Tests;

[TestClass]
public class MathOperationsTests
{
    private static readonly IList<int> ExpectedSquaresForMixedNumbers = new List<int> { 1, 9, 25 };
    private static readonly IList<int> ExpectedSquaresForAllOddNumbers = new List<int> { 1, 9, 25, 49 };
    private static readonly IList<int> ExpectedSquaresForNegativeNumbers = new List<int> { 9, 1, 1, 9 };

    [TestMethod]
    public void SquaresReturnsCorrectSquaresForMixedNumbers()
    {
        IList<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        CollectionAssert.AreEqual(ExpectedSquaresForMixedNumbers.ToList(), result.ToList());
    }

    [TestMethod]
    public void SquaresWithOnlyEvenNumbersReturnsEmpty()
    {
        IList<int> numbers = new List<int> { 2, 4, 6, 8 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithOnlyOddNumbersReturnsAllSquares()
    {
        IList<int> numbers = new List<int> { 1, 3, 5, 7 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        CollectionAssert.AreEqual(ExpectedSquaresForAllOddNumbers.ToList(), result.ToList());
    }

    [TestMethod]
    public void SquaresWithEmptyArrayReturnsEmpty()
    {
        IList<int> numbers = new List<int>();
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithNegativeOddNumbersReturnsPositiveSquares()
    {
        IList<int> numbers = new List<int> { -3, -2, -1, 0, 1, 2, 3 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        CollectionAssert.AreEqual(ExpectedSquaresForNegativeNumbers.ToList(), result.ToList());
    }

    [TestMethod]
    public void SquaresWithZeroReturnsEmpty()
    {
        IList<int> numbers = new List<int> { 0 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SquaresWithLargeNumbersWorksCorrectly()
    {
        IList<int> numbers = new List<int> { 999, 1000, 1001 };
        Squares squares = new Squares(numbers);

        IList<int> result = squares.ToList();

        CollectionAssert.AreEqual(new List<int> { 999 * 999, 1001 * 1001 }, result.ToList());
    }

    [TestMethod]
    public void SquaresUsesYieldReturn()
    {
        IList<int> numbers = new List<int> { 1, 2, 3 };
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

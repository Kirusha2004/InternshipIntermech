namespace Lesson1Task3.Tests;

[TestClass]
public class MathOperationsTests
{
    private static readonly int[] ExpectedSquaresForMixedNumbers = [1, 9, 25];
    private static readonly int[] ExpectedSquaresForAllOddNumbers = [1, 9, 25, 49];
    private static readonly int[] ExpectedSquaresForNegativeNumbers = [9, 1, 1, 9];

    [TestMethod]
    public void GetSquaresOfOddNumbersReturnsCorrectSquares()
    {
        int[] numbers = [1, 2, 3, 4, 5, 6];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        CollectionAssert.AreEqual(ExpectedSquaresForMixedNumbers, result);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithOnlyEvenNumbersReturnsEmpty()
    {
        int[] numbers = [2, 4, 6, 8];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithOnlyOddNumbersReturnsAllSquares()
    {
        int[] numbers = [1, 3, 5, 7];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        CollectionAssert.AreEqual(ExpectedSquaresForAllOddNumbers, result);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithEmptyArrayReturnsEmpty()
    {
        int[] numbers = [];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithNegativeOddNumbersReturnsPositiveSquares()
    {
        int[] numbers = [-3, -2, -1, 0, 1, 2, 3];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        CollectionAssert.AreEqual(ExpectedSquaresForNegativeNumbers, result);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersUsesYieldReturn()
    {
        int[] numbers = [1, 2, 3];
        IEnumerable<int> collection = MathOperations.GetSquaresOfOddNumbers(numbers);

        int count = 0;
        foreach (int item in collection)
        {
            count++;
            Assert.IsTrue(item is 1 or 9);
        }

        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithZeroReturnsEmpty()
    {
        int[] numbers = [0];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void GetSquaresOfOddNumbersWithLargeNumbersWorksCorrectly()
    {
        int[] numbers = [999, 1000, 1001];
        int[] result = MathOperations.GetSquaresOfOddNumbers(numbers).ToArray();

        CollectionAssert.AreEqual(new[] { 999 * 999, 1001 * 1001 }, result);
    }
}

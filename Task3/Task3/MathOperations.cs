namespace Lesson1Task3;

public static class MathOperations
{
    public static IEnumerable<int> GetSquaresOfOddNumbers(int[] numbers)
    {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (int number in numbers)
        {
            if (number % 2 != 0)
            {
                yield return number * number;
            }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    }
}

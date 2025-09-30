using System.Collections;

namespace Task3;

public sealed class Squares : IEnumerable<int>
{
    private readonly IEnumerable<int> _numbers;

    public Squares(IList<int> numbers)
    {
        _numbers = numbers ?? throw new ArgumentNullException(nameof(numbers));
    }

    public IEnumerator<int> GetEnumerator()
    {
        IEnumerator<int> enumerator = _numbers.GetEnumerator();
        while (enumerator.MoveNext())
        {
            int number = enumerator.Current;

            if (number % 2 != 0)
            {
                yield return number * number;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

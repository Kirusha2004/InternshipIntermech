using System.Collections;
using System.Collections.Generic;

namespace Task3
{
    public sealed class Squares : IEnumerable<int>
    {
        private readonly IList<int> _numbers;

        public Squares(IList<int> numbers)
        {
            _numbers = numbers ?? throw new ArgumentNullException(nameof(numbers));
        }

        public IEnumerator<int> GetEnumerator()
        {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (int number in _numbers)
            {
                if (number % 2 != 0)
                {
                    yield return number * number;
                }
            }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

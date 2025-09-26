using System.Collections;
using System.Collections.Generic;

class Numbers : IEnumerable<int>
{
    private IEnumerable<int> collection;

    public Numbers(int[] collection) => this.collection = collection;

    public IEnumerator<int> GetEnumerator()
    {
        foreach (int number in collection)
        {
            if (number % 2 != 0)
            {
                yield return number * number;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Program
{
    static void Main(string[] args)
    {
        int[] array = { 1, 4, 5, 6 };

        Numbers numbers = new Numbers(array);
        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }

        Numbers emptyNumbers = new Numbers(new int[0]);
        foreach (int n in emptyNumbers)
        {
            Console.WriteLine(n);
        }
    }
}

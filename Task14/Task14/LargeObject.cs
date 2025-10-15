namespace Task14;

public class LargeObject
{
    private readonly int[] _array;

    public LargeObject(int size = 1000000)
    {
        _array = new int[size];
    }

    public int ArraySize => _array.Length;

    public void Demonstrate(int index)
    {
        for (int i = 0; i < Math.Min(10, _array.Length); i++)
        {
            _array[i] = index + i;
        }
    }
}

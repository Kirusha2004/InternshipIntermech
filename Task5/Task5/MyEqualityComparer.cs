namespace Task5;

public class MyEqualityComparer<TKey> : IEqualityComparer<TKey>
{
    public bool Equals(TKey? x, TKey? y)
    {
        return x is string strX && y is string strY
            ? string.Equals(strX, strY, StringComparison.OrdinalIgnoreCase)
            : EqualityComparer<TKey>.Default.Equals(x, y);
    }

    public int GetHashCode(TKey obj)
    {
        return obj is string str
            ? str.ToLowerInvariant().GetHashCode()
            : obj?.GetHashCode() ?? 0;
    }
}

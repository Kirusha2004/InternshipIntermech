namespace Task5;

public class MyEqualityComparer<TKey> : IEqualityComparer<TKey>
{
    public bool Equals(TKey x, TKey y)
    {
        if (x is string strX && y is string strY)
        {
            return string.Equals(strX, strY, StringComparison.OrdinalIgnoreCase);
        }

        return EqualityComparer<TKey>.Default.Equals(x, y);
    }

    public int GetHashCode(TKey obj)
    {
        if (obj is string str)
        {
            return str.ToLowerInvariant().GetHashCode();
        }

        return EqualityComparer<TKey>.Default.GetHashCode(obj);
    }
}

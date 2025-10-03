using System.Collections;
using System.Collections.Generic;

namespace Task5;

public class MyEqualityComparer<TKey> : IEqualityComparer<TKey>
{
    public bool Equals(TKey x, TKey y)
    {
        return string.Equals(x?.ToString(), y?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(TKey obj)
    {
        return obj?.ToString().ToLowerInvariant().GetHashCode() ?? 0;
    }
}

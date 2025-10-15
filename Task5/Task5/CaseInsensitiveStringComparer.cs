namespace Task5;

public class CaseInsensitiveStringComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(string obj)
    {
        return obj?.ToLowerInvariant().GetHashCode() ?? 0;
    }
}

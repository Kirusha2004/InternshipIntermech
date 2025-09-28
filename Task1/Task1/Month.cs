namespace UserCollections1;

public sealed class Month
{
    public Month(int number, string name, int days)
    {
        Number = number;
        Name = name;
        Days = days;
    }

    public int Number { get; }

    public string Name { get; }

    public int Days { get; }
}

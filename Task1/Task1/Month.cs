namespace UserCollections1;

public class Month
{
    public Month(int number, string name, int days)
    {
        Number = number;
        Name = name;
        Days = days;
    }

    public int Number { get; set; }

    public string Name { get; set; }

    public int Days { get; set; }
}

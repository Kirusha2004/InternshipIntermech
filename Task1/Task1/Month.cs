public class Month
{
    public int Number { get; set; }
    public string Name { get; set; }
    public int Days { get; set; }

    public Month(int number, string name, int days)
    {
        Number = number;
        Name = name;
        Days = days;
    }
}
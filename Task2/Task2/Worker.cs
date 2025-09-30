namespace Task2;

public sealed class Worker : Citizen
{
    public string Position { get; }

    public Worker(string passportNumber, string fullName, string position)
        : base(passportNumber, fullName)
    {
        Position = position ?? throw new ArgumentNullException(nameof(position));
    }
}

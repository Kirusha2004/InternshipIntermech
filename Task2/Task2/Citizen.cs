namespace Task2;

public abstract class Citizen
{
    public string PassportNumber { get; }
    public string FullName { get; }

    protected Citizen(string passportNumber, string fullName)
    {
        PassportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }

    public override bool Equals(object? obj)
    {
        return obj is Citizen other && PassportNumber == other.PassportNumber;
    }

    public override int GetHashCode()
    {
        return PassportNumber.GetHashCode();
    }
}

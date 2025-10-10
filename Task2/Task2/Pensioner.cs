namespace Task2;

public sealed class Pensioner : Citizen
{
    public decimal Pension { get; }

    public Pensioner(string passportNumber, string fullName, decimal pension)
        : base(passportNumber, fullName)
    {
        Pension = pension;
    }
}

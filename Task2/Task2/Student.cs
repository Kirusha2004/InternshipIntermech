namespace Task2;

public sealed class Student : Citizen
{
    public string University { get; }

    public Student(string passportNumber, string fullName, string university)
        : base(passportNumber, fullName)
    {
        University = university ?? throw new ArgumentNullException(nameof(university));
    }
}

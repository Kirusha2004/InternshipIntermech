namespace Task5.Tests;

public class Person
{
    public int Id { get; init; }
    public required string Name { get; init; }

    public override bool Equals(object? obj)
    {
        return obj is Person person && Id == person.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"Person(Id: {Id}, Name: {Name})";
    }
}

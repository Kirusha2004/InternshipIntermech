namespace Task2;

public readonly struct CitizenWithPosition
{
    public Citizen? Citizen { get; }
    public int Position { get; }

    public CitizenWithPosition(Citizen? citizen, int position)
    {
        Citizen = citizen;
        Position = position;
    }

    public void Deconstruct(out Citizen? citizen, out int position)
    {
        citizen = Citizen;
        position = Position;
    }
}

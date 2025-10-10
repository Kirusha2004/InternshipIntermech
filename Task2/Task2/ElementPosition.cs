namespace Task2;

public readonly struct ElementPosition
{
    public bool Exists { get; }
    public int Position { get; }

    public ElementPosition(bool exists, int position)
    {
        Exists = exists;
        Position = position;
    }

    public void Deconstruct(out bool exists, out int position)
    {
        exists = Exists;
        position = Position;
    }
}

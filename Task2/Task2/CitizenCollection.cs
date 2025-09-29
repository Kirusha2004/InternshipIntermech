using System.Collections;

namespace Task2;

public sealed class CitizenCollection : IEnumerable<Citizen>
{
    private readonly List<Citizen> _citizens = [];

    public int Add(Citizen citizen)
    {
        ArgumentNullException.ThrowIfNull(citizen);

        if (_citizens.Any(c => c.Equals(citizen)))
        {
            Console.WriteLine(
                "Гражданин с таким номером паспорта уже существует в коллекции."
            );
            return -1;
        }

        if (citizen is Pensioner)
        {
            int lastPensionerIndex = _citizens.FindLastIndex(c => c is Pensioner);
            int insertIndex = lastPensionerIndex == -1 ? 0 : lastPensionerIndex + 1;
            _citizens.Insert(insertIndex, citizen);
            return insertIndex + 1;
        }
        else
        {
            _citizens.Add(citizen);
            return _citizens.Count;
        }
    }

    public Citizen Remove()
    {
        if (_citizens.Count == 0)
        {
            throw new InvalidOperationException("Коллекция пуста.");
        }

        Citizen firstCitizen = _citizens[0];
        _citizens.RemoveAt(0);
        return firstCitizen;
    }

    public bool Remove(Citizen citizen)
    {
        return citizen == null ? throw new ArgumentNullException(nameof(citizen)) : _citizens.Remove(citizen);
    }

    public (bool exists, int position) Contains(Citizen citizen)
    {
        ArgumentNullException.ThrowIfNull(citizen);

        int index = _citizens.FindIndex(c => c.Equals(citizen));
        return (index != -1, index != -1 ? index + 1 : -1);
    }

    public (Citizen? citizen, int position) ReturnLast()
    {
        return _citizens.Count == 0 ? ((Citizen? citizen, int position))(null, -1) : ((Citizen? citizen, int position))(_citizens[^1], _citizens.Count);
    }

    public void Clear()
    {
        _citizens.Clear();
    }

    public int Count => _citizens.Count;

    public IEnumerator<Citizen> GetEnumerator()
    {
        return _citizens.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

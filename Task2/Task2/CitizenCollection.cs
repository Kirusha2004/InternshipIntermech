using System.Collections;

namespace Task2;

public sealed class CitizenCollection : IEnumerable<Citizen>
{
    private readonly IList<Citizen> _citizens = [];

    public int Add(Citizen citizen)
    {
        ArgumentNullException.ThrowIfNull(citizen);

        if (_citizens.Contains(citizen))
        {
            Console.WriteLine("Гражданин с таким номером паспорта уже существует в коллекции.");
            return -1;
        }

        if (citizen is Pensioner)
        {
            int lastPensionerIndex = -1;
            for (int i = _citizens.Count - 1; i >= 0; i--)
            {
                if (_citizens[i] is Pensioner)
                {
                    lastPensionerIndex = i;
                    break;
                }
            }

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
        if (!_citizens.Any())
        {
            throw new InvalidOperationException("Коллекция пуста.");
        }

        Citizen firstCitizen = _citizens.First();
        _citizens.RemoveAt(0);
        return firstCitizen;
    }

    public bool Remove(Citizen citizen)
    {
        ArgumentNullException.ThrowIfNull(citizen);
        return _citizens.Remove(citizen);
    }

    public ElementPosition Contains(Citizen citizen)
    {
        ArgumentNullException.ThrowIfNull(citizen);

        int index = _citizens.IndexOf(citizen);
        return new ElementPosition(index != -1, index != -1 ? index + 1 : -1);
    }

    public CitizenWithPosition ReturnLast()
    {
        return !_citizens.Any()
            ? new CitizenWithPosition(null, -1)
            : new CitizenWithPosition(_citizens.Last(), _citizens.Count);
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

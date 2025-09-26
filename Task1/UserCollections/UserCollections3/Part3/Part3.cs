using System;
using System.Collections;
using System.Collections.Generic;

public class CitizenCollection : IEnumerable<Citizen>
{
    private readonly List<Citizen> _citizens = new List<Citizen>();

    public int Add(Citizen citizen)
    {
        if (Contains(citizen).exists)
        {
            Console.WriteLine("Гражданин с таким номером паспорта уже существует в коллекции.");
            return -1;
        }

        if (citizen is Pensioner)
        {
            int lastPensionerIndex = _citizens.FindLastIndex(c => c is Pensioner);
            int insertIndex = lastPensionerIndex + 1;
            _citizens.Insert(insertIndex, citizen);
            return insertIndex + 1;
        }
        else
        {
            _citizens.Add(citizen);
            return _citizens.Count;
        }
    }

    public void Remove()
    {
        if (_citizens.Count > 0)
            _citizens.RemoveAt(0);
    }

    public void Remove(Citizen citizen)
    {
        _citizens.Remove(citizen);
    }

    public (bool exists, int index) Contains(Citizen citizen)
    {
        int index = _citizens.IndexOf(citizen);
        return (index != -1, index + 1);
    }

    public (Citizen citizen, int index) ReturnLast()
    {
        if (_citizens.Count == 0)
            return (null, 0);
        int lastIndex = _citizens.Count;
        return (_citizens[lastIndex - 1], lastIndex);
    }

    public void Clear()
    {
        _citizens.Clear();
    }

    public IEnumerator<Citizen> GetEnumerator()
    {
        return _citizens.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public abstract class Citizen
{
    public string FIO { get; set; }
    public string NumberOfPassport { get; set; }
    public int Age { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Citizen other)
            return NumberOfPassport == other.NumberOfPassport;
        return false;
    }

    public override int GetHashCode()
    {
        return NumberOfPassport?.GetHashCode() ?? 0;
    }
}

public class Student : Citizen { }
public class Pensioner : Citizen { }
public class Worker : Citizen { }

class Program
{
    static void Main()
    {
        var collection = new CitizenCollection();

        var pensioner1 = new Pensioner { NumberOfPassport = "12345" };
        var pensioner2 = new Pensioner { NumberOfPassport = "67890" };
        var student = new Student { NumberOfPassport = "11111" };
        var worker = new Worker { NumberOfPassport = "22222" };

        Console.WriteLine("Позиции при добавлении:");
        Console.WriteLine($"Пенсионер1: {collection.Add(pensioner1)}");
        Console.WriteLine($"Студент: {collection.Add(student)}");
        Console.WriteLine($"Пенсионер2: {collection.Add(pensioner2)}");
        Console.WriteLine($"Рабочий: {collection.Add(worker)}");

        var result = collection.Contains(student);
        Console.WriteLine($"\nСтудент найден: {result.exists}, позиция: {result.index}");

        var last = collection.ReturnLast();
        Console.WriteLine($"Последний - рабочий: {last.citizen == worker}, позиция: {last.index}");

        collection.Remove();
        Console.WriteLine("\nПосле удаления первого элемента:");

        foreach (var citizen in collection)
        {
            Console.WriteLine(citizen.NumberOfPassport);
        }
    }
}
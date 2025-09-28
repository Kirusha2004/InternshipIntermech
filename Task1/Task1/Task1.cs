using System.Collections;

namespace UserCollections1;

public sealed class MonthsInYear : IEnumerable<Month>
{
    private readonly int _year;
    private static readonly IReadOnlyCollection<string> RussianMonthNames =
    [
        "Январь",
        "Февраль",
        "Март",
        "Апрель",
        "Май",
        "Июнь",
        "Июль",
        "Август",
        "Сентябрь",
        "Октябрь",
        "Ноябрь",
        "Декабрь",
    ];

    public MonthsInYear()
        : this(DateTime.Now.Year) { }

    public MonthsInYear(int year)
    {
        _year = year;
    }

    public IEnumerator<Month> GetEnumerator()
    {
        IEnumerable<int> daysInMonths = Enumerable.Range(1, 12)
            .Select(month => DateTime.DaysInMonth(_year, month));

        return RussianMonthNames
            .Zip(Enumerable.Range(1, 12), (name, number) => (name, number))
            .Zip(daysInMonths, (first, days) => new Month(first.number, first.name, days))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

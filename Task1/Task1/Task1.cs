using System.Collections;

namespace UserCollections1;

public sealed class MonthsInYear : IEnumerable<Month>
{
    private readonly int _year;
    private static readonly string[] RussianMonthNames =
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
        foreach (int monthNumber in Enumerable.Range(1, 12))
        {
            yield return new Month(
                monthNumber,
                RussianMonthNames[monthNumber - 1],
                DateTime.DaysInMonth(_year, monthNumber)
            );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

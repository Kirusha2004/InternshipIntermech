using UserCollections1;

namespace UserCollections.Tests1;

public static class TestDataFactory
{
    public static MonthsInYear CreateMonthsInYear(int year = 2023)
    {
        return new MonthsInYear(year);
    }

    public static List<Month> GetMonthsWith31Days()
    {
        return [.. CreateMonthsInYear().Where(m => m.Days == 31)];
    }

    public static List<Month> GetMonthsAfterJuneWith30Days()
    {
        return [.. CreateMonthsInYear().Where(m => m.Number > 6 && m.Days == 30)];
    }
}

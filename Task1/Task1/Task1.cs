using UserCollections1;

List<Month> list =
[
    new Month(1, "Январь", 31),
    new Month(2, "Февраль", 28),
    new Month(3, "Март", 31),
    new Month(4, "Апрель", 30),
    new Month(5, "Май", 31),
    new Month(6, "Июнь", 30),
    new Month(7, "Июль", 31),
    new Month(8, "Август", 31),
    new Month(9, "Сентябрь", 30),
    new Month(10, "Октябрь", 31),
    new Month(11, "Ноябрь", 30),
    new Month(12, "Декабрь", 31),
];

IEnumerable<Month> monthByNumber = list.Where(m => m.Number == 5);
Console.WriteLine("Месяц по номеру 5");
foreach (Month? month in monthByNumber)
{
    Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
}
Console.WriteLine();

IEnumerable<Month> monthsWith31Days = list.Where(m => m.Days == 31);
Console.WriteLine("Месяцы с 31 днем");
foreach (Month? month in monthsWith31Days)
{
    Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
}
Console.WriteLine();

IEnumerable<Month> combinedResult = list.Where(m => m.Number > 6 && m.Days == 30);
Console.WriteLine("Месяцы после июня с 30 днями");
foreach (Month? month in combinedResult)
{
    Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
}

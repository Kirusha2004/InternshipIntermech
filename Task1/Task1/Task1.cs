using System;
using System.Collections.Generic;
using System.Linq;

namespace UserCollections1
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            List<Month> list = new List<Month> {
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
                new Month(12, "Декабрь", 31)
            };

            var monthByNumber = list.Where(m => m.Number == 5);
            Console.WriteLine("Месяц по номеру 5");
            foreach (var month in monthByNumber)
            {
                Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
            }
            Console.WriteLine();

            var monthsWith31Days = list.Where(m => m.Days == 31);
            Console.WriteLine("Месяцы с 31 днем");
            foreach (var month in monthsWith31Days)
            {
                Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
            }
            Console.WriteLine();

            var combinedResult = list.Where(m => m.Number > 6 && m.Days == 30);
            Console.WriteLine("Месяцы после июня с 30 днями");
            foreach (var month in combinedResult)
            {
                Console.WriteLine($"{month.Number:00}. {month.Name} - {month.Days} дней");
            }
        }
    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UserCollections2;

namespace UserCollections.Tests
{
    [TestClass]
    public class Task1Tests
    {
        private List<Month> CreateTestMonths()
        {
            return new List<Month> {
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
        }

        [TestMethod]
        public void MonthByNumber_ReturnsCorrectMonth()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Number == 5).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Май", result[0].Name);
            Assert.AreEqual(31, result[0].Days);
        }

        [TestMethod]
        public void MonthsWith31Days_Returns7Months()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Days == 31).ToList();

            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result.All(m => m.Days == 31));
        }

        [TestMethod]
        public void MonthsAfterJuneWith30Days_Returns2Months()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Number > 6 && m.Days == 30).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(m => m.Number > 6 && m.Days == 30));
        }

        [TestMethod]
        public void MonthClass_CreatesCorrectly()
        {
            var month = new Month(5, "Май", 31);

            Assert.AreEqual(5, month.Number);
            Assert.AreEqual("Май", month.Name);
            Assert.AreEqual(31, month.Days);
        }
    }
}
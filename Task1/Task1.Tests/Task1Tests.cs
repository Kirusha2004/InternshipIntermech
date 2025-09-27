using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserCollections1;

namespace UserCollections.Tests1
{
    [TestClass]
    public class Task1Tests
    {
        private static List<Month> CreateTestMonths()
        {
            return new List<Month>
            {
                new Month(1, "������", 31),
                new Month(2, "�������", 28),
                new Month(3, "����", 31),
                new Month(4, "������", 30),
                new Month(5, "���", 31),
                new Month(6, "����", 30),
                new Month(7, "����", 31),
                new Month(8, "������", 31),
                new Month(9, "��������", 30),
                new Month(10, "�������", 31),
                new Month(11, "������", 30),
                new Month(12, "�������", 31),
            };
        }

        [TestMethod]
        public void MonthByNumberReturnsCorrectMonth()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Number == 5).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("���", result[0].Name);
            Assert.AreEqual(31, result[0].Days);
        }

        [TestMethod]
        public void MonthsWith31DaysReturns7Months()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Days == 31).ToList();

            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result.All(m => m.Days == 31));
        }

        [TestMethod]
        public void MonthsAfterJuneWith30DaysReturns2Months()
        {
            var list = CreateTestMonths();
            var result = list.Where(m => m.Number > 6 && m.Days == 30).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(m => m.Number > 6 && m.Days == 30));
        }

        [TestMethod]
        public void MonthClassCreatesCorrectly()
        {
            var month = new Month(5, "���", 31);

            Assert.AreEqual(5, month.Number);
            Assert.AreEqual("���", month.Name);
            Assert.AreEqual(31, month.Days);
        }
    }
}

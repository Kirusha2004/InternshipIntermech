namespace Task7.Tests;

[TestClass]
public class FinancialReportGeneratorTests
{
    [TestMethod]
    public void TestGenerateReportWithProducts()
    {
        FinancialReportGenerator generator = new();
        List<Product> products =
        [
            new Product
            {
                Name = "Apple",
                Price = 1.5m,
                Amount = 3,
            },
            new Product
            {
                Name = "Bread",
                Price = 2.0m,
                Amount = 2,
            },
        ];

        string report = generator.GenerateReport(products);

        Assert.IsNotNull(report);
        Assert.IsTrue(report.Contains("FINANCIAL REPORT"));
        Assert.IsTrue(report.Contains("Apple"));
        Assert.IsTrue(report.Contains("Bread"));
        Assert.IsTrue(report.Contains("Total:"));
        Assert.IsTrue(report.Length > 0);
    }

    [TestMethod]
    public void TestGenerateReportWithEmptyProducts()
    {
        FinancialReportGenerator generator = new();
        IList<Product> products = [];

        string report = generator.GenerateReport(products);

        Assert.IsNotNull(report);
        Assert.IsTrue(report.Contains("FINANCIAL REPORT"));
        Assert.IsTrue(report.Contains("Total:"));

        Assert.IsFalse(string.IsNullOrEmpty(report));
    }

    [TestMethod]
    public void TestGenerateReportFormatting()
    {
        FinancialReportGenerator generator = new FinancialReportGenerator();
        IList<Product> products =
        [
            new Product
            {
                Name = "Test Product",
                Price = 10.99m,
                Amount = 1,
            },
        ];

        string report = generator.GenerateReport(products);

        Assert.IsTrue(report.Contains("Test Product"));
        Assert.IsTrue(report.Contains("Product Name"));
        Assert.IsTrue(report.Contains("Price"));
        Assert.IsTrue(report.Contains("Quantity"));
    }

    [TestMethod]
    public void TestGenerateReportContainsCorrectCalculations()
    {
        FinancialReportGenerator generator = new FinancialReportGenerator();
        IList<Product> products =
        [
            new Product
            {
                Name = "Item1",
                Price = 10.0m,
                Amount = 2,
            },
            new Product
            {
                Name = "Item2",
                Price = 15.5m,
                Amount = 3,
            },
        ];
        decimal expectedTotal = 66.50m;

        string report = generator.GenerateReport(products);
        Assert.IsTrue(report.Contains(expectedTotal.ToString()));


        decimal actualTotal = products.Sum(p => p.Price * p.Amount);
        Assert.AreEqual(expectedTotal, actualTotal);
        Assert.IsTrue(report.Contains("46.50"));
    }

    [TestMethod]
    public void TestGenerateReportStructure()
    {
        FinancialReportGenerator generator = new FinancialReportGenerator();
        IList<Product> products =
        [
            new Product
            {
                Name = "Test",
                Price = 1.0m,
                Amount = 1,
            },
        ];

        string report = generator.GenerateReport(products);

        StringAssert.Contains(report, "FINANCIAL REPORT");
        StringAssert.Contains(report, "Report Date:");
        StringAssert.Contains(report, "Culture:");
        StringAssert.Contains(report, "Product Name");
        StringAssert.Contains(report, "Price");
        StringAssert.Contains(report, "Quantity");
        StringAssert.Contains(report, "Total");
        StringAssert.Contains(report, "Test");
    }
}

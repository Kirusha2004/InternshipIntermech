namespace Task7.Tests;

[TestClass]
public class FinancialReportGeneratorTests
{
    [TestMethod]
    public void TestGenerateReportWithProducts()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Apple", 1.5m, 3),
            new Product("Bread", 2.0m, 2)
        };

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
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new List<Product>();

        string report = generator.GenerateReport(products);

        Assert.IsNotNull(report);
        Assert.IsTrue(report.Contains("FINANCIAL REPORT"));
        Assert.IsTrue(report.Contains("Total:"));
        Assert.IsFalse(string.IsNullOrEmpty(report));
    }

    [TestMethod]
    public void TestGenerateReportFormatting()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Test Product", 10.99m, 1)
        };

        string report = generator.GenerateReport(products);

        Assert.IsTrue(report.Contains("Test Product"));
        Assert.IsTrue(report.Contains("Product Name"));
        Assert.IsTrue(report.Contains("Price"));
        Assert.IsTrue(report.Contains("Quantity"));
        Assert.IsTrue(report.Contains("Total"));
    }

    [TestMethod]
    public void TestGenerateReportContainsCorrectCalculations()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Item1", 10.0m, 2),
            new Product("Item2", 15.5m, 3)
        };

        decimal expectedTotal = 66.50m;
        string report = generator.GenerateReport(products);

        Assert.IsTrue(report.Contains(expectedTotal.ToString("C")));

        decimal actualTotal = products.Sum(p => p.GetTotalPrice());
        Assert.AreEqual(expectedTotal, actualTotal);
    }

    [TestMethod]
    public void TestGenerateReportStructure()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Test", 1.0m, 1)
        };

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

    [TestMethod]
    public void TestGenerateReportUsesStringJoin()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Test", 1.0m, 1)
        };

        string report = generator.GenerateReport(products);

        Assert.IsTrue(report.Contains(Environment.NewLine));
        Assert.IsTrue(report.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length > 10);
    }

    [TestMethod]
    public void TestGenerateReportWithSpecialCharacters()
    {
        IReportGenerator generator = new FinancialReportGenerator();
        IEnumerable<Product> products = new[]
        {
            new Product("Café", 25.75m, 2),
            new Product("München Beer", 15.99m, 1)
        };

        string report = generator.GenerateReport(products);

        Assert.IsNotNull(report);
        Assert.IsTrue(report.Contains("Café"));
        Assert.IsTrue(report.Contains("München Beer"));
    }
}

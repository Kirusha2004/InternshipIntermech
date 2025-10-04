namespace Task7.Tests;

[TestClass]
public class ReceiptTests
{
    [TestMethod]
    public void TestAddProduct()
    {
        Receipt receipt = new Receipt();
        Product product = new Product { Name = "Test", Price = 10.0m, Amount = 2 };

        receipt.AddProduct(product);

        Assert.AreEqual(1, receipt.ProductCount);
    }

    [TestMethod]
    public void TestGetTotalCalculation()
    {
        Receipt receipt = new Receipt();
        receipt.AddProduct(new Product { Name = "A", Price = 10.0m, Amount = 2 });
        receipt.AddProduct(new Product { Name = "B", Price = 5.0m, Amount = 3 });

        decimal total = receipt.GetTotal();

        Assert.AreEqual(35.0m, total);
    }
}

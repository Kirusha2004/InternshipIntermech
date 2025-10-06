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

    [TestMethod]
    public void TestGetProductsReturnsCopy()
    {
        var receipt = new Receipt();
        var product = new Product { Name = "Test", Price = 10.0m, Amount = 1 };
        receipt.AddProduct(product);

        var products = receipt.GetProducts();
        products.Clear(); 

        Assert.AreEqual(1, receipt.ProductCount);
    }

    [TestMethod]
    public void TestEmptyReceiptTotal()
    {
        var receipt = new Receipt();

        decimal total = receipt.GetTotal();

        Assert.AreEqual(0m, total);
    }
}

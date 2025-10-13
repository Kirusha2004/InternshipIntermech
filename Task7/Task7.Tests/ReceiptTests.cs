namespace Task7.Tests;

[TestClass]
public class ReceiptTests
{
    [TestMethod]
    public void TestAddProduct()
    {
        IReceipt receipt = new Receipt();
        Product product = new Product("Test", 10.0m, 2);

        receipt.AddProduct(product);

        Assert.AreEqual(1, receipt.ProductCount);
    }

    [TestMethod]
    public void TestTotalCalculation()
    {
        IReceipt receipt = new Receipt();
        receipt.AddProduct(new Product("A", 10.0m, 2));
        receipt.AddProduct(new Product("B", 5.0m, 3));

        decimal total = receipt.Total;

        Assert.AreEqual(35.0m, total);
    }

    [TestMethod]
    public void TestProductsReturnsReadOnlyCollection()
    {
        IReceipt receipt = new Receipt();
        Product product = new Product("Test", 10.0m, 1);
        receipt.AddProduct(product);

        IList<Product> products = [.. receipt.Products];
        products.Clear();

        Assert.AreEqual(1, receipt.ProductCount);
    }

    [TestMethod]
    public void TestEmptyReceiptTotal()
    {
        IReceipt receipt = new Receipt();

        decimal total = receipt.Total;

        Assert.AreEqual(0m, total);
    }

    [TestMethod]
    public void TestProductsPropertyIsImmutable()
    {
        IReceipt receipt = new Receipt();
        receipt.AddProduct(new Product("Test1", 10.0m, 1));
        receipt.AddProduct(new Product("Test2", 20.0m, 2));

        int count = 0;
        foreach (Product product in receipt.Products)
        {
            count++;
            Assert.IsNotNull(product);
        }
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public void TestProductValidationInReceipt()
    {
        IReceipt receipt = new Receipt();

        _ = Assert.ThrowsException<ArgumentException>(() =>
            receipt.AddProduct(new Product("", -10.0m, -1)));
    }

    [TestMethod]
    public void TestMultipleProducts()
    {
        IReceipt receipt = new Receipt();

        for (int i = 0; i < 5; i++)
        {
            receipt.AddProduct(new Product($"Product{i}", i * 10.0m, i + 1));
        }

        Assert.AreEqual(5, receipt.ProductCount);
        Assert.IsTrue(receipt.Total > 0);
    }

    [TestMethod]
    public void TestReceiptImplementsInterface()
    {
        IReceipt receipt = new Receipt();
        Assert.IsInstanceOfType(receipt, typeof(IReceipt));
    }
}

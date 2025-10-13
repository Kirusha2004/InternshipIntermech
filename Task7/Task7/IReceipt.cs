namespace Task7;
public interface IReceipt
{
    int ProductCount { get; }
    decimal Total { get; }
    IEnumerable<Product> Products { get; }
    void AddProduct(Product product);
}

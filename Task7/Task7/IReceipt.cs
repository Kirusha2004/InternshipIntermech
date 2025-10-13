namespace Task7;
public interface IReceipt
{
    public int ProductCount { get; }
    public decimal Total { get; }
    public IEnumerable<Product> Products { get; }
    public void AddProduct(Product product);
}

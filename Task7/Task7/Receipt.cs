namespace Task7;

public class Receipt : IReceipt
{
    private readonly IList<Product> _products = [];

    public int ProductCount => _products.Count;
    public decimal Total => _products.Sum(p => p.Price * p.Amount);
    public IEnumerable<Product> Products => _products.AsReadOnly();

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}

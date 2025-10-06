namespace Task7;
public class Receipt
{
    private IList<Product> _products = [];
    public int ProductCount => _products.Count;

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotal()
    {
        return _products.Sum(p => p.Price * p.Amount);
    }

    public IList<Product> GetProducts()
    {
        return new List<Product>(_products);
    }
}

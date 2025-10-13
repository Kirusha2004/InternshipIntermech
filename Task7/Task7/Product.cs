namespace Task7;

public class Product
{
    public Product(string name, decimal price, int amount)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty");
        }

        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }

        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }

        Name = name;
        Price = price;
        Amount = amount;
    }

    public string Name { get; }
    public decimal Price { get; }
    public int Amount { get; }

    public decimal GetTotalPrice()
    {
        return Price * Amount;
    }
}

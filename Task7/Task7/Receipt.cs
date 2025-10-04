using System.Globalization;
using System.Text;

namespace Task7;
public class Receipt
{
    private List<Product> _products = [];
    public decimal EndOfAmount { get; set; }

    public int ProductCount => _products.Count;

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotal()
    {
        return _products.Sum(p => p.Price * p.Amount);
    }

    public string GenerateFinancialReport()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("FINANCIAL REPORT");
        stringBuilder.AppendLine("================\n");

        stringBuilder.AppendLine($"Report Date: {DateTime.Now.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture)}");
        stringBuilder.AppendLine($"Culture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.EnglishName})");
        stringBuilder.AppendLine();

        stringBuilder.AppendLine(string.Format("{0,-20} {1,-10} {2,-10} {3,-10}", "Product Name", "Price", "Quantity", "Total"));
        stringBuilder.AppendLine(new string('-', 50));

        foreach (var product in _products)
        {
            string productTotal = (product.Price * product.Amount).ToString("C", CultureInfo.CurrentCulture);
            string priceFormatted = product.Price.ToString("C", CultureInfo.CurrentCulture);
            stringBuilder.AppendLine(string.Format("{0,-20} {1,-10} {2,-10} {3,-10}", product.Name, priceFormatted, product.Amount, productTotal));
        }

        stringBuilder.AppendLine(new string('-', 50));
        stringBuilder.AppendLine($"Total: {GetTotal().ToString("C", CultureInfo.CurrentCulture)}");

        return stringBuilder.ToString();
    }
    internal static void SaveReportToFile(string content, string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.Write(content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }
    }
}

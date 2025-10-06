using System.Globalization;
using System.Text;

namespace Task7;

public class FinancialReportGenerator : IReportGenerator
{
    public string GenerateReport(IList<Product> products)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("FINANCIAL REPORT");
        stringBuilder.AppendLine("================\n");

        stringBuilder.AppendLine($"Report Date: {DateTime.Now.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture)}");
        stringBuilder.AppendLine($"Culture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.EnglishName})");
        stringBuilder.AppendLine();

        stringBuilder.AppendLine(string.Format("{0,-20} {1,-10} {2,-10} {3,-10}", "Product Name", "Price", "Quantity", "Total"));
        stringBuilder.AppendLine(new string('-', 50));

        decimal total = products.Sum(p => p.Price * p.Amount);

        foreach (var product in products)
        {
            string productTotal = (product.Price * product.Amount).ToString("C", CultureInfo.CurrentCulture);
            string priceFormatted = product.Price.ToString("C", CultureInfo.CurrentCulture);
            stringBuilder.AppendLine(string.Format("{0,-20} {1,-10} {2,-10} {3,-10}", product.Name, priceFormatted, product.Amount, productTotal));
        }

        stringBuilder.AppendLine(new string('-', 50));
        stringBuilder.AppendLine($"Total: {total.ToString("C", CultureInfo.CurrentCulture)}");

        return stringBuilder.ToString();
    }
}

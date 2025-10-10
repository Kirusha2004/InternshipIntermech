using System.Globalization;
using System.Text;

namespace Task7;

public class FinancialReportGenerator : IReportGenerator
{
    public string GenerateReport(IList<Product> products)
    {
        StringBuilder stringBuilder = new StringBuilder();

        _ = stringBuilder.AppendLine("FINANCIAL REPORT");
        _ = stringBuilder.AppendLine("================\n");

        _ = stringBuilder.AppendLine(
            $"Report Date: {DateTime.Now.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture)}"
        );
        _ = stringBuilder.AppendLine(
            $"Culture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.EnglishName})"
        );
        _ = stringBuilder.AppendLine();

        _ = stringBuilder.AppendLine(
            string.Format(
                "{0,-20} {1,-10} {2,-10} {3,-10}",
                "Product Name",
                "Price",
                "Quantity",
                "Total"
            )
        );
        _ = stringBuilder.AppendLine(new string('-', 50));

        decimal total = products.Sum(p => p.Price * p.Amount);

        foreach (Product product in products)
        {
            string productTotal = (product.Price * product.Amount).ToString(
                "C",
                CultureInfo.CurrentCulture
            );
            string priceFormatted = product.Price.ToString(
                "C",
                CultureInfo.CurrentCulture
            );
            _ = stringBuilder.AppendLine(
                string.Format(
                    "{0,-20} {1,-10} {2,-10} {3,-10}",
                    product.Name,
                    priceFormatted,
                    product.Amount,
                    productTotal
                )
            );
        }

        _ = stringBuilder.AppendLine(new string('-', 50));
        _ = stringBuilder.AppendLine(
            $"Total: {total.ToString("C", CultureInfo.CurrentCulture)}"
        );

        return stringBuilder.ToString();
    }
}

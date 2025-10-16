using System.Globalization;

namespace Task7;

public class FinancialReportGenerator : IReportGenerator
{
    public string GenerateReport(IEnumerable<Product> products)
    {
        IList<Product> productList = [.. products];
        decimal total = productList.Sum(p => p.GetTotalPrice());

        IList<string> reportLines =
        [
            "FINANCIAL REPORT",
            "================",
            "",
            $"Report Date: {DateTime.Now.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture)}",
            $"Culture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.EnglishName})",
            "",
            string.Format("{0,-20} {1,-10} {2,-10} {3,-10}", "Product Name", "Price", "Quantity", "Total"),
            new string('-', 50),
            .. productList.Select(product =>
            {
                string priceFormatted = product.Price.ToString("C", CultureInfo.CurrentCulture);
                string productTotal = product.GetTotalPrice().ToString("C", CultureInfo.CurrentCulture);
                return string.Format("{0,-20} {1,-10} {2,-10} {3,-10}",
                    product.Name, priceFormatted, product.Amount, productTotal);
            }),
            .. new[]
            {
                new string('-', 50),
                $"Total: {total.ToString("C", CultureInfo.CurrentCulture)}"
            },
        ];

        return string.Join(Environment.NewLine, reportLines);
    }
}

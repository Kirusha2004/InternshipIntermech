namespace Task7;

public interface IReportGenerator
{
    public string GenerateReport(IEnumerable<Product> products);
}

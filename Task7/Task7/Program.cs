using Task7;

Receipt receipt = new Receipt();

receipt.AddProduct(new Product { Name = "Apple", Price = 1.50m, Amount = 2 });
receipt.AddProduct(new Product { Name = "Bread", Price = 2.00m, Amount = 1 });

string report = receipt.GenerateFinancialReport();
Console.WriteLine(report);

Console.Write("Save report to file? (y/n): ");
string answer = Console.ReadLine();
if (answer.ToLower() == "y")
{
    Receipt.SaveReportToFile(report, "receipt.txt");
    Console.WriteLine("Report saved to receipt.txt");
}


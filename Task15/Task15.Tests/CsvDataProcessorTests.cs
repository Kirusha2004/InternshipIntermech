namespace Task15.Tests;
[TestClass]
public class CsvDataProcessorTests
{
    [TestMethod]
    public void CsvDataProcessorInheritsFromDataProcessor()
    {
        CsvDataProcessor processor = new CsvDataProcessor();

        Assert.IsInstanceOfType(processor, typeof(DataProcessor));
    }

    [TestMethod]
    public void ProcessDataExecutesFullPipelineForCsv()
    {
        CsvDataProcessor processor = new CsvDataProcessor();
        using StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        processor.ProcessData();
        string result = sw.ToString();

        StringAssert.Contains(result, "Reading data from source");
        StringAssert.Contains(result, "Processing CSV data specifically");
        StringAssert.Contains(result, "Saving result to database");
    }

    [TestMethod]
    public void ProcessCoreOutputsCorrectMessageForCsv()
    {
        CsvDataProcessor processor = new CsvDataProcessor();
        using StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        System.Reflection.MethodInfo? processCoreMethod = typeof(DataProcessor).GetMethod("ProcessCore",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        _ = processCoreMethod.Invoke(processor, null);

        string result = sw.ToString();

        StringAssert.Contains(result, "Processing CSV data specifically");
    }
}

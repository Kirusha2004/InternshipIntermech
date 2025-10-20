namespace Task15.Tests;
[TestClass]
public class XmlDataProcessorTests
{
    [TestMethod]
    public void XmlDataProcessorInheritsFromDataProcessor()
    {
        XmlDataProcessor processor = new XmlDataProcessor();

        Assert.IsInstanceOfType(processor, typeof(DataProcessor));
    }

    [TestMethod]
    public void ProcessDataExecutesFullPipelineForXml()
    {
        XmlDataProcessor processor = new XmlDataProcessor();
        using StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        processor.ProcessData();
        string result = sw.ToString();

        StringAssert.Contains(result, "Reading data from source");
        StringAssert.Contains(result, "Processing XML data with special parser");
        StringAssert.Contains(result, "Saving result to database");
    }

    [TestMethod]
    public void ProcessCoreOutputsCorrectMessageForXml()
    {
        XmlDataProcessor processor = new XmlDataProcessor();
        using StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        System.Reflection.MethodInfo? processCoreMethod = typeof(DataProcessor).GetMethod("ProcessCore",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        _ = processCoreMethod.Invoke(processor, null);

        string result = sw.ToString();

        StringAssert.Contains(result, "Processing XML data with special parser");
    }
}

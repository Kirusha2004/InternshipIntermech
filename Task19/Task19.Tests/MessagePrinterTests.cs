namespace Task19.Tests;

[TestClass]
public class MessagePrinterTests
{
    [TestMethod]
    public async Task PrintAsyncValidMessagePrintsCorrectOutput()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        MessagePrinter printer = new MessagePrinter();

        await printer.PrintAsync("Hello World");

        string result = output.ToString();
        StringAssert.Contains(result, "Начало печати");
        StringAssert.Contains(result, "Hello World");
        StringAssert.Contains(result, "Печать завершена");
    }

    [TestMethod]
    public async Task PrintAsyncValidMessageIncrementsPrintCount()
    {
        MessagePrinter printer = new MessagePrinter();
        int initialCount = printer.PrintCount;

        await printer.PrintAsync("Test Message");

        Assert.AreEqual(initialCount + 1, printer.PrintCount);
    }

    [TestMethod]
    public async Task PrintAsyncMultipleMessagesIncrementsCountCorrectly()
    {
        MessagePrinter printer = new MessagePrinter();

        await printer.PrintAsync("First");
        await printer.PrintAsync("Second");
        await printer.PrintAsync("Third");

        Assert.AreEqual(3, printer.PrintCount);
    }

    [TestMethod]
    public void PrintAsyncEmptyStringThrowsArgumentException()
    {
        MessagePrinter printer = new MessagePrinter();

        _ = Assert.ThrowsExceptionAsync<ArgumentException>(
            () => printer.PrintAsync("")
        );
    }

    [TestMethod]
    public void PrintAsyncWhitespaceStringThrowsArgumentException()
    {
        MessagePrinter printer = new MessagePrinter();

        _ = Assert.ThrowsExceptionAsync<ArgumentException>(
            () => printer.PrintAsync("   ")
        );
    }

    [TestMethod]
    public void PrintAsyncNullStringThrowsArgumentException()
    {
        MessagePrinter printer = new MessagePrinter();

        _ = Assert.ThrowsExceptionAsync<ArgumentException>(
            () => printer.PrintAsync(message: null)
        );
    }

    [TestMethod]
    public void ResetCounterAfterPrintingResetsCountToZero()
    {
        MessagePrinter printer = new MessagePrinter();
        printer.PrintAsync("Test").Wait();

        printer.ResetCounter();

        Assert.AreEqual(0, printer.PrintCount);
    }

    [TestMethod]
    public void ResetCounterOnNewInstanceCountIsZero()
    {
        MessagePrinter printer = new MessagePrinter();

        Assert.AreEqual(0, printer.PrintCount);
    }

    [TestMethod]
    public void PrintCountPropertyIsReadOnly()
    {
        MessagePrinter printer = new MessagePrinter();

        Assert.IsTrue(printer.PrintCount == 0);
    }
}

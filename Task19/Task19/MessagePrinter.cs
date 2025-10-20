namespace Task19;

public class MessagePrinter
{
    public int PrintCount { get; private set; }

    public async Task PrintAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Сообщение не может быть пустым");
        }

        Console.WriteLine("Начало печати");

        await ProcessMessageAsync(message);

        PrintCount++;
        Console.WriteLine("Печать завершена");
    }

    private async Task ProcessMessageAsync(string text)
    {
        await Task.Run(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine(text);
        });
    }

    public void ResetCounter()
    {
        PrintCount = 0;
    }
}

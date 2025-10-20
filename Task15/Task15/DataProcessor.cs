namespace Task15;

public abstract class DataProcessor
{
    public void ProcessData()
    {
        ReadData();
        SaveResult();
        ProcessCore();
    }

    private void ReadData()
    {
        Console.WriteLine("Reading data from source...");
    }

    private void SaveResult()
    {
        Console.WriteLine("Saving result to database...");
    }

    protected abstract void ProcessCore();
}

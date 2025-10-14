namespace Task10;

public class TemperatureConverter
{
    public double Celsius { get; private set; }

    public TemperatureConverter(double celsius)
    {
        Celsius = celsius;
    }

    public double CelsiusToFahrenheit()
    {
        double result = (Celsius * 9 / 5) + 32;
        Console.WriteLine($"CelsiusToFahrenheit: {Celsius}°C = {result}°F");
        return result;
    }

    public void SetCelsius(double celsius)
    {
        Celsius = celsius;
    }

    public override string ToString()
    {
        return $"Temperature: {Celsius}°C ({CelsiusToFahrenheit()}°F)";
    }
}

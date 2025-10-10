using System.Reflection;

namespace ReflectionTaskExtentionProject;

public class TemperatureReflectionService
{
    private readonly object _converterInstance;
    private readonly MethodInfo _conversionMethod;

    public TemperatureReflectionService(Assembly assembly)
    {
        Type? type =
            assembly.GetType("Task10.TemperatureConverter")
            ?? throw new InvalidOperationException(
                "Класс Task10.TemperatureConverter не найден в сборке"
            );

        _converterInstance =
            Activator.CreateInstance(type)
            ?? throw new InvalidOperationException(
                "Не удалось создать экземпляр TemperatureConverter"
            );

        _conversionMethod =
            type.GetMethod("CelsiusToFahrenheit")
            ?? throw new InvalidOperationException("Метод CelsiusToFahrenheit не найден");
    }

    public double ConvertCelsiusToFahrenheit(double celsius)
    {
        try
        {
            object[] parameters = [celsius];
            object? result = _conversionMethod.Invoke(_converterInstance, parameters);
            return result == null ? throw new InvalidOperationException("Метод конвертации вернул null") : (double)result;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                $"Ошибка при вызове метода конвертации: {e.Message}",
                e
            );
        }
    }

    public MethodInfo GetConversionMethodInfo()
    {
        return _conversionMethod;
    }

    public Type GetConverterType()
    {
        return _converterInstance.GetType();
    }
}

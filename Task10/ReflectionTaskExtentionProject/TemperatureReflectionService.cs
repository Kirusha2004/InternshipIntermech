using System.Reflection;

namespace ReflectionTaskExtentionProject;

public class TemperatureReflectionService
{
    private readonly Type _converterType;
    private readonly MethodInfo _conversionMethod;

    public TemperatureReflectionService(Assembly assembly)
    {
        _converterType = assembly.GetType("Task10.TemperatureConverter")
            ?? throw new InvalidOperationException("Класс Task10.TemperatureConverter не найден в сборке");

        _conversionMethod = _converterType.GetMethod("CelsiusToFahrenheit")
            ?? throw new InvalidOperationException("Метод CelsiusToFahrenheit не найден");
    }

    public double ConvertCelsiusToFahrenheit(double celsius)
    {
        try
        {
            object converterInstance = Activator.CreateInstance(_converterType, new object[] { celsius })
                ?? throw new InvalidOperationException("Не удалось создать экземпляр TemperatureConverter");

            object? result = _conversionMethod.Invoke(converterInstance, null);
            return result == null
                ? throw new InvalidOperationException("Метод конвертации вернул null")
                : (double)result;
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
        return _converterType;
    }
}

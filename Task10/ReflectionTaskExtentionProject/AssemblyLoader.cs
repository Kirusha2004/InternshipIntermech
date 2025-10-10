using System.Reflection;

namespace ReflectionTaskExtentionProject;

public class AssemblyLoader
{
    public Assembly LoadAssembly(string assemblyPath)
    {
        try
        {
            return Assembly.LoadFrom(assemblyPath);
        }
        catch (FileNotFoundException e)
        {
            throw new InvalidOperationException($"Файл сборки не найден: {e.Message}", e);
        }
        catch (BadImageFormatException e)
        {
            throw new InvalidOperationException($"Неверный формат сборки: {e.Message}", e);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                $"Ошибка загрузки сборки: {e.Message}",
                e
            );
        }
    }
}

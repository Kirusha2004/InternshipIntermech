using System.Reflection;

namespace ReflectionTaskExtentionProject;

public class AssemblyLoader
{
    public Assembly LoadAssembly(string assemblyPath)
    {
        try
        {
            if (!File.Exists(assemblyPath))
            {
                string currentDir = Directory.GetCurrentDirectory();
                string fullPath = Path.Combine(currentDir, assemblyPath);

                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"Сборка не найдена: {assemblyPath}. Текущая директория: {currentDir}");
                }

                assemblyPath = fullPath;
            }

            Console.WriteLine($"Загружаем сборку из: {assemblyPath}");

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

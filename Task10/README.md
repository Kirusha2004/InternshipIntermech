# Temperature Converter Reflection Project

## Описание проекта

Проект демонстрирует использование рефлексии в C# для работы с внешней сборкой температурного конвертера. Реализована чистая архитектура с разделением ответственности между классами и полным покрытием unit-тестами.

## Архитектура проекта

### Основные компоненты

- **AssemblyLoader** - отвечает за загрузку внешних сборок
- **TemperatureReflectionService** - предоставляет функциональность для работы с температурным конвертером через рефлексию
- **TemperatureConverter** - внешняя библиотека с бизнес-логикой конвертации температур

### Тестирование

Проект включает комплексные unit-тесты с использованием MSTest, покрывающие:
- Загрузку сборок
- Корректность конвертации температур
- Работу рефлексии
- Обработку ошибок

## Требования

- .NET 6.0 или выше
- MSTest.TestFramework
- Внешняя сборка Task10.dll

## Использование

```csharp
var assemblyLoader = new AssemblyLoader();
var assembly = assemblyLoader.LoadAssembly("Task10.dll");
var reflectionService = new TemperatureReflectionService(assembly);

double fahrenheit = reflectionService.ConvertCelsiusToFahrenheit(25);
```

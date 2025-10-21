# Task11 - Расширенный рефлектор типов

## Описание
Программа-рефлектор для анализа типов C# с возможностью:
- Выбора отображаемых членов (методы, свойства)
- Выводом информации об атрибутах типов и членов

## Ключевые особенности
- **MemberTypesToShow**: enum с флагами для выбора методов/свойств
- **TypeReflector**: класс рефлексии с зависимостями через конструктор
- **SampleClass**: демонстрационный класс с поведением и атрибутами `[Obsolete]`

## Использование
```csharp
var reflector = new TypeReflector(typeof(SampleClass), 
    MemberTypesToShow.Methods | MemberTypesToShow.Properties);
string info = reflector.GetReflectionInfo();
```

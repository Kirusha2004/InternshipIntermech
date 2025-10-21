# Парсер телефонной книги

## Описание
Проект содержит реализацию парсера телефонной книги из XML файла и соответствующие unit-тесты.

## Функциональность
- Чтение XML файла с контактами
- Извлечение телефонных номеров из структуры XML
- Возврат списка номеров в строковом формате

## Структура проекта
- `TelephoneBookParser.cs` - основной класс парсера
- `TelephoneBookParserTests.cs` - unit-тесты для проверки функциональности

## Использование
```csharp
List<string> numbers = TelephoneBookParser.ParseTelephoneNumbers("path/to/TelephoneBook.xml");
```

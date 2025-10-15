Проект для преобразования объектов C# в XML файлы.

### Основные компоненты

- **IXmlSerializationService** - интерфейс сервиса сериализации
- **XmlSerializationService** - реализация сериализации в XML
- **IPersonManager** - интерфейс управления сериализацией персонала
- **PersonManager** - менеджер для работы с сериализацией
- **Person** - модель данных с сериализацией в XML элементы
- **PersonAsAttributes** - модель данных с сериализацией в XML атрибуты

### Использование

```csharp
var serializationService = new XmlSerializationService();
var personManager = new PersonManager(serializationService);

// Сериализация в элементы
var person = new Person("John", 25);
personManager.SerializePerson(person, "person.xml");

// Сериализация в атрибуты
var personAttr = new PersonAsAttributes("Jane", 30);
personManager.SerializePersonAsAttributes(personAttr, "person_attr.xml");
```

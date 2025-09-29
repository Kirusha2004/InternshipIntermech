# Задание 4: Коллекция квадратов нечетных чисел

## Описание задачи
Создать метод, который принимает массив целых чисел и возвращает коллекцию квадратов всех нечетных чисел массива. Для формирования коллекции используется оператор yield.

## Решение
- Создан статический класс `MathOperations` с методом `GetSquaresOfOddNumbers`
- Метод использует `yield return` для ленивого вычисления квадратов
- Реализована фильтрация нечетных чисел и их возведение в квадрат
- Поддержка всех граничных случаев и особых ситуаций

## Примеры использования
```csharp
// Базовое использование
int[] numbers = [1, 2, 3, 4, 5, 6];
IEnumerable<int> squares = MathOperations.GetSquaresOfOddNumbers(numbers);
// Результат: 1, 9, 25

// Отрицательные числа
int[] negativeNumbers = [-3, -2, -1, 0, 1, 2, 3];
IEnumerable<int> negativeSquares = MathOperations.GetSquaresOfOddNumbers(negativeNumbers);
// Результат: 9, 1, 1, 9

// Lazy evaluation
var collection = MathOperations.GetSquaresOfOddNumbers(numbers);
foreach (var square in collection)
{
    Console.WriteLine(square); // Вычисляется по мере необходимости
}
```

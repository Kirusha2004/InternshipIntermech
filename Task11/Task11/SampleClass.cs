namespace Task11;

[Obsolete("Класс устарел, используйте новый класс в будущем")]
public class SampleClass
{
    private int _myProperty;

    [Obsolete("Не использовать, заменено новым свойством")]
    public int MyProperty
    {
        get => _myProperty;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Значение не может быть отрицательным");
            }

            _myProperty = value;
        }
    }

    [Obsolete("Устарел, используйте новый метод")]
    public void MyMethod()
    {
        MyProperty += 10;
    }

    public bool IsPropertyInRange(int min, int max)
    {
        return _myProperty >= min && _myProperty <= max;
    }
}


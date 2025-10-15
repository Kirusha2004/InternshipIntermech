namespace Task12;

public class Person
{
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public Person() { Name = string.Empty; }


    public string Name { get; set; }
    public int Age { get; set; }

}


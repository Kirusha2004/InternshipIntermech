namespace Task18;

public class FileCreator : IFileCreator
{
    public void CreateFile()
    {
        if (!File.Exists("text1.txt"))
        {
            File.WriteAllText("text1.txt", "Это содержимое первого файла\nВторая строка text1.txt");
        }

        if (!File.Exists("text2.txt"))
        {
            File.WriteAllText("text2.txt", "А это второй файл\nЕще одна строка\nИ третья строка text2.txt");
        }
    }
}

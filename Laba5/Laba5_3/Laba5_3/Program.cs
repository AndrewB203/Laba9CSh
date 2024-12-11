namespace Laba5_3;

public class Program
{
    public static void Main()
    {
        // Создание экземпляра словаря
        MyDictionary<string, int> myDict = new MyDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };

        // Добавление элемента
        myDict.Add("four", 4);

        // Получение элемента по ключу
        Console.WriteLine(myDict["two"]); // Вывод: 2

        // Получение количества элементов
        Console.WriteLine(myDict.Count); // Вывод: 4

        // Перебор элементов с использованием foreach
        foreach (var kvp in myDict)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
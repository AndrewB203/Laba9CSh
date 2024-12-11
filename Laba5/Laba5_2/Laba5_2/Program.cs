namespace Laba5_2;
public class Program
{
    public static void Main()
    {
        // Инициализация коллекции с помощью инициализатора
        MyList<int> myList = new MyList<int> { 1, 2, 3, 4 };

        // Добавление элемента
        myList.Add(5);

        // Получение элемента по индексу
        Console.WriteLine(myList[2]); // Вывод: 3

        // Получение количества элементов
        Console.WriteLine(myList.Count); // Вывод: 5
    }
}
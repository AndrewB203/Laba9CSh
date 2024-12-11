namespace laba4_3;

class Program
{
    static void Main()
    {
        // Создаем массив элементов Car
        Car[] cars = new Car[]
        {
            new Car("BMW", 2020, 250),
            new Car("Audi", 2018, 240),
            new Car("Mercedes", 2021, 260),
            new Car("Ford", 2019, 230)
        };

        // Создаем экземпляр CarCatalog
        CarCatalog catalog = new CarCatalog(cars);

        // Прямой проход с первого элемента до последнего
        Console.WriteLine("Прямой проход:");
        foreach (var car in catalog)
        {
            Console.WriteLine(car);
        }

        // Обратный проход от последнего к первому
        Console.WriteLine("\nОбратный проход:");
        foreach (var car in catalog.GetReverseEnumerator())
        {
            Console.WriteLine(car);
        }

        // Проход по элементам массива с фильтром по году выпуска
        Console.WriteLine("\nПроход с фильтром по году выпуска (2020):");
        foreach (var car in catalog.GetFilteredByYearEnumerator(2020))
        {
            Console.WriteLine(car);
        }

        // Проход по элементам массива с фильтром по максимальной скорости
        Console.WriteLine("\nПроход с фильтром по максимальной скорости (240):");
        foreach (var car in catalog.GetFilteredBySpeedEnumerator(240))
        {
            Console.WriteLine(car);
        }
    }
}
using Laba4_2;

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

        // Сортировка по названию
        Array.Sort(cars, new CarComparer(CarComparer.SortBy.Name));
        Console.WriteLine("Сортировка по названию:");
        PrintCars(cars);

        // Сортировка по году выпуска
        Array.Sort(cars, new CarComparer(CarComparer.SortBy.ProductionYear));
        Console.WriteLine("\nСортировка по году выпуска:");
        PrintCars(cars);

        // Сортировка по максимальной скорости
        Array.Sort(cars, new CarComparer(CarComparer.SortBy.MaxSpeed));
        Console.WriteLine("\nСортировка по максимальной скорости:");
        PrintCars(cars);
    }

    static void PrintCars(Car[] cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
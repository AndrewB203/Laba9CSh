using System;
using System.Collections;
using System.Collections.Generic;

// Класс Car из задания №2
public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name} ({ProductionYear}) - Max Speed: {MaxSpeed} km/h";
    }
}

// Класс CarCatalog с различными итераторами
public class CarCatalog : IEnumerable<Car>
{
    private Car[] _cars;

    public CarCatalog(Car[] cars)
    {
        _cars = cars;
    }

    // Прямой проход с первого элемента до последнего
    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            yield return _cars[i];
        }
    }

    // Обратный проход от последнего к первому
    public IEnumerable<Car> GetReverseEnumerator()
    {
        for (int i = _cars.Length - 1; i >= 0; i--)
        {
            yield return _cars[i];
        }
    }

    // Проход по элементам массива с фильтром по году выпуска
    public IEnumerable<Car> GetFilteredByYearEnumerator(int year)
    {
        foreach (var car in _cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    // Проход по элементам массива с фильтром по максимальной скорости
    public IEnumerable<Car> GetFilteredBySpeedEnumerator(int speed)
    {
        foreach (var car in _cars)
        {
            if (car.MaxSpeed == speed)
            {
                yield return car;
            }
        }
    }

    // Явная реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

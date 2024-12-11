using System;
using System.Collections.Generic;
namespace Laba4_2;

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

// Класс CarComparer для сортировки массива Car
public class CarComparer : IComparer<Car>
{
    public enum SortBy
    {
        Name,
        ProductionYear,
        MaxSpeed
    }

    private SortBy _sortBy;

    public CarComparer(SortBy sortBy)
    {
        _sortBy = sortBy;
    }

    public int Compare(Car x, Car y)
    {
        switch (_sortBy)
        {
            case SortBy.Name:
                return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            case SortBy.ProductionYear:
                return x.ProductionYear.CompareTo(y.ProductionYear);
            case SortBy.MaxSpeed:
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default:
                throw new ArgumentException("Неизвестный критерий сортировки");
        }
    }
}

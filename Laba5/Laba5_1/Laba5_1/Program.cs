namespace Laba5_1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите минимальное значение для генерации случайных чисел:");
        int minValue = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение для генерации случайных чисел:");
        int maxValue = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество строк матрицы:");
        int rows = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы:");
        int cols = int.Parse(Console.ReadLine());

        MyMatrix matrix = new MyMatrix(rows, cols, minValue, maxValue);

        Console.WriteLine("Исходная матрица:");
        matrix.Show();

        Console.WriteLine("Изменение размера матрицы:");
        matrix.ChangeSize(5, 5);
        matrix.Show();

        Console.WriteLine("Частичный вывод матрицы:");
        matrix.ShowPartialy(1, 3, 1, 3);

        Console.WriteLine("Изменение элемента матрицы через индексатор:");
        matrix[0, 0] = 100;
        matrix.Show();
    }
}
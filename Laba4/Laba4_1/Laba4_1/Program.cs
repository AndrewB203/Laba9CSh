namespace Laba4_1;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество строк матрицы A: ");
        int rowsa = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы A: ");
        int colsa = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество строк матрицы B: ");
        int rowsb = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы B: ");
        int colsb = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите минимальное значение для случайного числа:");
        int minValue = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение для случайного числа:");
        int maxValue = int.Parse(Console.ReadLine());

        MyMatrix matrix1 = new MyMatrix(rowsa, colsa, minValue, maxValue);
        MyMatrix matrix2 = new MyMatrix(rowsb, colsb, minValue, maxValue);

        Console.WriteLine("Матрица 1:");
        matrix1.PrintMatrix();

        Console.WriteLine("Матрица 2:");
        matrix2.PrintMatrix();

        MyMatrix sumMatrix = matrix1 + matrix2;
        Console.WriteLine("Сумма матриц:");
        sumMatrix.PrintMatrix();

        MyMatrix diffMatrix = matrix1 - matrix2;
        Console.WriteLine("Разность матриц:");
        diffMatrix.PrintMatrix();

        MyMatrix multMatrix = matrix1 * matrix2;
        Console.WriteLine("Произведение матриц:");
        multMatrix.PrintMatrix();

        MyMatrix multByScalarMatrix = matrix1 * 2;
        Console.WriteLine("Умножение матрицы на число:");
        multByScalarMatrix.PrintMatrix();

        MyMatrix divByScalarMatrix = matrix1 / 2;
        Console.WriteLine("Деление матрицы на число:");
        divByScalarMatrix.PrintMatrix();
    }
}
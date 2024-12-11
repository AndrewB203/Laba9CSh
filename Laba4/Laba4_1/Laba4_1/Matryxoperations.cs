using System;

public class MyMatrix
{
    private int[,] matrix;
    private int rows;
    private int cols;

    // Конструктор, принимающий число строк и столбцов, заполняющий матрицу случайными числами
    public MyMatrix(int rows, int cols, int minValue, int maxValue)
    {
        this.rows = rows;
        this.cols = cols;
        matrix = new int[rows, cols];
        Random rand = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rand.Next(minValue, maxValue + 1);
            }
        }
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                throw new IndexOutOfRangeException("Индексы выходят за пределы матрицы");
            }
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                throw new IndexOutOfRangeException("Индексы выходят за пределы матрицы");
            }
            matrix[row, col] = value;
        }
    }

    // Оператор сложения матриц
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new ArgumentException("Размеры матриц должны совпадать для сложения");
        }

        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    // Оператор вычитания матриц
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new ArgumentException("Размеры матриц должны совпадать для вычитания");
        }

        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    // Оператор умножения матриц
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.cols != b.rows)
        {
            throw new ArgumentException("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы для умножения");
        }

        MyMatrix result = new MyMatrix(a.rows, b.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.cols; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < a.cols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    // Оператор умножения матрицы на число
    public static MyMatrix operator *(MyMatrix a, int scalar)
    {
        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] * scalar;
            }
        }
        return result;
    }

    // Оператор деления матрицы на число
    public static MyMatrix operator /(MyMatrix a, int scalar)
    {
        if (scalar == 0)
        {
            throw new DivideByZeroException("Деление на ноль невозможно");
        }

        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] / scalar;
            }
        }
        return result;
    }

    // Метод для вывода матрицы на экран
    public void PrintMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
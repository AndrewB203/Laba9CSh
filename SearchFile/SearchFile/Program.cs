using System;
using System.IO;
using System.IO.Compression;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь для поиска:");
        string searchPath = Console.ReadLine();

        Console.WriteLine("Введите имя файла для поиска:");
        string fileName = Console.ReadLine();

        SearchFile(searchPath, fileName);
    }

    static void SearchFile(string path, string fileName)
    {
        try
        {
            // Ищем файл в текущей директории
            string[] files = Directory.GetFiles(path, fileName, SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    Console.WriteLine($"Найден файл: {file}");

                    // Выводим содержимое файла на консоль
                    DisplayFileContent(file);

                    // Сжимаем файл
                    CompressFile(file);
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DisplayFileContent(string filePath)
    {
        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    Console.WriteLine("Содержимое файла:");
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    static void CompressFile(string filePath)
    {
        try
        {
            string compressedFilePath = filePath + ".gz";

            using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }

            Console.WriteLine($"Файл успешно сжат: {compressedFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сжатии файла: {ex.Message}");
        }
    }
}
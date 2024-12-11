using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace StockMarketApp
{
    class Program
    {
        public static async Task<Dictionary<DateTime, double>> GetTickerPrices(string uri)
        {
            Dictionary<DateTime, double> result = new();
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string mdString = await response.Content.ReadAsStringAsync();
                MarketData md = JsonConvert.DeserializeObject<MarketData>(mdString);
                if (md is not null)
                {
                    for (int i = 0; i < md.t.Count(); i++)
                    {
                        DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(md.t[i]);//Перевод в секунды 
                        DateTime timeValue = dtOffset.UtcDateTime;

                        double price = md.c[i];

                        result.Add(timeValue, price);
                    }
                }
            }
            return result;
        }

        static async Task Main(string[] args)
        {
            // Чтение тикеров из файла
            List<string> tickers = ReadTickersFromFile("tickers.txt");

            // Загрузка данных и сохранение в базу данных
            await LoadDataAsync(tickers);

            // Проверка данных в базе данных
            using (var db = new StockMarketContext())
            {
                var stocks = db.Stocks.ToList();
                Console.WriteLine("Данные в таблице Stocks:");
                foreach (var stock in stocks)
                {
                    Console.WriteLine($"Id: {stock.Id}, Ticker: {stock.Ticker}, Name: {stock.Name}, Price: {stock.Price}, Date: {stock.Date}");
                }

                var todaysConditions = db.TodaysCondition.Include(tc => tc.Stock).ToList();
                Console.WriteLine("Данные в таблице TodaysCondition:");
                foreach (var condition in todaysConditions)
                {
                    Console.WriteLine($"Id: {condition.Id}, StockId: {condition.StockId}, PriceChange: {condition.PriceChange}, Ticker: {condition.Stock.Ticker}");
                }
            }

            // Запрос пользователя
            Console.Write("Введите тикер акции: ");
            string ticker = Console.ReadLine();

            // Получение состояния акции на сегодняшний день
            try
            {
                if (string.IsNullOrEmpty(ticker))
                {
                    Console.WriteLine("Некорректный тикер.");
                    return;
                }                                      

                using (var db = new StockMarketContext())
                {
                    var query = db.TodaysCondition
                        .Include(tc => tc.Stock)
                        .Where(tc => tc.Stock.Ticker == ticker);

                    //Console.WriteLine(query.ToQueryString()); // Выводит SQL-запрос для отладки

                    var todayCondition = query.FirstOrDefault();

                    if (todayCondition != null)
                    {
                        string condition = todayCondition.PriceChange > 0 ? "выросла" : "упала";
                        Console.WriteLine($"Акция {ticker} {condition} на {todayCondition.PriceChange:F2} пунктов.");
                    }
                    else
                    {
                        Console.WriteLine($"Акция с тикером {ticker} не найдена.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static List<string> ReadTickersFromFile(string filePath)
        {
            List<string> tickers = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        tickers.Add(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла тикеров: {ex.Message}");
            }
            return tickers;
        }

        static async Task LoadDataAsync(List<string> tickers)
        {
            using (var db = new StockMarketContext())
            {
                // Создание и применение миграций
                db.Database.Migrate();

                foreach (var ticker in tickers)
                {
                    var md = await GetTickerPrices($"https://api.marketdata.app/v1/stocks/candles/D/{ticker}/?from=2024-11-04&to=2024-11-06&token=Ti1Vc1JHSVFDWmU4QW5PMlhEOE8tTVJPZHVacmd6aTJ6VG1jTmI2bC1vbz0");

                    foreach (var data in md)
                    {
                        Stock stock = new() { Date = data.Key, Name = ticker, Price = Convert.ToDecimal(data.Value), Ticker = ticker };
                        db.Stocks.Add(stock);
                    }

                    await db.SaveChangesAsync();
                }

                // Анализ данных и заполнение TodaysCondition
                var stocks = db.Stocks.ToList();
                foreach (var stock in stocks)
                {
                    var yesterdayStock = stocks.FirstOrDefault(s => s.Ticker == stock.Ticker && s.Date == stock.Date.AddDays(-1));
                    if (yesterdayStock != null)
                    {
                        var todaysCondition = new TodaysCondition
                        {
                            StockId = stock.Id,
                            PriceChange = stock.Price - yesterdayStock.Price
                        };

                        db.TodaysCondition.Add(todaysCondition);
                        Console.WriteLine($"Добавлена запись в TodaysCondition: StockId: {todaysCondition.StockId}, PriceChange: {todaysCondition.PriceChange}");
                    }
                }

                await db.SaveChangesAsync();
            }
        }
    }

    public class StockMarketContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<TodaysCondition> TodaysCondition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=stockmarket.db");
        }
    }

    public class MarketData
    {
        public string s { get; set; }
        public int[] t { get; set; }
        public double[] o { get; set; }
        public double[] h { get; set; }
        public double[] l { get; set; }
        public double[] c { get; set; }
        public int[] v { get; set; }
    }

    public class Stock
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }

    public class TodaysCondition
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal PriceChange { get; set; }

        public Stock Stock { get; set; }
    }
}

/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;


namespace StockMarketApp
{
    class Program
    {

        public static async Task<Dictionary<DateTime, double>> GetTickerPrices(string uri)
        {
            Dictionary<DateTime, double> result = new();
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string mdString = await response.Content.ReadAsStringAsync();
                MarketData md = JsonConvert.DeserializeObject<MarketData>(mdString);
                if (md is not null)
                {
                    for(int i =0;i<md.t.Count();i++)
                    {
                        DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(md.t[i]);
                        DateTime timeValue = dtOffset.UtcDateTime;

                        double price = md.c[i];

                        result.Add(timeValue, price);
                    }
                }
            }
            return result;

        }
        static  async Task Main(string[] args)
        {

            var md = await GetTickerPrices("https://api.marketdata.app/v1/stocks/candles/D/X/?from=2024-11-04&to=2024-11-06&token=Ti1Vc1JHSVFDWmU4QW5PMlhEOE8tTVJPZHVacmd6aTJ6VG1jTmI2bC1vbz0");


            // Создание и применение миграций
            using (var db = new StockMarketContext())
            {
                db.Database.Migrate();
                foreach(var data in md)
                {
                    Stock stock = new() { Date=data.Key,Name="X",Price=Convert.ToDecimal(data.Value),Ticker="X"};
                    db.Stocks.Add(stock);
                    db.SaveChanges();
                }

                var stocks = db.Stocks;
                if(stocks.Any())
                {
                    foreach(var stock in stocks)
                    {
                        Console.WriteLine($"{stock.Name}  {stock.Price}");
                    }
                }
            }

            // Чтение тикеров из файла
            List<string> tickers = ReadTickersFromFile("tickers.txt");

            // Загрузка тестовых данных и сохранение в базу данных
            await LoadTestDataAsync(tickers);

            // Проверка данных в базе данных
            using (var db = new StockMarketContext())
            {
                var stocks = db.Stocks.ToList();
                Console.WriteLine("Данные в таблице Stocks:");
                foreach (var stock in stocks)
                {
                    Console.WriteLine($"Id: {stock.Id}, Ticker: {stock.Ticker}, Name: {stock.Name}, Price: {stock.Price}, Date: {stock.Date}");
                }

                var todaysConditions = db.TodaysCondition.Include(tc => tc.Stock).ToList();
                Console.WriteLine("Данные в таблице TodaysCondition:");
                foreach (var condition in todaysConditions)
                {
                    Console.WriteLine($"Id: {condition.Id}, StockId: {condition.StockId}, PriceChange: {condition.PriceChange}, Ticker: {condition.Stock.Ticker}");
                }
            }

            // Запрос пользователя
            Console.Write("Введите тикер акции: ");
            string ticker = Console.ReadLine();

            // Получение состояния акции на сегодняшний день
            try
            {
                if (string.IsNullOrEmpty(ticker))
                {
                    Console.WriteLine("Некорректный тикер.");
                    return;
                }

                using (var db = new StockMarketContext())
                {
                    var query = db.TodaysCondition
                        .Include(tc => tc.Stock)
                        .Where(tc => tc.Stock.Ticker == ticker);

                    Console.WriteLine(query.ToQueryString()); // Выводит SQL-запрос для отладки

                    var todayCondition = query.FirstOrDefault();

                    if (todayCondition != null)
                    {
                        string condition = todayCondition.PriceChange > 0 ? "выросла" : "упала";
                        Console.WriteLine($"Акция {ticker} {condition} на {todayCondition.PriceChange:F2} пунктов.");
                    }
                    else
                    {
                        Console.WriteLine($"Акция с тикером {ticker} не найдена.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static List<string> ReadTickersFromFile(string filePath)
        {
            List<string> tickers = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        tickers.Add(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла тикеров: {ex.Message}");
            }
            return tickers;
        }

        static async Task LoadTestDataAsync(List<string> tickers)
        {
            using (var db = new StockMarketContext())
            {
                // Проверка наличия данных
                if (db.Stocks.Any())
                {
                    Console.WriteLine("Данные уже загружены.");
                    return;
                }

                // Тестовые данные
                var testData = new List<Stock>
                {
                    new Stock { Ticker = "AAPL", Name = "Apple Inc.", Price = 150.0m, Date = DateTime.Now },
                    new Stock { Ticker = "AAPL", Name = "Apple Inc.", Price = 145.0m, Date = DateTime.Now.AddDays(-1) },
                    new Stock { Ticker = "MSFT", Name = "Microsoft Corporation", Price = 300.0m, Date = DateTime.Now },
                    new Stock { Ticker = "MSFT", Name = "Microsoft Corporation", Price = 295.0m, Date = DateTime.Now.AddDays(-1) }
                };

                // Добавление тестовых данных в базу данных
                foreach (var stock in testData)
                {
                    if (tickers.Contains(stock.Ticker))
                    {
                        db.Stocks.Add(stock);
                    }
                }

                await db.SaveChangesAsync();

                // Анализ данных и заполнение TodaysCondition
                var stocks = db.Stocks.ToList();
                foreach (var stock in stocks)
                {
                    var yesterdayStock = stocks.FirstOrDefault(s => s.Ticker == stock.Ticker && s.Date == stock.Date.AddDays(-1));
                    if (yesterdayStock != null)
                    {
                        var todaysCondition = new TodaysCondition
                        {
                            StockId = stock.Id,
                            PriceChange = stock.Price - yesterdayStock.Price
                        };

                        db.TodaysCondition.Add(todaysCondition);
                        Console.WriteLine($"Добавлена запись в TodaysCondition: StockId: {todaysCondition.StockId}, PriceChange: {todaysCondition.PriceChange}");
                    }
                }

                await db.SaveChangesAsync();
            }
        }
    }



    public class StockMarketContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<TodaysCondition> TodaysCondition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=stockmarket.db");
        }
    }
    public class MarketData
    {
        public string s { get; set; }
        public int[] t { get; set; }
        public double[] o { get; set; }
        public double[] h { get; set; }
        public double[] l { get; set; }
        public double[] c { get; set; }
        public int[] v { get; set; }
    }
    public class Stock
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }

    public class TodaysCondition
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal PriceChange { get; set; }

        public Stock Stock { get; set; }
    }
}*/
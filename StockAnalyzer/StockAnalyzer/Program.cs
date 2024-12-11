using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StockAnalyzer.Models;

namespace StockAnalyzer
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            // Загрузка конфигурации
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            // Чтение тикеров из файла
            var tickers = TickerReader.ReadTickers("tickers.txt");

            // Загрузка данных о ценах акций
            await LoadStockPrices(tickers);

            // Анализ данных о ценах акций
            await AnalyzeStockPrices();

            Console.WriteLine("Enter a ticker to check the condition:");
            string ticker = Console.ReadLine();
            var condition = GetTodaysCondition(ticker);
            Console.WriteLine($"The stock {ticker} has {condition} today.");
        }

        private static async Task LoadStockPrices(List<string> tickers)
        {
            string apiKey = _configuration["MarketDataApi:ApiKey"];
            string apiUrl = "https://www.marketdata.app";

            foreach (var ticker in tickers)
            {
                try
                {
                    var requestUrl = $"https://api.marketdata.app/v1/stocks/candles/D/AAPL/?from=2020-01-01&to=2020-01-03&token={apiKey}";
                    var response = await client.GetStringAsync(requestUrl);

                    // Проверка на наличие ошибок в ответе
                    if (response.StartsWith("<"))
                    {
                        Console.WriteLine($"Error parsing JSON for {ticker}: Unexpected character encountered while parsing value: <. Path '', line 0, position 0.");
                        continue;
                    }

                    var json = JObject.Parse(response);

                    using (var context = new StockDbContext())
                    {
                        foreach (var stock in json["s"])
                        {
                            var stockPrice = new StockPrice
                            {
                                Ticker = stock["ticker"].ToString(),
                                Price = decimal.Parse(stock["price"].ToString()),
                                Date = DateTimeOffset.Parse(stock["date"].ToString())
                            };
                            context.StockPrices.Add(stockPrice);
                        }
                        await context.SaveChangesAsync();
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error loading stock prices for {ticker}: {ex.Message}");
                }
                catch (Newtonsoft.Json.JsonReaderException ex)
                {
                    Console.WriteLine($"Error parsing JSON for {ticker}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error for {ticker}: {ex.Message}");
                }
            }
        }

        private static async Task AnalyzeStockPrices()
        {
            using (var context = new StockDbContext())
            {
                var stocks = await context.StockPrices.ToListAsync();
                foreach (var stock in stocks.GroupBy(s => s.Ticker))
                {
                    var latestPrices = stock.OrderByDescending(s => s.Date).Take(2).ToList();
                    if (latestPrices.Count == 2)
                    {
                        var condition = latestPrices[0].Price > latestPrices[1].Price ? "risen" : "fallen";
                        var todaysCondition = new TodaysCondition
                        {
                            Ticker = stock.Key,
                            Condition = condition,
                            Date = DateTimeOffset.UtcNow
                        };
                        context.TodaysConditions.Add(todaysCondition);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        private static string GetTodaysCondition(string ticker)
        {
            using (var context = new StockDbContext())
            {
                var condition = context.TodaysConditions
                    .Where(c => c.Ticker == ticker && c.Date.Date == DateTimeOffset.UtcNow.Date)
                    .OrderByDescending(c => c.Date)
                    .FirstOrDefault();

                return condition?.Condition ?? "no data";
            }
        }
    }
}
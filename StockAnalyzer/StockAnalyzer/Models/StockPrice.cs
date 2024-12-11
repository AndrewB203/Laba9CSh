using System;

namespace StockAnalyzer.Models
{
    public class StockPrice
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
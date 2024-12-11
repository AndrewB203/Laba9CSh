using System;

namespace StockAnalyzer.Models
{
    public class TodaysCondition
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Condition { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
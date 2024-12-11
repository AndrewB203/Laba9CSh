using System.Collections.Generic;
using System.IO;

namespace StockAnalyzer
{
    public static class TickerReader
    {
        public static List<string> ReadTickers(string filePath)
        {
            var tickers = new List<string>();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tickers.Add(line.Trim());
                }
            }
            return tickers;
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Stock
{
    [Key]
    public string Ticker { get; set; }
    public decimal Price { get; set; }
}

public class StockMarketContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=stockmarket.db");
    }

    public void SeedData()
    {
        if (!Stocks.Any())
        {
            Stocks.AddRange(
                new Stock { Ticker = "AAPL", Price = 150.00m },
                new Stock { Ticker = "GOOGL", Price = 2800.00m },
                new Stock { Ticker = "MSFT", Price = 300.00m }
            );
            SaveChanges();
        }
    }
}

public class TcpServer
{
    private TcpListener _listener;

    public TcpServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
    }

    public void Start()
    {
        using (var context = new StockMarketContext())
        {
            context.Database.EnsureCreated();
            context.SeedData();
        }

        _listener.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        while (true)
        {
            TcpClient client = _listener.AcceptTcpClient();
            Task.Run(() => HandleClient(client));
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        using (var context = new StockMarketContext())
        {
            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string ticker = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            var stock = await context.Stocks.FirstOrDefaultAsync(s => s.Ticker == ticker);
            string response = stock != null ? stock.Price.ToString() : "Ticker not found";

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }

        client.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        TcpServer server = new TcpServer(5000);
        server.Start();
    }
}
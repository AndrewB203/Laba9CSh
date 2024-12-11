using System;
using System.Net.Sockets;
using System.Text;

public class TcpClient
{
    private string _serverIp;
    private int _port;

    public TcpClient(string serverIp, int port)
    {
        _serverIp = serverIp;
        _port = port;
    }

    public string RequestPrice(string ticker)
    {
        using (var client = new System.Net.Sockets.TcpClient(_serverIp, _port))
        {
            var stream = client.GetStream();
            byte[] requestBytes = Encoding.UTF8.GetBytes(ticker);
            stream.Write(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        Console.Write("Enter ticker: ");
        string ticker = Console.ReadLine();
        string price = client.RequestPrice(ticker);
        Console.WriteLine($"Price for {ticker}: {price}");
    }
}
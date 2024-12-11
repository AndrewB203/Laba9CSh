/*using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public struct Weather
{
    public string Country { get; set; }
    public string Name { get; set; }
    public double Temp { get; set; }
    public string Description { get; set; }
}

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
    }

    public async Task<Weather> GetWeatherAsync(double latitude, double longitude)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherData>(json);

        if (weatherData.Sys.Country == null || weatherData.Name == null)
        {
            throw new Exception("Invalid coordinates");
        }

        return new Weather
        {
            Country = weatherData.Sys.Country,
            Name = weatherData.Name,
            Temp = weatherData.Main.Temp,
            Description = weatherData.Weather[0].Description
        };
    }

    private class WeatherData
    {
        public MainData Main { get; set; }
        public WeatherDescription[] Weather { get; set; }
        public SysData Sys { get; set; }
        public string Name { get; set; }
    }

    private class MainData
    {
        public double Temp { get; set; }
    }

    private class WeatherDescription
    {
        public string Description { get; set; }
    }

    private class SysData
    {
        public string Country { get; set; }
    }
}*/
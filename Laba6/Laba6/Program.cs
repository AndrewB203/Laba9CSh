using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

class Weather
{
    public string Country { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Temp { get; set; }
}

class Program
{
    private static readonly HttpClient browser = new HttpClient();

    static async Task Main()
    {
        List<Weather> spisok = new List<Weather>();
        Random rnd = new Random();
        //int lon = rnd.Next(-180, 180);// Долгота
        //int lat = rnd.Next(-90, 90 );//Широта

        for (int i = 0; i < 10; i++)
        {
            int lon = rnd.Next(-180, 180);// Долгота
            int lat = rnd.Next(-90, 90);//Широта

            //var response = await browser.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=5ef065fcbf967a9246eb757c0fc22a0b");
            var response = await browser.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=bdb5e1b353de9900d4b7d5cffc3d9109");
            if (response.IsSuccessStatusCode == false)
            {
                return;
            }
            var jsonstr = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(jsonstr);
            if (json.SelectToken($"sys.country")==null)
                continue;
            Weather syn = new Weather();
            syn.Country = json.SelectToken($"sys.country").ToString();
            syn.Name = json.SelectToken($"name").ToString();
            syn.Description = json.SelectToken($"weather[0].description").ToString();
            syn.Temp = json.SelectToken($"main.temp").Value<float>();
            spisok.Add(syn);

        }
        var selecteTemp = spisok.OrderBy(p => p.Temp);
        Console.WriteLine($"min: {spisok[0].Temp}");
        Console.WriteLine($"max: {spisok[spisok.Count - 1].Temp}");

        var selectmidTemp = spisok.Select(p => p.Temp).Average();
        Console.WriteLine($"midTemp: {selectmidTemp}");

        var NumofCountry = spisok.GroupBy(p => p.Country).Count();
        Console.WriteLine($"Number: {NumofCountry}");

        var condition = spisok.Where(p => p.Description.Contains("clear sky") || p.Description.Contains("rain") || p.Description.Contains("few clouds"));
        var result = condition.ToList();
        if (result.Count() >= 0)
        {
            Console.WriteLine($"Country: {result[0].Country}; Name: {result[0].Name}");
        }
    }
}
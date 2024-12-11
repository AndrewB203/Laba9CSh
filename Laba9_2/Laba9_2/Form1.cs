using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Laba9_2
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiKey = "e408d66774ed8dc5b1041e9b829c0658";

        public Form1()
        {
            InitializeComponent();
            LoadCities();
        }

        private async void LoadCities()
        {
            var cities = await File.ReadAllLinesAsync("city.txt");
            foreach (var city in cities)
            {
                listBoxCities.Items.Add(city);
            }
        }

        private async void buttonLoadWeather_Click(object sender, EventArgs e)
        {
            if (listBoxCities.SelectedItem == null)
            {
                MessageBox.Show("Please select a city.");
                return;
            }

            var selectedCity = listBoxCities.SelectedItem.ToString();
            var weather = await GetWeatherAsync(selectedCity);
            labelWeather.Text = $"Weather in {selectedCity}: {weather.Temperature}°C, {weather.Description}";
        }

        private async Task<WeatherData> GetWeatherAsync(string city)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to get weather data.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return new WeatherData
            {
                Temperature = data.Main.Temp,
                Description = data.Weather[0].Description
            };
        }

        private class WeatherResponse
        {
            public MainData Main { get; set; }
            public List<WeatherData> Weather { get; set; }
        }

        private class MainData
        {
            public double Temp { get; set; }
        }

        private class WeatherData
        {
            public double Temperature { get; set; }
            public string Description { get; set; }
        }
    }
}
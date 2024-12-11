using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NorthwindClient.Models;

namespace NorthwindClient.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7010/api/");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("Products");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<Product> GetProductAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Product>($"Products/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> CreateProductAsync(Product product)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync("Products", product);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateProductAsync(int id, Product product)
        {
            try
            {
                return await _httpClient.PutAsJsonAsync($"Products/{id}", product);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteProductAsync(int id)
        {
            try
            {
                return await _httpClient.DeleteAsync($"Products/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
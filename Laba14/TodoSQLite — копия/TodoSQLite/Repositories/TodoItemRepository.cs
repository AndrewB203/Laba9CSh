using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TodoSQLite.Models;
using TodoSQLite.Data;
using System.Text.Json;

namespace TodoSQLite.Repositories
{
    public class TodoItemRepository
    {
        private readonly TodoItemDatabase _localDatabase;
        private readonly HttpClient _httpClient;

        public TodoItemRepository(TodoItemDatabase localDatabase, HttpClient httpClient)
        {
            _localDatabase = localDatabase;
            _httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            try
            {
                var localItems = await _localDatabase.GetItemsAsync();
                var remoteItems = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/TodoItems");
                return remoteItems ?? localItems;
            }

            catch (HttpRequestException ex)
            {
                //Логируем или обрабатываем ошибку
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return await _localDatabase.GetItemsAsync();
            }

            catch (JsonException ex)
            {
                // Логируем или обрабатываем ошибку
                Console.WriteLine($"JSON parsing failed: {ex.Message}");
                return await _localDatabase.GetItemsAsync();
            }
        }

        public async Task<TodoItem> GetItemAsync(int id)
        {
            var localItem = await _localDatabase.GetItemAsync(id);
            var remoteItem = await _httpClient.GetFromJsonAsync<TodoItem>($"api/TodoItems/{id}");
            return remoteItem ?? localItem;
        }

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Items", item);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {responseContent}");

                    var remoteEmployee = await response.Content.ReadFromJsonAsync<TodoItem>();
                    if (remoteEmployee == null)
                    {
                        Console.WriteLine("Failed to deserialize response content.");
                        return await _localDatabase.SaveItemAsync(item);
                    }

                    return await _localDatabase.SaveItemAsync(item);
                }
                else
                {
                    // Логирование ошибки
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Логирование ошибки
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Логирование ошибки
                Console.WriteLine($"JSON parsing failed: {ex.Message}");
            }

            return await _localDatabase.SaveItemAsync(item);
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            var response = await _httpClient.DeleteAsync($"api/TodoItems/{item.ID}");
            if (response.IsSuccessStatusCode)
            {
                return await _localDatabase.DeleteItemAsync(item);
            }
            return 0;
        }
    }
}

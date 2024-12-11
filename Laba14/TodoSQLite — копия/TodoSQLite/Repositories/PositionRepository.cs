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
    // PositionRepository.cs
    public class PositionRepository
    {
        private readonly TodoItemDatabase _localDatabase;
        private readonly HttpClient _httpClient;

        public PositionRepository(TodoItemDatabase localDatabase, HttpClient httpClient)
        {
            _localDatabase = localDatabase;
            _httpClient = httpClient;
        }

        public async Task<List<Position>> GetPositionsAsync()
        {
            try
            {
                var localPositions = await _localDatabase.GetPositionsAsync();
                var remotePositions = await _httpClient.GetFromJsonAsync<List<Position>>("api/Positions");
                return remotePositions ?? localPositions;
            }

            catch (HttpRequestException ex)
            {
                //Логируем или обрабатываем ошибку
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return await _localDatabase.GetPositionsAsync();
            }

            catch (JsonException ex)
            {
                // Логируем или обрабатываем ошибку
                Console.WriteLine($"JSON parsing failed: {ex.Message}");
                return await _localDatabase.GetPositionsAsync();
            }
        }

        public async Task<Position> GetPositionAsync(int id)
        {
            var localPosition = await _localDatabase.GetPositionAsync(id);
            var remotePosition = await _httpClient.GetFromJsonAsync<Position>($"api/Positions/{id}");
            return remotePosition ?? localPosition;
        }

        public async Task<int> SavePositionAsync(Position position)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Positions",position);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {responseContent}");

                    var remoteEmployee = await response.Content.ReadFromJsonAsync<Position>();
                    if (remoteEmployee == null)
                    {
                        Console.WriteLine("Failed to deserialize response content.");
                        return await _localDatabase.SavePositionAsync(position);
                    }

                    return await _localDatabase.SavePositionAsync(position);
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

            return await _localDatabase.SavePositionAsync(position);
        }
        

        public async Task<int> DeletePositionAsync(Position position)
        {
            var response = await _httpClient.DeleteAsync($"api/Positions/{position.ID}");
            if (response.IsSuccessStatusCode)
            {
                return await _localDatabase.DeletePositionAsync(position);
            }
            return 0;
        }
    }
}

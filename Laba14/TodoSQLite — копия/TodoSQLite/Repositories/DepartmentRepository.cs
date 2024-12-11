using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoSQLite.Models;
using TodoSQLite.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


namespace TodoSQLite.Repositories
{
    // DepartmentRepository.cs
    public class DepartmentRepository
    {
        private readonly TodoItemDatabase _localDatabase;
        private readonly HttpClient _httpClient;

        public DepartmentRepository(TodoItemDatabase localDatabase, HttpClient httpClient)
        {
            _localDatabase = localDatabase;
            _httpClient = httpClient;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            try
            {
                var localDepartments = await _localDatabase.GetDepartmentsAsync();
                var remoteDepartments = await _httpClient.GetFromJsonAsync<List<Department>>("api/Departments");
                return remoteDepartments ?? localDepartments;
            }
            catch (HttpRequestException ex)
            {
                // Логируем или обрабатываем ошибку
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return await _localDatabase.GetDepartmentsAsync();
            }
            catch (JsonException ex)
            {
                // Логируем или обрабатываем ошибку
                Console.WriteLine($"JSON parsing failed: {ex.Message}");
                return await _localDatabase.GetDepartmentsAsync();
            }
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            var localDepartment = await _localDatabase.GetDepartmentAsync(id);
            var remoteDepartment = await _httpClient.GetFromJsonAsync<Department>($"api/Departments/{id}");
            return remoteDepartment ?? localDepartment;
        }

        public async Task<int> SaveDepartmentAsync(Department department)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Departments", department);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {responseContent}");

                    var remoteDepartment = await response.Content.ReadFromJsonAsync<Department>();
                    if (remoteDepartment == null)
                    {
                        Console.WriteLine("Failed to deserialize response content.");
                        return await _localDatabase.SaveDepartmentAsync(department);
                    }

                    return await _localDatabase.SaveDepartmentAsync(remoteDepartment);
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

            return await _localDatabase.SaveDepartmentAsync(department);
        }

        public async Task<int> DeleteDepartmentAsync(Department department)
        {
            var response = await _httpClient.DeleteAsync($"api/Departments/{department.ID}");
            if (response.IsSuccessStatusCode)
            {
                return await _localDatabase.DeleteDepartmentAsync(department);
            }
            return 0;
        }
    }
}

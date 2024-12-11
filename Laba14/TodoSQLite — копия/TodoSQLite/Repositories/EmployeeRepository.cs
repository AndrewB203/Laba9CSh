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
    // EmployeeRepository.cs
    public class EmployeeRepository
    {
        private readonly TodoItemDatabase _localDatabase;
        private readonly HttpClient _httpClient;

        public EmployeeRepository(TodoItemDatabase localDatabase, HttpClient httpClient)
        {
            _localDatabase = localDatabase;
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                var localEmployees = await _localDatabase.GetEmployeesAsync();
                var remoteEmployees = await _httpClient.GetFromJsonAsync<List<Employee>>("api/Employees");
                return remoteEmployees ?? localEmployees;
            }
            catch (HttpRequestException ex)
            { 
                //Логируем или обрабатываем ошибку
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return await _localDatabase.GetEmployeesAsync();
            }

            catch (JsonException ex)
            {
                // Логируем или обрабатываем ошибку
                Console.WriteLine($"JSON parsing failed: {ex.Message}");
                return await _localDatabase.GetEmployeesAsync();
            }
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var localEmployee = await _localDatabase.GetEmployeeAsync(id);
            var remoteEmployee = await _httpClient.GetFromJsonAsync<Employee>($"api/Employees/{id}");
            return remoteEmployee ?? localEmployee;
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Employees", employee);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {responseContent}");

                    var remoteEmployee = await response.Content.ReadFromJsonAsync<Employee>();
                    if (remoteEmployee== null)
                    {
                        Console.WriteLine("Failed to deserialize response content.");
                        return await _localDatabase.SaveEmployeeAsync(employee);
                    }

                    return await _localDatabase.SaveEmployeeAsync(employee);
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

            return await _localDatabase.SaveEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/{employee.ID}");
            if (response.IsSuccessStatusCode)
            {
                return await _localDatabase.DeleteEmployeeAsync(employee);
            }
            return 0;
        }
    }
}

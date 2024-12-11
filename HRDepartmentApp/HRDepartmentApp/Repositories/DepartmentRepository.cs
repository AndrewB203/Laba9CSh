using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HRDepartmentApp.Models;

namespace HRDepartmentApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/api/departments";

        public DepartmentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Department>>();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        public async Task AddAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, department);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Department department)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{department.Id}", department);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

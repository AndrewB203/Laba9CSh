using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndustrialEnterpriseApp.Data;
using IndustrialEnterpriseApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialEnterpriseApp.Services
{
    public class EmployeeService
    {
        private readonly IndustrialEnterpriseContext _context;

        public EmployeeService(IndustrialEnterpriseContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                // Проверяем существование DepartmentId
                var departmentExists = _context.Departments.Any(d => d.Id == employee.Id);
                if (!departmentExists)
                {
                    throw new ArgumentException($"Department with ID {employee.Id} does not exist.");
                }

                // Преобразуем значения DateTime в UTC перед сохранением
                employee.BirthDate = employee.BirthDate.ToUniversalTime();
                employee.HireDate = employee.HireDate.ToUniversalTime();

                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while adding the employee:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                // Проверяем существование DepartmentId
                var departmentExists = _context.Departments.Any(d => d. Id == employee.Id);
                if (!departmentExists)
                {
                    throw new ArgumentException($"Department with ID {employee.Id} does not exist.");
                }

                // Преобразуем значения DateTime в UTC перед сохранением
                employee.BirthDate = employee.BirthDate.ToUniversalTime();
                employee.HireDate = employee.HireDate.ToUniversalTime();

                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while updating the employee:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while deleting the employee:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
    }
}
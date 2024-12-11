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
    public class DepartmentService
    {
        private readonly IndustrialEnterpriseContext _context;

        public DepartmentService(IndustrialEnterpriseContext context)
        {
            _context = context;
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void AddDepartment(Department department)
        {
            try
            {
                // Проверяем существование HeadId
                if (department.HeadId.HasValue)
                {
                    var headExists = _context.Employees.Any(e => e.Id == department.HeadId.Value);
                    if (!headExists)
                    {
                        throw new ArgumentException($"Employee with ID {department.HeadId.Value} does not exist.");
                    }
                }

                _context.Departments.Add(department);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while adding the department:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                // Проверяем существование HeadId
                if (department.HeadId.HasValue)
                {
                    var headExists = _context.Employees.Any(e => e.Id == department.HeadId.Value);
                    if (!headExists)
                    {
                        throw new ArgumentException($"Employee with ID {department.HeadId.Value} does not exist.");
                    }
                }

                _context.Departments.Update(department);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while updating the department:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void DeleteDepartment(int id)
        {
            try
            {
                var department = _context.Departments.FirstOrDefault(d => d.Id == id);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                // Вывод внутреннего исключения для получения дополнительной информации
                Console.WriteLine("An error occurred while deleting the department:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDepartmentApp.Models;
using HRDepartmentApp.Repositories;

namespace HRDepartmentApp.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeRepository _repository;

        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeeViewModel(IEmployeeRepository repository)
        {
            _repository = repository;
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
            await LoadEmployeesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
            await LoadEmployeesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await LoadEmployeesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}

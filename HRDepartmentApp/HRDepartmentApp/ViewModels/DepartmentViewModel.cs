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
    public class DepartmentViewModel : INotifyPropertyChanged
    {
        private readonly IDepartmentRepository _repository;

        public ObservableCollection<Department> Departments { get; set; }

        public DepartmentViewModel(IDepartmentRepository repository)
        {
            _repository = repository;
            Departments = new ObservableCollection<Department>();
            LoadDepartmentsAsync();
        }

        private async Task LoadDepartmentsAsync()
        {
            var departments = await _repository.GetAllAsync();
            Departments.Clear();
            foreach (var department in departments)
            {
                Departments.Add(department);
            }
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _repository.AddAsync(department);
            await LoadDepartmentsAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _repository.UpdateAsync(department);
            await LoadDepartmentsAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await LoadDepartmentsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

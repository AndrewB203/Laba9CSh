using IndustrialEnterpriseApp.Data;
using IndustrialEnterpriseApp.Models;
using IndustrialEnterpriseApp.Services;
using System;
using System.Linq;

namespace IndustrialEnterpriseApp.Pages
{
    public partial class EmployeesPage : ContentPage
    {
        private readonly EmployeeService _employeeService;
        private Employee _selectedEmployee;

        public EmployeesPage()
        {
            InitializeComponent();
            _employeeService = new EmployeeService(new IndustrialEnterpriseContext());
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeesListView.ItemsSource = _employeeService.GetAllEmployees();
        }

        private async void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            var employee = new Employee
            {
                FullName = "New Employee",
                BirthDate = DateTime.Now,
                PositionId = 1,
                DepartmentId = 0,
                Salary = 50000,
                HireDate = DateTime.Now
            };

            _employeeService.AddEmployee(employee);
            LoadEmployees();
        }

        private async void OnEditEmployeeClicked(object sender, EventArgs e)
        {
            if (_selectedEmployee != null)
            {
                _selectedEmployee.FullName = "Updated Employee";
                _employeeService.UpdateEmployee(_selectedEmployee);
                LoadEmployees();
            }
        }

        private async void OnDeleteEmployeeClicked(object sender, EventArgs e)
        {
            if (_selectedEmployee != null)
            {
                _employeeService.DeleteEmployee(_selectedEmployee.Id);
                LoadEmployees(); // Обновляем список после удаления
                _selectedEmployee = null; // Сбрасываем выбранный элемент
            }
        }

        private void EmployeesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedEmployee = e.SelectedItem as Employee;
        }
    }
}
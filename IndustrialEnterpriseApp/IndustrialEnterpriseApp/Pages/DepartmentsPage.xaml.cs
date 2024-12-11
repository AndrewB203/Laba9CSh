using IndustrialEnterpriseApp.Data;
using IndustrialEnterpriseApp.Models;
using IndustrialEnterpriseApp.Services;
using System;
using System.Linq;

namespace IndustrialEnterpriseApp.Pages
{
    public partial class DepartmentsPage : ContentPage
    {
        private readonly DepartmentService _departmentService;
        private Department _selectedDepartment;

        public DepartmentsPage()
        {
            InitializeComponent();
            _departmentService = new DepartmentService(new IndustrialEnterpriseContext());
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            DepartmentsListView.ItemsSource = _departmentService.GetAllDepartments();
        }

        private async void OnAddDepartmentClicked(object sender, EventArgs e)
        {
            var department = new Department
            {
                Title = "New Department",
                HeadId = null
            };

            _departmentService.AddDepartment(department);
            LoadDepartments();
        }

        private async void OnEditDepartmentClicked(object sender, EventArgs e)
        {
            if (_selectedDepartment != null)
            {
                _selectedDepartment.Title = "Updated Department";
                _departmentService.UpdateDepartment(_selectedDepartment);
                LoadDepartments();
            }
        }

        private async void OnDeleteDepartmentClicked(object sender, EventArgs e)
        {
            if (_selectedDepartment != null)
            {
                _departmentService.DeleteDepartment(_selectedDepartment.Id);
                LoadDepartments();
            }
        }

        private void DepartmentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedDepartment = e.SelectedItem as Department;
        }
    }
}
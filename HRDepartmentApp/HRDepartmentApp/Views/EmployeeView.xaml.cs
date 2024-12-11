using HRDepartmentApp.Models;
using HRDepartmentApp.ViewModels;

namespace HRDepartmentApp.Views;

public partial class EmployeeView : ContentPage
{
    private readonly EmployeeViewModel _viewModel;

    public EmployeeView(EmployeeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void AddEmployee_Clicked(object sender, EventArgs e)
    {
        var employee = new Employee
        {
            FirstName = "New",
            LastName = "Employee",
            DateOfBirth = DateTime.Now,
            Position = "Developer",
            Salary = 50000
        };
        await _viewModel.AddEmployeeAsync(employee);
    }

    private async void UpdateEmployee_Clicked(object sender, EventArgs e)
    {
        if (EmployeeListView.SelectedItem is Employee selectedEmployee)
        {
            selectedEmployee.FirstName = "Updated";
            await _viewModel.UpdateEmployeeAsync(selectedEmployee);
        }
    }

    private async void DeleteEmployee_Clicked(object sender, EventArgs e)
    {
        if (EmployeeListView.SelectedItem is Employee selectedEmployee)
        {
            await _viewModel.DeleteEmployeeAsync(selectedEmployee.Id);
        }
    }
}
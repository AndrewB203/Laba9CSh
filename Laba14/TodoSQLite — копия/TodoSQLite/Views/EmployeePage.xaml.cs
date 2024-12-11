using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;
namespace TodoSQLite.Views;

[QueryProperty("Employee", "Employee")]
public partial class EmployeePage : ContentPage
{
    private readonly EmployeeRepository _repository;
    public Employee Employee
    {
        get => BindingContext as Employee;
        set => BindingContext = value;
    }

    public EmployeePage(EmployeeRepository repository)
    {
        InitializeComponent();
        _repository = repository;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Employee.FirstName) || string.IsNullOrWhiteSpace(Employee.LastName))
        {
            await DisplayAlert("Name Required", "Please enter a first and last name for the employee.", "OK");
            return;
        }

        await _repository.SaveEmployeeAsync(Employee);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Employee.ID == 0)
            return;
        await _repository.DeleteEmployeeAsync(Employee);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
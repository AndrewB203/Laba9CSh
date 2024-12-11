using TodoSQLite.Data;
using TodoSQLite.Models;

namespace TodoSQLite.Views;

[QueryProperty("Employee", "Employee")]
public partial class EmployeePage : ContentPage
{
    Employee employee;
    public Employee Employee
    {
        get => BindingContext as Employee;
        set => BindingContext = value;
    }
    TodoItemDatabase database;
    public EmployeePage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Employee.FirstName) || string.IsNullOrWhiteSpace(Employee.LastName))
        {
            await DisplayAlert("Name Required", "Please enter a first and last name for the employee.", "OK");
            return;
        }

        await database.SaveEmployeeAsync(Employee);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Employee.ID == 0)
            return;
        await database.DeleteEmployeeAsync(Employee);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
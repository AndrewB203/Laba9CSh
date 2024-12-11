using TodoSQLite.Data;
using TodoSQLite.Models;

namespace TodoSQLite.Views;

[QueryProperty("Department", "Department")]
public partial class DepartmentPage : ContentPage
{
    Department department;
    public Department Department
    {
        get => BindingContext as Department;
        set => BindingContext = value;
    }
    TodoItemDatabase database;
    public DepartmentPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Department.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the department.", "OK");
            return;
        }

        await database.SaveDepartmentAsync(Department);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Department.ID == 0)
            return;
        await database.DeleteDepartmentAsync(Department);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;
namespace TodoSQLite.Views;

[QueryProperty("Department", "Department")]
public partial class DepartmentPage : ContentPage
{
    private readonly DepartmentRepository _repository;
    public Department Department
    {
        get => BindingContext as Department;
        set => BindingContext = value;
    }

    public DepartmentPage(DepartmentRepository repository)
    {
        InitializeComponent();
        _repository = repository;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Department.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the department.", "OK");
            return;
        }

        await _repository.SaveDepartmentAsync(Department);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Department.ID == 0)
            return;
        await _repository.DeleteDepartmentAsync(Department);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
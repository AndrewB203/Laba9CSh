using TodoSQLite.Data;
using TodoSQLite.Views;

namespace TodoSQLite.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnTasksClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoListPage));
    }

    async void OnDepartmentsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DepartmentListPage));
    }
    async void OnPositionsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PositionListPage));
    }

    async void OnEmployeesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EmployeeListPage));
    }
}
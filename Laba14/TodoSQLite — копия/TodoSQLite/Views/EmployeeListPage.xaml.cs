using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;

namespace TodoSQLite.Views;

public partial class EmployeeListPage : ContentPage
{
    private readonly EmployeeRepository _repository;
    public ObservableCollection<Employee> Employees { get; set; } = new();

    public EmployeeListPage(EmployeeRepository repository)
    {
        InitializeComponent();
        _repository = repository;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var employees = await _repository.GetEmployeesAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Employees.Clear();
            foreach (var employee in employees)
                Employees.Add(employee);
        });
    }

    async void OnEmployeeAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EmployeePage), true, new Dictionary<string, object>
        {
            ["Employee"] = new Employee()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Employee employee)
            return;

        await Shell.Current.GoToAsync(nameof(EmployeePage), true, new Dictionary<string, object>
        {
            ["Employee"] = employee
        });
    }
}
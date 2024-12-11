using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;
namespace TodoSQLite.Views;

public partial class DepartmentListPage : ContentPage
{
    private readonly DepartmentRepository _repository;
    public ObservableCollection<Department> Departments { get; set; } = new();

    public DepartmentListPage(DepartmentRepository repository)
    {
        InitializeComponent();
        _repository = repository;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var departments = await _repository.GetDepartmentsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Departments.Clear();
            foreach (var department in departments)
                Departments.Add(department);
        });
    }

    async void OnDepartmentAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DepartmentPage), true, new Dictionary<string, object>
        {
            ["Department"] = new Department()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Department department)
            return;

        await Shell.Current.GoToAsync(nameof(DepartmentPage), true, new Dictionary<string, object>
        {
            ["Department"] = department
        });
    }
}
using HRDepartmentApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HRDepartmentApp;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private async void OnEmployeesClicked(object sender, EventArgs e)
    {
        var employeeView = _serviceProvider.GetRequiredService<EmployeeView>();
        await Navigation.PushAsync(employeeView);
    }

    private async void OnDepartmentsClicked(object sender, EventArgs e)
    {
        var departmentView = _serviceProvider.GetRequiredService<DepartmentView>();
        await Navigation.PushAsync(departmentView);
    }
}
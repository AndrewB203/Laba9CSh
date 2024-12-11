using Microsoft.Extensions.DependencyInjection;
using HRDepartmentApp.ViewModels;
using HRDepartmentApp.Views;
using HRDepartmentApp.Repositories;
using System.Net.Http;

namespace HRDepartmentApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register services
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddSingleton<IDepartmentRepository, DepartmentRepository>();

        builder.Services.AddSingleton<EmployeeViewModel>();
        builder.Services.AddSingleton<DepartmentViewModel>();

        builder.Services.AddTransient<EmployeeView>();
        builder.Services.AddTransient<DepartmentView>();

        return builder.Build();
    }
}
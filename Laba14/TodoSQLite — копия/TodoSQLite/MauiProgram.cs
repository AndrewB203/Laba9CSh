using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoSQLite.Data;
using TodoSQLite.Views;
using TodoSQLite.Repositories;

namespace TodoSQLite;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Регистрация сервисов для TodoListPage и TodoItemPage
        builder.Services.AddSingleton<TodoListPage>();
        builder.Services.AddTransient<TodoItemPage>();

        // Регистрация сервисов для DepartmentListPage и DepartmentPage
        builder.Services.AddSingleton<DepartmentListPage>();
        builder.Services.AddTransient<DepartmentPage>();

        // Регистрация сервисов для PositionListPage и PositionPage
        builder.Services.AddSingleton<PositionListPage>();
        builder.Services.AddTransient<PositionPage>();

        // Регистрация сервисов для EmployeeListPage и EmployeePage
        builder.Services.AddSingleton<EmployeeListPage>();
        builder.Services.AddTransient<EmployeePage>();

        // Регистрация сервиса для базы данных
        builder.Services.AddSingleton<TodoItemDatabase>();

        builder.Services.AddSingleton<TodoItemRepository>();
        builder.Services.AddSingleton<DepartmentRepository>();
        builder.Services.AddSingleton<EmployeeRepository>();
        builder.Services.AddSingleton<PositionRepository>();

        // Регистрация HttpClient
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7183/") });


        return builder.Build();
    }
}
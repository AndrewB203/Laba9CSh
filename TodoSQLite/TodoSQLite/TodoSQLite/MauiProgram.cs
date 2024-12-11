using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoSQLite.Data;
using TodoSQLite.Views;

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

        return builder.Build();
    }
}
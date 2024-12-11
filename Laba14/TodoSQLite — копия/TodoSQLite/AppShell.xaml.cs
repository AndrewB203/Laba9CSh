using TodoSQLite.Views;

namespace TodoSQLite;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TodoListPage), typeof(TodoListPage));
        Routing.RegisterRoute(nameof(TodoItemPage), typeof(TodoItemPage));

        Routing.RegisterRoute(nameof(DepartmentListPage), typeof(DepartmentListPage));
        Routing.RegisterRoute(nameof(DepartmentPage), typeof(DepartmentPage));
        
        Routing.RegisterRoute(nameof(PositionListPage), typeof(PositionListPage));
        Routing.RegisterRoute(nameof(PositionPage), typeof(PositionPage));
        
        Routing.RegisterRoute(nameof(EmployeeListPage), typeof(EmployeeListPage));
        Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage)); 
    }
}
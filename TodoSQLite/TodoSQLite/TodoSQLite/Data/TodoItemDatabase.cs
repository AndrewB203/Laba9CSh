using SQLite;
using TodoSQLite.Models;

namespace TodoSQLite.Data;

public class TodoItemDatabase
{
    SQLiteAsyncConnection Database;
    public TodoItemDatabase()
    {
    }
    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<TodoItem>();
        await Database.CreateTableAsync<Department>();
        await Database.CreateTableAsync<Employee>();
        await Database.CreateTableAsync<Position>();
    }

    // Методы для работы с TodoItem
    public async Task<List<TodoItem>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<TodoItem>().ToListAsync();
    }

    public async Task<List<TodoItem>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<TodoItem>().Where(t => !t.Done).ToListAsync();
    }

    public async Task<TodoItem> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(TodoItem item)
    {
        await Init();
        if (item.ID != 0)
        {
            return await Database.UpdateAsync(item);
        }
        else
        {
            return await Database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(TodoItem item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    // Методы для работы с Department
    public async Task<List<Department>> GetDepartmentsAsync()
    {
        await Init();
        return await Database.Table<Department>().ToListAsync();
    }

    public async Task<Department> GetDepartmentAsync(int id)
    {
        await Init();
        return await Database.Table<Department>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveDepartmentAsync(Department department)
    {
        await Init();
        if (department.ID != 0)
        {
            return await Database.UpdateAsync(department);
        }
        else
        {
            return await Database.InsertAsync(department);
        }
    }

    public async Task<int> DeleteDepartmentAsync(Department department)
    {
        await Init();
        return await Database.DeleteAsync(department);
    }

    // Методы для работы с Employee
    public async Task<List<Employee>> GetEmployeesAsync()
    {
        await Init();
        return await Database.Table<Employee>().ToListAsync();
    }

    public async Task<Employee> GetEmployeeAsync(int id)
    {
        await Init();
        return await Database.Table<Employee>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveEmployeeAsync(Employee employee)
    {
        await Init();
        if (employee.ID != 0)
        {
            return await Database.UpdateAsync(employee);
        }
        else
        {
            return await Database.InsertAsync(employee);
        }
    }

    public async Task<int> DeleteEmployeeAsync(Employee employee)
    {
        await Init();
        return await Database.DeleteAsync(employee);
    }

    // Методы для работы с Position
    public async Task<List<Position>> GetPositionsAsync()
    {
        await Init();
        return await Database.Table<Position>().ToListAsync();
    }

    public async Task<Position> GetPositionAsync(int id)
    {
        await Init();
        return await Database.Table<Position>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SavePositionAsync(Position position)
    {
        await Init();
        if (position.ID != 0)
        {
            return await Database.UpdateAsync(position);
        }
        else
        {
            return await Database.InsertAsync(position);
        }
    }

    public async Task<int> DeletePositionAsync(Position position)
    {
        await Init();
        return await Database.DeleteAsync(position);
    }
}
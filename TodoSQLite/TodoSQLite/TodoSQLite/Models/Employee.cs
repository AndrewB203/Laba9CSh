using SQLite;

namespace TodoSQLite.Models;

public class Employee
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}

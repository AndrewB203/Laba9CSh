using SQLite;

namespace TodoSQLite.Models;

public class Position
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Title { get; set; }
    public string Salary { get; set; }
    public bool IsOpen { get; set; }
}

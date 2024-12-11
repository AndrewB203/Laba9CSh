namespace HRWebApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public Employee? HeadOfDepartment { get; set; }
    }
}
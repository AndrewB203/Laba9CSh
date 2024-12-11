using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialEnterpriseApp.Models
{
    public class Department
    {
        public int Id { get; set; } // Изменено с Id на DepartmentId
        public string Title { get; set; }
        public int? HeadId { get; set; }
        public Employee Head { get; set; }
    }
}
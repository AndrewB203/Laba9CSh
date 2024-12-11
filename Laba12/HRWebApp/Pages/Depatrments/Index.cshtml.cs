using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Models;

namespace HRWebApp.Pages.Depatrments
{
    public class IndexModel : PageModel
    {
        private readonly HRWebApp.Models.HRDbContext _context;

        public IndexModel(HRWebApp.Models.HRDbContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments
                .Include(d => d.HeadOfDepartment).ToListAsync();
        }
    }
}

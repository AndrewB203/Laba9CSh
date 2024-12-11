using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRWebApp.Models;

namespace HRWebApp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly HRDbContext _context;

        public IndexModel(HRDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get; set; }

        public async Task OnGetAsync()
        {
            Employees = await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .ToListAsync();
        }
    }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HRWebApp.Models;

namespace HRWebApp.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly HRDbContext _context;

        public DetailsModel(HRDbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id == null)
            {
                return;
            }

            Employee = await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Employee == null)
            {
                return;
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using HRWebApp.Models;

namespace HRWebApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly HRDbContext _context;

        public CreateModel(HRDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public IActionResult OnGet()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRWebApp.Models;

namespace HRWebApp.Pages.Depatrments
{
    public class CreateModel : PageModel
    {
        private readonly HRWebApp.Models.HRDbContext _context;

        public CreateModel(HRWebApp.Models.HRDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["HeadOfDepartmentId"] = new SelectList(_context.Employees, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

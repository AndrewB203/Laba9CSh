using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Models;

namespace HRWebApp.Pages.Positions
{
    public class DetailsModel : PageModel
    {
        private readonly HRWebApp.Models.HRDbContext _context;

        public DetailsModel(HRWebApp.Models.HRDbContext context)
        {
            _context = context;
        }

        public Position Position { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }
            else
            {
                Position = position;
            }
            return Page();
        }
    }
}

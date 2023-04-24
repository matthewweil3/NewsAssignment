using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public DeleteModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ApplicationUserCategory ApplicationUserCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ApplicationUserCategory == null)
            {
                return NotFound();
            }

            var applicationusercategory = await _context.ApplicationUserCategory.FirstOrDefaultAsync(m => m.ID == id);

            if (applicationusercategory == null)
            {
                return NotFound();
            }
            else 
            {
                ApplicationUserCategory = applicationusercategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ApplicationUserCategory == null)
            {
                return NotFound();
            }
            var applicationusercategory = await _context.ApplicationUserCategory.FindAsync(id);

            if (applicationusercategory != null)
            {
                ApplicationUserCategory = applicationusercategory;
                _context.ApplicationUserCategory.Remove(ApplicationUserCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

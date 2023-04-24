using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public EditModel(NewsAssignment.Data.ApplicationDbContext context)
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

            var applicationusercategory =  await _context.ApplicationUserCategory.FirstOrDefaultAsync(m => m.ID == id);
            if (applicationusercategory == null)
            {
                return NotFound();
            }
            ApplicationUserCategory = applicationusercategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ApplicationUserCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserCategoryExists(ApplicationUserCategory.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicationUserCategoryExists(int id)
        {
          return (_context.ApplicationUserCategory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

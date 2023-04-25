using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string storeID;

        public CreateModel(NewsAssignment.Data.ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        public IActionResult OnGet()
        {
            storeID = _userManager.GetUserId(HttpContext.User);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "display");
            return Page();
        }

        [BindProperty]
        public ApplicationUserCategory ApplicationUserCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ApplicationUserCategory == null || ApplicationUserCategory == null)
            {
                return Page();
            }

            _context.ApplicationUserCategory.Add(ApplicationUserCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

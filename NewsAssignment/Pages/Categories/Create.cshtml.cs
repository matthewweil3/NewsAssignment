using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Categories
{
    [Authorize]
    public class CreateModel : PageModel
    {
        
       private readonly NewsAssignment.Data.ApplicationDbContext _context;
       private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(NewsAssignment.Data.ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        public IActionResult OnGet()
        {
            string loginid = _userManager.GetUserId(HttpContext.User);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id",loginid);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "name");
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

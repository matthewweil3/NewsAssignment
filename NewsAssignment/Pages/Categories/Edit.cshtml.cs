using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string storeID;

        public EditModel(NewsAssignment.Data.ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        [BindProperty]
        public ApplicationUserCategory ApplicationUserCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "display");
            
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            
            storeID = _userManager.GetUserId(HttpContext.User); 
            _context.ApplicationUserCategory.RemoveRange(_context.ApplicationUserCategory.Where(x => x.ApplicationUserId == storeID));
            await _context.SaveChangesAsync();

            var cats = Request.Form["favorite"];
            for (int i = 0; i < 5; i++)  //loops 5 times because only select 5 categories
            {
                var APUC = new ApplicationUserCategory { ApplicationUserId = storeID, CategoryId = Int32.Parse(cats[i]) };
                _context.ApplicationUserCategory.Add(APUC);
                await _context.SaveChangesAsync();
            }

            return Redirect("~/Index");
        }

        private bool ApplicationUserCategoryExists(int id)
        {
          return (_context.ApplicationUserCategory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

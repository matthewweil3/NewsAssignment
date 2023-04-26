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
        private string storeID;

        public CreateModel(NewsAssignment.Data.ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "display");
            return Page();
        }

        [BindProperty]
        public ApplicationUserCategory ApplicationUserCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            storeID = _userManager.GetUserId(HttpContext.User);
            var cats = Request.Form["selected"];
            for(int i = 0; i < 5; i++)  //loops 5 times because only select 5 categories
            {
                var APUC = new ApplicationUserCategory { ApplicationUserId = storeID, CategoryId = Int32.Parse(cats[i]) };
                _context.ApplicationUserCategory.Add(APUC);
                await _context.SaveChangesAsync();
            }
            

            return RedirectToPage("./Index");
        }
    }
}

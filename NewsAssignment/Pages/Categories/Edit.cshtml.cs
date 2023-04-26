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
            //var cats = Request.Form["selected"];

            //var ids = _context.ApplicationUserCategory.Count();

            //for (int i = 0; i < ids; i++)
            //{
            //    if(storeID == ApplicationUserCategory.ID.)
            //}

            //need to delete out then add

            //https://stackoverflow.com/questions/41882419/linq-query-for-removing-all-objects-with-an-id-not-equal-to-a-list-of-ids-now-wo


            var record = await _context.ApplicationUserCategory.FindAsync(storeID);
            ApplicationUserCategory = record;
            _context.ApplicationUserCategory.Remove(ApplicationUserCategory);


            return RedirectToPage("./Index");
        }

        private bool ApplicationUserCategoryExists(int id)
        {
          return (_context.ApplicationUserCategory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

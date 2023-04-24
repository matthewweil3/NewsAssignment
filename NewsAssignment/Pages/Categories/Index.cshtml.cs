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
    public class IndexModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public IndexModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUserCategory> ApplicationUserCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ApplicationUserCategory != null)
            {
                ApplicationUserCategory = await _context.ApplicationUserCategory.ToListAsync();
            }
        }
    }
}

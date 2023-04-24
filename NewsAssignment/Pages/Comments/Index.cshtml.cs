using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public IndexModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Comment != null)
            {
                Comment = await _context.Comment.ToListAsync();
            }
        }
    }
}

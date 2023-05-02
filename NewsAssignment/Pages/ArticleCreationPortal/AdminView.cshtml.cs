using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Data;
using Microsoft.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NewsAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    [Authorize(Roles=("Admin"))]
    public class AdminViewModel : PageModel
    {
        private ApplicationDbContext _context;

        public AdminViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (_context !=null && _context.Article != null)
            {
                Articles = await _context.Article.Where(x => x.status == Article.State.Published).ToListAsync();
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            if (!ModelState.IsValid || _context == null)
            {
                return StatusCode(404);
            }

            try
            {
                _context.Article.Remove(_context.Article.Where(x => x.Id == id).First());
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(404);
            }

            return RedirectToPage();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;
using System.Data;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    [Authorize(Roles = "Writer,Admin")]
    public class RewriteModel : PageModel
    {
        ApplicationDbContext _context;

        public RewriteModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;

        public async Task<IActionResult> OnGetSubmit(int? id)
        {
            if (!ModelState.IsValid || _context.Article == null)
            {
                return Page();
            }

            if (_context.Article.Where(x => x.Id == id) != null)
            {
                foreach (var v in this._context.Article.Where(x => x.Id == id))
                {
                    v.status = Article.State.Authored;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }


        public async Task<IActionResult> OnGet()
        {
            if (!ModelState.IsValid || _context.Article == null)
            {
                return Page();
            }

            if (_context.Article != null && User != null)
            {
                Articles = await _context.Article.Where(x => x.status == Article.State.Rewrite && x.creator == User.Identity.Name).ToListAsync();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

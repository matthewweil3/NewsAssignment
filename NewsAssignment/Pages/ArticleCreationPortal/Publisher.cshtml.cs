using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;
using NewsAssignment.Models;
using System.Data;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    [Authorize(Roles = "Publisher,Admin")]
    public class PublisherModel : PageModel
    {
        ApplicationDbContext _context;

        public PublisherModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;

        public async Task<IActionResult> OnGetEdit(int? id)
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
                    // v.ReturnReason = "";
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetRewrite(int? id)
        {
            if (!ModelState.IsValid || _context.Article == null)
            {
                return Page();
            }
            if (_context.Article.Where(x => x.Id == id) != null)
            {
                foreach (var v in this._context.Article.Where(x => x.Id == id))
                {
                    v.status = Article.State.Rewrite;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetPublish(int? id)
        {
            if (!ModelState.IsValid || _context.Article == null)
            {
                return Page();
            }

            if (_context.Article.Where(x => x.Id == id) != null)
            {
                foreach (var v in this._context.Article.Where(x => x.Id == id))
                {
                    v.status = Article.State.Published;
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

            if (_context.Article != null)
            {
                Articles = await _context.Article.Where(x => x.status == Article.State.Edited).ToListAsync();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

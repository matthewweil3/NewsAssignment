using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using NewsAssignment.Models;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Data;

namespace NewsAssignment.Pages.Articles
{
    public class ArticleCommentTestModel : PageModel
    {
        [BindProperty]
        public Comment Comment { get; set; } = default!;
        public List<Comment> comments;
        private ApplicationDbContext _context;
        public ArticleCommentTestModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            comments = _context.Comment.Where(x => x.ArticleID == 1).ToList();
            ViewData["Username"] = User.Identity.Name;
            ViewData["ArticleID"] = 1;
        }
        public async Task OnPostAsync() 
        {
            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();
            comments = _context.Comment.Where(x => x.ArticleID == 1).ToList();
            ViewData["Username"] = User.Identity.Name;
            ViewData["ArticleID"] = 1;
        }
    }
}

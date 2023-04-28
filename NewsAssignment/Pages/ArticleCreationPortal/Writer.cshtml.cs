using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Models;
using System.Data;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    [Authorize(Roles = "Writer")]
    public class WriterModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public WriterModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string DaysAgo { get; set; }
        public string Color { get; set; }
        public string Creator { get; set; }

        public async Task<IActionResult> OnGetAsync(string Title, string Description, string Category, string DaysAgo,
                            string Image, string Color, string Content, string Icon, string Creator)
        {

            if (Category != null)
            {
                this.Color = ArticleViewModel.CategoryColors[Category];
            }

            if (Icon != null)
            {
                this.Icon = ArticleViewModel.CategoryIcons[Category];
            }

            if (!ModelState.IsValid || _context.Article == null)
            {
                return Page();
            }

            // we need to add: Icon, Category, and written/editor/published states to the Article Model. The link will be wherever it is within the pub/writ/edited stage after that is programmed in
            Article = new Article { title = Title, description = Description, image_url = Image, content = Content, category = Category, pubDate = DateTime.Today.ToString(), creator = User.Identity.Name, language = "en", link = "placeholder"};

            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

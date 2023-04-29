using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Data;
using NewsAssignment.Models;


namespace NewsAssignment.Pages.Articles
{
    public class ArticleModel : PageModel
    {
        private ApplicationDbContext _context;
        public ArticleViewModel Article { get; set; }

        public ArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            // For comments
            ViewData["Username"] = User.Identity.Name;
            ViewData["ArticleID"] = id;

            if (id == -1)
            {
                // Random article
                Article = ArticleViewModel.CreateRandomModel();
                return Page();
            }

            // Get from database
            var article = _context.Article.Where(x => x.Id == id).FirstOrDefault();

            if (article == null)
                return NotFound();

            Article = new ArticleViewModel();

            Article.Id = article.Id;
            Article.Title = article.title;
            Article.Creator = article.creator;
            Article.VideoUrl = article.video_url;
            Article.Description = article.description;
            Article.Content = article.content;
            Article.ImageUrl = article.image_url;
            Article.Country = article.country;
            Article.Categories = article.category.Split(",").Select(x => x.Trim()).ToList();
            Article.Language = article.language;

            return Page();
        }
    }
}

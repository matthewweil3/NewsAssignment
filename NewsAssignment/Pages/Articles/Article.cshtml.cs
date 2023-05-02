using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Data;
using NewsAssignment.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace NewsAssignment.Pages.Articles
{
    public class ArticleModel : PageModel
    {
        [BindProperty]
        public Comment Comment { get; set; } = default!;
        public List<Comment> comments;
        private ApplicationDbContext _context;
        public ArticleViewModel Article { get; set; }

        public ArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult OnGet(int id)
        {
            // For comments
            comments = _context.Comment.Where(x => x.ArticleID == id).ToList();
            ViewData["Username"] = User.Identity.Name;
            ViewData["ArticleID"] = id;
            ViewData["Comments"] = comments;

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
            Article.Link = article.link;
            Article.Language = article.language;

            // https://stackoverflow.com/questions/5366285/parse-string-to-datetime-in-c-sharp
            try
            {
                Article.PublishDate = DateTime.ParseExact(article.pubDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                Article.PublishDate = DateTime.Parse(article.pubDate);
            }

            return Page();
        }
        public IActionResult OnPost(int id)
        {
            // For comments
            comments = _context.Comment.Where(x => x.ArticleID == 1).ToList();
            ViewData["Username"] = User.Identity.Name;
            ViewData["ArticleID"] = id;
            ViewData["Comments"] = comments;

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
            Article.Link = article.link;
            Article.Language = article.language;

            // https://stackoverflow.com/questions/5366285/parse-string-to-datetime-in-c-sharp
            try
            {
                Article.PublishDate = DateTime.ParseExact(article.pubDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                Article.PublishDate = DateTime.Parse(article.pubDate);
            }

            return Page();
        }
    }
}

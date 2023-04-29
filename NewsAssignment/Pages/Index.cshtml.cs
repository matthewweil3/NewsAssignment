using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewsAssignment.Data;
using NewsAssignment.Models;
using NewsAssignment.Providers;
using System.Globalization;

namespace NewsAssignment.Pages
{
    public class IndexModel : PageModel
    {
        private IConfiguration _config;
        private ArticleProvider _provider;
        private bool _useRandomArticles;
        ApplicationDbContext _context;
        public List<ArticleViewModel> Articles;

        public IndexModel(IConfiguration configuration, NewsAssignment.Data.ApplicationDbContext context)
        {
            _config = configuration;
            _provider = new ArticleProvider();
            _useRandomArticles = _config.GetValue<bool>("UseRandomArticles");
            _context = context;

            Articles = new List<ArticleViewModel>();
        }

        public async Task<IActionResult> OnGet()
        {
            if (_useRandomArticles)
            {
                for (int i = 0; i < 100; i++)
                {
                    Articles.Add(ArticleViewModel.CreateRandomModel());
                }

                return Page();
            }

            // Get articles and convert to view models
            var dbArticles = await _provider.FetchAllArticles();

            foreach (var article in dbArticles)
            {
                var articleViewModel = new ArticleViewModel();

                articleViewModel.Link = article.link;
                articleViewModel.Title = article.title;
                articleViewModel.Creator = article.creator;
                articleViewModel.VideoUrl = article.video_url;
                articleViewModel.Description = article.description;
                articleViewModel.Content = article.content;
                articleViewModel.ImageUrl = article.image_url;
                articleViewModel.Country = article.country;
                articleViewModel.Categories = article.category.Split(",").Select(x => x.Trim()).ToList();
                articleViewModel.Language = article.language;

                // https://stackoverflow.com/questions/5366285/parse-string-to-datetime-in-c-sharp
                articleViewModel.PublishDate = DateTime.ParseExact(article.pubDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                
                Articles.Add(articleViewModel);
            }

            if (_context.Article != null)
            {
                foreach (var article in _context.Article)
                {
                    if (article.status == Article.State.Published)
                    {
                        var articleViewModel = new ArticleViewModel();

                        //articleViewModel.Link = article.link;
                        articleViewModel.Title = article.title;
                        articleViewModel.Creator = article.creator;
                        articleViewModel.VideoUrl = article.video_url;
                        articleViewModel.Description = article.description;
                        articleViewModel.Content = article.content;
                        articleViewModel.ImageUrl = article.image_url;
                        articleViewModel.Country = article.country;
                        articleViewModel.Categories = article.category.Split(",").Select(x => x.Trim()).ToList();
                        articleViewModel.Language = article.language;

                        // https://stackoverflow.com/questions/5366285/parse-string-to-datetime-in-c-sharp
                        articleViewModel.PublishDate = DateTime.Parse(article.pubDate);

                        Articles.Add(articleViewModel);
                    }
                }
            }

            return Page();
        }
    }
}
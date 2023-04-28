using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public List<ArticleViewModel> Articles;

        public IndexModel(IConfiguration configuration)
        {
            _config = configuration;
            _provider = new ArticleProvider();
            _useRandomArticles = _config.GetValue<bool>("UseRandomArticles");

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
                articleViewModel.PublishDate = DateTime.ParseExact(article.pubDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);  // https://stackoverflow.com/questions/5366285/parse-string-to-datetime-in-c-sharp
                articleViewModel.ImageUrl = article.image_url;
                articleViewModel.Country = article.country;
                articleViewModel.Categories = article.category.Split(",").Select(x => x.Trim()).ToList();
                articleViewModel.Language = article.language;

                Articles.Add(articleViewModel);
            }

            return Page();
        }
    }
}
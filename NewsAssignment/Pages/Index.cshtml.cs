using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewsAssignment.Data;
using NewsAssignment.Models;
using NewsAssignment.Providers;
using System.Globalization;

namespace NewsAssignment.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private IConfiguration _config;
        private ArticleProvider _provider;
        private bool _useRandomArticles;
        private ApplicationDbContext _context;

        public List<ArticleViewModel> Articles;
        public Dictionary<string, List<ArticleViewModel>> ArticlesByCategory;

        public IndexModel(IConfiguration configuration, NewsAssignment.Data.ApplicationDbContext context)
        {
            _config = configuration;
            _provider = new ArticleProvider(context);
            _useRandomArticles = _config.GetValue<bool>("UseRandomArticles");
            _context = context;

            Articles = new List<ArticleViewModel>();
            ArticlesByCategory = new Dictionary<string, List<ArticleViewModel>>();
        }

        public async Task<IActionResult> OnGet()
        {
            // Get user categories
            var userId = _context.Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.Id).FirstOrDefault();
            var userCategoryIds = _context.ApplicationUserCategory.Where(x => x.ApplicationUserId == userId).Select(x => x.CategoryId).ToList();
            var userCategories = _context.Category.Where(x => userCategoryIds.Contains(x.CategoryId)).Select(x => x.name).ToList();

            if (_useRandomArticles)
            {
                // Generate 10 articles for each category
                if (userCategories != null && userCategories.Count() > 0)
                {
                    foreach (var category in userCategories)
                    {
                        var categoryArticles = new List<ArticleViewModel>();

                        for (int i = 0; i < 10; i++)
                        {
                            categoryArticles.Add(ArticleViewModel.CreateRandomModel(category));
                        }

                        ArticlesByCategory[category] = categoryArticles;
                    }
                }

                // Generate random articles for Latest News
                for (int i = 0; i < 50; i++)
                {
                    Articles.Add(ArticleViewModel.CreateRandomModel());
                }
            }
            else
            {
                // Get 10 articles for each category
                if (userCategories != null && userCategories.Count() > 0)
                {
                    foreach (var category in userCategories)
                    {
                        var categoryArticles = await _provider.FetchCategoryArticles(category);
                        ArticlesByCategory[category] = categoryArticles.Take(10).ToList();
                    }
                }

                // Get articles from API and database for Latest News
                Articles = await _provider.FetchAllArticles(20);
            }

            return Page();
        }

        // Endless Scroll
        // https://programmingcsharp.com/ajax-calls-in-asp-net-core-razor-pages/
        public async Task<IActionResult> OnPostEndlessScroll(int startIndex)
        {
            var endlessArticles = new List<ArticleViewModel>();

            if (_useRandomArticles)
            {
                // Generate 10 more random articles
                for (int i = 0; i < 10; i++)
                {
                    endlessArticles.Add(ArticleViewModel.CreateRandomModel());
                }
            }
            else
            {
                var nextArticles = await _provider.FetchAllArticles(20, startIndex);
                endlessArticles.AddRange(nextArticles);
            }

            return Partial("Articles/EndlessArticles", endlessArticles);
        }
    }
}

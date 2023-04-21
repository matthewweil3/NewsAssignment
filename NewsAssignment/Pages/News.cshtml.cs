using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Data.Migrations;
using NewsAssignment.Models;
using Newtonsoft.Json;

namespace NewsAssignment.Pages
{
    public class NewsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public NewsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //TODO: access topics from user.
        //var currentUserName = HttpContext.User.Identity.Name;

        public string[] articleCategories = { "business", "entertainment", "enviornment", "food", "health", "politics", "science", "sports", "technology", "top", "tourism", "world" };
        public List<Article> englishArticles { get; set; }
        public ArticleList ArticleResults = new ArticleList();

        public async Task<IActionResult> OnGet()
        {
            englishArticles = new List<Article>();
            Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&category=" + topic);

            for (int i = 0; i < 12; i++)
            {
                Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&language=en&category=" + articleCategories[i]);

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(mb.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    ArticleResults = JsonConvert.DeserializeObject<ArticleList>(data);
                    foreach (var item in ArticleResults.results)
                    {
                        englishArticles.Add(item);
                    }
                }
            }

            ArticleList retval = new ArticleList();
            retval.results = englishArticles;
            return Page();
        }
    }
}

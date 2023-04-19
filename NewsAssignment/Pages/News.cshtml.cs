using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        //var currentUserName = HttpContext.User.Identity.Name;

        public List<Article> articles { get; set; }
        public ArticleList ArticleResults = new ArticleList();

        public async Task<IActionResult> OnGet()
        {
            articles = new List<Article>();
            //Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&category=" + topic);
            Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&language=en");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                ArticleResults = JsonConvert.DeserializeObject<ArticleList>(data);
                articles = ArticleResults.results;
            }
            ArticleList retval = new ArticleList();
            retval.results = articles;
            return Page();
        }
    }
}

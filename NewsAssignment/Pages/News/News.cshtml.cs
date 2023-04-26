using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
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
        public List<Article>? DBArticles { get; set; }
        public List<ArticleDTO>? DTOArticles { get; set; }
        public ArticleDTOList? ArticleResults = new ArticleDTOList();

        public List<ArticleDTO>? GetDTOArticles()
        {
            return DTOArticles;
        }

        public List<Article>? GetDBArticles()
        {
            return DBArticles;
        }

        public async Task<IActionResult> OnGet(List<ArticleDTO>? dTOArticles, List<Article>? dBArticles)
        {
            for (int i = 0; i < 12; i++)
            {
                Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&language=en&category=" + articleCategories[i]);

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(mb.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    ArticleResults = JsonConvert.DeserializeObject<ArticleDTOList>(data);
                    foreach (var item in ArticleResults.results)
                    {
                        Article current = new Article();
                        dTOArticles.Add(item);

                        current.title = item.title;
                        current.link = item.link;
                        current.source_id = item.source_id;
                        if (item.keywords != null)
                        {
                            foreach (var keyword in item.keywords)
                            {
                                current.keywords += keyword;
                                current.keywords += ", ";
                            }

                            current.keywords = current.keywords.Remove(current.keywords.Length - 2);
                        }

                        if (item.creator != null)
                        {
                            foreach (var creator in item.creator)
                            {
                                current.creator += creator;
                                current.creator += ", ";
                            }

                            current.creator = current.creator.Remove(current.creator.Length - 2);
                        }

                        current.image_url = item.image_url;
                        current.video_url = item.video_url;
                        current.description = item.description;
                        current.pubDate = item.pubDate;
                        current.content = item.content;
                        if (item.country != null)
                        {
                            foreach (var country in item.country)
                            {
                                current.country += country;
                                current.country += ", ";
                            }

                            current.country = current.country.Remove(current.country.Length - 2);
                        }

                        if (item.category != null)
                        {
                            foreach (var category in item.category)
                            {
                                current.category += category;
                                current.category += ", ";
                            }
                            current.category = current.category.Remove(current.category.Length - 2);
                        }

                        current.language= item.language;
                        current.fullDescription= item.fullDescription;
                        dBArticles.Add(current);
                    }
                }
            }
            DBArticles = dBArticles;
            DTOArticles = dTOArticles;
            return Page();
        }
    }
}

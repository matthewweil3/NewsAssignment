using Microsoft.AspNetCore.Mvc;
using NewsAssignment.Data;
using NewsAssignment.Data.Migrations;
using NewsAssignment.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace NewsAssignment.Providers
{
    public class ArticleProvider
    {
        public string[] articleCategories = { "business", "entertainment", "enviornment", "food", "health", "politics", "science", "sports", "technology", "top", "tourism", "world" };
        public List<Article>? DBArticles { get; set; }
        public List<ArticleDTO>? DTOArticles { get; set; }
        public ArticleDTOList? ArticleResults = new ArticleDTOList();
        private ApplicationDbContext _context;

        public ArticleProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticleViewModel>> FetchCategoryArticles(string topic)
        {
            DBArticles = new List<Article>();
            DTOArticles = new List<ArticleDTO>();

            Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&language=en&category=" + topic);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                ArticleResults = JsonConvert.DeserializeObject<ArticleDTOList>(data);
                foreach (var item in ArticleResults.results)
                {
                    Article current = new Article();
                    DTOArticles.Add(item);

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

                    current.language = item.language;
                    current.fullDescription = item.fullDescription;
                    DBArticles.Add(current);
                }
            }

            // Add API articles to database
            var urls = _context.Article.Select(x => x.link).ToList();
            DBArticles = DBArticles.Where(x => !urls.Contains(x.link)).ToList();
            _context.Article.AddRange(DBArticles);
            await _context.SaveChangesAsync();

            // Grab published articles from database and filter by category
            var dbArticles = _context.Article.Where(x => (x.status == null || x.status == Article.State.Published) && x.category.Contains(topic)).ToList();
            var viewModels = ArticleToViewModel(dbArticles).OrderByDescending(x => x.PublishDate).ToList();

            return viewModels;
        }

        public async Task<List<ArticleViewModel>> FetchAllArticles(int take, int skip = 0)
        {
            DBArticles = new List<Article>();
            DTOArticles = new List<ArticleDTO>();

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
                        DTOArticles.Add(item);

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

                        current.language = item.language;
                        current.fullDescription = item.fullDescription;
                        DBArticles.Add(current);
                    }
                }
            }

            // Add API articles to database
            var urls = _context.Article.Select(x => x.link).ToList();
            DBArticles = DBArticles.Where(x => !urls.Contains(x.link)).ToList();
            _context.Article.AddRange(DBArticles);
            await _context.SaveChangesAsync();

            // Grab published articles from database and filter by category
            var dbArticles = _context.Article.Where(x => x.status == null || x.status == Article.State.Published).ToList();
            var viewModels = ArticleToViewModel(dbArticles).OrderByDescending(x => x.PublishDate).Skip(skip).Take(take).ToList();

            return viewModels;
        }

        private List<ArticleViewModel> ArticleToViewModel(List<Article> articles)
        {
            var viewModels = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                var articleViewModel = new ArticleViewModel();

                articleViewModel.Id = article.Id;
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
                try
                {
                    articleViewModel.PublishDate = DateTime.ParseExact(article.pubDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch
                {
                    articleViewModel.PublishDate = DateTime.Parse(article.pubDate);
                }

                viewModels.Add(articleViewModel);
            }

            return viewModels;
        }
    }
}

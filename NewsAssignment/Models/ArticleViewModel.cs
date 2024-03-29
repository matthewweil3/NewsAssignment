﻿namespace NewsAssignment.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public string Country { get; set; }
        public List<string> Categories { get; set; }
        public string Language { get; set; }
        public string Category
        {
            get
            {
                var firstCategory = Categories.First();
                return firstCategory[0].ToString().ToUpper() + firstCategory[1..];
            }
        }
        public string CategoriesString
        {
            get
            {
                var capitalCategories = Categories.Select(x => x[0].ToString().ToUpper() + x[1..]);
                return string.Join(", ", capitalCategories);
            }
        }
        public string DaysAgo
        {
            get
            {
                var timeSince = DateTime.UtcNow - PublishDate;

                if (timeSince.Days == 0)
                {
                    if (timeSince.Hours == 0)
                    {
                        if (timeSince.Minutes == 0)
                        {
                            return "Now";
                        }
                        else
                        {
                            return $"{timeSince.Minutes} Minutes Ago";
                        }
                    }
                    else
                    {
                        return $"{timeSince.Hours} Hours Ago";
                    }
                }
                else
                {
                    return $"{timeSince.Days} Days Ago";
                }
            }
        }
        public string Icon {
            get
            {
                return CategoryIcons[Categories.First()];
            }
        }
        public string Color
        {
            get
            {
                return CategoryColors[Categories.First()];
            }
        }

        public static List<string> AllCategories = new List<string>()
        {
            "business",
            "entertainment",
            "environment",
            "food",
            "health",
            "politics",
            "science",
            "sports",
            "technology",
            "top",
            "tourism",
            "world"
        };

        public static Dictionary<string, string> CategoryIcons = new Dictionary<string, string>()
        {
            { "business", "shop" },  // cash-stack or currency-exchange or shop
            { "entertainment", "film" },  // film
            { "environment", "cloud-sun" },  // cloud-sun or hurricane or tornado
            { "food", "egg-fried" },  // egg-fried
            { "health", "hospital" },  // hospital
            { "politics", "people" },  // people
            { "science", "mortarboard" },  // lightning, lightbulb, magnet, mortarboard, radioactive, rocketship
            { "sports", "dribbble" },  // dribbble
            { "technology", "cpu" },  // robot or cpu
            { "top", "graph-up-arrow" },  // graph-up-arrow
            { "tourism", "pin-map" },  // pin-map
            { "world", "globe" }  // globe
        };

        public static Dictionary<string, string> CategoryColors = new Dictionary<string, string>()
        {
            { "business", "bg-blue-green text-white" },
            { "entertainment", "bg-dark text-white" },
            { "environment", "bg-success text-white" },
            { "food", "bg-magenta text-white" },
            { "health", "bg-warning text-dark" },
            { "politics", "bg-violet text-white" },
            { "science", "bg-primary text-white" },
            { "sports", "bg-danger text-white" },
            { "technology", "bg-info text-dark" },
            { "top", "bg-secondary text-white" },
            { "tourism", "bg-orange text-white" },
            { "world", "bg-dark-red text-white" }
        };

        public static List<string> ExampleImages = new List<string>()
        {
            "",
            "https://www.thomasmore.edu/wp-content/uploads/TMU_SIG_3_BL_RGB.png",
            "https://www.thomasmore.edu/wp-content/uploads/TMU_SIG_2_BL_RGB-1024x305.png"
        };

        public static ArticleViewModel CreateRandomModel(string? category = null)
        {
            var article = new ArticleViewModel();
            var random = new Random();

            article.Id = -1;
            article.Title = $"Title {random.Next(1_000_000_000)}";
            article.Creator = "Random";
            article.Content = $"Content {random.Next(1_000_000_000)}";
            article.Country = "US";
            article.PublishDate = new DateTime(2023, random.Next(1, 4), random.Next(1, 29));
            article.ImageUrl = ExampleImages[random.Next(ExampleImages.Count)];
            article.Language = "English";

            // https://stackoverflow.com/questions/532892/can-i-multiply-a-string-in-c
            article.Description = $"Description {random.Next(1_000_000_000)} ";
            article.Description = string.Concat(Enumerable.Repeat(article.Description, random.Next(1, 10))).Trim();  // Repeat Description

            // https://stackoverflow.com/questions/55817517/get-a-random-list-of-integer-array-in-c-sharp/55817602
            var categoryIndices = Enumerable.Range(0, AllCategories.Count).OrderBy(_ => random.Next()).Take(2);
            article.Categories = categoryIndices.Select(x => AllCategories[x]).ToList();

            if (!string.IsNullOrEmpty(category) && !article.Categories.Contains(category))
            {
                article.Categories.Insert(0, category);
            }

            return article;
        }
    }
}

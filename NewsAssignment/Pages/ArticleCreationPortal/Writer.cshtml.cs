using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    public class WriterModel : PageModel
    {
        public ArticleViewModel Article { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string DaysAgo { get; set; }
        public string Color { get; set; }
        public string Creator { get; set; }

        public void OnGet(string Title, string Description, string Category, string DaysAgo,
                            string Image, string Color, string Content, string Icon, string Creator)
        {
            Article = new ArticleViewModel { Title = Title, Description = Description, ImageUrl = Image, Content = Content };
            this.Category = Category;
            this.DaysAgo = DaysAgo;
            this.Color = Color;
            this.Icon = Icon;
            this.Creator = Creator;

            // add article to DB
        }
    }
}

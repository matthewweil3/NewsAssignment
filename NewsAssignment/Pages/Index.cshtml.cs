using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Models;

namespace NewsAssignment.Pages
{
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> Articles;

        public IndexModel()
        {
            Articles = new List<ArticleViewModel>();
        }

        public void OnGet()
        {
            for (int i = 0; i < 100; i++)
            {
                Articles.Add(ArticleViewModel.CreateRandomModel());
            }
        }
    }
}
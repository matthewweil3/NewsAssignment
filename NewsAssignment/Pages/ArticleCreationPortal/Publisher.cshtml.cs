using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    [Authorize(Roles = "Publisher,Admin")]
    public class PublisherModel : PageModel
    { 
        public void OnGet()
        {
        }
    }
}

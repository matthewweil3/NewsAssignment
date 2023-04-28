using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Models;
using System.Data;
using Microsoft.Identity;
using NuGet.Protocol.Core.Types;
using System.Web;
using System.Xml.Linq;
using Azure.Core;
using NewsAssignment.Data;
using Microsoft.EntityFrameworkCore;

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    public class IndexModel : PageModel
    {
        public UserManager<ApplicationUser> userManager;
        public List<string> roleinfo;
        ApplicationDbContext _context;

        public IndexModel(UserManager<ApplicationUser> uManager, NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
            userManager = uManager;          
        }

        public IList<Article> Articles { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var userrole = await userManager.GetRolesAsync(userManager.Users.Where(x => x.Email == User.Identity.Name).First());
                if (userrole != null)
                {
                    roleinfo = userrole.ToList();
                }
                Articles = await _context.Article.Where(x => x.status == Article.State.Rewrite && x.creator == User.Identity.Name).ToListAsync();
                return Page();
            }
            catch
            {
                roleinfo = new List<string>();
                roleinfo.Add("Public User");
            }
            return Page();
        }
    }
}

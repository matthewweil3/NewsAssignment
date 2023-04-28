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

namespace NewsAssignment.Pages.ArticleCreationPortal
{
    public class IndexModel : PageModel
    {
        public UserManager<ApplicationUser> userManager;
        public List<string> roleinfo;

        public IndexModel(UserManager<ApplicationUser> uManager)
        {
            userManager = uManager;          
        }

        public async void OnGet()
        {
            try
            {
                var userrole = await userManager.GetRolesAsync(userManager.Users.Where(x => x.Email == User.Identity.Name).First());
                if (userrole != null)
                {
                    roleinfo = userrole.ToList();
                }
            }
            catch
            {
                roleinfo = new List<string>();
                roleinfo.Add("public user");
            }

        }
    }
}

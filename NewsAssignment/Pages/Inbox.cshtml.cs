using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages
{
   [Authorize(Roles ="Admin,Writer,Publisher,Editor")]
    public class InboxModel : PageModel
    {
        public UserManager<ApplicationUser> userManager;
        ApplicationDbContext _context;

        public InboxModel(UserManager<ApplicationUser> uManager, NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
            userManager = uManager;
        }

        public async Task OnGetAsync()

        {
            
            var userrole = await userManager.GetRolesAsync(userManager.Users.Where(x => x.Email == User.Identity.Name).First());
            SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            foreach (var role in roles)
            {
                string r = role.Text;
                string u = userrole[0];
                if(r == u)
                {
                    string x = role.Value;
                 
                    ViewData["UserRole"] = x;
                    break;
                }
            }
            
        }
    }
}

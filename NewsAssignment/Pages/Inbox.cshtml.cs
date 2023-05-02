using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace NewsAssignment.Pages
{
   // [Authorize(Roles ="Admin,Writer,Publisher,Editor")]
    public class InboxModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

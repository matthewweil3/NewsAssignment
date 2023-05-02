using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using NewsAssignment.Data;
using NewsAssignment.Models;

namespace NewsAssignment.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly NewsAssignment.Data.ApplicationDbContext _context;

        public CreateModel(NewsAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Comment == null || Comment == null || Comment.CommentBody.IsNullOrEmpty())
            {
                return Redirect("../../Articles/Article?id=" + Comment.ArticleID.ToString());
            }

          if(User.Identity.Name != Comment.Username)
            {
                return StatusCode(401); // prevent people from impersonating comments
            }

            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();

            return Redirect("../../Articles/Article?id=" + Comment.ArticleID.ToString());
        }
    }
}

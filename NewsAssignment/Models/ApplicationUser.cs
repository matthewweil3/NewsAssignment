using Microsoft.AspNetCore.Identity;

namespace NewsAssignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;

        //Nav Props
        public List<Category> Categories { get; set; }
       // public List<Article> Articles { get; set; }
    }
}

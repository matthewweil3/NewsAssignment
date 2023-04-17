using NewsAssignment.Models;
using NewsAssignment.Areas;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace NewsAssignment.Data
{
    public class SeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        RoleManager<IdentityRole> RoleManager;
        UserManager<ApplicationUser> userManager;

        public SeedData(
       UserManager<ApplicationUser> userManager,

       RoleManager<IdentityRole> roleManager)
   
        {
            _userManager = userManager;
            
            _roleManager = roleManager;
           
        }

        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any Users
            if (context.Roles.Any()||context.Category.Any()||context.Users.Any())
            {
                return;   // DB has been seeded
            }


            var roles = new IdentityRole[]
            {
                new IdentityRole{Name="Sub",NormalizedName="SUB"},
                new IdentityRole{Name="Editor",NormalizedName="EDITOR"},
                new IdentityRole{Name="Publisher",NormalizedName="PUBLISHER"},
                new IdentityRole{Name="Writer",NormalizedName="WRITER"},
                new IdentityRole{Name="Admin",NormalizedName="ADMIN", Id = "123"}


            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            var categories = new Category[]
           {
                new Category{name="business",display="Business",icon="shop", color ="bg-blue-green text-white"},
                new Category{name="entertainment",display="Entertainment",icon="film", color ="bg-dark text-white"},
                new Category{name="environment",display="Environment",icon="cloud-sun", color ="bg-success text-white"},
                new Category{name="food",display="Food",icon="egg-fried", color ="bg-magenta text-white"},
                new Category{name="health",display="Health",icon="hospital", color ="bg-warning text-dark"},
                new Category{name="politics",display="Politics",icon="people", color ="bg-violet text-white"},
                new Category{name="science",display="Science",icon="mortarboard", color ="bg-primary text-white"},
                new Category{name="sports",display="Sports",icon="dribble", color ="bg-danger text-white"},
                new Category{name="technology",display="Technology",icon="cpu", color ="bg-info text-dark"},
                new Category{name="top",display="Hot Right Now",icon="graph-up-arrow", color ="bg-secondary text-white"},
                new Category{name="tourism",display="Tourism",icon="pin-map", color ="bg-orange text-white"},
                new Category{name="world",display="World",icon="globe", color ="bg-dark-red text-white"},

           };

            context.Category.AddRange(categories);
            context.SaveChanges();
            var user = new ApplicationUser {FirstName = "Matthew", LastName = "Weil", Email = "matthewweil3@gmail.com" , Id = "456" };
           // var defaultrole = _roleManager.FindByNameAsync("SUB").Result;

            context.Users.Add(user); 
            context.SaveChanges();


            


        }
    }
}

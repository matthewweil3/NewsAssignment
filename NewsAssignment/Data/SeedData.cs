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
            if (context.Roles.Any() || context.Category.Any() || context.Users.Any())
            {
                return;   // DB has been seeded
            }


            var roles = new IdentityRole[]
            {
                new IdentityRole{Name="Sub",NormalizedName="SUB", Id = "0"},
                new IdentityRole{Name="Editor",NormalizedName="EDITOR" , Id = "2"},
                new IdentityRole{Name="Publisher",NormalizedName="PUBLISHER", Id = "3"},
                new IdentityRole{Name="Writer",NormalizedName="WRITER", Id = "1"},
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
            var hasher = new PasswordHasher<ApplicationUser>();

            var Users = new ApplicationUser[]
            {
                new ApplicationUser{Email="jesse@thomasmoretimes.com", EmailConfirmed=true, UserName="jesse@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Jesse", LastName="Zetko", NormalizedEmail="JESSE@THOMASMORETIMES.COM", NormalizedUserName="JESSE@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")},
                new ApplicationUser{Email="ben@thomasmoretimes.com", EmailConfirmed=true, UserName="ben@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Ben", LastName="Albrinck", NormalizedEmail="BEN@THOMASMORETIMES.COM", NormalizedUserName="BEN@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")},
                new ApplicationUser{Email="micah@thomasmoretimes.com", EmailConfirmed=true, UserName="micah@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Micah", LastName="Impanis", NormalizedEmail="MICAH@THOMASMORETIMES.COM", NormalizedUserName="MICAH@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")},
                new ApplicationUser{Email="mathew@thomasmoretimes.com", EmailConfirmed=true, UserName="mathew@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Mathew", LastName="Weil", NormalizedEmail="MATHEW@THOMASMORETIMES.COM", NormalizedUserName="MATHEW@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")},
                new ApplicationUser{Email="mackyle@thomasmoretimes.com", EmailConfirmed=true, UserName="mackyle@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Mackyle", LastName="Borchers", NormalizedEmail="MACKYLE@THOMASMORETIMES.COM", NormalizedUserName="MACKYLE@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")},
                new ApplicationUser{Email="jacob@thomasmoretimes.com", EmailConfirmed=true, UserName="jacob@thomasmoretimes.com", SecurityStamp=Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "password"), FirstName="Jacob", LastName="Toelke", NormalizedEmail="JACOB@THOMASMORETIMES.COM", NormalizedUserName="JACOB@THOMASMORETIMES.COM", ConcurrencyStamp=Guid.NewGuid().ToString("D")}
            };

            context.Users.AddRange(Users);
            context.SaveChanges();

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = "123", UserId = context.Users.Where(x => x.Email == "jesse@thomasmoretimes.com").First().Id}, // Jesse : Admin
                new IdentityUserRole<string>{RoleId = "3", UserId = context.Users.Where(x => x.Email == "ben@thomasmoretimes.com").First().Id}, // Ben: Publisher
                new IdentityUserRole<string>{RoleId = "2", UserId = context.Users.Where(x => x.Email == "jacob@thomasmoretimes.com").First().Id},
                new IdentityUserRole<string>{RoleId = "2", UserId = context.Users.Where(x => x.Email == "mackyle@thomasmoretimes.com").First().Id}, // Jacob, Mackyle: Editor
                new IdentityUserRole<string>{RoleId = "1", UserId = context.Users.Where(x => x.Email == "micah@thomasmoretimes.com").First().Id},
                new IdentityUserRole<string>{RoleId = "1", UserId = context.Users.Where(x => x.Email == "mathew@thomasmoretimes.com").First().Id} // Micah, Mathew: Writers
            };

            context.UserRoles.AddRange(UserRoles);
            context.SaveChanges();
        }
    }
}

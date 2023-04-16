namespace NewsAssignment.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string name { get; set; }
        public string display { get; set; } 
        public string icon { get; set; }
        public string color { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
        //public List<Article> Articles { get; set; }

    }
}

namespace NewsAssignment.Models
{
    public class ApplicationUserCategory
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

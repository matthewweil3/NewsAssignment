using Microsoft.EntityFrameworkCore;

namespace NewsAssignment.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ArticleID { get; set; }
        public DateTime Date { get; set; }
        public string? UserName { get; set; }
        public string? Description { get; set; }
    }
}

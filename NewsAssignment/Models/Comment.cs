using Microsoft.EntityFrameworkCore;

namespace NewsAssignment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleID { get; set; }
        public string? Username { get; set; }
        public string? CommentBody { get; set; }
    }
}

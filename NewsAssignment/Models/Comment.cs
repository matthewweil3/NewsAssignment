namespace NewsAssignment.Models
{
    public class Comment
    {
        public int Id {get; set;}
        public int ArticleID {get; set;}
        public string? username {get; set;}
        public string? commentBody {get; set;}
    }
}

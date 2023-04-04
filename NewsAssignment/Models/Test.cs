namespace NewsAssignment.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? link { get; set; }
        public List<string>? keywords { get; set; }
        public List<string>? creators { get; set; }
        public string? videoURL { get; set; }
        public string? description { get; set; }
        public string? content { get; set; }
        public string? pubDate { get; set; }
        public string? fullDescription { get; set; }
        public string? imageURL { get; set; }
        public string? sourceID { get; set; }
        public List<string>? countries { get; set; }
        public List<string>? categories { get; set; }
        public string? language { get; set; }
    }
}

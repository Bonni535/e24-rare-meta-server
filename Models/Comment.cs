namespace e24_rare_meta_server.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; } 
        public DateTime CreatedOn { get; set; }
    }
}

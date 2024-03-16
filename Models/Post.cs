namespace e24_rare_meta_server.Models
{
    public class Post
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}

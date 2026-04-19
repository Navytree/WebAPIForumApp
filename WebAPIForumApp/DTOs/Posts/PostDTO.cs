namespace WebAPIForumApp.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
    }
}

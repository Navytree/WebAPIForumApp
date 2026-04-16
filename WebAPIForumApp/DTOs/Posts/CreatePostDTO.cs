namespace WebAPIForumApp.DTOs.Posts
{
    public class CreatePostDTO
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }

    }
}

namespace WebAPIForumApp.DTOs.Topics
{
    public class CreateTopicDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}

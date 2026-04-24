using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIForumApp.DTOs
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public int RepliesCount { get; set; }
        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }

}

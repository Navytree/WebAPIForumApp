using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIForumApp.DTOs.Posts;

namespace WebAPIForumApp.DTOs.Topics
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }

}
